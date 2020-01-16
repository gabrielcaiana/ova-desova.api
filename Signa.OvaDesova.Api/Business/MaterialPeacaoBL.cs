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
  public class MaterialPeacaoBL
  {
    private readonly IMapper _mapper;
    private readonly MaterialPeacaoDAO _materialPeacaoDao;

    public MaterialPeacaoBL(
        IMapper mapper,
        MaterialPeacaoDAO materialPeacaoDAO
    )
    {
      _mapper = mapper;
      _materialPeacaoDao = materialPeacaoDAO;
    }

    public IEnumerable<MaterialPeacaoModel> ConsultarMaterialPeacao(int tabelaPrecoFornecedorId)
    {
      return _mapper.Map<IEnumerable<MaterialPeacaoModel>>(_materialPeacaoDao.ConsultarMaterialPeacao(tabelaPrecoFornecedorId));
    }

    public int Save(MaterialPeacaoModel materialPeacao)
    {
      if (materialPeacao.TabelaTarifaMaterialId.IsZeroOrNull())
      {
        if (_materialPeacaoDao.VerificarDuplicidade(_mapper.Map<MaterialPeacaoEntity>(materialPeacao)))
        {
          throw new SignaRegraNegocioException("Já existe este Material de Peação cadastrado para este fornecedor.");
        }

        materialPeacao.TabelaTarifaMaterialId = _materialPeacaoDao.Insert(_mapper.Map<MaterialPeacaoEntity>(materialPeacao));

        if (materialPeacao.TabelaTarifaMaterialId.IsZeroOrNull())
        {
          throw new SignaRegraNegocioException("Erro na inserção do Material de Peação.");
        }
      }
      else
      {
        _materialPeacaoDao.Update(_mapper.Map<MaterialPeacaoEntity>(materialPeacao));
      }

      _materialPeacaoDao.GravarHistorico(materialPeacao.TabelaTarifaMaterialId, Global.UsuarioId);

      return materialPeacao.TabelaTarifaMaterialId;
    }

    public void Delete(int tabelaTarifaMaterialId)
    {
      _materialPeacaoDao.Delete(tabelaTarifaMaterialId);
      _materialPeacaoDao.GravarHistorico(tabelaTarifaMaterialId, Global.UsuarioId);
    }

    public IEnumerable<MaterialPeacaoModel> ConsultarHistorico(int tabelaTarifaMaterialId)
    {
      return _mapper.Map<IEnumerable<MaterialPeacaoModel>>(_materialPeacaoDao.ConsultarHistorico(tabelaTarifaMaterialId));
    }

    public IEnumerable<MaterialPeacaoModel> ConsultarHistoricoExclusao(int tabelaPrecoFornecedorId)
    {
      return _mapper.Map<IEnumerable<MaterialPeacaoModel>>(_materialPeacaoDao.ConsultarHistoricoExclusao(tabelaPrecoFornecedorId));
    }
  }
}