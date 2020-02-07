using AutoMapper;
using Signa.Library.Core.Exceptions;
using Signa.Library.Core.Extensions;
using Signa.OvaDesova.Api.Data.Repository;
using Signa.OvaDesova.Api.Domain.Entities;
using Signa.OvaDesova.Api.Domain.Models;
using System.Collections.Generic;
using Signa.Library.Core;

namespace Signa.OvaDesova.Api.Business
{
    public class TarifaEspecialBL
    {
        private readonly IMapper _mapper;
        private readonly TarifaEspecialDAO _tarifaEspecialDAO;

        public TarifaEspecialBL(
            IMapper mapper,
            TarifaEspecialDAO tarifaEspecialDAO
        )
        {
            _mapper = mapper;
            _tarifaEspecialDAO = tarifaEspecialDAO;
        }
        public IEnumerable<TarifaEspecialModel> ConsultarTarifaEspecial(int tabelaPrecoFornecedorId)
        {
            return _mapper.Map<IEnumerable<TarifaEspecialModel>>(_tarifaEspecialDAO.ConsultarTarifaEspecial(tabelaPrecoFornecedorId));
        }

        public int Save(TarifaEspecialModel tarifaEspecial)
        {
            if (tarifaEspecial.Veiculo.TabTipoVeiculoId.IsZeroOrNull())
            {
                tarifaEspecial.Veiculo.TabTipoVeiculoId = null;
            }


            if (tarifaEspecial.AcordoRodoviario.TabTipoAcordoId.IsZeroOrNull())
            {
                tarifaEspecial.AcordoRodoviario.TabTipoAcordoId = null;
            }


            if (tarifaEspecial.AcordoEspecial.TabTipoAcordoEspecialId.IsZeroOrNull())
            {
                tarifaEspecial.AcordoEspecial.TabTipoAcordoEspecialId = null;
            }


            if (tarifaEspecial.FamiliaMercadoria.FamiliaProdutoId.IsZeroOrNull())
            {
                tarifaEspecial.FamiliaMercadoria.FamiliaProdutoId = null;
            }

            if (_tarifaEspecialDAO.VerificarDuplicidade(_mapper.Map<TarifaEspecialEntity>(tarifaEspecial)))
            {
                throw new SignaRegraNegocioException("Já existe Tarifa Especial para este fornecedor e localidade");
            }

            if (tarifaEspecial.TabelaTarifaEspecialId.IsZeroOrNull())
            {
                tarifaEspecial.TabelaTarifaEspecialId = _tarifaEspecialDAO.Insert(_mapper.Map<TarifaEspecialEntity>(tarifaEspecial));

                if (tarifaEspecial.TabelaTarifaEspecialId.IsZeroOrNull())
                {
                    throw new SignaRegraNegocioException("Erro na inserção das Tarifas Especial");
                }
            }
            else
            {
                _tarifaEspecialDAO.Update(_mapper.Map<TarifaEspecialEntity>(tarifaEspecial));
            }

            _tarifaEspecialDAO.GravarHistorico(tarifaEspecial.TabelaTarifaEspecialId, Global.UsuarioId);

            return tarifaEspecial.TabelaTarifaEspecialId;
        }

        public void Delete(int tabelaTarifaEspecialId)
        {
            _tarifaEspecialDAO.Delete(tabelaTarifaEspecialId);
            _tarifaEspecialDAO.GravarHistorico(tabelaTarifaEspecialId, Global.UsuarioId);
        }

        public void DeleteAll(int tabelaPrecoFornecedorId)
        {
            _tarifaEspecialDAO.GravarHistoricoAll(tabelaPrecoFornecedorId);
            _tarifaEspecialDAO.DeleteAll(tabelaPrecoFornecedorId);
        }

        public IEnumerable<TarifaEspecialModel> ConsultarHistorico(int tabelaTarifaEspecialId)
        {
            return _mapper.Map<IEnumerable<TarifaEspecialModel>>(_tarifaEspecialDAO.ConsultarHistorico(tabelaTarifaEspecialId));
        }

        public IEnumerable<TarifaEspecialModel> ConsultarHistoricoExclusao(int tabelaPrecoFornecedorId)
        {
            return _mapper.Map<IEnumerable<TarifaEspecialModel>>(_tarifaEspecialDAO.ConsultarHistoricoExclusao(tabelaPrecoFornecedorId));
        }
    }
}