namespace OnlineCoffeShop.Web.Models;

public enum ArtStyle
{
    Dark,
    Medium,
    Tan,
    Gear,
    Gift
}

public static class ArtStyleExtensions
{
    public static string CssClass(this ArtStyle a) => a switch
    {
        ArtStyle.Dark   => "dark",
        ArtStyle.Medium => "med",
        ArtStyle.Tan    => "tan",
        ArtStyle.Gear   => "gear",
        ArtStyle.Gift   => "gift",
        _ => "dark"
    };
}