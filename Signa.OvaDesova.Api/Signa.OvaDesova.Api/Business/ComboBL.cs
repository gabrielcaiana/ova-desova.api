using AutoMapper;
using Signa.Library.Core.Exceptions;
using Signa.Library.Core.Extensions;
using Signa.OvaDesova.Api.Data.Repository;
using Signa.OvaDesova.Api.Domain.Entities;
using Signa.OvaDesova.Api.Domain.Models;
using System.Collections.Generic;

namespace Signa.OvaDesova.Api.Business
{
  public class ComboBL
  {
    private readonly IMapper _mapper;
    private readonly ComboDAO _comboDAO;

    public ComboBL(
        IMapper mapper,
        ComboDAO comboDAO
    )
    {
      _mapper = mapper;
      _comboDAO = comboDAO;
    }
    public IEnumerable<FornecedorModel> GetAllFornecedor()
    {
      return _mapper.Map<IEnumerable<FornecedorModel>>(_comboDAO.GetAllFornecedor());
    }

    public IEnumerable<MunicipioModel> GetAllMunicipio()
    {
      return _mapper.Map<IEnumerable<MunicipioModel>>(_comboDAO.GetAllMunicipio());
    }

    public IEnumerable<UfModel> GetAllUf()
    {
      return _mapper.Map<IEnumerable<UfModel>>(_comboDAO.GetAllUf());
    }

    public IEnumerable<VeiculoModel> GetAllVeiculo()
    {
      return _mapper.Map<IEnumerable<VeiculoModel>>(_comboDAO.GetAllVeiculo());
    }

    public IEnumerable<AcordoRodoviarioModel> GetAllAcordoRodoviario()
    {
      return _mapper.Map<IEnumerable<AcordoRodoviarioModel>>(_comboDAO.GetAllAcordoRodoviario());
    }

    public IEnumerable<AcordoEspecialModel> GetAllAcordoEspecial()
    {
      return _mapper.Map<IEnumerable<AcordoEspecialModel>>(_comboDAO.GetAllAcordoEspecial());
    }

    public IEnumerable<FamiliaMercadoriaModel> GetAllFamiliaMercadoria()
    {
      return _mapper.Map<IEnumerable<FamiliaMercadoriaModel>>(_comboDAO.GetAllFamiliaMercadoria());
    }

    public IEnumerable<MaterialModel> GetAllMaterial()
    {
      return _mapper.Map<IEnumerable<MaterialModel>>(_comboDAO.GetAllMaterial());
    }

    public IEnumerable<UnidadeMedidaModel> GetAllUnidadeMedida()
    {
      return _mapper.Map<IEnumerable<UnidadeMedidaModel>>(_comboDAO.GetAllUnidadeMedida());
    }
  }
}