namespace ControleDeVendas.Services.Exceptions
{
    public class DbCuncurrencyException : ApplicationException
    {
        public DbCuncurrencyException (string message) : base(message)
        {

        }
    }
}
