using AutoMapper;
using Signa.Library.Core.Exceptions;
using Signa.Library.Core.Extensions;
using Signa.OvaDesova.Api.Data.Repository;
using Signa.OvaDesova.Api.Domain.Entities;
using Signa.OvaDesova.Api.Domain.Models;
using System.Collections.Generic;
using System.Data;
using Signa.Library.Core;
using System.Linq;

namespace Signa.OvaDesova.Api.Business
{
  public class OvaDesovaBL
  {
    private readonly IMapper _mapper;
    private readonly OvaDesovaDAO _ovaDesovaDAO;
    private readonly DadosGeraisBL _dadosGeraisBLL;
    private readonly TarifasPadraoBL _tarifasPadraoBLL;
    private readonly TarifaEspecialBL _tarifaEspecialBLL;
    private readonly MaterialPeacaoBL _materialPeacaoBLL;

    public OvaDesovaBL(
        IMapper mapper,
        OvaDesovaDAO ovaDesovaDAO,
        DadosGeraisBL dadosGeraisBL,
        TarifasPadraoBL tarifasPadraoBL,
        TarifaEspecialBL tarifaEspecialBL,
        MaterialPeacaoBL materialPeacaoBL
    )
    {
      _mapper = mapper;
      _ovaDesovaDAO = ovaDesovaDAO;
      _dadosGeraisBLL = dadosGeraisBL;
      _tarifasPadraoBLL = tarifasPadraoBL;
      _tarifaEspecialBLL = tarifaEspecialBL;
      _materialPeacaoBLL = materialPeacaoBL;
    }

    public IEnumerable<ResultadoModel> GetAll(ConsultaModel consulta)
    {
      return _mapper.Map<IEnumerable<ResultadoModel>>(_ovaDesovaDAO.GetAll(_mapper.Map<ConsultaEntity>(consulta)));
    }

    public void Delete(int tabelaPrecoFornecedorId)
    {
      _dadosGeraisBLL.Delete(tabelaPrecoFornecedorId);

      List<TarifasPadraoModel> listaTarifasPadrao = _tarifasPadraoBLL.ConsultarTarifasPadrao(tabelaPrecoFornecedorId).ToList();

      foreach (TarifasPadraoModel tarifasPadrao in listaTarifasPadrao)
      {
        _tarifasPadraoBLL.Delete(tarifasPadrao.TabelaOvaDesovaId);
      }

      List<TarifaEspecialModel> listaTarifaEspecial = _tarifaEspecialBLL.ConsultarTarifaEspecial(tabelaPrecoFornecedorId).ToList();

      foreach (TarifaEspecialModel tarifaEspecial in listaTarifaEspecial)
      {
        _tarifaEspecialBLL.Delete(tarifaEspecial.TabelaTarifaEspecialId);
      }

      List<MaterialPeacaoModel> listaMaterialPeacao = _materialPeacaoBLL.ConsultarMaterialPeacao(tabelaPrecoFornecedorId).ToList();

      foreach (MaterialPeacaoModel materialPeacao in listaMaterialPeacao)
      {
        _materialPeacaoBLL.Delete(materialPeacao.TabelaTarifaMaterialId);
      }
    }
  }
}