using Signa.Library.Helpers;
using Signa.OvaDesova.Api.Data.Interface;
using Signa.OvaDesova.Api.Domain.Models;
using System.Collections.Generic;
using System.Data;

namespace Signa.OvaDesova.Api.Data.Repository
{
    class OvaDesovaRepository : RepositoryBase, IOvaDesovaRepository
    {
        public IEnumerable<ResultadoModel> GetAll(ConsultaModel consulta)
        {
            var sql = @"
                        SELECT DISTINCT
	                        TPF.TABELA_PRECO_FORNECEDOR_ID							TabelaPrecoFornecedorId,
	                        ISNULL(F.NOME_FANTASIA, '')								NomeFantasia,
	                        ISNULL(F.CNPJ_CPF, '')									Cnpj,
	                        ISNULL(CONVERT(VARCHAR(10), TPF.DATA_INICIO, 103), '')  DataInicio,
	                        ISNULL(CONVERT(VARCHAR(10), TPF.DATA_FIM, 103), '')     DataFim
                        FROM
	                        TABELA_PRECO_FORNECEDOR TPF
	                        INNER JOIN TABELA_OVA_DESOVA TOD ON TOD.TABELA_PRECO_FORNECEDOR_ID = TPF.TABELA_PRECO_FORNECEDOR_ID
	                        INNER JOIN MUNICIPIO M ON M.MUNICIPIO_ID = TOD.MUNICIPIO_ID
	                        OUTER APPLY (SELECT NOME_FANTASIA, CNPJ_CPF FROM VFORNEC_TAB_TIPO_FORNEC2 WHERE PESSOA_ID = TPF.FORNECEDOR_ID) F
                        WHERE
	                        TPF.TAB_STATUS_ID = 1
	                        AND TOD.TAB_STATUS_ID = 1
	                        AND TPF.TAB_TIPO_TABELA_ID = 50
	                        AND (TOD.MUNICIPIO_ID = @MunicipioId OR ISNULL(@MunicipioId, 0) = 0)
	                        AND (M.TAB_UF_ID = @UfId OR ISNULL(@UfId, 0) = 0)
							AND (
									(ISNULL(TPF.FORNECEDOR_ID, 0) = 0 AND @TipoTabela = 'TP') OR
									(ISNULL(TPF.FORNECEDOR_ID, 0) <> 0 AND (TPF.FORNECEDOR_ID = @FornecedorId OR ISNULL(@FornecedorId, 0) = 0) AND @TipoTabela = 'P') OR
									(TPF.FORNECEDOR_ID = @FornecedorId OR ISNULL(@FornecedorId, 0) = 0 AND ISNULL(@TipoTabela, '') = '')
								)
	                        AND	(TPF.DATA_INICIO >= @PeriodoDe OR ISNULL(@PeriodoDe, '') = '')
	                        AND	(TPF.DATA_FIM <= @PeriodoAte OR ISNULL(@PeriodoAte, '') = '')
                        ORDER BY NomeFantasia";

            var param = new
            {
                consulta.MunicipioId,
                consulta.UfId,
                consulta.FornecedorId,
                consulta.PeriodoDe,
                consulta.PeriodoAte,
                consulta.TipoTabela
            };

            return RepositoryHelper.Query<ResultadoModel>(sql, param, CommandType.Text);
        }

        public TabelaPrecoFornecedorModel ConsultaDadosGerais(int tabelaPrecoFornecedorId)
        {
            var sql = @"
                        SELECT DISTINCT
	                        TPF.TABELA_PRECO_FORNECEDOR_ID							TabelaPrecoFornecedorId,
	                        ISNULL(CONVERT(VARCHAR(10), TPF.DATA_INICIO, 103), '')	DataInicio,
	                        ISNULL(CONVERT(VARCHAR(10), TPF.DATA_FIM, 103), '')		DataFim,
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

            return RepositoryHelper.QueryFirst<TabelaPrecoFornecedorModel>(sql, param, CommandType.Text);
        }
    }
}