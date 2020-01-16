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
  public class OvaDesovaBL
  {
    private readonly IMapper _mapper;
    private readonly OvaDesovaDAO _ovaDesovaDAO;

    public OvaDesovaBL(
        IMapper mapper,
        OvaDesovaDAO ovaDesovaDAO
    )
    {
      _mapper = mapper;
      _ovaDesovaDAO = ovaDesovaDAO;
    }

    public IEnumerable<ResultadoModel> GetAll(ConsultaModel consulta)
    {
      return _mapper.Map<IEnumerable<ResultadoModel>>(_ovaDesovaDAO.GetAll(_mapper.Map<ConsultaEntity>(consulta)));
    }
  }
}