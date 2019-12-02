namespace Signa.OvaDesova.Api.Domain.Models
{
    public class TarifasPadraoModel
    {
        public int TabelaOvaDesovaId { get; set; }
        public int TabelaPrecoFornecedorId { get; set; }
        public decimal Conferente { get; set; }
        public decimal Ajudante1 { get; set; }
        public decimal Ajudante2 { get; set; }
        public decimal Ajudante3 { get; set; }
        public decimal Ajudante4 { get; set; }
        public decimal Ajudante5 { get; set; }
        public decimal Ajudante6 { get; set; }
        public decimal Ajudante7 { get; set; }
        public decimal Ajudante8 { get; set; }
        public MunicipioModel Municipio { get; set; }
    }
}