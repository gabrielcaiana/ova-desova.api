using Dapper;
using Signa.Library.Core.Data.Repository;
using Signa.OvaDesova.Api.Domain.Entities;
using System.Collections.Generic;
using Signa.OvaDesova.Api;

namespace Signa.OvaDesova.Api.Data.Repository
{
  public class DadosGeraisDAO : RepositoryBase
  {
    public DadosGeraisEntity ConsultarDadosGerais(int tabelaPrecoFornecedorId)
    {
      var sql = @"
                        SELECT DISTINCT
                            TPF.TABELA_PRECO_FORNECEDOR_ID																				TabelaPrecoFornecedorId,
	                        ISNULL(CONVERT(VARCHAR(10), TPF.DATA_INICIO, 23), '')														DataInicio,
	                        ISNULL(CONVERT(VARCHAR(10), TPF.DATA_FIM, 23), '')															DataFim,
	                        CASE
		                        WHEN TPF.FORNECEDOR_ID > 0 THEN 'P'
		                        ELSE 'TP'
	                        END																											TipoTabela,
	                        TPF.FORNECEDOR_ID																							FornecedorId,
	                        ISNULL(F.NOME_FANTASIA, '')																					NomeFantasia,
	                        ISNULL(F.CNPJ_CPF, '')																						Cnpj,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.PERC_CARGA_IMO,0),'0,00'))								PercCargaImo,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.VAL_CARGA_IMO,0),'0,00'))								ValCargaImo,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.PERC_CARGA_REFRIGERADA,0),'0,00'))						PercCargaRefrigerada,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.VAL_CARGA_REFRIGERADA,0),'0,00'))						ValCargaRefrigerada,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.VAL_CARGA_PALETIZADA,0),'0,00'))						ValCargaPaletizada,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.VAL_CARGA_BATIDA,0),'0,00'))							ValCargaBatida,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.PERC_CARGA_MOVEIS_TINTAS,0),'0,00'))					PercCargaMoveisTintas,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.PERC_PALETIZACAO_CARGA,0),'0,00'))						PercPaletizacaoCarga,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.VAL_PALETIZACAO_CARGA,0),'0,00'))						ValPaletizacaoCarga,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.VAL_DESPALETIZACAO_CARGA,0),'0,00'))					ValDespaletizacaoCarga,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.VAL_USO_EMPILHADEIRA,0),'0,00'))						ValUsoEmpilhadeira,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.VAL_USO_PALETEIRA,0),'0,00'))							ValUsoPaleteira,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.VAL_ETIQUETAGEM,0),'0,00'))								ValEtiquetagem,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.PERC_OPERACAO_FRUSTRADA_COM_AVISO_PREVIO,0),'0,00'))	PercOperacaoFrustradaComAvisoPrevio,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.PERC_OPERACAO_FRUSTRADA_SEM_AVISO_PREVIO,0),'0,00'))	PercOperacaoFrustradaSemAvisoPrevio,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.PERC_OPERACAO_FIM_DE_SEMANA,0),'0,00'))					PercOperacaoFimDeSemana,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.VAL_HORAS_EXTRAS_ESTADIA,0),'0,00'))					ValHorasExtrasEstadia,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.PERC_ADICIONAL_NOTURNO,0),'0,00'))						PercAdicionalNoturno,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.PERC_ADICIONAL_NOTURNO_FIM_DE_SEMANA,0),'0,00'))		PercAdicionalNoturnoFimDeSemana,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.VAL_REEMBALAGEM_MERCADORIA,0),'0,00'))					ValReembalagemMercadoria,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.PERC_REENVIO_DE_EQUIPE,0),'0,00'))						PercReenvioDeEquipe,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.VAL_TRANSPORTE_FIXO,0),'0,00'))							ValTransporteFixo,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.VAL_VISTORIA,0),'0,00'))								ValVistoria,
							Cast(Case
                                When TPF.TAB_STATUS_ID = 2
                                Then 1
                                Else 0
                            End As Bit) IsCanceled
                        FROM
	                        TABELA_PRECO_FORNECEDOR TPF
	                        OUTER APPLY (SELECT PESSOA_ID, NOME_FANTASIA, CNPJ_CPF FROM VFORNEC_TAB_TIPO_FORNEC2 WHERE PESSOA_ID = TPF.FORNECEDOR_ID) F
                        WHERE
	                        TPF.TABELA_PRECO_FORNECEDOR_ID = @TabelaPrecoFornecedorId";

      var param = new
      {
        TabelaPrecoFornecedorId = tabelaPrecoFornecedorId
      };
      using (var db = Connection)
      {
        return db.QueryFirst<DadosGeraisEntity>(sql, param);
      }

    }

    public int Insert(DadosGeraisEntity dadosGerais)
    {
      var sql = @"
                        DECLARE
                            @ID INT

                        UPDATE
                            INFRA_IDS
                        SET
                            @ID = TABELA_PRECO_FORNECEDOR_ID += 1
        
                        INSERT INTO TABELA_PRECO_FORNECEDOR
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
	                        TAB_STATUS_ID
                        )
                        VALUES
                        (
                            @ID,
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

                        SELECT @ID";

      var param = new
      {
        dadosGerais.FornecedorId,
        dadosGerais.DataInicio,
        dadosGerais.DataFim,
        PercCargaImo = Utils.ConverterValor(dadosGerais.PercCargaImo),
        ValCargaImo = Utils.ConverterValor(dadosGerais.ValCargaImo),
        PercCargaRefrigerada = Utils.ConverterValor(dadosGerais.PercCargaRefrigerada),
        ValCargaRefrigerada = Utils.ConverterValor(dadosGerais.ValCargaRefrigerada),
        ValCargaPaletizada = Utils.ConverterValor(dadosGerais.ValCargaPaletizada),
        ValCargaBatida = Utils.ConverterValor(dadosGerais.ValCargaBatida),
        PercCargaMoveisTintas = Utils.ConverterValor(dadosGerais.PercCargaMoveisTintas),
        PercPaletizacaoCarga = Utils.ConverterValor(dadosGerais.PercPaletizacaoCarga),
        ValPaletizacaoCarga = Utils.ConverterValor(dadosGerais.ValPaletizacaoCarga),
        ValDespaletizacaoCarga = Utils.ConverterValor(dadosGerais.ValDespaletizacaoCarga),
        ValUsoEmpilhadeira = Utils.ConverterValor(dadosGerais.ValUsoEmpilhadeira),
        ValUsoPaleteira = Utils.ConverterValor(dadosGerais.ValUsoPaleteira),
        ValEtiquetagem = Utils.ConverterValor(dadosGerais.ValEtiquetagem),
        PercOperacaoFrustradaComAvisoPrevio = Utils.ConverterValor(dadosGerais.PercOperacaoFrustradaComAvisoPrevio),
        PercOperacaoFrustradaSemAvisoPrevio = Utils.ConverterValor(dadosGerais.PercOperacaoFrustradaSemAvisoPrevio),
        PercOperacaoFimDeSemana = Utils.ConverterValor(dadosGerais.PercOperacaoFimDeSemana),
        ValHorasExtrasEstadia = Utils.ConverterValor(dadosGerais.ValHorasExtrasEstadia),
        PercAdicionalNoturno = Utils.ConverterValor(dadosGerais.PercAdicionalNoturno),
        PercAdicionalNoturnoFimDeSemana = Utils.ConverterValor(dadosGerais.PercAdicionalNoturnoFimDeSemana),
        ValReembalagemMercadoria = Utils.ConverterValor(dadosGerais.ValReembalagemMercadoria),
        PercReenvioDeEquipe = Utils.ConverterValor(dadosGerais.PercReenvioDeEquipe),
        ValTransporteFixo = Utils.ConverterValor(dadosGerais.ValTransporteFixo),
        ValVistoria = Utils.ConverterValor(dadosGerais.ValVistoria)
      };
      using (var db = Connection)
      {
        return db.QueryFirstOrDefault<int>(sql, param);
      }

    }

    public void Update(DadosGeraisEntity dadosGerais)
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
        PercCargaImo = Utils.ConverterValor(dadosGerais.PercCargaImo),
        ValCargaImo = Utils.ConverterValor(dadosGerais.ValCargaImo),
        PercCargaRefrigerada = Utils.ConverterValor(dadosGerais.PercCargaRefrigerada),
        ValCargaRefrigerada = Utils.ConverterValor(dadosGerais.ValCargaRefrigerada),
        ValCargaPaletizada = Utils.ConverterValor(dadosGerais.ValCargaPaletizada),
        ValCargaBatida = Utils.ConverterValor(dadosGerais.ValCargaBatida),
        PercCargaMoveisTintas = Utils.ConverterValor(dadosGerais.PercCargaMoveisTintas),
        PercPaletizacaoCarga = Utils.ConverterValor(dadosGerais.PercPaletizacaoCarga),
        ValPaletizacaoCarga = Utils.ConverterValor(dadosGerais.ValPaletizacaoCarga),
        ValDespaletizacaoCarga = Utils.ConverterValor(dadosGerais.ValDespaletizacaoCarga),
        ValUsoEmpilhadeira = Utils.ConverterValor(dadosGerais.ValUsoEmpilhadeira),
        ValUsoPaleteira = Utils.ConverterValor(dadosGerais.ValUsoPaleteira),
        ValEtiquetagem = Utils.ConverterValor(dadosGerais.ValEtiquetagem),
        PercOperacaoFrustradaComAvisoPrevio = Utils.ConverterValor(dadosGerais.PercOperacaoFrustradaComAvisoPrevio),
        PercOperacaoFrustradaSemAvisoPrevio = Utils.ConverterValor(dadosGerais.PercOperacaoFrustradaSemAvisoPrevio),
        PercOperacaoFimDeSemana = Utils.ConverterValor(dadosGerais.PercOperacaoFimDeSemana),
        ValHorasExtrasEstadia = Utils.ConverterValor(dadosGerais.ValHorasExtrasEstadia),
        PercAdicionalNoturno = Utils.ConverterValor(dadosGerais.PercAdicionalNoturno),
        PercAdicionalNoturnoFimDeSemana = Utils.ConverterValor(dadosGerais.PercAdicionalNoturnoFimDeSemana),
        ValReembalagemMercadoria = Utils.ConverterValor(dadosGerais.ValReembalagemMercadoria),
        PercReenvioDeEquipe = Utils.ConverterValor(dadosGerais.PercReenvioDeEquipe),
        ValTransporteFixo = Utils.ConverterValor(dadosGerais.ValTransporteFixo),
        ValVistoria = Utils.ConverterValor(dadosGerais.ValVistoria)
      };
      using (var db = Connection)
      {
        db.Execute(sql, param);
      }
    }

    public bool VerificarDuplicidade(DadosGeraisEntity dadosGerais)
    {
      var sql = @"
                        SELECT
	                        1
                        FROM
	                        TABELA_PRECO_FORNECEDOR
                        WHERE
	                        TAB_STATUS_ID = 1
	                        AND TAB_TIPO_TABELA_ID = 50
	                        AND (FORNECEDOR_ID = @FornecedorId OR ISNULL(@FornecedorId, 0) = 0)
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
      using (var db = Connection)
      {
        return db.QueryFirstOrDefault<int>(sql, param) >= 1;
      }
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
      using (var db = Connection)
      {
        db.Execute(sql, param);
      }
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
      using (var db = Connection)
      {
        db.Execute(sql, param);
      }
    }

    public IEnumerable<DadosGeraisEntity> ConsultarHistorico(int tabelaPrecoFornecedorId)
    {
      var sql = @"
                        SELECT DISTINCT
                            TPF.TABELA_PRECO_FORNECEDOR_ID																				TabelaPrecoFornecedorId,
	                        CASE
		                        WHEN TPF.FORNECEDOR_ID > 0 THEN ISNULL(CONVERT(VARCHAR, TPF.DATA_INICIO, 103), 'N/A')
		                        ELSE 'N/A'
	                        END																											DataInicio,
	                        CASE
		                        WHEN TPF.FORNECEDOR_ID > 0 THEN ISNULL(CONVERT(VARCHAR, TPF.DATA_FIM, 103), 'N/A')
		                        ELSE 'N/A'
	                        END																											DataFim,
	                        CASE
		                        WHEN TPF.FORNECEDOR_ID > 0 THEN 'P'
		                        ELSE 'TP'
	                        END																											TipoTabela,
	                        TPF.FORNECEDOR_ID																							FornecedorId,
	                        ISNULL(F.NOME_FANTASIA, 'N/A')																				NomeFantasia,
	                        ISNULL(F.CNPJ_CPF, 'N/A')																					Cnpj,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.PERC_CARGA_IMO,0),'0,00'))								PercCargaImo,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.VAL_CARGA_IMO,0),'0,00'))								ValCargaImo,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.PERC_CARGA_REFRIGERADA,0),'0,00'))						PercCargaRefrigerada,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.VAL_CARGA_REFRIGERADA,0),'0,00'))						ValCargaRefrigerada,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.VAL_CARGA_PALETIZADA,0),'0,00'))						ValCargaPaletizada,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.VAL_CARGA_BATIDA,0),'0,00'))							ValCargaBatida,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.PERC_CARGA_MOVEIS_TINTAS,0),'0,00'))					PercCargaMoveisTintas,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.PERC_PALETIZACAO_CARGA,0),'0,00'))						PercPaletizacaoCarga,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.VAL_PALETIZACAO_CARGA,0),'0,00'))						ValPaletizacaoCarga,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.VAL_DESPALETIZACAO_CARGA,0),'0,00'))					ValDespaletizacaoCarga,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.VAL_USO_EMPILHADEIRA,0),'0,00'))						ValUsoEmpilhadeira,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.VAL_USO_PALETEIRA,0),'0,00'))							ValUsoPaleteira,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.VAL_ETIQUETAGEM,0),'0,00'))								ValEtiquetagem,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.PERC_OPERACAO_FRUSTRADA_COM_AVISO_PREVIO,0),'0,00'))	PercOperacaoFrustradaComAvisoPrevio,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.PERC_OPERACAO_FRUSTRADA_SEM_AVISO_PREVIO,0),'0,00'))	PercOperacaoFrustradaSemAvisoPrevio,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.PERC_OPERACAO_FIM_DE_SEMANA,0),'0,00'))					PercOperacaoFimDeSemana,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.VAL_HORAS_EXTRAS_ESTADIA,0),'0,00'))					ValHorasExtrasEstadia,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.PERC_ADICIONAL_NOTURNO,0),'0,00'))						PercAdicionalNoturno,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.PERC_ADICIONAL_NOTURNO_FIM_DE_SEMANA,0),'0,00'))		PercAdicionalNoturnoFimDeSemana,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.VAL_REEMBALAGEM_MERCADORIA,0),'0,00'))					ValReembalagemMercadoria,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.PERC_REENVIO_DE_EQUIPE,0),'0,00'))						PercReenvioDeEquipe,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.VAL_TRANSPORTE_FIXO,0),'0,00'))							ValTransporteFixo,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TPF.VAL_VISTORIA,0),'0,00'))								ValVistoria,
		                    TPF.TAB_STATUS_ID																							TabStatusId,
                            CONVERT(VARCHAR, TPF.DATA_INCL, 103) + ' ' + CONVERT(VARCHAR, TPF.DATA_INCL, 108)							DataLog,
		                    VU.NOME_USUARIO																								UsuarioLog,
                            TPF.DATA_INCL
                        FROM
	                        HIST_TABELA_PRECO_FORNECEDOR TPF
	                        OUTER APPLY (SELECT PESSOA_ID, NOME_FANTASIA, CNPJ_CPF FROM VFORNEC_TAB_TIPO_FORNEC2 WHERE PESSOA_ID = TPF.FORNECEDOR_ID) F
		                    INNER JOIN VUSUARIO VU ON VU.USUARIO_ID = TPF.USUARIO_INCL_ID
                        WHERE
		                    TPF.TABELA_PRECO_FORNECEDOR_ID = @TabelaPrecoFornecedorId
	                    ORDER BY TPF.DATA_INCL DESC";

      var param = new
      {
        TabelaPrecoFornecedorId = tabelaPrecoFornecedorId
      };
      using (var db = Connection)
      {
        return db.Query<DadosGeraisEntity>(sql, param);
      }
    }
  }
}