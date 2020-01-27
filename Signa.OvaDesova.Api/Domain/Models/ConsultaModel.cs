namespace Signa.OvaDesova.Api.Domain.Models
{
    public class ConsultaModel
    {
        public int MunicipioId { get; set; }
        public int UfId { get; set; }
        public int FornecedorId { get; set; }
        public string PeriodoDe { get; set; }
        public string PeriodoAte { get; set; }
        public string TipoTabela { get; set; }
    }
}