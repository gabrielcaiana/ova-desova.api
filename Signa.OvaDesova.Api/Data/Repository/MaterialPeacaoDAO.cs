using Dapper;
using Signa.Library.Core.Data.Repository;
using Signa.OvaDesova.Api.Domain.Entities;
using System.Collections.Generic;
using Signa.OvaDesova.Api;

namespace Signa.OvaDesova.Api.Data.Repository
{
  public class MaterialPeacaoDAO : RepositoryBase
  {
    public IEnumerable<MaterialPeacaoEntity> ConsultarMaterialPeacao(int tabelaPrecoFornecedorId)
    {
      var sql = @"
                        SELECT
	                        TTM.TABELA_TARIFA_MATERIAL_ID											TabelaTarifaMaterialId,
	                        TTM.TABELA_PRECO_FORNECEDOR_ID											TabelaPrecoFornecedorId,
		                    TTM.QUANTIDADE_BASE														QtdBase,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TTM.VALOR,0),'0,00'))	Valor,
		                    CAST(CASE
			                    WHEN TTM.FLAG_NECESSITA_FRETE = 'S'
			                    THEN 1
			                    ELSE 0
		                    END AS Bit)																NecessitaFrete,
		                    CASE
                                WHEN TTM.FLAG_NECESSITA_FRETE = 'S'
			                    THEN 'Sim'
			                    ELSE 'Não'
		                    END														        		NecessitaFreteTexto,
		                    TTM.TAB_TIPO_EQUIPAM_ID													TabTipoEquipamId,
		                    TTE.DESCR																DescMaterial,
		                    TTM.TAB_UNIDADE_MEDIDA_ID												TabUnidadeMedidaId,
		                    TUM.DESC_UNIDADE_MEDIDA													DescUnidadeMedida
                        FROM
	                        TABELA_TARIFA_MATERIAL TTM
                            INNER JOIN TABELA_PRECO_FORNECEDOR TPF ON TPF.TABELA_PRECO_FORNECEDOR_ID = TTM.TABELA_PRECO_FORNECEDOR_ID
	                        INNER JOIN TAB_TIPO_EQUIPAM TTE ON TTE.TAB_TIPO_EQUIPAM_ID = TTM.TAB_TIPO_EQUIPAM_ID
		                    INNER JOIN TAB_UNIDADE_MEDIDA TUM ON TUM.TAB_UNIDADE_MEDIDA_ID = TTM.TAB_UNIDADE_MEDIDA_ID
                        WHERE
	                        TPF.TABELA_PRECO_FORNECEDOR_ID = @TabelaPrecoFornecedorId
                            AND TTM.TAB_STATUS_ID = 1
                        ORDER BY TTE.DESCR";

      var param = new
      {
        TabelaPrecoFornecedorId = tabelaPrecoFornecedorId
      };

      using (var db = Connection)
      {
        return db.Query<MaterialPeacaoEntity, MaterialEntity, UnidadeMedidaEntity, MaterialPeacaoEntity>(
                sql,
                (materialPeacaoEntity, materialEntity, unidadeMedidaEntity) =>
                {
                  materialPeacaoEntity.Material = materialEntity;
                  materialPeacaoEntity.Unidade = unidadeMedidaEntity;
                  return materialPeacaoEntity;
                },
                param,
                splitOn: "TabTipoEquipamId, TabUnidadeMedidaId"
                );
      }
    }

    public int Insert(MaterialPeacaoEntity materialPeacao)
    {
      var sql = @"
	                    INSERT INTO TABELA_TARIFA_MATERIAL
	                    (
		                    TABELA_PRECO_FORNECEDOR_ID,
		                    QUANTIDADE_BASE,
		                    VALOR,
		                    FLAG_NECESSITA_FRETE,
		                    TAB_TIPO_EQUIPAM_ID,
		                    TAB_UNIDADE_MEDIDA_ID,
		                    TAB_STATUS_ID
	                    )
	                    VALUES
	                    (
                            @TabelaPrecoFornecedorId,
                            @QtdBase,
                            @Valor,
                            @NecessitaFrete,
                            @TabTipoEquipamId,
                            @TabUnidadeMedidaId,
                            1
	                    )

                        SELECT SCOPE_IDENTITY()";

      var param = new
      {
        materialPeacao.TabelaPrecoFornecedorId,
        materialPeacao.QtdBase,
        Valor = Utils.ConverterValor(materialPeacao.Valor),
        NecessitaFrete = materialPeacao.NecessitaFrete ? 'S' : 'N',
        materialPeacao.Material.TabTipoEquipamId,
        materialPeacao.Unidade.TabUnidadeMedidaId
      };
      using (var db = Connection)
      {
        return db.QueryFirstOrDefault<int>(sql, param);
      }

    }

    public void Update(MaterialPeacaoEntity materialPeacao)
    {
      var sql = @"
                        UPDATE
	                        TABELA_TARIFA_MATERIAL
                        SET
		                    QUANTIDADE_BASE = @QtdBase,
		                    VALOR = @Valor,
		                    FLAG_NECESSITA_FRETE = @NecessitaFrete,
		                    TAB_TIPO_EQUIPAM_ID = @TabTipoEquipamId,
		                    TAB_UNIDADE_MEDIDA_ID = @TabUnidadeMedidaId
                        WHERE
	                        TABELA_TARIFA_MATERIAL_ID = @TabelaTarifaMaterialId";

      var param = new
      {
        materialPeacao.TabelaTarifaMaterialId,
        materialPeacao.TabelaPrecoFornecedorId,
        materialPeacao.QtdBase,
        Valor = Utils.ConverterValor(materialPeacao.Valor),
        NecessitaFrete = materialPeacao.NecessitaFrete ? 'S' : 'N',
        materialPeacao.Material.TabTipoEquipamId,
        materialPeacao.Unidade.TabUnidadeMedidaId
      };
      using (var db = Connection)
      {
        db.Execute(sql, param);
      }

    }

    public void Delete(int tabelaTarifaMaterialId)
    {
      var sql = @"
                        UPDATE
	                        TABELA_TARIFA_MATERIAL
                        SET
	                        TAB_STATUS_ID = 2
                        WHERE
	                        TABELA_TARIFA_MATERIAL_ID = @TabelaTarifaMaterialId";

      var param = new
      {
        TabelaTarifaMaterialId = tabelaTarifaMaterialId
      };
      using (var db = Connection)
      {
        db.Execute(sql, param);
      }

    }

    public bool VerificarDuplicidade(MaterialPeacaoEntity materialPeacao)
    {
      var sql = @"
                        SELECT
		                      1
                        FROM
	                        TABELA_TARIFA_MATERIAL
                        WHERE
	                        TAB_STATUS_ID = 1
	                        AND TABELA_TARIFA_MATERIAL_ID <> @TabelaTarifaMaterialId
	                        AND TABELA_PRECO_FORNECEDOR_ID = @TabelaPrecoFornecedorId
	                        AND TAB_TIPO_EQUIPAM_ID = @TabTipoEquipamId";

      var param = new
      {
        materialPeacao.TabelaTarifaMaterialId,
        materialPeacao.TabelaPrecoFornecedorId,
        materialPeacao.Material.TabTipoEquipamId
      };
      using (var db = Connection)
      {
        return db.QueryFirstOrDefault<int>(sql, param) >= 1;
      }

    }

    public void GravarHistorico(int tabelaTarifaMaterialId, int usuarioId)
    {
      var sql = @"
                        INSERT INTO HIST_TABELA_TARIFA_MATERIAL
                        (
                          TABELA_TARIFA_MATERIAL_ID,
                          TABELA_PRECO_FORNECEDOR_ID,
                          QUANTIDADE_BASE,
                          VALOR,
                          FLAG_NECESSITA_FRETE,
                          TAB_TIPO_EQUIPAM_ID,
                          TAB_UNIDADE_MEDIDA_ID,
                          TAB_STATUS_ID,
                          DATA_INCL,
                          USUARIO_INCL_ID
                        )
                        SELECT
                          TABELA_TARIFA_MATERIAL_ID,
                          TABELA_PRECO_FORNECEDOR_ID,
                          QUANTIDADE_BASE,
                          VALOR,
                          FLAG_NECESSITA_FRETE,
                          TAB_TIPO_EQUIPAM_ID,
                          TAB_UNIDADE_MEDIDA_ID,
                          TAB_STATUS_ID,
                          GETDATE(),
                          @UsuarioId
                        FROM
  	                      TABELA_TARIFA_MATERIAL
                        WHERE
	                        TABELA_TARIFA_MATERIAL_ID = @TabelaTarifaMaterialId";

      var param = new
      {
        TabelaTarifaMaterialId = tabelaTarifaMaterialId,
        UsuarioId = usuarioId
      };
      using (var db = Connection)
      {
        db.Execute(sql, param);
      }

    }

    public IEnumerable<MaterialPeacaoEntity> ConsultarHistorico(int tabelaTarifaMaterialId)
    {
      var sql = @"
                        SELECT
	                        TTM.TABELA_TARIFA_MATERIAL_ID														TabelaTarifaMaterialId,
	                        TTM.TABELA_PRECO_FORNECEDOR_ID														TabelaPrecoFornecedorId,
		                    TTM.QUANTIDADE_BASE																	QtdBase,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TTM.VALOR,0),'0,00'))				Valor,
		                    CAST(CASE
			                    WHEN TTM.FLAG_NECESSITA_FRETE = 'S'
			                    THEN 1
			                    ELSE 0
		                    END AS Bit)																			NecessitaFrete,
		                    CASE
                                WHEN TTM.FLAG_NECESSITA_FRETE = 'S'
			                    THEN 'Sim'
			                    ELSE 'Não'
		                    END														        					NecessitaFreteTexto,
		                    TTM.TAB_STATUS_ID																	TabStatusId,
		                    CONVERT(VARCHAR, TTM.DATA_INCL, 103) + ' ' + CONVERT(VARCHAR, TTM.DATA_INCL, 108)	DataLog,
		                    VU.NOME_USUARIO																		UsuarioLog,
		                    TTM.TAB_TIPO_EQUIPAM_ID																TabTipoEquipamId,
		                    TTE.DESCR																			DescMaterial,
		                    TTM.TAB_UNIDADE_MEDIDA_ID															TabUnidadeMedidaId,
		                    TUM.DESC_UNIDADE_MEDIDA																DescUnidadeMedida
                        FROM
	                        HIST_TABELA_TARIFA_MATERIAL TTM
                            INNER JOIN TABELA_PRECO_FORNECEDOR TPF ON TPF.TABELA_PRECO_FORNECEDOR_ID = TTM.TABELA_PRECO_FORNECEDOR_ID
	                        INNER JOIN TAB_TIPO_EQUIPAM TTE ON TTE.TAB_TIPO_EQUIPAM_ID = TTM.TAB_TIPO_EQUIPAM_ID
		                    INNER JOIN TAB_UNIDADE_MEDIDA TUM ON TUM.TAB_UNIDADE_MEDIDA_ID = TTM.TAB_UNIDADE_MEDIDA_ID
		                    LEFT JOIN VUSUARIO VU ON VU.USUARIO_ID = TTM.USUARIO_INCL_ID
                        WHERE
	                        TTM.TABELA_TARIFA_MATERIAL_ID = @TabelaTarifaMaterialId
                        ORDER BY TTM.DATA_INCL DESC";

      var param = new
      {
        TabelaTarifaMaterialId = tabelaTarifaMaterialId
      };

      using (var db = Connection)
      {
        return db.Query<MaterialPeacaoEntity, MaterialEntity, UnidadeMedidaEntity, MaterialPeacaoEntity>(
                sql,
                (materialPeacaoEntity, materialEntity, unidadeMedidaEntity) =>
                {
                  materialPeacaoEntity.Material = materialEntity;
                  materialPeacaoEntity.Unidade = unidadeMedidaEntity;
                  return materialPeacaoEntity;
                },
                param,
                splitOn: "TabTipoEquipamId, TabUnidadeMedidaId"
                );
      }
    }

    public IEnumerable<MaterialPeacaoEntity> ConsultarHistoricoExclusao(int tabelaPrecoFornecedorId)
    {
      var sql = @"
                        SELECT
	                        TTM.TABELA_TARIFA_MATERIAL_ID														TabelaTarifaMaterialId,
	                        TTM.TABELA_PRECO_FORNECEDOR_ID														TabelaPrecoFornecedorId,
		                    TTM.QUANTIDADE_BASE																	QtdBase,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TTM.VALOR,0),'0,00'))				Valor,
		                    CAST(CASE
			                    WHEN TTM.FLAG_NECESSITA_FRETE = 'S'
			                    THEN 1
			                    ELSE 0
		                    END AS Bit)																			NecessitaFrete,
		                    CASE
                                WHEN TTM.FLAG_NECESSITA_FRETE = 'S'
			                    THEN 'Sim'
			                    ELSE 'Não'
		                    END														        					NecessitaFreteTexto,
		                    TTM.TAB_STATUS_ID																	TabStatusId,
		                    CONVERT(VARCHAR, TTM.DATA_INCL, 103) + ' ' + CONVERT(VARCHAR, TTM.DATA_INCL, 108)	DataLog,
		                    VU.NOME_USUARIO																		UsuarioLog,
		                    TTM.TAB_TIPO_EQUIPAM_ID																TabTipoEquipamId,
		                    TTE.DESCR																			DescMaterial,
		                    TTM.TAB_UNIDADE_MEDIDA_ID															TabUnidadeMedidaId,
		                    TUM.DESC_UNIDADE_MEDIDA																DescUnidadeMedida
                        FROM
	                        HIST_TABELA_TARIFA_MATERIAL TTM
                            INNER JOIN TABELA_PRECO_FORNECEDOR TPF ON TPF.TABELA_PRECO_FORNECEDOR_ID = TTM.TABELA_PRECO_FORNECEDOR_ID
	                        INNER JOIN TAB_TIPO_EQUIPAM TTE ON TTE.TAB_TIPO_EQUIPAM_ID = TTM.TAB_TIPO_EQUIPAM_ID
		                    INNER JOIN TAB_UNIDADE_MEDIDA TUM ON TUM.TAB_UNIDADE_MEDIDA_ID = TTM.TAB_UNIDADE_MEDIDA_ID
		                    LEFT JOIN VUSUARIO VU ON VU.USUARIO_ID = TTM.USUARIO_INCL_ID
                        WHERE
		                    TTM.TAB_STATUS_ID = 2
	                        AND TTM.TABELA_PRECO_FORNECEDOR_ID = @TabelaPrecoFornecedorId
                        ORDER BY TTM.DATA_INCL DESC";

      var param = new
      {
        TabelaPrecoFornecedorId = tabelaPrecoFornecedorId
      };

      using (var db = Connection)
      {
        return db.Query<MaterialPeacaoEntity, MaterialEntity, UnidadeMedidaEntity, MaterialPeacaoEntity>(
                sql,
                (materialPeacaoEntity, materialEntity, unidadeMedidaEntity) =>
                {
                  materialPeacaoEntity.Material = materialEntity;
                  materialPeacaoEntity.Unidade = unidadeMedidaEntity;
                  return materialPeacaoEntity;
                },
                param,
                splitOn: "TabTipoEquipamId, TabUnidadeMedidaId"
                );
      }
    }
  }
}