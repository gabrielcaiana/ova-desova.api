using Signa.OvaDesova.Api.Domain.Models;
using System.Collections.Generic;

namespace Signa.OvaDesova.Api.Data.Interface
{
    interface IFornecedorRepository
    {
        IEnumerable<FornecedorModel> GetAll();
    }
}