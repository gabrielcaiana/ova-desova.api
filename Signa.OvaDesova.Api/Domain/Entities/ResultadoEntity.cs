namespace Signa.OvaDesova.Api.Domain.Entities
{
    public class ResultadoEntity
    {
        public int TabelaPrecoFornecedorId { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        public string Status { get; set; }
    }
}