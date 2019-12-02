using Signa.Library;
using Signa.Library.Exceptions;
using Signa.Library.Extensions;
using Signa.OvaDesova.Api.Data.Interface;
using Signa.OvaDesova.Api.Data.Repository;
using Signa.OvaDesova.Api.Domain.Models;
using Signa.OvaDesova.Api.Services.Interfaces;

namespace Signa.OvaDesova.Api.Services.Impls
{
    class TarifasPadraoService : ITarifasPadraoService
    {
        ITarifasPadraoRepository _tarifasPadrao;

        public TarifasPadraoService()
        {
            _tarifasPadrao = new TarifasPadraoRepository();
        }

        public TarifasPadraoModel ConsultarTarifasPadrao(int tabelaOvaDesovaId)
        {
            return _tarifasPadrao.ConsultarTarifasPadrao(tabelaOvaDesovaId);
        }
    }
}