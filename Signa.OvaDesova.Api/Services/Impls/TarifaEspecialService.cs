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
    class TarifaEspecialService : ITarifaEspecialService
    {
        ITarifaEspecialRepository _tarifaEspecial;

        public TarifaEspecialService()
        {
            _tarifaEspecial = new TarifaEspecialRepository();
        }

        public IEnumerable<TarifaEspecialModel> ConsultarTarifaEspecial(int tabelaPrecoFornecedorId)
        {
            return _tarifaEspecial.ConsultarTarifaEspecial(tabelaPrecoFornecedorId);
        }

        public int Save(TarifaEspecialModel tarifaEspecial)
        {
            if (tarifaEspecial.TabelaTarifaEspecialId.IsZeroOrNull())
            {
                if (_tarifaEspecial.VerificarDuplicidade(tarifaEspecial))
                {
                    throw new SignaRegraNegocioException("Já existe Tarifa Especial para este fornecedor e localidade.");
                }

                tarifaEspecial.TabelaTarifaEspecialId = _tarifaEspecial.Insert(tarifaEspecial);

                if (tarifaEspecial.TabelaTarifaEspecialId.IsZeroOrNull())
                {
                    throw new SignaRegraNegocioException("Erro na inserção das Tarifas Especial.");
                }
            }
            else
            {
                _tarifaEspecial.Update(tarifaEspecial);
            }

            _tarifaEspecial.GravarHistorico(tarifaEspecial.TabelaTarifaEspecialId, Global.UsuarioId);

            return tarifaEspecial.TabelaTarifaEspecialId;
        }

        public void Delete(int tabelaTarifaEspecialId)
        {
            _tarifaEspecial.Delete(tabelaTarifaEspecialId);
            _tarifaEspecial.GravarHistorico(tabelaTarifaEspecialId, Global.UsuarioId);
        }
    }
}