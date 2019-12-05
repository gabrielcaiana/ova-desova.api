using Dapper;
using Signa.Library.Helpers;
using Signa.OvaDesova.Api.Data.Interface;
using Signa.OvaDesova.Api.Domain.Models;
using Signa.OvaDesova.Api.Helpers;
using System.Collections.Generic;
using System.Data;

namespace Signa.OvaDesova.Api.Data.Repository
{
    class MaterialPeacaoRepository : RepositoryBase, IMaterialPeacaoRepository
    {
        public IEnumerable<MaterialPeacaoModel> ConsultarMaterialPeacao(int tabelaPrecoFornecedorId)
        {
            var sql = @"
                        SELECT
	                        TTE.TABELA_TARIFA_ESPECIAL_ID																TabelaTarifaMaterialId,
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
                return db.Query<MaterialPeacaoModel, MaterialModel, UnidadeMedidaModel, MaterialPeacaoModel>(
                        sql,
                        (materialPeacaoModel, materialModel, unidadeMedidaModel) =>
                        {
                            materialPeacaoModel.Material = materialModel;
                            materialPeacaoModel.Unidade = unidadeMedidaModel;
                            return materialPeacaoModel;
                        },
                        param,
                        splitOn: "MunicipioId, TabTipoVeiculoId"
                        );
            }
        }

        public int Insert(MaterialPeacaoModel materialPeacao)
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
                //materialPeacao.TabelaPrecoFornecedorId,
                //Conferente = Utils.ConverterValor(materialPeacao.Conferente),
                //SemanalDiurno = Utils.ConverterValor(materialPeacao.SemanalDiurno),
                //SemanalNoturno = Utils.ConverterValor(materialPeacao.SemanalNoturno),
                //FdsDiurno = Utils.ConverterValor(materialPeacao.FdsDiurno),
                //FdsNoturno = Utils.ConverterValor(materialPeacao.FdsNoturno),
                //materialPeacao.Municipio.MunicipioId,
                //materialPeacao.Veiculo.TabTipoVeiculoId,
                //materialPeacao.AcordoRodoviario.TabTipoAcordoId,
                //materialPeacao.AcordoEspecial.TabTipoAcordoEspecialId,
                //materialPeacao.FamiliaMercadoria.FamiliaProdutoId
            };

            return RepositoryHelper.QueryFirstOrDefault<int>(sql, param, CommandType.Text);
        }

        public void Update(MaterialPeacaoModel materialPeacao)
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
	                        TABELA_TARIFA_ESPECIAL_ID = @TabelaTarifaMaterialId";

            var param = new
            {
                //materialPeacao.TabelaTarifaMaterialId,
                //Conferente = Utils.ConverterValor(materialPeacao.Conferente),
                //SemanalDiurno = Utils.ConverterValor(materialPeacao.SemanalDiurno),
                //SemanalNoturno = Utils.ConverterValor(materialPeacao.SemanalNoturno),
                //FdsDiurno = Utils.ConverterValor(materialPeacao.FdsDiurno),
                //FdsNoturno = Utils.ConverterValor(materialPeacao.FdsNoturno),
                //materialPeacao.Municipio.MunicipioId,
                //materialPeacao.Veiculo.TabTipoVeiculoId,
                //materialPeacao.AcordoRodoviario.TabTipoAcordoId,
                //materialPeacao.AcordoEspecial.TabTipoAcordoEspecialId,
                //materialPeacao.FamiliaMercadoria.FamiliaProdutoId
            };

            RepositoryHelper.Execute(sql, param, CommandType.Text);
        }

        public void Delete(int tabelaTarifaMaterialId)
        {
            var sql = @"
                        UPDATE
	                        TABELA_TARIFA_ESPECIAL
                        SET
	                        TAB_STATUS_ID = 2
                        WHERE
	                        TABELA_TARIFA_ESPECIAL_ID = @TabelaTarifaMaterialId";

            var param = new
            {
                TabelaTarifaMaterialId = tabelaTarifaMaterialId
            };

            RepositoryHelper.Execute(sql, param, CommandType.Text);
        }

        public bool VerificarDuplicidade(MaterialPeacaoModel materialPeacao)
        {
            var sql = @"
                        SELECT
		                    1
                        FROM
	                        TABELA_TARIFA_ESPECIAL
                        WHERE
	                        TAB_STATUS_ID = 1
	                        AND TABELA_TARIFA_ESPECIAL_ID <> @TabelaTarifaMaterialId
	                        AND TABELA_PRECO_FORNECEDOR_ID = @TabelaPrecoFornecedorId
	                        AND MUNICIPIO_ID = @MunicipioId
		                    AND TAB_TIPO_VEICULO_ID = @TabTipoVeiculoId
		                    AND TAB_TIPO_ACORDO_ID = @TabTipoAcordoId
		                    AND TAB_TIPO_ACORDO_ESPECIAL_ID = @TabTipoAcordoEspecialId
		                    AND FAMILIA_PRODUTO_ID = @FamiliaProdutoId";

            var param = new
            {
                //materialPeacao.TabelaTarifaMaterialId,
                //materialPeacao.TabelaPrecoFornecedorId,
                //materialPeacao.Municipio.MunicipioId,
                //materialPeacao.Veiculo.TabTipoVeiculoId,
                //materialPeacao.AcordoRodoviario.TabTipoAcordoId,
                //materialPeacao.AcordoEspecial.TabTipoAcordoEspecialId,
                //materialPeacao.FamiliaMercadoria.FamiliaProdutoId
            };

            return RepositoryHelper.QueryFirstOrDefault<int>(sql, param, CommandType.Text) == 1;
        }

        public void GravarHistorico(int tabelaTarifaMaterialId, int usuarioId)
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
		                    TAB_STATUS_ID
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
		                    TAB_STATUS_ID
	                        GETDATE(),
	                        @UsuarioId
                        FROM
	                        TABELA_TARIFA_ESPECIAL
                        WHERE
	                        TABELA_TARIFA_ESPECIAL_ID = @TabelaTarifaMaterialId";

            var param = new
            {
                TabelaTarifaMaterialId = tabelaTarifaMaterialId,
                UsuarioId = usuarioId
            };

            RepositoryHelper.Execute(sql, param, CommandType.Text);
        }
    }
}