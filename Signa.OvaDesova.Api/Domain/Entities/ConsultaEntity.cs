namespace Signa.OvaDesova.Api.Domain.Entities
{
    public class ConsultaEntity
    {
        public int MunicipioId { get; set; }
        public int UfId { get; set; }
        public int FornecedorId { get; set; }
        public string PeriodoDe { get; set; }
        public string PeriodoAte { get; set; }
        public string TipoTabela { get; set; }
    }
}