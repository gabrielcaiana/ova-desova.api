namespace Signa.OvaDesova.Api.Domain.Models
{
    public class TarifaEspecialModel
    {
        public int TabelaTarifaEspecialId { get; set; }
        public int TabelaPrecoFornecedorId { get; set; }
        public string Conferente { get; set; }
        public string SemanalDiurno { get; set; }
        public string SemanalNoturno { get; set; }
        public string FdsDiurno { get; set; }
        public string FdsNoturno { get; set; }
        public MunicipioModel Municipio { get; set; }
        public VeiculoModel Veiculo { get; set; }
        public AcordoRodoviarioModel AcordoRodoviario { get; set; }
        public AcordoEspecialModel AcordoEspecial { get; set; }
        public FamiliaMercadoriaModel FamiliaMercadoria { get; set; }
        public int TabStatusId { get; set; }
        public string DataLog { get; set; }
        public string UsuarioLog { get; set; }
    }
}