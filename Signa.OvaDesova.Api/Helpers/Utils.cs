namespace Signa.OvaDesova.Api.Helpers
{
    public class Utils
    {
        public static string ConverterValor(string valor)
        {
            return (valor == "" ? "0" : valor).Replace(".", "").Replace(",", ".");
        }
    }
}