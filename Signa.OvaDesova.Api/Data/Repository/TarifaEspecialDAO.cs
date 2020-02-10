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
                        Select 
                            Tte.Tabela_Tarifa_Especial_Id                                                                Tabelatarifaespecialid, 
                            Tte.Tabela_Preco_Fornecedor_Id                                                               Tabelaprecofornecedorid, 
                            Convert(VarChar, Dbo.Fn_Cgs_Edita_Campo04(IsNull(Tte.Val_Conferente, 0), '0,00'))            Conferente, 
                            Convert(VarChar, Dbo.Fn_Cgs_Edita_Campo04(IsNull(Tte.Val_Semanal_Diurno, 0), '0,00'))        Semanaldiurno, 
                            Convert(VarChar, Dbo.Fn_Cgs_Edita_Campo04(IsNull(Tte.Val_Semanal_Noturno, 0), '0,00'))       Semanalnoturno, 
                            Convert(VarChar, Dbo.Fn_Cgs_Edita_Campo04(IsNull(Tte.Val_Fim_De_Semana_Diurno, 0), '0,00'))  Fdsdiurno, 
                            Convert(VarChar, Dbo.Fn_Cgs_Edita_Campo04(IsNull(Tte.Val_Fim_De_Semana_Noturno, 0), '0,00')) Fdsnoturno, 
                            Convert(VarChar, Dbo.Fn_Cgs_Edita_Campo04(IsNull(Tte.Ajudante_1, 0), '0,00'))                Ajudante1, 
                            Convert(VarChar, Dbo.Fn_Cgs_Edita_Campo04(IsNull(Tte.Ajudante_2, 0), '0,00'))                Ajudante2, 
                            Convert(VarChar, Dbo.Fn_Cgs_Edita_Campo04(IsNull(Tte.Ajudante_3, 0), '0,00'))                Ajudante3, 
                            Convert(VarChar, Dbo.Fn_Cgs_Edita_Campo04(IsNull(Tte.Ajudante_4, 0), '0,00'))                Ajudante4, 
                            Convert(VarChar, Dbo.Fn_Cgs_Edita_Campo04(IsNull(Tte.Ajudante_5, 0), '0,00'))                Ajudante5, 
                            Convert(VarChar, Dbo.Fn_Cgs_Edita_Campo04(IsNull(Tte.Ajudante_6, 0), '0,00'))                Ajudante6, 
                            Convert(VarChar, Dbo.Fn_Cgs_Edita_Campo04(IsNull(Tte.Ajudante_7, 0), '0,00'))                Ajudante7, 
                            Convert(VarChar, Dbo.Fn_Cgs_Edita_Campo04(IsNull(Tte.Ajudante_8, 0), '0,00'))                Ajudante8, 
                            Tte.Municipio_Id                                                                             Municipioid, 
                            Mun.Municipio + ' - ' + Mun.Uf                                                               Nomemunicipio, 
                            Tte.Tab_Tipo_Veiculo_Id                                                                      Tabtipoveiculoid, 
                            Ttv.Desc_Tipo_Veiculo                                                                        Desctipoveiculo, 
                            Tte.Tab_Tipo_Acordo_Id                                                                       Tabtipoacordoid, 
                            Tta.Desc_Tipo_Acordo                                                                         Desctipoacordo, 
                            Tte.Tab_Tipo_Acordo_Especial_Id                                                              Tabtipoacordoespecialid, 
                            Tae.Desc_Tipo_Acordo_Especial                                                                Desctipoacordoespecial, 
                            Tte.Familia_Produto_Id                                                                       Familiaprodutoid, 
                            Fp.Desc_Familia                                                                              Descfamilia
                        From   Tabela_Tarifa_Especial Tte
                            Inner Join Tabela_Preco_Fornecedor Tpf On Tpf.Tabela_Preco_Fornecedor_Id = Tte.Tabela_Preco_Fornecedor_Id
                            Inner Join Municipio Mun On Mun.Municipio_Id = Tte.Municipio_Id
                            Left Join Tab_Tipo_Veiculo Ttv On Ttv.Tab_Tipo_Veiculo_Id = Tte.Tab_Tipo_Veiculo_Id
                            Left Join Tab_Tipo_Acordo Tta On Tta.Tab_Tipo_Acordo_Id = Tte.Tab_Tipo_Acordo_Id
                            Left Join Tab_Tipo_Acordo_Especial Tae On Tae.Tab_Tipo_Acordo_Especial_Id = Tte.Tab_Tipo_Acordo_Especial_Id
                            Left Join Familia_Produto Fp On Fp.Familia_Produto_Id = Tte.Familia_Produto_Id
                        Where  Tpf.Tabela_Preco_Fornecedor_Id = @tabelaPrecoFornecedorId
                            And Tte.Tab_Status_Id = 1
                        Order By Mun.Municipio";

            var param = new
            {
                tabelaPrecoFornecedorId
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
		                    TAB_STATUS_ID,
                            AJUDANTE_1,
	                        AJUDANTE_2,
	                        AJUDANTE_3,
	                        AJUDANTE_4,
	                        AJUDANTE_5,
	                        AJUDANTE_6,
	                        AJUDANTE_7,
	                        AJUDANTE_8
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
                            1,
                            @Ajudante1,
                            @Ajudante2,
                            @Ajudante3,
                            @Ajudante4,
                            @Ajudante5,
                            @Ajudante6,
                            @Ajudante7,
                            @Ajudante8
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
                Ajudante1 = Utils.ConverterValor(tarifaEspecial.Ajudante1),
                Ajudante2 = Utils.ConverterValor(tarifaEspecial.Ajudante2),
                Ajudante3 = Utils.ConverterValor(tarifaEspecial.Ajudante3),
                Ajudante4 = Utils.ConverterValor(tarifaEspecial.Ajudante4),
                Ajudante5 = Utils.ConverterValor(tarifaEspecial.Ajudante5),
                Ajudante6 = Utils.ConverterValor(tarifaEspecial.Ajudante6),
                Ajudante7 = Utils.ConverterValor(tarifaEspecial.Ajudante7),
                Ajudante8 = Utils.ConverterValor(tarifaEspecial.Ajudante8),
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
		                    FAMILIA_PRODUTO_ID = @FamiliaProdutoId,
                            AJUDANTE_1 = @Ajudante1,
	                        AJUDANTE_2 = @Ajudante2,
	                        AJUDANTE_3 = @Ajudante3,
	                        AJUDANTE_4 = @Ajudante4,
	                        AJUDANTE_5 = @Ajudante5,
	                        AJUDANTE_6 = @Ajudante6,
	                        AJUDANTE_7 = @Ajudante7,
	                        AJUDANTE_8 = @Ajudante8
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
                Ajudante1 = Utils.ConverterValor(tarifaEspecial.Ajudante1),
                Ajudante2 = Utils.ConverterValor(tarifaEspecial.Ajudante2),
                Ajudante3 = Utils.ConverterValor(tarifaEspecial.Ajudante3),
                Ajudante4 = Utils.ConverterValor(tarifaEspecial.Ajudante4),
                Ajudante5 = Utils.ConverterValor(tarifaEspecial.Ajudante5),
                Ajudante6 = Utils.ConverterValor(tarifaEspecial.Ajudante6),
                Ajudante7 = Utils.ConverterValor(tarifaEspecial.Ajudante7),
                Ajudante8 = Utils.ConverterValor(tarifaEspecial.Ajudante8),
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
	                        USUARIO_INCL_ID,
                            AJUDANTE_1,
	                        AJUDANTE_2,
	                        AJUDANTE_3,
	                        AJUDANTE_4,
	                        AJUDANTE_5,
	                        AJUDANTE_6,
	                        AJUDANTE_7,
	                        AJUDANTE_8
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
	                        @UsuarioId,
                            AJUDANTE_1,
	                        AJUDANTE_2,
	                        AJUDANTE_3,
	                        AJUDANTE_4,
	                        AJUDANTE_5,
	                        AJUDANTE_6,
	                        AJUDANTE_7,
	                        AJUDANTE_8
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
	                        USUARIO_INCL_ID,
                            AJUDANTE_1,
	                        AJUDANTE_2,
	                        AJUDANTE_3,
	                        AJUDANTE_4,
	                        AJUDANTE_5,
	                        AJUDANTE_6,
	                        AJUDANTE_7,
	                        AJUDANTE_8
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
	                        @UsuarioId,
                            AJUDANTE_1,
	                        AJUDANTE_2,
	                        AJUDANTE_3,
	                        AJUDANTE_4,
	                        AJUDANTE_5,
	                        AJUDANTE_6,
	                        AJUDANTE_7,
	                        AJUDANTE_8
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
                            Convert(VarChar, Dbo.Fn_Cgs_Edita_Campo04(IsNull(Tte.Ajudante_1, 0), '0,00'))                Ajudante1, 
                            Convert(VarChar, Dbo.Fn_Cgs_Edita_Campo04(IsNull(Tte.Ajudante_2, 0), '0,00'))                Ajudante2, 
                            Convert(VarChar, Dbo.Fn_Cgs_Edita_Campo04(IsNull(Tte.Ajudante_3, 0), '0,00'))                Ajudante3, 
                            Convert(VarChar, Dbo.Fn_Cgs_Edita_Campo04(IsNull(Tte.Ajudante_4, 0), '0,00'))                Ajudante4, 
                            Convert(VarChar, Dbo.Fn_Cgs_Edita_Campo04(IsNull(Tte.Ajudante_5, 0), '0,00'))                Ajudante5, 
                            Convert(VarChar, Dbo.Fn_Cgs_Edita_Campo04(IsNull(Tte.Ajudante_6, 0), '0,00'))                Ajudante6, 
                            Convert(VarChar, Dbo.Fn_Cgs_Edita_Campo04(IsNull(Tte.Ajudante_7, 0), '0,00'))                Ajudante7, 
                            Convert(VarChar, Dbo.Fn_Cgs_Edita_Campo04(IsNull(Tte.Ajudante_8, 0), '0,00'))                Ajudante8, 
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
                            Convert(VarChar, Dbo.Fn_Cgs_Edita_Campo04(IsNull(Tte.Ajudante_1, 0), '0,00'))                Ajudante1, 
                            Convert(VarChar, Dbo.Fn_Cgs_Edita_Campo04(IsNull(Tte.Ajudante_2, 0), '0,00'))                Ajudante2, 
                            Convert(VarChar, Dbo.Fn_Cgs_Edita_Campo04(IsNull(Tte.Ajudante_3, 0), '0,00'))                Ajudante3, 
                            Convert(VarChar, Dbo.Fn_Cgs_Edita_Campo04(IsNull(Tte.Ajudante_4, 0), '0,00'))                Ajudante4, 
                            Convert(VarChar, Dbo.Fn_Cgs_Edita_Campo04(IsNull(Tte.Ajudante_5, 0), '0,00'))                Ajudante5, 
                            Convert(VarChar, Dbo.Fn_Cgs_Edita_Campo04(IsNull(Tte.Ajudante_6, 0), '0,00'))                Ajudante6, 
                            Convert(VarChar, Dbo.Fn_Cgs_Edita_Campo04(IsNull(Tte.Ajudante_7, 0), '0,00'))                Ajudante7, 
                            Convert(VarChar, Dbo.Fn_Cgs_Edita_Campo04(IsNull(Tte.Ajudante_8, 0), '0,00'))                Ajudante8, 
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