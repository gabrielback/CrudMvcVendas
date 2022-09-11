namespace ControleDeVendas.Services.Exceptions
{
    public class NotFoundException : ApplicationException // herança do ApplicationException
    {
        public NotFoundException(string message) : base(message) // construtor recebe message e repassa para a classe base.
        {

        }
    }
}
