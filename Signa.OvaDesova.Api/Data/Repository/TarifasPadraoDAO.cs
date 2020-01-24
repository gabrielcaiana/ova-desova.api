using Dapper;
using Signa.Library.Core.Data.Repository;
using Signa.OvaDesova.Api.Domain.Entities;
using System.Collections.Generic;
using Signa.OvaDesova.Api;

namespace Signa.OvaDesova.Api.Data.Repository
{
  public class TarifasPadraoDAO : RepositoryBase
  {
    public IEnumerable<TarifasPadraoEntity> ConsultarTarifasPadrao(int tabelaPrecoFornecedorId)
    {
      var sql = @"
                        SELECT
	                        TOD.TABELA_OVA_DESOVA_ID														TabelaOvaDesovaId,
	                        TOD.TABELA_PRECO_FORNECEDOR_ID													TabelaPrecoFornecedorId,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TOD.VAL_CONFERENTE,0),'0,00'))	Conferente,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TOD.AJUDANTE_1,0),'0,00'))		Ajudante1,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TOD.AJUDANTE_2,0),'0,00'))		Ajudante2,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TOD.AJUDANTE_3,0),'0,00'))		Ajudante3,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TOD.AJUDANTE_4,0),'0,00'))		Ajudante4,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TOD.AJUDANTE_5,0),'0,00'))		Ajudante5,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TOD.AJUDANTE_6,0),'0,00'))		Ajudante6,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TOD.AJUDANTE_7,0),'0,00'))		Ajudante7,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TOD.AJUDANTE_8,0),'0,00'))		Ajudante8,
	                        TOD.MUNICIPIO_ID                                                                MunicipioId,
	                        MUN.MUNICIPIO + ' - ' + MUN.UF                                                  NomeMunicipio
                        FROM
	                        TABELA_OVA_DESOVA TOD
                            INNER JOIN TABELA_PRECO_FORNECEDOR TPF ON TPF.TABELA_PRECO_FORNECEDOR_ID = TOD.TABELA_PRECO_FORNECEDOR_ID
	                        INNER JOIN MUNICIPIO MUN ON MUN.MUNICIPIO_ID = TOD.MUNICIPIO_ID
                        WHERE
	                        TPF.TABELA_PRECO_FORNECEDOR_ID = @TabelaPrecoFornecedorId
                            AND TOD.TAB_STATUS_ID = 1
                        ORDER BY MUN.MUNICIPIO";

      var param = new
      {
        TabelaPrecoFornecedorId = tabelaPrecoFornecedorId
      };

      using (var db = Connection)
      {
        return db.Query<TarifasPadraoEntity, MunicipioEntity, TarifasPadraoEntity>(
                sql,
                (tarifasPadraoEntity, municipioEntity) =>
                {
                  tarifasPadraoEntity.Municipio = municipioEntity;
                  return tarifasPadraoEntity;
                },
                param,
                splitOn: "MunicipioId"
                );
      }
    }

    public int Insert(TarifasPadraoEntity tarifasPadrao)
    {
      var sql = @"
                        DECLARE
                            @ID INT

                        UPDATE
                            INFRA_IDS
                        SET
                            @ID = TABELA_OVA_DESOVA_ID += 1
        
                        INSERT INTO TABELA_OVA_DESOVA
                        (
                            TABELA_OVA_DESOVA_ID,
	                        TABELA_PRECO_FORNECEDOR_ID,
	                        VAL_CONFERENTE,
	                        AJUDANTE_1,
	                        AJUDANTE_2,
	                        AJUDANTE_3,
	                        AJUDANTE_4,
	                        AJUDANTE_5,
	                        AJUDANTE_6,
	                        AJUDANTE_7,
	                        AJUDANTE_8,
	                        MUNICIPIO_ID,
	                        TAB_STATUS_ID
                        )
                        VALUES
                        (
                            @ID,
                            @TabelaPrecoFornecedorId,
                            @Conferente,
                            @Ajudante1,
                            @Ajudante2,
                            @Ajudante3,
                            @Ajudante4,
                            @Ajudante5,
                            @Ajudante6,
                            @Ajudante7,
                            @Ajudante8,
                            @MunicipioId,
                            1
                        )

                        SELECT @ID";

      var param = new
      {
        tarifasPadrao.TabelaPrecoFornecedorId,
        Conferente = Utils.ConverterValor(tarifasPadrao.Conferente),
        Ajudante1 = Utils.ConverterValor(tarifasPadrao.Ajudante1),
        Ajudante2 = Utils.ConverterValor(tarifasPadrao.Ajudante2),
        Ajudante3 = Utils.ConverterValor(tarifasPadrao.Ajudante3),
        Ajudante4 = Utils.ConverterValor(tarifasPadrao.Ajudante4),
        Ajudante5 = Utils.ConverterValor(tarifasPadrao.Ajudante5),
        Ajudante6 = Utils.ConverterValor(tarifasPadrao.Ajudante6),
        Ajudante7 = Utils.ConverterValor(tarifasPadrao.Ajudante7),
        Ajudante8 = Utils.ConverterValor(tarifasPadrao.Ajudante8),
        tarifasPadrao.Municipio.MunicipioId
      };
      using (var db = Connection)
      {
        return db.QueryFirstOrDefault<int>(sql, param);
      }

    }

    public void Update(TarifasPadraoEntity tarifasPadrao)
    {
      var sql = @"
                        UPDATE
	                        TABELA_OVA_DESOVA
                        SET
	                        VAL_CONFERENTE = @Conferente,
	                        AJUDANTE_1 = @Ajudante1,
	                        AJUDANTE_2 = @Ajudante2,
	                        AJUDANTE_3 = @Ajudante3,
	                        AJUDANTE_4 = @Ajudante4,
	                        AJUDANTE_5 = @Ajudante5,
	                        AJUDANTE_6 = @Ajudante6,
	                        AJUDANTE_7 = @Ajudante7,
	                        AJUDANTE_8 = @Ajudante8,
	                        MUNICIPIO_ID = @MunicipioId
                        WHERE
	                        TABELA_OVA_DESOVA_ID = @TabelaOvaDesovaId";

      var param = new
      {
        tarifasPadrao.TabelaOvaDesovaId,
        Conferente = Utils.ConverterValor(tarifasPadrao.Conferente),
        Ajudante1 = Utils.ConverterValor(tarifasPadrao.Ajudante1),
        Ajudante2 = Utils.ConverterValor(tarifasPadrao.Ajudante2),
        Ajudante3 = Utils.ConverterValor(tarifasPadrao.Ajudante3),
        Ajudante4 = Utils.ConverterValor(tarifasPadrao.Ajudante4),
        Ajudante5 = Utils.ConverterValor(tarifasPadrao.Ajudante5),
        Ajudante6 = Utils.ConverterValor(tarifasPadrao.Ajudante6),
        Ajudante7 = Utils.ConverterValor(tarifasPadrao.Ajudante7),
        Ajudante8 = Utils.ConverterValor(tarifasPadrao.Ajudante8),
        tarifasPadrao.Municipio.MunicipioId
      };
      using (var db = Connection)
      {
        db.Execute(sql, param);
      }

    }

    public void Delete(int tabelaOvaDesovaId)
    {
      var sql = @"
                        UPDATE
	                        TABELA_OVA_DESOVA
                        SET
	                        TAB_STATUS_ID = 2
                        WHERE
                          TAB_STATUS_ID = 1
	                        AND TABELA_OVA_DESOVA_ID = @TabelaOvaDesovaId";

      var param = new
      {
        TabelaOvaDesovaId = tabelaOvaDesovaId
      };
      using (var db = Connection)
      {
        db.Execute(sql, param);
      }

    }

    public bool VerificarDuplicidade(TarifasPadraoEntity tarifasPadrao)
    {
      var sql = @"
                        SELECT
	                        1
                        FROM
	                        TABELA_OVA_DESOVA
                        WHERE
	                        TAB_STATUS_ID = 1
	                        AND TABELA_OVA_DESOVA_ID <> @TabelaOvaDesovaId
	                        AND TABELA_PRECO_FORNECEDOR_ID = @TabelaPrecoFornecedorId
	                        AND MUNICIPIO_ID = @MunicipioId";

      var param = new
      {
        tarifasPadrao.TabelaOvaDesovaId,
        tarifasPadrao.TabelaPrecoFornecedorId,
        tarifasPadrao.Municipio.MunicipioId
      };
      using (var db = Connection)
      {
        return db.QueryFirstOrDefault<int>(sql, param) >= 1;
      }

    }

    public void GravarHistorico(int tabelaOvaDesovaId, int usuarioId)
    {
      var sql = @"
                        INSERT INTO HIST_TABELA_OVA_DESOVA
                        (
	                        TABELA_OVA_DESOVA_ID,
	                        TABELA_PRECO_FORNECEDOR_ID,
	                        MUNICIPIO_ID,
	                        VAL_CONFERENTE,
	                        AJUDANTE_1,
	                        AJUDANTE_2,
	                        AJUDANTE_3,
	                        AJUDANTE_4,
	                        AJUDANTE_5,
	                        AJUDANTE_6,
	                        AJUDANTE_7,
	                        AJUDANTE_8,
	                        TAB_STATUS_ID,
	                        DATA_INCL,
	                        USUARIO_INCL_ID
                        )
                        SELECT
	                        TABELA_OVA_DESOVA_ID,
	                        TABELA_PRECO_FORNECEDOR_ID,
	                        MUNICIPIO_ID,
	                        VAL_CONFERENTE,
	                        AJUDANTE_1,
	                        AJUDANTE_2,
	                        AJUDANTE_3,
	                        AJUDANTE_4,
	                        AJUDANTE_5,
	                        AJUDANTE_6,
	                        AJUDANTE_7,
	                        AJUDANTE_8,
	                        TAB_STATUS_ID,
	                        GETDATE(),
	                        @UsuarioId
                        FROM
	                        TABELA_OVA_DESOVA
                        WHERE
	                        TABELA_OVA_DESOVA_ID = @TabelaOvaDesovaId";

      var param = new
      {
        TabelaOvaDesovaId = tabelaOvaDesovaId,
        UsuarioId = usuarioId
      };
      using (var db = Connection)
      {
        db.Execute(sql, param);
      }

    }

    public IEnumerable<TarifasPadraoEntity> ConsultarHistorico(int tabelaOvaDesovaId)
    {
      var sql = @"
                        SELECT
	                        TOD.TABELA_OVA_DESOVA_ID															TabelaOvaDesovaId,
	                        TOD.TABELA_PRECO_FORNECEDOR_ID														TabelaPrecoFornecedorId,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TOD.VAL_CONFERENTE,0),'0,00'))		Conferente,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TOD.AJUDANTE_1,0),'0,00'))			Ajudante1,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TOD.AJUDANTE_2,0),'0,00'))			Ajudante2,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TOD.AJUDANTE_3,0),'0,00'))			Ajudante3,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TOD.AJUDANTE_4,0),'0,00'))			Ajudante4,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TOD.AJUDANTE_5,0),'0,00'))			Ajudante5,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TOD.AJUDANTE_6,0),'0,00'))			Ajudante6,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TOD.AJUDANTE_7,0),'0,00'))			Ajudante7,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TOD.AJUDANTE_8,0),'0,00'))			Ajudante8,
		                    TOD.TAB_STATUS_ID																	TabStatusId,
		                    CONVERT(VARCHAR, TOD.DATA_INCL, 103) + ' ' + CONVERT(VARCHAR, TOD.DATA_INCL, 108)	DataLog,
                            VU.NOME_USUARIO																		UsuarioLog,
	                        TOD.MUNICIPIO_ID																	MunicipioId,
	                        MUN.MUNICIPIO + ' - ' + MUN.UF														NomeMunicipio
                        FROM
	                        HIST_TABELA_OVA_DESOVA TOD
		                    INNER JOIN TABELA_PRECO_FORNECEDOR TPF ON TPF.TABELA_PRECO_FORNECEDOR_ID = TOD.TABELA_PRECO_FORNECEDOR_ID
		                    INNER JOIN MUNICIPIO MUN ON MUN.MUNICIPIO_ID = TOD.MUNICIPIO_ID
	                        OUTER APPLY (SELECT PESSOA_ID, NOME_FANTASIA, CNPJ_CPF FROM VFORNEC_TAB_TIPO_FORNEC2 WHERE PESSOA_ID = TPF.FORNECEDOR_ID) F
		                    INNER JOIN VUSUARIO VU ON VU.USUARIO_ID = TOD.USUARIO_INCL_ID
                        WHERE
		                    TOD.TABELA_OVA_DESOVA_ID = @TabelaOvaDesovaId
	                    ORDER BY TOD.DATA_INCL DESC";

      var param = new
      {
        TabelaOvaDesovaId = tabelaOvaDesovaId
      };

      using (var db = Connection)
      {
        return db.Query<TarifasPadraoEntity, MunicipioEntity, TarifasPadraoEntity>(
                sql,
                (tarifasPadraoEntity, municipioEntity) =>
                {
                  tarifasPadraoEntity.Municipio = municipioEntity;
                  return tarifasPadraoEntity;
                },
                param,
                splitOn: "MunicipioId"
                );
      }
    }

    public IEnumerable<TarifasPadraoEntity> ConsultarHistoricoExclusao(int tabelaPrecoFornecedorId)
    {
      var sql = @"
                        SELECT DISTINCT
	                        TOD.TABELA_OVA_DESOVA_ID															TabelaOvaDesovaId,
	                        TOD.TABELA_PRECO_FORNECEDOR_ID														TabelaPrecoFornecedorId,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TOD.VAL_CONFERENTE,0),'0,00'))		Conferente,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TOD.AJUDANTE_1,0),'0,00'))			Ajudante1,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TOD.AJUDANTE_2,0),'0,00'))			Ajudante2,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TOD.AJUDANTE_3,0),'0,00'))			Ajudante3,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TOD.AJUDANTE_4,0),'0,00'))			Ajudante4,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TOD.AJUDANTE_5,0),'0,00'))			Ajudante5,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TOD.AJUDANTE_6,0),'0,00'))			Ajudante6,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TOD.AJUDANTE_7,0),'0,00'))			Ajudante7,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TOD.AJUDANTE_8,0),'0,00'))			Ajudante8,
		                    TOD.TAB_STATUS_ID																	TabStatusId,
		                    CONVERT(VARCHAR, TOD.DATA_INCL, 103) + ' ' + CONVERT(VARCHAR, TOD.DATA_INCL, 108)	DataLog,
		                    VU.NOME_USUARIO																		UsuarioLog,
	                        TOD.MUNICIPIO_ID																	MunicipioId,
	                        MUN.MUNICIPIO + ' - ' + MUN.UF														NomeMunicipio,
		                    TOD.DATA_INCL
                        FROM
	                        HIST_TABELA_OVA_DESOVA TOD
		                    INNER JOIN TABELA_PRECO_FORNECEDOR TPF ON TPF.TABELA_PRECO_FORNECEDOR_ID = TOD.TABELA_PRECO_FORNECEDOR_ID
		                    INNER JOIN MUNICIPIO MUN ON MUN.MUNICIPIO_ID = TOD.MUNICIPIO_ID
	                        OUTER APPLY (SELECT PESSOA_ID, NOME_FANTASIA, CNPJ_CPF FROM VFORNEC_TAB_TIPO_FORNEC2 WHERE PESSOA_ID = TPF.FORNECEDOR_ID) F
		                    INNER JOIN VUSUARIO VU ON VU.USUARIO_ID = TOD.USUARIO_INCL_ID
                        WHERE
		                    TOD.TAB_STATUS_ID = 2
                            AND TOD.TABELA_PRECO_FORNECEDOR_ID = @TabelaPrecoFornecedorId
	                    ORDER BY TOD.DATA_INCL DESC";

      var param = new
      {
        TabelaPrecoFornecedorId = tabelaPrecoFornecedorId
      };

      using (var db = Connection)
      {
        return db.Query<TarifasPadraoEntity, MunicipioEntity, TarifasPadraoEntity>(
                sql,
                (tarifasPadraoEntity, municipioEntity) =>
                {
                  tarifasPadraoEntity.Municipio = municipioEntity;
                  return tarifasPadraoEntity;
                },
                param,
                splitOn: "MunicipioId"
                );
      }
    }
  }
}