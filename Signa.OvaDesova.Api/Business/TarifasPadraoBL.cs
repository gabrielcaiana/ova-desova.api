using AutoMapper;
using Signa.Library.Core.Exceptions;
using Signa.Library.Core.Extensions;
using Signa.OvaDesova.Api.Data.Repository;
using Signa.OvaDesova.Api.Domain.Entities;
using Signa.OvaDesova.Api.Domain.Models;
using System.Collections.Generic;
using System.Data;
using Signa.Library.Core;

namespace Signa.OvaDesova.Api.Business
{
  public class TarifasPadraoBL
  {
    private readonly IMapper _mapper;
    private readonly TarifasPadraoDAO _tarifasPadraoDAO;

    public TarifasPadraoBL(
        IMapper mapper,
        TarifasPadraoDAO tarifasPadraoDAO
    )
    {
      _mapper = mapper;
      _tarifasPadraoDAO = tarifasPadraoDAO;
    }

    public IEnumerable<TarifasPadraoModel> ConsultarTarifasPadrao(int tabelaPrecoFornecedorId)
    {
      return _mapper.Map<IEnumerable<TarifasPadraoModel>>(_tarifasPadraoDAO.ConsultarTarifasPadrao(tabelaPrecoFornecedorId));
    }

    public int Save(TarifasPadraoModel tarifasPadrao)
    {
      if (tarifasPadrao.TabelaOvaDesovaId.IsZeroOrNull())
      {
        if (_tarifasPadraoDAO.VerificarDuplicidade(_mapper.Map<TarifasPadraoEntity>(tarifasPadrao)))
        {
          throw new SignaRegraNegocioException("Já existe Tarifa Padrão para este fornecedor e localidade.");
        }

        tarifasPadrao.TabelaOvaDesovaId = _tarifasPadraoDAO.Insert(_mapper.Map<TarifasPadraoEntity>(tarifasPadrao));

        if (tarifasPadrao.TabelaOvaDesovaId.IsZeroOrNull())
        {
          throw new SignaRegraNegocioException("Erro na inserção das Tarifas Padrão.");
        }
      }
      else
      {
        _tarifasPadraoDAO.Update(_mapper.Map<TarifasPadraoEntity>(tarifasPadrao));
      }

      _tarifasPadraoDAO.GravarHistorico(tarifasPadrao.TabelaOvaDesovaId, Global.UsuarioId);

      return tarifasPadrao.TabelaOvaDesovaId;
    }

    public void Delete(int tabelaOvaDesovaId)
    {
      _tarifasPadraoDAO.Delete(tabelaOvaDesovaId);
      _tarifasPadraoDAO.GravarHistorico(tabelaOvaDesovaId, Global.UsuarioId);
    }

    public IEnumerable<TarifasPadraoModel> ConsultarHistorico(int tabelaOvaDesovaId)
    {
      return _mapper.Map<IEnumerable<TarifasPadraoModel>>(_tarifasPadraoDAO.ConsultarHistorico(tabelaOvaDesovaId));
    }

    public IEnumerable<TarifasPadraoModel> ConsultarHistoricoExclusao(int tabelaPrecoFornecedorId)
    {
      return _mapper.Map<IEnumerable<TarifasPadraoModel>>(_tarifasPadraoDAO.ConsultarHistoricoExclusao(tabelaPrecoFornecedorId));
    }
  }
}