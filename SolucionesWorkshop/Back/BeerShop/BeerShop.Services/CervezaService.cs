public class CervezaService : ICervezaService
{
    private readonly IBeerRepository _beerRepository;

    public CervezaService(IBeerRepository beerRepository)
    {
        _beerRepository = beerRepository;
    }

    public IEnumerable<Cerveza> GetCervezas(int offset, int limit)
    {
        return _beerRepository.GetBeers(offset, limit);
    }

    public Cerveza GetCerveza(int id)
    {
        return _beerRepository.GetBeer(id);
    }

    public IEnumerable<Cerveza> SearchCervezas(string nombre, string descripcion, string eslogan, string maridaje, decimal? precio)
    {
        return _beerRepository.SearchBeers(nombre, descripcion, eslogan, maridaje, precio);
    }
}
