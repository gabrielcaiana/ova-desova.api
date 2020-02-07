using Dapper;
using Signa.Library.Core.Data.Repository;
using Signa.OvaDesova.Api.Domain.Entities;
using System.Collections.Generic;

namespace Signa.OvaDesova.Api.Data.Repository
{
    public class ComboDAO : RepositoryBase
    {
        public IEnumerable<FornecedorEntity> GetAllFornecedor()
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
            using (var db = Connection)
            {
                return db.Query<FornecedorEntity>(sql);
            }
        }

        public IEnumerable<MunicipioEntity> GetAllMunicipio()
        {
            var sql = @"Select 
                               Municipio_Id MunicipioId,
                               Municipio + ' - ' + Uf NomeMunicipio
                        From   
                             Municipio
                        Where  Tab_Status_Id = 1
                        Order By 
                                 Municipio";
            using (var db = Connection)
            {
                return db.Query<MunicipioEntity>(sql);
            }
        }

        public IEnumerable<UfEntity> GetAllUf()
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
            using (var db = Connection)
            {
                return db.Query<UfEntity>(sql);
            }
        }

        public IEnumerable<VeiculoEntity> GetAllVeiculo()
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
            using (var db = Connection)
            {
                return db.Query<VeiculoEntity>(sql);
            }
        }

        public IEnumerable<AcordoRodoviarioEntity> GetAllAcordoRodoviario()
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
            using (var db = Connection)
            {
                return db.Query<AcordoRodoviarioEntity>(sql);
            }
        }

        public IEnumerable<AcordoEspecialEntity> GetAllAcordoEspecial(int tabTipoAcordoId)
        {
            var sql = @"                        
                        Select 
                            Ttae.Tab_Tipo_Acordo_Especial_Id Tabtipoacordoespecialid, 
                            Ttae.Desc_Tipo_Acordo_Especial   Desctipoacordoespecial
                        From   Tab_Tipo_Acordo_Rod_Esp Tare
                            Inner Join Tab_Tipo_Acordo_Especial Ttae On Ttae.Tab_Tipo_Acordo_Especial_Id = Tare.Tab_Tipo_Acordo_Especial_Id
                                                                        And Ttae.Tab_Status_Id = 1
                        Where  Tare.Tab_Status_Id = 1
                            And Tare.Tab_Tipo_Acordo_Id = @tabTipoAcordoId
                        Order By Ttae.Desc_Tipo_Acordo_Especial";

            using (var db = Connection)
            {
                return db.Query<AcordoEspecialEntity>(sql, new { tabTipoAcordoId });
            }
        }

        public IEnumerable<FamiliaMercadoriaEntity> GetAllFamiliaMercadoria()
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
            using (var db = Connection)
            {
                return db.Query<FamiliaMercadoriaEntity>(sql);
            }
        }

        public IEnumerable<MaterialEntity> GetAllMaterial()
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
            using (var db = Connection)
            {
                return db.Query<MaterialEntity>(sql);
            }
        }

        public IEnumerable<UnidadeMedidaEntity> GetAllUnidadeMedida()
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
            using (var db = Connection)
            {
                return db.Query<UnidadeMedidaEntity>(sql);
            }
        }
    }
}