public class Cerveza
{
    public int Id { get; set; } // Id
    public string Nombre { get; set; } // Name
    public string Lema { get; set; } // Tagline
    public string PrimeraProduccion { get; set; } // First_brewed
    public string Descripcion { get; set; } // Description
    public string UrlImagen { get; set; } // Image_url
    public decimal? Precio { get; set; } // Price
    public decimal? AlcoholPorVolumen { get; set; } // Abv
    public decimal? UnidadesInternacionalesAmargor { get; set; } // Ibu
    public decimal? GravedadFinalObjetivo { get; set; } // Target_fg
    public decimal? GravedadOriginalObjetivo { get; set; } // Target_og
    public decimal? ColorCervezaEuropeo { get; set; } // Ebc
    public decimal? MetodoReferenciaColor { get; set; } // Srm
    public decimal? Ph { get; set; } // Ph
    public decimal? NivelAtenuacion { get; set; } // Attenuation_level
    public Volumen Volumen { get; set; } // Volume
    public Volumen VolumenHervido { get; set; } // Boil_volume
    public List<string> Maridaje { get; set; } // Food_pairing
    public string ConsejosCerveceros { get; set; } // Brewers_tips
    public string ContribuidoPor { get; set; } // Contributed_by
}

public class Volumen
{
    public decimal? Valor { get; set; } // Value
    public string Unidad { get; set; } // Unit
}
