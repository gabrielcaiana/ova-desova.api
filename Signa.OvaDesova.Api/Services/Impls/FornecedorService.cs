using Signa.OvaDesova.Api.Data.Interface;
using Signa.OvaDesova.Api.Data.Repository;
using Signa.OvaDesova.Api.Domain.Models;
using Signa.OvaDesova.Api.Services.Interfaces;
using System.Collections.Generic;

namespace Signa.OvaDesova.Api.Services.Impls
{
    class FornecedorService : IFornecedorService
    {
        IFornecedorRepository _fornecedor;

        public FornecedorService()
        {
            _fornecedor = new FornecedorRepository();
        }

        public IEnumerable<FornecedorModel> GetAll()
        {
            return _fornecedor.GetAll();
        }
    }
}