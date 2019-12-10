namespace Signa.OvaDesova.Api.Domain.Models
{
    public class MaterialPeacaoModel
    {
        public int TabelaTarifaMaterialId { get; set; }
        public int TabelaPrecoFornecedorId { get; set; }
        public int QtdBase { get; set; }
        public string Valor { get; set; }
        public bool NecessitaFrete { get; set; }
        public string NecessitaFreteTexto { get; set; }
        public MaterialModel Material { get; set; }
        public UnidadeMedidaModel Unidade { get; set; }
        public int TabStatusId { get; set; }
        public string DataLog { get; set; }
        public string UsuarioLog { get; set; }
    }
}