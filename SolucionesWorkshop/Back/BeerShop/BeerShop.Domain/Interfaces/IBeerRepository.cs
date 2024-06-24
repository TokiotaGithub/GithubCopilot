public interface IBeerRepository
{
    Cerveza GetBeer(int id);
    List<Cerveza> GetBeers(int offset, int limit);
    List<Cerveza> SearchBeers(string nombre, string descripcion, string eslogan, string maridaje, decimal? precio);
}
