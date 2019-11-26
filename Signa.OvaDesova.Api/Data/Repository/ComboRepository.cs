using Signa.Library.Helpers;
using Signa.OvaDesova.Api.Data.Interface;
using Signa.OvaDesova.Api.Domain.Models;
using System.Collections.Generic;
using System.Data;

namespace Signa.OvaDesova.Api.Data.Repository
{
    class ComboRepository : RepositoryBase, IComboRepository
    {
        public IEnumerable<FornecedorModel> GetAllFornecedor()
        {
            var sql = @"
                        SELECT DISTINCT
                            FORNECEDOR_ID FornecedorId,
	                        NOME_FANTASIA NomeFantasia,
                            CNPJ_CPF Cnpj,
	                        IE_RG InscricaoEstadual,
                            MUNICIPIO + ' - ' + UF Municipio
                        FROM
	                        VFORNEC_TAB_TIPO_FORNEC2
                        WHERE
	                        TAB_STATUS_ID = 1
                        ORDER BY NOME_FANTASIA";

            return RepositoryHelper.Query<FornecedorModel>(sql, null, CommandType.Text);
        }

        public IEnumerable<MunicipioModel> GetAllMunicipio()
        {
            var sql = @"Select 
                               Municipio_Id MunicipioId,
                               Municipio + ' - ' + Uf NomeMunicipio
                        From   
                             Municipio
                        Where  Tab_Status_Id = 1
                        Order By 
                                 Municipio";

            return RepositoryHelper.Query<MunicipioModel>(sql, null, CommandType.Text);
        }

        public IEnumerable<UfModel> GetAllUf()
        {
            var sql = @"
                        SELECT
	                        TAB_UF_ID UfId,
	                        SIGLA_UF SiglaUf
                        FROM
	                        TAB_UF
                        WHERE
	                        TAB_STATUS_ID = 1
	                        AND TAB_UF_ID NOT IN (28, 29)
                        ORDER BY
	                        SIGLA_UF";

            return RepositoryHelper.Query<UfModel>(sql, null, CommandType.Text);
        }

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