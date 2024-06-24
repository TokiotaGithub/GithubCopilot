using Newtonsoft.Json;

public class BeerRepository : IBeerRepository
{
    private readonly List<Beer> beers;

    public BeerRepository()
    {
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "beers.json");
        var json = File.ReadAllText(path);
        beers = JsonConvert.DeserializeObject<List<Beer>>(json);
    }

    private Cerveza ConvertToCerveza(Beer? beer)
    {
        if (beer == null)
        {
            return null;
        }

        return new Cerveza
        {
            Id = beer.Id, // Id
            Nombre = beer.Name, // Name
            Lema = beer.Tagline, // Tagline
            PrimeraProduccion = beer.First_brewed, // First_brewed
            Descripcion = beer.Description, // Description
            UrlImagen = beer.Image_url, // Image_url
            Precio = beer.Price, // Price
            AlcoholPorVolumen = beer.Abv, // Abv
            UnidadesInternacionalesAmargor = beer.Ibu, // Ibu
            GravedadFinalObjetivo = beer.Target_fg, // Target_fg
            GravedadOriginalObjetivo = beer.Target_og, // Target_og
            ColorCervezaEuropeo = beer.Ebc, // Ebc
            MetodoReferenciaColor = beer.Srm, // Srm
            Ph = beer.Ph, // Ph
            NivelAtenuacion = beer.Attenuation_level, // Attenuation_level
            Volumen = new Volumen { Valor = beer.Volume.Value, Unidad = beer.Volume.Unit }, // Volume
            VolumenHervido = new Volumen { Valor = beer.Boil_volume.Value, Unidad = beer.Boil_volume.Unit }, // Boil_volume
            Maridaje = beer.Food_pairing, // Food_pairing
            ConsejosCerveceros = beer.Brewers_tips, // Brewers_tips
            ContribuidoPor = beer.Contributed_by // Contributed_by
        };
    }

    public Cerveza GetBeer(int id)
    {
        var beer = beers.FirstOrDefault(b => b.Id == id);
        return ConvertToCerveza(beer);
    }

    public List<Cerveza> GetBeers(int offset, int limit)
    {
        var selectedBeers = beers.Skip(offset).Take(limit);
        return selectedBeers.Select(ConvertToCerveza).ToList();
    }

    public List<Cerveza> SearchBeers(string nombre, string descripcion, string eslogan, string maridaje, decimal? precio)
    {
        return beers
            .Where(b =>
                (string.IsNullOrEmpty(nombre) || b.Name == nombre) &&
                (string.IsNullOrEmpty(descripcion) || b.Description == descripcion) &&
                (string.IsNullOrEmpty(eslogan) || b.Tagline == eslogan) &&
                (string.IsNullOrEmpty(maridaje) || string.Join(' ',b.Food_pairing).Contains(maridaje)) &&
                (!precio.HasValue || b.Price == precio.Value))
            .Select(ConvertToCerveza)
            .ToList();
    }
}
