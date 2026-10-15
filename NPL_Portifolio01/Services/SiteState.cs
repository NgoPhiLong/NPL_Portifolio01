using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using NPL_Portifolio01.Models;

namespace NPL_Portifolio01.Services;

/// <summary>App-wide state: portfolio content, language, theme and the section currently in view.</summary>
public sealed class SiteState(HttpClient http, IJSRuntime js)
{
    public static readonly IReadOnlyList<string> Languages = ["en", "vi"];

    public string Language { get; private set; } = Languages[0];
    public string Theme { get; private set; } = "dark";
    public bool ReducedMotion { get; private set; }
    public PortfolioData Data { get; private set; } = new();
    public string? LoadError { get; private set; }
    public string? ActiveSection { get; private set; }
    public IReadOnlyList<string> NavSections { get; private set; } = [];

    /// <summary>Raised when the language or theme changes; the whole UI re-renders.</summary>
    public event Action? Changed;

    /// <summary>Raised when the section in view changes; only the navigation re-renders.</summary>
    public event Action? ActiveSectionChanged;

    public async Task InitializeAsync()
    {
        try
        {
            var preferences = await js.InvokeAsync<ClientPreferences>("npl.getPreferences");
            Language = Languages.Contains(preferences.Language) ? preferences.Language : Languages[0];
            Theme = preferences.Theme == "light" ? "light" : "dark";
            ReducedMotion = preferences.ReducedMotion;
        }
        catch (JSException)
        {
            // site.js is missing: keep the defaults.
        }

        try
        {
            // Always revalidate so edits to the JSON show up right after a deploy (GitHub Pages caches files for 10 minutes).
            using var request = new HttpRequestMessage(HttpMethod.Get, "data/portfolio.json");
            request.SetBrowserRequestCache(BrowserRequestCache.NoCache);
            using var response = await http.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Data = await response.Content.ReadFromJsonAsync(PortfolioJsonContext.Default.PortfolioData) ?? new();
        }
        catch (Exception ex) when (ex is HttpRequestException or JsonException or NotSupportedException)
        {
            LoadError = ex.Message;
        }

        NavSections = BuildNavSections(Data);
    }

    public bool HasSection(string id) => NavSections.Contains(id);

    /// <summary>Two-digit position of a section in the navigation ("01", "02"…), used in section headings.</summary>
    public string SectionNumber(string id)
    {
        for (var i = 0; i < NavSections.Count; i++)
        {
            if (NavSections[i] == id)
            {
                return (i + 1).ToString("00", System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        return string.Empty;
    }

    public string T(LocalizedText? text) => text?.Get(Language) ?? string.Empty;

    public string L(string key) => UiText.Get(key, Language);

    public async Task SetLanguageAsync(string language)
    {
        if (!Languages.Contains(language) || language == Language)
        {
            return;
        }

        Language = language;
        await js.InvokeVoidAsync("npl.setLanguage", language);
        Changed?.Invoke();
    }

    public Task ToggleLanguageAsync() => SetLanguageAsync(Language == "vi" ? "en" : "vi");

    public async Task ToggleThemeAsync()
    {
        Theme = Theme == "dark" ? "light" : "dark";
        await js.InvokeVoidAsync("npl.setTheme", Theme);
        Changed?.Invoke();
    }

    [JSInvokable]
    public void SetActiveSection(string? sectionId)
    {
        if (sectionId == ActiveSection)
        {
            return;
        }

        ActiveSection = sectionId;
        ActiveSectionChanged?.Invoke();
    }

    private static List<string> BuildNavSections(PortfolioData data)
    {
        var sections = new List<string>();
        if (data.About.Paragraphs.Count > 0 || data.About.Facts.Count > 0 || data.About.Stats.Count > 0) sections.Add("about");
        if (data.SkillGroups.Count > 0 || data.Toolbox.Count > 0) sections.Add("skills");
        if (data.Experience.Count > 0 || data.Education.Count > 0) sections.Add("experience");
        if (data.Projects.Count > 0) sections.Add("projects");
        sections.Add("contact");
        return sections;
    }

    public sealed class ClientPreferences
    {
        public string Language { get; set; } = "en";
        public string Theme { get; set; } = "dark";
        public bool ReducedMotion { get; set; }
    }
}
