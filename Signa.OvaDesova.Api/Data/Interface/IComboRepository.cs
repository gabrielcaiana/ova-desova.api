using Signa.OvaDesova.Api.Domain.Models;
using System.Collections.Generic;

namespace Signa.OvaDesova.Api.Data.Interface
{
    interface IComboRepository
    {
        IEnumerable<FornecedorModel> GetAllFornecedor();
        IEnumerable<MunicipioModel> GetAllMunicipio();
        IEnumerable<UfModel> GetAllUf();
        IEnumerable<VeiculoModel> GetAllVeiculo();
        IEnumerable<AcordoRodoviarioModel> GetAllAcordoRodoviario();
        IEnumerable<AcordoEspecialModel> GetAllAcordoEspecial();
        IEnumerable<FamiliaMercadoriaModel> GetAllFamiliaMercadoria();
        IEnumerable<MaterialModel> GetAllMaterial();
        IEnumerable<UnidadeMedidaModel> GetAllUnidadeMedida();
    }
}