using Signa.OvaDesova.Api.Domain.Models;
using System.Collections.Generic;

namespace Signa.OvaDesova.Api.Services.Interfaces
{
    interface IComboService
    {
        IEnumerable<VeiculoModel> GetAllVeiculo();
        IEnumerable<AcordoRodoviarioModel> GetAllAcordoRodoviario();
        IEnumerable<AcordoEspecialModel> GetAllAcordoEspecial();
        IEnumerable<FamiliaMercadoriaModel> GetAllFamiliaMercadoria();
        IEnumerable<MaterialModel> GetAllMaterial();
        IEnumerable<UnidadeMedidaModel> GetAllUnidadeMedida();
    }
}