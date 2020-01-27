using AutoMapper;
using Signa.OvaDesova.Api.Data.Repository;
using Signa.OvaDesova.Api.Domain.Entities;
using Signa.OvaDesova.Api.Domain.Models;
using System.Collections.Generic;
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
            _materialPeacaoBLL.DeleteAll(tabelaPrecoFornecedorId);
            _tarifaEspecialBLL.DeleteAll(tabelaPrecoFornecedorId);
            _tarifasPadraoBLL.DeleteAll(tabelaPrecoFornecedorId);
            _dadosGeraisBLL.Delete(tabelaPrecoFornecedorId);
        }
    }
}