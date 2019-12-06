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
        public string PercCargaImo { get; set; }
        public string ValCargaImo { get; set; }
        public string PercCargaRefrigerada { get; set; }
        public string ValCargaRefrigerada { get; set; }
        public string ValCargaPaletizada { get; set; }
        public string ValCargaBatida { get; set; }
        public string PercCargaMoveisTintas { get; set; }
        public string PercPaletizacaoCarga { get; set; }
        public string ValPaletizacaoCarga { get; set; }
        public string ValDespaletizacaoCarga { get; set; }
        public string ValUsoEmpilhadeira { get; set; }
        public string ValUsoPaleteira { get; set; }
        public string ValEtiquetagem { get; set; }
        public string PercOperacaoFrustradaComAvisoPrevio { get; set; }
        public string PercOperacaoFrustradaSemAvisoPrevio { get; set; }
        public string PercOperacaoFimDeSemana { get; set; }
        public string ValHorasExtrasEstadia { get; set; }
        public string PercAdicionalNoturno { get; set; }
        public string PercAdicionalNoturnoFimDeSemana { get; set; }
        public string ValReembalagemMercadoria { get; set; }
        public string PercReenvioDeEquipe { get; set; }
        public string ValTransporteFixo { get; set; }
        public string ValVistoria { get; set; }
        public string TabStatusId { get; set; }
        public string DataLog { get; set; }
        public string UsuarioLog { get; set; }
    }
}