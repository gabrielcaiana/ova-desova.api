using Signa.OvaDesova.Api.Data.Interface;
using Signa.OvaDesova.Api.Data.Repository;
using Signa.OvaDesova.Api.Domain.Models;
using Signa.OvaDesova.Api.Services.Interfaces;
using System.Collections.Generic;

namespace Signa.OvaDesova.Api.Services.Impls
{
    class ComboService : IComboService
    {
        IComboRepository _combo;

        public ComboService()
        {
            _combo = new ComboRepository();
        }

        public IEnumerable<VeiculoModel> GetAllVeiculo()
        {
            return _combo.GetAllVeiculo();
        }

        public IEnumerable<AcordoRodoviarioModel> GetAllAcordoRodoviario()
        {
            return _combo.GetAllAcordoRodoviario();
        }

        public IEnumerable<AcordoEspecialModel> GetAllAcordoEspecial()
        {
            return _combo.GetAllAcordoEspecial();
        }

        public IEnumerable<FamiliaMercadoriaModel> GetAllFamiliaMercadoria()
        {
            return _combo.GetAllFamiliaMercadoria();
        }

        public IEnumerable<MaterialModel> GetAllMaterial()
        {
            return _combo.GetAllMaterial();
        }

        public IEnumerable<UnidadeMedidaModel> GetAllUnidadeMedida()
        {
            return _combo.GetAllUnidadeMedida();
        }
    }
}