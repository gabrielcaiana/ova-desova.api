namespace Signa.OvaDesova.Api.Helpers
{
    public class Utils
    {
        public static string ConverterValor(string valor)
        {
            return valor.Replace(".", "").Replace(",", ".");
        }
    }
}