namespace Signa.OvaDesova.Api.Domain.Entities
{
    public class TarifaEspecialEntity
    {
        public int TabelaTarifaEspecialId { get; set; }
        public int TabelaPrecoFornecedorId { get; set; }
        public string Conferente { get; set; }
        public string SemanalDiurno { get; set; }
        public string SemanalNoturno { get; set; }
        public string FdsDiurno { get; set; }
        public string FdsNoturno { get; set; }
        public MunicipioEntity Municipio { get; set; }
        public VeiculoEntity Veiculo { get; set; }
        public AcordoRodoviarioEntity AcordoRodoviario { get; set; }
        public AcordoEspecialEntity AcordoEspecial { get; set; }
        public FamiliaMercadoriaEntity FamiliaMercadoria { get; set; }
        public int TabStatusId { get; set; }
        public string DataLog { get; set; }
        public string UsuarioLog { get; set; }
        public string Ajudante1 { get; set; }
        public string Ajudante2 { get; set; }
        public string Ajudante3 { get; set; }
        public string Ajudante4 { get; set; }
        public string Ajudante5 { get; set; }
        public string Ajudante6 { get; set; }
        public string Ajudante7 { get; set; }
        public string Ajudante8 { get; set; }
    }
}