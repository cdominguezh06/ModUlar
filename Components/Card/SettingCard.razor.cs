using Microsoft.AspNetCore.Components;
using ModUlar.model;

namespace ModUlar.Components.Card;

public partial class SettingCard : ComponentBase
{
    [Parameter]
    public Setting Setting { get; set; }
    
    [Parameter]
    public string ModFolder { get; set; }
    
}