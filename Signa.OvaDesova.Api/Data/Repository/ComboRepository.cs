using Signa.Library.Helpers;
using Signa.OvaDesova.Api.Data.Interface;
using Signa.OvaDesova.Api.Domain.Models;
using System.Collections.Generic;
using System.Data;

namespace Signa.OvaDesova.Api.Data.Repository
{
    class ComboRepository : RepositoryBase, IComboRepository
    {
        public IEnumerable<VeiculoModel> GetAllVeiculo()
        {
            var sql = @"
                        SELECT
	                        TAB_TIPO_VEICULO_ID TabTipoVeiculoId,
	                        DESC_TIPO_VEICULO DescTipoVeiculo
                        FROM
	                        TAB_TIPO_VEICULO
                        WHERE
	                        TAB_STATUS_ID = 1
                        ORDER BY
	                        DESC_TIPO_VEICULO";

            return RepositoryHelper.Query<VeiculoModel>(sql, null, CommandType.Text);
        }

        public IEnumerable<AcordoRodoviarioModel> GetAllAcordoRodoviario()
        {
            var sql = @"
                        SELECT
	                        TAB_TIPO_ACORDO_ID TabTipoAcordoId,
	                        DESC_TIPO_ACORDO DescTipoAcordo
                        FROM
	                        TAB_TIPO_ACORDO
                        WHERE
	                        TAB_STATUS_ID = 1
                        ORDER BY
	                        DESC_TIPO_ACORDO";

            return RepositoryHelper.Query<AcordoRodoviarioModel>(sql, null, CommandType.Text);
        }

        public IEnumerable<AcordoEspecialModel> GetAllAcordoEspecial()
        {
            var sql = @"
                        SELECT
	                        TAB_TIPO_ACORDO_ESPECIAL_ID TabTipoAcordoEspecialId,
	                        DESC_TIPO_ACORDO_ESPECIAL DescTipoAcordoEspecial
                        FROM
	                        TAB_TIPO_ACORDO_ESPECIAL
                        WHERE
	                        TAB_STATUS_ID = 1
                        ORDER BY
	                        DESC_TIPO_ACORDO_ESPECIAL";

            return RepositoryHelper.Query<AcordoEspecialModel>(sql, null, CommandType.Text);
        }

        public IEnumerable<FamiliaMercadoriaModel> GetAllFamiliaMercadoria()
        {
            var sql = @"
                        SELECT
	                        FAMILIA_PRODUTO_ID FamiliaProdutoId,
	                        DESC_FAMILIA DescFamilia
                        FROM
	                        FAMILIA_PRODUTO
                        WHERE
	                        TAB_STATUS_ID = 1
                        ORDER BY
	                        DESC_FAMILIA";

            return RepositoryHelper.Query<FamiliaMercadoriaModel>(sql, null, CommandType.Text);
        }

        public IEnumerable<MaterialModel> GetAllMaterial()
        {
            var sql = @"
                        SELECT
	                        TAB_TIPO_EQUIPAM_ID TabTipoEquipamId,
	                        DESCR DescMaterial
                        FROM
	                        TAB_TIPO_EQUIPAM
                        WHERE
	                        TAB_STATUS_ID = 1
                        ORDER BY
	                        DESCR";

            return RepositoryHelper.Query<MaterialModel>(sql, null, CommandType.Text);
        }

        public IEnumerable<UnidadeMedidaModel> GetAllUnidadeMedida()
        {
            var sql = @"
                        SELECT
	                        TAB_UNIDADE_MEDIDA_ID,
	                        DESC_UNIDADE_MEDIDA
                        FROM
	                        TAB_UNIDADE_MEDIDA
                        WHERE
	                        TAB_STATUS_ID = 1
                        ORDER BY
	                        DESC_UNIDADE_MEDIDA";

            return RepositoryHelper.Query<UnidadeMedidaModel>(sql, null, CommandType.Text);
        }
    }
}