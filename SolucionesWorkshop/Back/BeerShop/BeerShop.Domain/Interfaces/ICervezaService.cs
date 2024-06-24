public interface ICervezaService
{
    IEnumerable<Cerveza> GetCervezas(int offset, int limit);
    Cerveza GetCerveza(int id);
    IEnumerable<Cerveza> SearchCervezas(string nombre, string descripcion, string eslogan, string maridaje, decimal? precio);
}
