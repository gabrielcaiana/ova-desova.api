using Signa.Library.Helpers;
using Signa.OvaDesova.Api.Data.Interface;
using Signa.OvaDesova.Api.Domain.Models;
using System.Data;

namespace Signa.OvaDesova.Api.Data.Repository
{
    class DadosGeraisRepository : RepositoryBase, IDadosGeraisRepository
    {
        public DadosGeraisModel ConsultarDadosGerais(int tabelaPrecoFornecedorId)
        {
            var sql = @"
                        SELECT DISTINCT
                            TPF.TABELA_PRECO_FORNECEDOR_ID                          TabelaPrecoFornecedorId,
	                        ISNULL(CONVERT(VARCHAR(10), TPF.DATA_INICIO, 23), '')	DataInicio,
	                        ISNULL(CONVERT(VARCHAR(10), TPF.DATA_FIM, 23), '')		DataFim,
	                        CASE
		                        WHEN TPF.FORNECEDOR_ID > 0 THEN 'P'
		                        ELSE 'TP'
	                        END														TipoTabela,
	                        TPF.FORNECEDOR_ID										FornecedorId,
	                        ISNULL(F.NOME_FANTASIA, '')								NomeFantasia,
	                        ISNULL(F.CNPJ_CPF, '')									Cnpj,
	                        TPF.PERC_CARGA_IMO										PercCargaImo,
	                        TPF.VAL_CARGA_IMO										ValCargaImo,
	                        TPF.PERC_CARGA_REFRIGERADA								PercCargaRefrigerada,
	                        TPF.VAL_CARGA_REFRIGERADA								ValCargaRefrigerada,
	                        TPF.VAL_CARGA_PALETIZADA								ValCargaPaletizada,
	                        TPF.VAL_CARGA_BATIDA									ValCargaBatida,
	                        TPF.PERC_CARGA_MOVEIS_TINTAS							PercCargaMoveisTintas,
	                        TPF.PERC_PALETIZACAO_CARGA								PercPaletizacaoCarga,
	                        TPF.VAL_PALETIZACAO_CARGA								ValPaletizacaoCarga,
	                        TPF.VAL_DESPALETIZACAO_CARGA							ValDespaletizacaoCarga,
	                        TPF.VAL_USO_EMPILHADEIRA								ValUsoEmpilhadeira,
	                        TPF.VAL_USO_PALETEIRA									ValUsoPaleteira,
	                        TPF.VAL_ETIQUETAGEM										ValEtiquetagem,
	                        TPF.PERC_OPERACAO_FRUSTRADA_COM_AVISO_PREVIO			PercOperacaoFrustradaComAvisoPrevio,
	                        TPF.PERC_OPERACAO_FRUSTRADA_SEM_AVISO_PREVIO			PercOperacaoFrustradaSemAvisoPrevio,
	                        TPF.PERC_OPERACAO_FIM_DE_SEMANA							PercOperacaoFimDeSemana,
	                        TPF.VAL_HORAS_EXTRAS_ESTADIA							ValHorasExtrasEstadia,
	                        TPF.PERC_ADICIONAL_NOTURNO								PercAdicionalNoturno,
	                        TPF.PERC_ADICIONAL_NOTURNO_FIM_DE_SEMANA				PercAdicionalNoturnoFimDeSemana,
	                        TPF.VAL_REEMBALAGEM_MERCADORIA							ValReembalagemMercadoria,
	                        TPF.PERC_REENVIO_DE_EQUIPE								PercReenvioDeEquipe,
	                        TPF.VAL_TRANSPORTE_FIXO									ValTransporteFixo,
	                        TPF.VAL_VISTORIA										ValVistoria
                        FROM
	                        TABELA_PRECO_FORNECEDOR TPF
	                        OUTER APPLY (SELECT PESSOA_ID, NOME_FANTASIA, CNPJ_CPF FROM VFORNEC_TAB_TIPO_FORNEC2 WHERE PESSOA_ID = TPF.FORNECEDOR_ID) F
                        WHERE
	                        TPF.TAB_STATUS_ID = 1
	                        AND TPF.TAB_TIPO_TABELA_ID = 50
	                        AND TPF.TABELA_PRECO_FORNECEDOR_ID = @TabelaPrecoFornecedorId";

            var param = new
            {
                TabelaPrecoFornecedorId = tabelaPrecoFornecedorId
            };

            return RepositoryHelper.QueryFirst<DadosGeraisModel>(sql, param, CommandType.Text);
        }

        public int Insert(DadosGeraisModel dadosGerais)
        {
            var sql = @"
                        INSERT INTO TABELA_PRECO_FORNECEDOR
                        (
	                        FORNECEDOR_ID,
	                        TAB_TIPO_TABELA_ID,
	                        DATA_INICIO,
	                        DATA_FIM,
	                        PERC_CARGA_IMO,
	                        VAL_CARGA_IMO,
	                        PERC_CARGA_REFRIGERADA,
	                        VAL_CARGA_REFRIGERADA,
	                        VAL_CARGA_PALETIZADA,
	                        VAL_CARGA_BATIDA,
	                        PERC_CARGA_MOVEIS_TINTAS,
	                        PERC_PALETIZACAO_CARGA,
	                        VAL_PALETIZACAO_CARGA,
	                        VAL_DESPALETIZACAO_CARGA,
	                        VAL_USO_EMPILHADEIRA,
	                        VAL_USO_PALETEIRA,
	                        VAL_ETIQUETAGEM,
	                        PERC_OPERACAO_FRUSTRADA_COM_AVISO_PREVIO,
	                        PERC_OPERACAO_FRUSTRADA_SEM_AVISO_PREVIO,
	                        PERC_OPERACAO_FIM_DE_SEMANA,
	                        VAL_HORAS_EXTRAS_ESTADIA,
	                        PERC_ADICIONAL_NOTURNO,
	                        PERC_ADICIONAL_NOTURNO_FIM_DE_SEMANA,
	                        VAL_REEMBALAGEM_MERCADORIA,
	                        PERC_REENVIO_DE_EQUIPE,
	                        VAL_TRANSPORTE_FIXO,
	                        VAL_VISTORIA,
	                        TAB_STATUS_ID
                        )
                        VALUES
                        (
                            @FornecedorId,
                            50,
                            @DataInicio,
                            @DataFim,
                            @PercCargaImo,
                            @ValCargaImo,
                            @PercCargaRefrigerada,
                            @ValCargaRefrigerada,
                            @ValCargaPaletizada,
                            @ValCargaBatida,
                            @PercCargaMoveisTintas,
                            @PercPaletizacaoCarga,
                            @ValPaletizacaoCarga,
                            @ValDespaletizacaoCarga,
                            @ValUsoEmpilhadeira,
                            @ValUsoPaleteira,
                            @ValEtiquetagem,
                            @PercOperacaoFrustradaComAvisoPrevio,
                            @PercOperacaoFrustradaSemAvisoPrevio,
                            @PercOperacaoFimDeSemana,
                            @ValHorasExtrasEstadia,
                            @PercAdicionalNoturno,
                            @PercAdicionalNoturnoFimDeSemana,
                            @ValReembalagemMercadoria,
                            @PercReenvioDeEquipe,
                            @ValTransporteFixo,
                            @ValVistoria,
                            1
                        )

                        SELECT SCOPE_IDENTITY()";

            var param = new
            {
                dadosGerais.FornecedorId,
                dadosGerais.DataInicio,
                dadosGerais.DataFim,
                dadosGerais.PercCargaImo,
                dadosGerais.ValCargaImo,
                dadosGerais.PercCargaRefrigerada,
                dadosGerais.ValCargaRefrigerada,
                dadosGerais.ValCargaPaletizada,
                dadosGerais.ValCargaBatida,
                dadosGerais.PercCargaMoveisTintas,
                dadosGerais.PercPaletizacaoCarga,
                dadosGerais.ValPaletizacaoCarga,
                dadosGerais.ValDespaletizacaoCarga,
                dadosGerais.ValUsoEmpilhadeira,
                dadosGerais.ValUsoPaleteira,
                dadosGerais.ValEtiquetagem,
                dadosGerais.PercOperacaoFrustradaComAvisoPrevio,
                dadosGerais.PercOperacaoFrustradaSemAvisoPrevio,
                dadosGerais.PercOperacaoFimDeSemana,
                dadosGerais.ValHorasExtrasEstadia,
                dadosGerais.PercAdicionalNoturno,
                dadosGerais.PercAdicionalNoturnoFimDeSemana,
                dadosGerais.ValReembalagemMercadoria,
                dadosGerais.PercReenvioDeEquipe,
                dadosGerais.ValTransporteFixo,
                dadosGerais.ValVistoria
            };

            return RepositoryHelper.QueryFirstOrDefault<int>(sql, param, CommandType.Text);
        }

        public void Update(DadosGeraisModel dadosGerais)
        {
            var sql = @"
                        UPDATE
	                        TABELA_PRECO_FORNECEDOR
                        SET
	                        FORNECEDOR_ID = @FornecedorId,
	                        DATA_INICIO = @DataInicio,
	                        DATA_FIM = @DataFim,
	                        PERC_CARGA_IMO = @PercCargaImo,
	                        VAL_CARGA_IMO = @ValCargaImo,
	                        PERC_CARGA_REFRIGERADA = @PercCargaRefrigerada,
	                        VAL_CARGA_REFRIGERADA = @ValCargaRefrigerada,
	                        VAL_CARGA_PALETIZADA = @ValCargaPaletizada,
	                        VAL_CARGA_BATIDA = @ValCargaBatida,
	                        PERC_CARGA_MOVEIS_TINTAS = @PercCargaMoveisTintas,
	                        PERC_PALETIZACAO_CARGA = @PercPaletizacaoCarga,
	                        VAL_PALETIZACAO_CARGA = @ValPaletizacaoCarga,
	                        VAL_DESPALETIZACAO_CARGA = @ValDespaletizacaoCarga,
	                        VAL_USO_EMPILHADEIRA = @ValUsoEmpilhadeira,
	                        VAL_USO_PALETEIRA = @ValUsoPaleteira,
	                        VAL_ETIQUETAGEM = @ValEtiquetagem,
	                        PERC_OPERACAO_FRUSTRADA_COM_AVISO_PREVIO = @PercOperacaoFrustradaComAvisoPrevio,
	                        PERC_OPERACAO_FRUSTRADA_SEM_AVISO_PREVIO = @PercOperacaoFrustradaSemAvisoPrevio,
	                        PERC_OPERACAO_FIM_DE_SEMANA = @PercOperacaoFimDeSemana,
	                        VAL_HORAS_EXTRAS_ESTADIA = @ValHorasExtrasEstadia,
	                        PERC_ADICIONAL_NOTURNO = @PercAdicionalNoturno,
	                        PERC_ADICIONAL_NOTURNO_FIM_DE_SEMANA = @PercAdicionalNoturnoFimDeSemana,
	                        VAL_REEMBALAGEM_MERCADORIA = @ValReembalagemMercadoria,
	                        PERC_REENVIO_DE_EQUIPE = @PercReenvioDeEquipe,
	                        VAL_TRANSPORTE_FIXO = @ValTransporteFixo,
	                        VAL_VISTORIA = @ValVistoria
                        WHERE
	                        TABELA_PRECO_FORNECEDOR_ID = @TabelaPrecoFornecedorId";

            var param = new
            {
                dadosGerais.TabelaPrecoFornecedorId,
                dadosGerais.FornecedorId,
                dadosGerais.DataInicio,
                dadosGerais.DataFim,
                dadosGerais.PercCargaImo,
                dadosGerais.ValCargaImo,
                dadosGerais.PercCargaRefrigerada,
                dadosGerais.ValCargaRefrigerada,
                dadosGerais.ValCargaPaletizada,
                dadosGerais.ValCargaBatida,
                dadosGerais.PercCargaMoveisTintas,
                dadosGerais.PercPaletizacaoCarga,
                dadosGerais.ValPaletizacaoCarga,
                dadosGerais.ValDespaletizacaoCarga,
                dadosGerais.ValUsoEmpilhadeira,
                dadosGerais.ValUsoPaleteira,
                dadosGerais.ValEtiquetagem,
                dadosGerais.PercOperacaoFrustradaComAvisoPrevio,
                dadosGerais.PercOperacaoFrustradaSemAvisoPrevio,
                dadosGerais.PercOperacaoFimDeSemana,
                dadosGerais.ValHorasExtrasEstadia,
                dadosGerais.PercAdicionalNoturno,
                dadosGerais.PercAdicionalNoturnoFimDeSemana,
                dadosGerais.ValReembalagemMercadoria,
                dadosGerais.PercReenvioDeEquipe,
                dadosGerais.ValTransporteFixo,
                dadosGerais.ValVistoria
            };

            RepositoryHelper.Execute(sql, param, CommandType.Text);
        }
        
        public void Delete(int tabelaPrecoFornecedorId)
        {
            var sql = @"
                        UPDATE
	                        TABELA_PRECO_FORNECEDOR
                        SET
	                        TAB_STATUS_ID = 2
                        WHERE
	                        TABELA_PRECO_FORNECEDOR_ID = @TabelaPrecoFornecedorId";

            var param = new
            {
                TabelaPrecoFornecedorId = tabelaPrecoFornecedorId
            };

            RepositoryHelper.Execute(sql, param, CommandType.Text);
        }
        
        public bool VerificarDuplicidade(DadosGeraisModel dadosGerais)
        {
            var sql = @"
                        SELECT
	                        1
                        FROM
	                        TABELA_PRECO_FORNECEDOR
                        WHERE
	                        TAB_STATUS_ID = 1
	                        AND TAB_TIPO_TABELA_ID = 50
	                        AND FORNECEDOR_ID = @FornecedorId
	                        AND (TABELA_PRECO_FORNECEDOR_ID <> @TabelaPrecoFornecedorId OR ISNULL(@TabelaPrecoFornecedorId, 0) = 0)
	                        AND (
			                        (DATA_INICIO BETWEEN @DataInicio AND @DataFim) OR
			                        (DATA_FIM BETWEEN @DataInicio AND @DataFim) OR
			                        (ISNULL(@DataInicio, '') = '' AND ISNULL(@DataFim, '') = '')
		                        )";

            var param = new
            {
                dadosGerais.FornecedorId,
                dadosGerais.TabelaPrecoFornecedorId,
                dadosGerais.DataInicio,
                dadosGerais.DataFim
            };

            return RepositoryHelper.QueryFirstOrDefault<int>(sql, param, CommandType.Text) == 1;
        }

        public void GravarHistorico(int tabelaPrecoFornecedorId, int usuarioId)
        {
            var sql = @"
                        INSERT INTO HIST_TABELA_PRECO_FORNECEDOR
                        (
	                        TABELA_PRECO_FORNECEDOR_ID,
	                        FORNECEDOR_ID,
	                        TAB_TIPO_TABELA_ID,
	                        DATA_INICIO,
	                        DATA_FIM,
	                        PERC_CARGA_IMO,
	                        VAL_CARGA_IMO,
	                        PERC_CARGA_REFRIGERADA,
	                        VAL_CARGA_REFRIGERADA,
	                        VAL_CARGA_PALETIZADA,
	                        VAL_CARGA_BATIDA,
	                        PERC_CARGA_MOVEIS_TINTAS,
	                        PERC_PALETIZACAO_CARGA,
	                        VAL_PALETIZACAO_CARGA,
	                        VAL_DESPALETIZACAO_CARGA,
	                        VAL_USO_EMPILHADEIRA,
	                        VAL_USO_PALETEIRA,
	                        VAL_ETIQUETAGEM,
	                        PERC_OPERACAO_FRUSTRADA_COM_AVISO_PREVIO,
	                        PERC_OPERACAO_FRUSTRADA_SEM_AVISO_PREVIO,
	                        PERC_OPERACAO_FIM_DE_SEMANA,
	                        VAL_HORAS_EXTRAS_ESTADIA,
	                        PERC_ADICIONAL_NOTURNO,
	                        PERC_ADICIONAL_NOTURNO_FIM_DE_SEMANA,
	                        VAL_REEMBALAGEM_MERCADORIA,
	                        PERC_REENVIO_DE_EQUIPE,
	                        VAL_TRANSPORTE_FIXO,
	                        VAL_VISTORIA,
	                        TAB_STATUS_ID,
	                        DATA_INCL,
	                        USUARIO_INCL_ID
                        )
                        SELECT
	                        TABELA_PRECO_FORNECEDOR_ID,
	                        FORNECEDOR_ID,
	                        TAB_TIPO_TABELA_ID,
	                        DATA_INICIO,
	                        DATA_FIM,
	                        PERC_CARGA_IMO,
	                        VAL_CARGA_IMO,
	                        PERC_CARGA_REFRIGERADA,
	                        VAL_CARGA_REFRIGERADA,
	                        VAL_CARGA_PALETIZADA,
	                        VAL_CARGA_BATIDA,
	                        PERC_CARGA_MOVEIS_TINTAS,
	                        PERC_PALETIZACAO_CARGA,
	                        VAL_PALETIZACAO_CARGA,
	                        VAL_DESPALETIZACAO_CARGA,
	                        VAL_USO_EMPILHADEIRA,
	                        VAL_USO_PALETEIRA,
	                        VAL_ETIQUETAGEM,
	                        PERC_OPERACAO_FRUSTRADA_COM_AVISO_PREVIO,
	                        PERC_OPERACAO_FRUSTRADA_SEM_AVISO_PREVIO,
	                        PERC_OPERACAO_FIM_DE_SEMANA,
	                        VAL_HORAS_EXTRAS_ESTADIA,
	                        PERC_ADICIONAL_NOTURNO,
	                        PERC_ADICIONAL_NOTURNO_FIM_DE_SEMANA,
	                        VAL_REEMBALAGEM_MERCADORIA,
	                        PERC_REENVIO_DE_EQUIPE,
	                        VAL_TRANSPORTE_FIXO,
	                        VAL_VISTORIA,
	                        TAB_STATUS_ID,
	                        GETDATE(),
	                        @UsuarioId
                        FROM
	                        TABELA_PRECO_FORNECEDOR
                        WHERE
	                        TABELA_PRECO_FORNECEDOR_ID = @TabelaPrecoFornecedorId";

            var param = new
            {
                TabelaPrecoFornecedorId = tabelaPrecoFornecedorId,
                UsuarioId = usuarioId
            };

            RepositoryHelper.Execute(sql, param, CommandType.Text);
        }
    }
}