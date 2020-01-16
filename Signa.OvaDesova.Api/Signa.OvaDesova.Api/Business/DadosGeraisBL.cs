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
  public class DadosGeraisBL
  {
    private readonly IMapper _mapper;
    private readonly DadosGeraisDAO _dadosGeraisDao;

    public DadosGeraisBL(
        IMapper mapper,
        DadosGeraisDAO dadosGeraisDAO
    )
    {
      _mapper = mapper;
      _dadosGeraisDao = dadosGeraisDAO;
    }

    public DadosGeraisModel ConsultarDadosGerais(int tabelaPrecoFornecedorId)
    {
      return _mapper.Map<DadosGeraisModel>(_dadosGeraisDao.ConsultarDadosGerais(tabelaPrecoFornecedorId));
    }

    public int Save(DadosGeraisModel dadosGerais)
    {
      if (dadosGerais.FornecedorId.IsZeroOrNull())
      {
        dadosGerais.FornecedorId = null;
      }

      if (dadosGerais.TabelaPrecoFornecedorId.IsZeroOrNull())
      {
        if (_dadosGeraisDao.VerificarDuplicidade(_mapper.Map<DadosGeraisEntity>(dadosGerais)))
        {
          throw new SignaRegraNegocioException("Já existe Cadastro de Tarifa para este fornecedor e validade.");
        }

        dadosGerais.TabelaPrecoFornecedorId = _dadosGeraisDao.Insert(_mapper.Map<DadosGeraisEntity>(dadosGerais));

        if (dadosGerais.TabelaPrecoFornecedorId.IsZeroOrNull())
        {
          throw new SignaRegraNegocioException("Erro na inserção dos Dados Gerais.");
        }
      }
      else
      {
        _dadosGeraisDao.Update(_mapper.Map<DadosGeraisEntity>(dadosGerais));
      }

      _dadosGeraisDao.GravarHistorico(dadosGerais.TabelaPrecoFornecedorId, Global.UsuarioId);

      return dadosGerais.TabelaPrecoFornecedorId;
    }

    public void Delete(int tabelaPrecoFornecedorId)
    {
      _dadosGeraisDao.Delete(tabelaPrecoFornecedorId);
      _dadosGeraisDao.GravarHistorico(tabelaPrecoFornecedorId, Global.UsuarioId);
    }

    public IEnumerable<DadosGeraisModel> ConsultarHistorico(int tabelaPrecoFornecedorId)
    {
      return _mapper.Map<IEnumerable<DadosGeraisModel>>(_dadosGeraisDao.ConsultarHistorico(tabelaPrecoFornecedorId));
    }
  }
}