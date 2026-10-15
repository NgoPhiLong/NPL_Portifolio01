using Microsoft.AspNetCore.Components;
using NPL_Portifolio01.Models;
using NPL_Portifolio01.Services;

namespace NPL_Portifolio01.Components;

/// <summary>Base class giving components access to the cascaded <see cref="SiteState"/> and translation helpers.</summary>
public abstract class SiteComponentBase : ComponentBase
{
    [CascadingParameter]
    protected SiteState Site { get; set; } = default!;

    protected PortfolioData Data => Site.Data;

    /// <summary>Translate content coming from portfolio.json.</summary>
    protected string T(LocalizedText? text) => Site.T(text);

    /// <summary>Translate an interface label from <see cref="UiText"/>.</summary>
    protected string L(string key) => Site.L(key);
}
