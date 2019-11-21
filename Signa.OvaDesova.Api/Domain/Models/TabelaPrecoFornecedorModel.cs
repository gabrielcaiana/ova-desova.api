namespace Signa.OvaDesova.Api.Domain.Models
{
    public class TabelaPrecoFornecedorModel
    {
        public int TabelaPrecoFornecedorId { get; set; }
        public int FornecedorId { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public string InscricaoEstadual { get; set; }
        public int Municipio_Id { get; set; }
        public string Municipio { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        public string TipoTabela { get; set; }
    }
}