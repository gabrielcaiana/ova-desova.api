namespace Signa.OvaDesova.Api.Domain.Models
{
    public class DadosGeraisModel
    {
        public int TabelaPrecoFornecedorId { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        public string TipoTabela { get; set; }
        public int? FornecedorId { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public decimal PercCargaImo { get; set; }
        public decimal ValCargaImo { get; set; }
        public decimal PercCargaRefrigerada { get; set; }
        public decimal ValCargaRefrigerada { get; set; }
        public decimal ValCargaPaletizada { get; set; }
        public decimal ValCargaBatida { get; set; }
        public decimal PercCargaMoveisTintas { get; set; }
        public decimal PercPaletizacaoCarga { get; set; }
        public decimal ValPaletizacaoCarga { get; set; }
        public decimal ValDespaletizacaoCarga { get; set; }
        public decimal ValUsoEmpilhadeira { get; set; }
        public decimal ValUsoPaleteira { get; set; }
        public decimal ValEtiquetagem { get; set; }
        public decimal PercOperacaoFrustradaComAvisoPrevio { get; set; }
        public decimal PercOperacaoFrustradaSemAvisoPrevio { get; set; }
        public decimal PercOperacaoFimDeSemana { get; set; }
        public decimal ValHorasExtrasEstadia { get; set; }
        public decimal PercAdicionalNoturno { get; set; }
        public decimal PercAdicionalNoturnoFimDeSemana { get; set; }
        public decimal ValReembalagemMercadoria { get; set; }
        public decimal PercReenvioDeEquipe { get; set; }
        public decimal ValTransporteFixo { get; set; }
        public decimal ValVistoria { get; set; }
    }
}