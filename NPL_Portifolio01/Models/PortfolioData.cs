using System.Text.Json;
using System.Text.Json.Serialization;

namespace NPL_Portifolio01.Models;

/// <summary>Root of <c>wwwroot/data/portfolio.json</c>.</summary>
public sealed class PortfolioData
{
    public Profile Profile { get; set; } = new();
    public List<SocialLink> Socials { get; set; } = [];
    public AboutInfo About { get; set; } = new();
    public List<SkillGroup> SkillGroups { get; set; } = [];
    public List<string> Toolbox { get; set; } = [];
    public List<TimelineEntry> Experience { get; set; } = [];
    public List<TimelineEntry> Education { get; set; } = [];
    public List<Project> Projects { get; set; } = [];
    public ContactInfo Contact { get; set; } = new();
}

public sealed class Profile
{
    public string Name { get; set; } = string.Empty;
    public string Initials { get; set; } = string.Empty;
    public List<LocalizedText> Roles { get; set; } = [];
    public LocalizedText Tagline { get; set; } = LocalizedText.Empty;
    public LocalizedText Availability { get; set; } = LocalizedText.Empty;
    public string? Avatar { get; set; }
    public LocalizedText Location { get; set; } = LocalizedText.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public LocalizedText Cv { get; set; } = LocalizedText.Empty;
    public List<string> HeroBadges { get; set; } = [];
}

public sealed class SocialLink
{
    public string Name { get; set; } = string.Empty;
    public string Icon { get; set; } = "globe";
    public string Url { get; set; } = string.Empty;
}

public sealed class AboutInfo
{
    public List<LocalizedText> Paragraphs { get; set; } = [];
    public List<Fact> Facts { get; set; } = [];
    public List<Stat> Stats { get; set; } = [];
}

public sealed class Fact
{
    public string Icon { get; set; } = "info";
    public LocalizedText Label { get; set; } = LocalizedText.Empty;
    public LocalizedText Value { get; set; } = LocalizedText.Empty;
}

public sealed class Stat
{
    public int Value { get; set; }
    public string Suffix { get; set; } = string.Empty;
    public LocalizedText Label { get; set; } = LocalizedText.Empty;
}

public sealed class SkillGroup
{
    public LocalizedText Title { get; set; } = LocalizedText.Empty;
    public string Icon { get; set; } = "code";
    public List<Skill> Skills { get; set; } = [];
}

public sealed class Skill
{
    public string Name { get; set; } = string.Empty;

    /// <summary>0–100. Leave empty to show the skill as a simple tag.</summary>
    public int? Level { get; set; }
}

public sealed class TimelineEntry
{
    public LocalizedText Title { get; set; } = LocalizedText.Empty;
    public LocalizedText Organization { get; set; } = LocalizedText.Empty;
    public string? Url { get; set; }
    public LocalizedText Period { get; set; } = LocalizedText.Empty;
    public LocalizedText Location { get; set; } = LocalizedText.Empty;
    public LocalizedText Description { get; set; } = LocalizedText.Empty;
    public List<LocalizedText> Highlights { get; set; } = [];
    public List<string> Tags { get; set; } = [];
}

public sealed class Project
{
    public string Id { get; set; } = string.Empty;
    public LocalizedText Title { get; set; } = LocalizedText.Empty;
    public LocalizedText Category { get; set; } = LocalizedText.Empty;
    public LocalizedText Summary { get; set; } = LocalizedText.Empty;
    public LocalizedText Description { get; set; } = LocalizedText.Empty;
    public List<LocalizedText> Highlights { get; set; } = [];
    public List<string> Tags { get; set; } = [];
    public string? Image { get; set; }
    public string Icon { get; set; } = "code";
    public string? Year { get; set; }
    public string? RepoUrl { get; set; }
    public string? DemoUrl { get; set; }
    public bool Featured { get; set; }
}

public sealed class ContactInfo
{
    public LocalizedText Heading { get; set; } = LocalizedText.Empty;
    public LocalizedText Text { get; set; } = LocalizedText.Empty;

    /// <summary>Optional Formspree form id. When empty the contact form opens the visitor's email app instead.</summary>
    public string? FormspreeId { get; set; }
}

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    PropertyNameCaseInsensitive = true,
    ReadCommentHandling = JsonCommentHandling.Skip,
    AllowTrailingCommas = true)]
[JsonSerializable(typeof(PortfolioData))]
internal sealed partial class PortfolioJsonContext : JsonSerializerContext
{
}
