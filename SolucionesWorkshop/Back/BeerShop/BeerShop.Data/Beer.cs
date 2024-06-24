public class Beer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Tagline { get; set; }
    public string First_brewed { get; set; }
    public string Description { get; set; }
    public string Image_url { get; set; }
    public decimal? Price { get; set; }
    public decimal? Abv { get; set; }
    public decimal? Ibu { get; set; }
    public decimal? Target_fg { get; set; }
    public decimal? Target_og { get; set; }
    public decimal? Ebc { get; set; }
    public decimal? Srm { get; set; }
    public decimal? Ph { get; set; }
    public decimal? Attenuation_level { get; set; }
    public Volume Volume { get; set; }
    public Volume Boil_volume { get; set; }
    public List<string> Food_pairing { get; set; }
    public string Brewers_tips { get; set; }
    public string Contributed_by { get; set; }
}

public class Volume
{
    public decimal? Value { get; set; }
    public string Unit { get; set; }
}
