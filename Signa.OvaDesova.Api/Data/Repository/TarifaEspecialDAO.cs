using Dapper;
using Signa.Library.Core;
using Signa.Library.Core.Data.Repository;
using Signa.OvaDesova.Api.Domain.Entities;
using System.Collections.Generic;

namespace Signa.OvaDesova.Api.Data.Repository
{
    public class TarifaEspecialDAO : RepositoryBase
    {
        public IEnumerable<TarifaEspecialEntity> ConsultarTarifaEspecial(int tabelaPrecoFornecedorId)
        {
            var sql = @"
                        SELECT
	                        TTE.TABELA_TARIFA_ESPECIAL_ID																TabelaTarifaEspecialId,
	                        TTE.TABELA_PRECO_FORNECEDOR_ID																TabelaPrecoFornecedorId,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TTE.VAL_CONFERENTE,0),'0,00'))				Conferente,
		                    CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TTE.VAL_SEMANAL_DIURNO,0),'0,00'))			SemanalDiurno,
		                    CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TTE.VAL_SEMANAL_NOTURNO,0),'0,00'))			SemanalNoturno,
		                    CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TTE.VAL_FIM_DE_SEMANA_DIURNO,0),'0,00'))	FdsDiurno,
		                    CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TTE.VAL_FIM_DE_SEMANA_NOTURNO,0),'0,00'))	FdsNoturno,
	                        TTE.MUNICIPIO_ID																			MunicipioId,
	                        MUN.MUNICIPIO + ' - ' + MUN.UF																NomeMunicipio,
	                        TTE.TAB_TIPO_VEICULO_ID																		TabTipoVeiculoId,
	                        TTV.DESC_TIPO_VEICULO																		DescTipoVeiculo,
	                        TTE.TAB_TIPO_ACORDO_ID																		TabTipoAcordoId,
	                        TTA.DESC_TIPO_ACORDO																		DescTipoAcordo,
	                        TTE.TAB_TIPO_ACORDO_ESPECIAL_ID																TabTipoAcordoEspecialId,
	                        TAE.DESC_TIPO_ACORDO_ESPECIAL																DescTipoAcordoEspecial,
	                        TTE.FAMILIA_PRODUTO_ID																		FamiliaProdutoId,
	                        FP.DESC_FAMILIA																				DescFamilia
                        FROM
	                        TABELA_TARIFA_ESPECIAL TTE
                            INNER JOIN TABELA_PRECO_FORNECEDOR TPF ON TPF.TABELA_PRECO_FORNECEDOR_ID = TTE.TABELA_PRECO_FORNECEDOR_ID
	                        INNER JOIN MUNICIPIO MUN ON MUN.MUNICIPIO_ID = TTE.MUNICIPIO_ID
		                    LEFT JOIN TAB_TIPO_VEICULO TTV ON TTV.TAB_TIPO_VEICULO_ID = TTE.TAB_TIPO_VEICULO_ID
		                    LEFT JOIN TAB_TIPO_ACORDO TTA ON TTA.TAB_TIPO_ACORDO_ID = TTE.TAB_TIPO_ACORDO_ID
		                    LEFT JOIN TAB_TIPO_ACORDO_ESPECIAL TAE ON TAE.TAB_TIPO_ACORDO_ESPECIAL_ID = TTE.TAB_TIPO_ACORDO_ESPECIAL_ID
		                    LEFT JOIN FAMILIA_PRODUTO FP ON FP.FAMILIA_PRODUTO_ID = TTE.FAMILIA_PRODUTO_ID
                        WHERE
	                        TPF.TABELA_PRECO_FORNECEDOR_ID = @TabelaPrecoFornecedorId
                            AND TTE.TAB_STATUS_ID = 1
                        ORDER BY MUN.MUNICIPIO";

            var param = new
            {
                TabelaPrecoFornecedorId = tabelaPrecoFornecedorId
            };

            using (var db = Connection)
            {
                return db.Query<TarifaEspecialEntity, MunicipioEntity, VeiculoEntity, AcordoRodoviarioEntity, AcordoEspecialEntity, FamiliaMercadoriaEntity, TarifaEspecialEntity>(
                        sql,
                        (tarifaEspecialEntity, municipioEntity, veiculoEntity, acordoRodoviarioEntity, acordoEspecialEntity, familiaMercadoriaEntity) =>
                        {
                            tarifaEspecialEntity.Municipio = municipioEntity;
                            tarifaEspecialEntity.Veiculo = veiculoEntity != null ? veiculoEntity : new VeiculoEntity();
                            tarifaEspecialEntity.AcordoRodoviario = acordoRodoviarioEntity != null ? acordoRodoviarioEntity : new AcordoRodoviarioEntity();
                            tarifaEspecialEntity.AcordoEspecial = acordoEspecialEntity != null ? acordoEspecialEntity : new AcordoEspecialEntity();
                            tarifaEspecialEntity.FamiliaMercadoria = familiaMercadoriaEntity != null ? familiaMercadoriaEntity : new FamiliaMercadoriaEntity();
                            return tarifaEspecialEntity;
                        },
                        param,
                        splitOn: "MunicipioId, TabTipoVeiculoId, TabTipoAcordoId, TabTipoAcordoEspecialId, FamiliaProdutoId"
                        );
            }
        }

        public int Insert(TarifaEspecialEntity tarifaEspecial)
        {
            var sql = @"
	                    INSERT INTO TABELA_TARIFA_ESPECIAL
	                    (
		                    TABELA_PRECO_FORNECEDOR_ID,
		                    VAL_CONFERENTE,
		                    VAL_SEMANAL_DIURNO,
		                    VAL_SEMANAL_NOTURNO,
		                    VAL_FIM_DE_SEMANA_DIURNO,
		                    VAL_FIM_DE_SEMANA_NOTURNO,
		                    MUNICIPIO_ID,
		                    TAB_TIPO_VEICULO_ID,
		                    TAB_TIPO_ACORDO_ID,
		                    TAB_TIPO_ACORDO_ESPECIAL_ID,
		                    FAMILIA_PRODUTO_ID,
		                    TAB_STATUS_ID
	                    )
	                    VALUES
	                    (
                            @TabelaPrecoFornecedorId,
                            @Conferente,
                            @SemanalDiurno,
                            @SemanalNoturno,
                            @FdsDiurno,
                            @FdsNoturno,
                            @MunicipioId,
                            @TabTipoVeiculoId,
                            @TabTipoAcordoId,
                            @TabTipoAcordoEspecialId,
                            @FamiliaProdutoId,
                            1
	                    )

                        SELECT SCOPE_IDENTITY()";

            var param = new
            {
                tarifaEspecial.TabelaPrecoFornecedorId,
                Conferente = Utils.ConverterValor(tarifaEspecial.Conferente),
                SemanalDiurno = Utils.ConverterValor(tarifaEspecial.SemanalDiurno),
                SemanalNoturno = Utils.ConverterValor(tarifaEspecial.SemanalNoturno),
                FdsDiurno = Utils.ConverterValor(tarifaEspecial.FdsDiurno),
                FdsNoturno = Utils.ConverterValor(tarifaEspecial.FdsNoturno),
                tarifaEspecial.Municipio.MunicipioId,
                tarifaEspecial.Veiculo.TabTipoVeiculoId,
                tarifaEspecial.AcordoRodoviario.TabTipoAcordoId,
                tarifaEspecial.AcordoEspecial.TabTipoAcordoEspecialId,
                tarifaEspecial.FamiliaMercadoria.FamiliaProdutoId
            };
            using (var db = Connection)
            {
                return db.QueryFirstOrDefault<int>(sql, param);
            }

        }

        public void Update(TarifaEspecialEntity tarifaEspecial)
        {
            var sql = @"
                        UPDATE
	                        TABELA_TARIFA_ESPECIAL
                        SET
		                    VAL_CONFERENTE = @Conferente,
		                    VAL_SEMANAL_DIURNO = @SemanalDiurno,
		                    VAL_SEMANAL_NOTURNO = @SemanalNoturno,
		                    VAL_FIM_DE_SEMANA_DIURNO = @FdsDiurno,
		                    VAL_FIM_DE_SEMANA_NOTURNO = @FdsNoturno,
		                    MUNICIPIO_ID = @MunicipioId,
		                    TAB_TIPO_VEICULO_ID = @TabTipoVeiculoId,
		                    TAB_TIPO_ACORDO_ID = @TabTipoAcordoId,
		                    TAB_TIPO_ACORDO_ESPECIAL_ID = @TabTipoAcordoEspecialId,
		                    FAMILIA_PRODUTO_ID = @FamiliaProdutoId
                        WHERE
	                        TABELA_TARIFA_ESPECIAL_ID = @TabelaTarifaEspecialId";

            var param = new
            {
                tarifaEspecial.TabelaTarifaEspecialId,
                Conferente = Utils.ConverterValor(tarifaEspecial.Conferente),
                SemanalDiurno = Utils.ConverterValor(tarifaEspecial.SemanalDiurno),
                SemanalNoturno = Utils.ConverterValor(tarifaEspecial.SemanalNoturno),
                FdsDiurno = Utils.ConverterValor(tarifaEspecial.FdsDiurno),
                FdsNoturno = Utils.ConverterValor(tarifaEspecial.FdsNoturno),
                tarifaEspecial.Municipio.MunicipioId,
                tarifaEspecial.Veiculo.TabTipoVeiculoId,
                tarifaEspecial.AcordoRodoviario.TabTipoAcordoId,
                tarifaEspecial.AcordoEspecial.TabTipoAcordoEspecialId,
                tarifaEspecial.FamiliaMercadoria.FamiliaProdutoId
            };

            using (var db = Connection)
            {
                db.Execute(sql, param);
            }
        }

        public void Delete(int tabelaTarifaEspecialId)
        {
            var sql = @"
                        UPDATE
	                        TABELA_TARIFA_ESPECIAL
                        SET
	                        TAB_STATUS_ID = 2
                        WHERE
                             TABELA_TARIFA_ESPECIAL_ID = @TabelaTarifaEspecialId";

            var param = new
            {
                TabelaTarifaEspecialId = tabelaTarifaEspecialId
            };

            using (var db = Connection)
            {
                db.Execute(sql, param);
            }
        }

        public void DeleteAll(int tabelaPrecoFornecedorId)
        {
            var sql = @"
                        UPDATE
	                        TABELA_TARIFA_ESPECIAL
                        SET
	                        TAB_STATUS_ID = 2
                        WHERE
                             TABELA_PRECO_FORNECEDOR_ID = @tabelaPrecoFornecedorId";

            var param = new
            {
                tabelaPrecoFornecedorId
            };
            using (var db = Connection)
            {
                db.Execute(sql, param);
            }
        }

        public bool VerificarDuplicidade(TarifaEspecialEntity tarifaEspecial)
        {
            var sql = @"
                        SELECT
		                    1
                        FROM
	                        TABELA_TARIFA_ESPECIAL
                        WHERE
	                        TAB_STATUS_ID = 1
	                        AND TABELA_TARIFA_ESPECIAL_ID <> @TabelaTarifaEspecialId
	                        AND TABELA_PRECO_FORNECEDOR_ID = @TabelaPrecoFornecedorId
	                        AND MUNICIPIO_ID = @MunicipioId
		                    AND (TAB_TIPO_VEICULO_ID = @TabTipoVeiculoId OR ISNULL(@TabTipoVeiculoId, 0) = 0)
		                    AND (TAB_TIPO_ACORDO_ID = @TabTipoAcordoId OR ISNULL(@TabTipoAcordoId, 0) = 0)
		                    AND (TAB_TIPO_ACORDO_ESPECIAL_ID = @TabTipoAcordoEspecialId OR ISNULL(@TabTipoAcordoEspecialId, 0) = 0)
		                    AND (FAMILIA_PRODUTO_ID = @FamiliaProdutoId OR ISNULL(@FamiliaProdutoId, 0) = 0)";

            var param = new
            {
                tarifaEspecial.TabelaTarifaEspecialId,
                tarifaEspecial.TabelaPrecoFornecedorId,
                tarifaEspecial.Municipio.MunicipioId,
                tarifaEspecial.Veiculo.TabTipoVeiculoId,
                tarifaEspecial.AcordoRodoviario.TabTipoAcordoId,
                tarifaEspecial.AcordoEspecial.TabTipoAcordoEspecialId,
                tarifaEspecial.FamiliaMercadoria.FamiliaProdutoId
            };
            using (var db = Connection)
            {
                return db.QueryFirstOrDefault<int>(sql, param) >= 1;
            }

        }

        public void GravarHistorico(int tabelaTarifaEspecialId, int usuarioId)
        {
            var sql = @"
                        INSERT INTO HIST_TABELA_TARIFA_ESPECIAL
                        (
	                        TABELA_TARIFA_ESPECIAL_ID,
		                    TABELA_PRECO_FORNECEDOR_ID,
		                    VAL_CONFERENTE,
		                    VAL_SEMANAL_DIURNO,
		                    VAL_SEMANAL_NOTURNO,
		                    VAL_FIM_DE_SEMANA_DIURNO,
		                    VAL_FIM_DE_SEMANA_NOTURNO,
		                    MUNICIPIO_ID,
		                    TAB_TIPO_VEICULO_ID,
		                    TAB_TIPO_ACORDO_ID,
		                    TAB_TIPO_ACORDO_ESPECIAL_ID,
		                    FAMILIA_PRODUTO_ID,
		                    TAB_STATUS_ID,
	                        DATA_INCL,
	                        USUARIO_INCL_ID
                        )
                        SELECT
	                        TABELA_TARIFA_ESPECIAL_ID,
		                    TABELA_PRECO_FORNECEDOR_ID,
		                    VAL_CONFERENTE,
		                    VAL_SEMANAL_DIURNO,
		                    VAL_SEMANAL_NOTURNO,
		                    VAL_FIM_DE_SEMANA_DIURNO,
		                    VAL_FIM_DE_SEMANA_NOTURNO,
		                    MUNICIPIO_ID,
		                    TAB_TIPO_VEICULO_ID,
		                    TAB_TIPO_ACORDO_ID,
		                    TAB_TIPO_ACORDO_ESPECIAL_ID,
		                    FAMILIA_PRODUTO_ID,
		                    TAB_STATUS_ID,
	                        GETDATE(),
	                        @UsuarioId
                        FROM
	                        TABELA_TARIFA_ESPECIAL
                        WHERE
	                        TABELA_TARIFA_ESPECIAL_ID = @TabelaTarifaEspecialId";

            var param = new
            {
                TabelaTarifaEspecialId = tabelaTarifaEspecialId,
                UsuarioId = usuarioId
            };
            using (var db = Connection)
            {
                db.Execute(sql, param);
            }
        }

        public void GravarHistoricoAll(int tabelaPrecoFornecedorId)
        {
            var sql = @"
                        INSERT INTO HIST_TABELA_TARIFA_ESPECIAL
                        (
	                        TABELA_TARIFA_ESPECIAL_ID,
		                    TABELA_PRECO_FORNECEDOR_ID,
		                    VAL_CONFERENTE,
		                    VAL_SEMANAL_DIURNO,
		                    VAL_SEMANAL_NOTURNO,
		                    VAL_FIM_DE_SEMANA_DIURNO,
		                    VAL_FIM_DE_SEMANA_NOTURNO,
		                    MUNICIPIO_ID,
		                    TAB_TIPO_VEICULO_ID,
		                    TAB_TIPO_ACORDO_ID,
		                    TAB_TIPO_ACORDO_ESPECIAL_ID,
		                    FAMILIA_PRODUTO_ID,
		                    TAB_STATUS_ID,
	                        DATA_INCL,
	                        USUARIO_INCL_ID
                        )
                        SELECT
	                        TABELA_TARIFA_ESPECIAL_ID,
		                    TABELA_PRECO_FORNECEDOR_ID,
		                    VAL_CONFERENTE,
		                    VAL_SEMANAL_DIURNO,
		                    VAL_SEMANAL_NOTURNO,
		                    VAL_FIM_DE_SEMANA_DIURNO,
		                    VAL_FIM_DE_SEMANA_NOTURNO,
		                    MUNICIPIO_ID,
		                    TAB_TIPO_VEICULO_ID,
		                    TAB_TIPO_ACORDO_ID,
		                    TAB_TIPO_ACORDO_ESPECIAL_ID,
		                    FAMILIA_PRODUTO_ID,
		                    2,
	                        GETDATE(),
	                        @UsuarioId
                        FROM
	                        TABELA_TARIFA_ESPECIAL
                        WHERE
	                        TABELA_PRECO_FORNECEDOR_ID = @tabelaPrecoFornecedorId
                            AND TAB_STATUS_ID = 1";

            var param = new
            {
                tabelaPrecoFornecedorId,
                UsuarioId = Global.UsuarioId
            };

            using (var db = Connection)
            {
                db.Execute(sql, param);
            }

        }

        public IEnumerable<TarifaEspecialEntity> ConsultarHistorico(int tabelaTarifaEspecialId)
        {
            var sql = @"
                        SELECT
	                        TTE.TABELA_TARIFA_ESPECIAL_ID																TabelaTarifaEspecialId,
	                        TTE.TABELA_PRECO_FORNECEDOR_ID																TabelaPrecoFornecedorId,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TTE.VAL_CONFERENTE,0),'0,00'))				Conferente,
		                    CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TTE.VAL_SEMANAL_DIURNO,0),'0,00'))			SemanalDiurno,
		                    CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TTE.VAL_SEMANAL_NOTURNO,0),'0,00'))			SemanalNoturno,
		                    CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TTE.VAL_FIM_DE_SEMANA_DIURNO,0),'0,00'))	FdsDiurno,
		                    CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TTE.VAL_FIM_DE_SEMANA_NOTURNO,0),'0,00'))	FdsNoturno,
		                    TTE.TAB_STATUS_ID																			TabStatusId,
		                    CONVERT(VARCHAR, TTE.DATA_INCL, 103) + ' ' + CONVERT(VARCHAR, TTE.DATA_INCL, 108)			DataLog,
		                    VU.NOME_USUARIO																				UsuarioLog,
	                        TTE.MUNICIPIO_ID																			MunicipioId,
	                        MUN.MUNICIPIO + ' - ' + MUN.UF																NomeMunicipio,
	                        TTE.TAB_TIPO_VEICULO_ID																		TabTipoVeiculoId,
	                        TTV.DESC_TIPO_VEICULO																		DescTipoVeiculo,
	                        TTE.TAB_TIPO_ACORDO_ID																		TabTipoAcordoId,
	                        TTA.DESC_TIPO_ACORDO																		DescTipoAcordo,
	                        TTE.TAB_TIPO_ACORDO_ESPECIAL_ID																TabTipoAcordoEspecialId,
	                        TAE.DESC_TIPO_ACORDO_ESPECIAL																DescTipoAcordoEspecial,
	                        TTE.FAMILIA_PRODUTO_ID																		FamiliaProdutoId,
	                        FP.DESC_FAMILIA																				DescFamilia
                        FROM
	                        HIST_TABELA_TARIFA_ESPECIAL TTE
                            INNER JOIN TABELA_PRECO_FORNECEDOR TPF ON TPF.TABELA_PRECO_FORNECEDOR_ID = TTE.TABELA_PRECO_FORNECEDOR_ID
	                        INNER JOIN MUNICIPIO MUN ON MUN.MUNICIPIO_ID = TTE.MUNICIPIO_ID
		                    LEFT JOIN TAB_TIPO_VEICULO TTV ON TTV.TAB_TIPO_VEICULO_ID = TTE.TAB_TIPO_VEICULO_ID
		                    LEFT JOIN TAB_TIPO_ACORDO TTA ON TTA.TAB_TIPO_ACORDO_ID = TTE.TAB_TIPO_ACORDO_ID
		                    LEFT JOIN TAB_TIPO_ACORDO_ESPECIAL TAE ON TAE.TAB_TIPO_ACORDO_ESPECIAL_ID = TTE.TAB_TIPO_ACORDO_ESPECIAL_ID
		                    LEFT JOIN FAMILIA_PRODUTO FP ON FP.FAMILIA_PRODUTO_ID = TTE.FAMILIA_PRODUTO_ID
		                    LEFT JOIN VUSUARIO VU ON VU.USUARIO_ID = TTE.USUARIO_INCL_ID
                        WHERE
	                        TTE.TABELA_TARIFA_ESPECIAL_ID = @TabelaTarifaEspecialId
                        ORDER BY TTE.DATA_INCL DESC";

            var param = new
            {
                TabelaTarifaEspecialId = tabelaTarifaEspecialId
            };

            using (var db = Connection)
            {
                return db.Query<TarifaEspecialEntity, MunicipioEntity, VeiculoEntity, AcordoRodoviarioEntity, AcordoEspecialEntity, FamiliaMercadoriaEntity, TarifaEspecialEntity>(
                        sql,
                        (tarifaEspecialEntity, municipioEntity, veiculoEntity, acordoRodoviarioEntity, acordoEspecialEntity, familiaMercadoriaEntity) =>
                        {
                            tarifaEspecialEntity.Municipio = municipioEntity;
                            tarifaEspecialEntity.Veiculo = veiculoEntity != null ? veiculoEntity : new VeiculoEntity();
                            tarifaEspecialEntity.AcordoRodoviario = acordoRodoviarioEntity != null ? acordoRodoviarioEntity : new AcordoRodoviarioEntity();
                            tarifaEspecialEntity.AcordoEspecial = acordoEspecialEntity != null ? acordoEspecialEntity : new AcordoEspecialEntity();
                            tarifaEspecialEntity.FamiliaMercadoria = familiaMercadoriaEntity != null ? familiaMercadoriaEntity : new FamiliaMercadoriaEntity();
                            return tarifaEspecialEntity;
                        },
                        param,
                        splitOn: "MunicipioId, TabTipoVeiculoId, TabTipoAcordoId, TabTipoAcordoEspecialId, FamiliaProdutoId"
                        );
            }
        }

        public IEnumerable<TarifaEspecialEntity> ConsultarHistoricoExclusao(int tabelaPrecoFornecedorId)
        {
            var sql = @"
                        SELECT DISTINCT
	                        TTE.TABELA_TARIFA_ESPECIAL_ID																TabelaTarifaEspecialId,
	                        TTE.TABELA_PRECO_FORNECEDOR_ID																TabelaPrecoFornecedorId,
	                        CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TTE.VAL_CONFERENTE,0),'0,00'))				Conferente,
		                    CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TTE.VAL_SEMANAL_DIURNO,0),'0,00'))			SemanalDiurno,
		                    CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TTE.VAL_SEMANAL_NOTURNO,0),'0,00'))			SemanalNoturno,
		                    CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TTE.VAL_FIM_DE_SEMANA_DIURNO,0),'0,00'))	FdsDiurno,
		                    CONVERT(VARCHAR,DBO.FN_CGS_EDITA_CAMPO04(ISNULL(TTE.VAL_FIM_DE_SEMANA_NOTURNO,0),'0,00'))	FdsNoturno,
		                    TTE.TAB_STATUS_ID																			TabStatusId,
		                    CONVERT(VARCHAR, TTE.DATA_INCL, 103) + ' ' + CONVERT(VARCHAR, TTE.DATA_INCL, 108)			DataLog,
		                    VU.NOME_USUARIO																				UsuarioLog,
	                        TTE.MUNICIPIO_ID																			MunicipioId,
	                        MUN.MUNICIPIO + ' - ' + MUN.UF																NomeMunicipio,
	                        TTE.TAB_TIPO_VEICULO_ID																		TabTipoVeiculoId,
	                        TTV.DESC_TIPO_VEICULO																		DescTipoVeiculo,
	                        TTE.TAB_TIPO_ACORDO_ID																		TabTipoAcordoId,
	                        TTA.DESC_TIPO_ACORDO																		DescTipoAcordo,
	                        TTE.TAB_TIPO_ACORDO_ESPECIAL_ID																TabTipoAcordoEspecialId,
	                        TAE.DESC_TIPO_ACORDO_ESPECIAL																DescTipoAcordoEspecial,
	                        TTE.FAMILIA_PRODUTO_ID																		FamiliaProdutoId,
	                        FP.DESC_FAMILIA																				DescFamilia,
		                    TTE.DATA_INCL
                        FROM
	                        HIST_TABELA_TARIFA_ESPECIAL TTE
                            INNER JOIN TABELA_PRECO_FORNECEDOR TPF ON TPF.TABELA_PRECO_FORNECEDOR_ID = TTE.TABELA_PRECO_FORNECEDOR_ID
	                        INNER JOIN MUNICIPIO MUN ON MUN.MUNICIPIO_ID = TTE.MUNICIPIO_ID
		                    LEFT JOIN TAB_TIPO_VEICULO TTV ON TTV.TAB_TIPO_VEICULO_ID = TTE.TAB_TIPO_VEICULO_ID
		                    LEFT JOIN TAB_TIPO_ACORDO TTA ON TTA.TAB_TIPO_ACORDO_ID = TTE.TAB_TIPO_ACORDO_ID
		                    LEFT JOIN TAB_TIPO_ACORDO_ESPECIAL TAE ON TAE.TAB_TIPO_ACORDO_ESPECIAL_ID = TTE.TAB_TIPO_ACORDO_ESPECIAL_ID
		                    LEFT JOIN FAMILIA_PRODUTO FP ON FP.FAMILIA_PRODUTO_ID = TTE.FAMILIA_PRODUTO_ID
		                    LEFT JOIN VUSUARIO VU ON VU.USUARIO_ID = TTE.USUARIO_INCL_ID
                        WHERE
		                    TTE.TAB_STATUS_ID = 2
	                        AND TTE.TABELA_PRECO_FORNECEDOR_ID = @TabelaPrecoFornecedorId
                        ORDER BY TTE.DATA_INCL DESC";

            var param = new
            {
                TabelaPrecoFornecedorId = tabelaPrecoFornecedorId
            };

            using (var db = Connection)
            {
                return db.Query<TarifaEspecialEntity, MunicipioEntity, VeiculoEntity, AcordoRodoviarioEntity, AcordoEspecialEntity, FamiliaMercadoriaEntity, TarifaEspecialEntity>(
                        sql,
                        (tarifaEspecialEntity, municipioEntity, veiculoEntity, acordoRodoviarioEntity, acordoEspecialEntity, familiaMercadoriaEntity) =>
                        {
                            tarifaEspecialEntity.Municipio = municipioEntity;
                            tarifaEspecialEntity.Veiculo = veiculoEntity != null ? veiculoEntity : new VeiculoEntity();
                            tarifaEspecialEntity.AcordoRodoviario = acordoRodoviarioEntity != null ? acordoRodoviarioEntity : new AcordoRodoviarioEntity();
                            tarifaEspecialEntity.AcordoEspecial = acordoEspecialEntity != null ? acordoEspecialEntity : new AcordoEspecialEntity();
                            tarifaEspecialEntity.FamiliaMercadoria = familiaMercadoriaEntity != null ? familiaMercadoriaEntity : new FamiliaMercadoriaEntity();
                            return tarifaEspecialEntity;
                        },
                        param,
                        splitOn: "MunicipioId, TabTipoVeiculoId, TabTipoAcordoId, TabTipoAcordoEspecialId, FamiliaProdutoId"
                        );
            }
        }
    }
}