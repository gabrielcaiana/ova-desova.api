using Signa.Library;
using Signa.Library.Exceptions;
using Signa.Library.Extensions;
using Signa.OvaDesova.Api.Data.Interface;
using Signa.OvaDesova.Api.Data.Repository;
using Signa.OvaDesova.Api.Domain.Models;
using Signa.OvaDesova.Api.Services.Interfaces;
using System.Collections.Generic;

namespace Signa.OvaDesova.Api.Services.Impls
{
    class TarifasPadraoService : ITarifasPadraoService
    {
        ITarifasPadraoRepository _tarifasPadrao;

        public TarifasPadraoService()
        {
            _tarifasPadrao = new TarifasPadraoRepository();
        }

        public IEnumerable<TarifasPadraoModel> ConsultarTarifasPadrao(int tabelaPrecoFornecedorId)
        {
            return _tarifasPadrao.ConsultarTarifasPadrao(tabelaPrecoFornecedorId);
        }

        public int Save(TarifasPadraoModel tarifasPadrao)
        {
            if (_tarifasPadrao.VerificarDuplicidade(tarifasPadrao))
            {
                throw new SignaRegraNegocioException("Já existe Tarifa Padrão para este fornecedor e localidade.");
            }

            if (tarifasPadrao.TabelaOvaDesovaId.IsZeroOrNull())
            {
                tarifasPadrao.TabelaOvaDesovaId = _tarifasPadrao.Insert(tarifasPadrao);

                if (tarifasPadrao.TabelaOvaDesovaId.IsZeroOrNull())
                {
                    throw new SignaRegraNegocioException("Erro na inserção das Tarifas Padrão.");
                }
            }
            else
            {
                _tarifasPadrao.Update(tarifasPadrao);
            }

            _tarifasPadrao.GravarHistorico(tarifasPadrao.TabelaOvaDesovaId, Global.UsuarioId);

            return tarifasPadrao.TabelaOvaDesovaId;
        }

        public void Delete(int tabelaOvaDesovaId)
        {
            _tarifasPadrao.Delete(tabelaOvaDesovaId);
            _tarifasPadrao.GravarHistorico(tabelaOvaDesovaId, Global.UsuarioId);
        }
    }
}