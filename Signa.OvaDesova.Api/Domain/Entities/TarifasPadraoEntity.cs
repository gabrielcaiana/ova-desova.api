namespace Signa.OvaDesova.Api.Domain.Entities
{
  public class TarifasPadraoEntity
  {
    public int TabelaOvaDesovaId { get; set; }
    public int TabelaPrecoFornecedorId { get; set; }
    public string Conferente { get; set; }
    public string Ajudante1 { get; set; }
    public string Ajudante2 { get; set; }
    public string Ajudante3 { get; set; }
    public string Ajudante4 { get; set; }
    public string Ajudante5 { get; set; }
    public string Ajudante6 { get; set; }
    public string Ajudante7 { get; set; }
    public string Ajudante8 { get; set; }
    public MunicipioEntity Municipio { get; set; }
    public int TabStatusId { get; set; }
    public string DataLog { get; set; }
    public string UsuarioLog { get; set; }
  }
}