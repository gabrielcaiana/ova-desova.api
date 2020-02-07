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
    public class MaterialPeacaoBL
    {
        private readonly IMapper _mapper;
        private readonly MaterialPeacaoDAO _materialPeacaoDAO;

        public MaterialPeacaoBL(
            IMapper mapper,
            MaterialPeacaoDAO materialPeacaoDAO
        )
        {
            _mapper = mapper;
            _materialPeacaoDAO = materialPeacaoDAO;
        }

        public IEnumerable<MaterialPeacaoModel> ConsultarMaterialPeacao(int tabelaPrecoFornecedorId)
        {
            return _mapper.Map<IEnumerable<MaterialPeacaoModel>>(_materialPeacaoDAO.ConsultarMaterialPeacao(tabelaPrecoFornecedorId));
        }

        public int Save(MaterialPeacaoModel materialPeacao)
        {
            if (materialPeacao.NecessitaFrete && !_materialPeacaoDAO.HasFreteMaterialPeacao(materialPeacao.TabelaPrecoFornecedorId))
            {
                throw new SignaRegraNegocioException("Necessário cadastrar valor do frete nos Dados Gerais");
            }


            if (materialPeacao.TabelaTarifaMaterialId.IsZeroOrNull())
            {
                if (_materialPeacaoDAO.VerificarDuplicidade(_mapper.Map<MaterialPeacaoEntity>(materialPeacao)))
                {
                    throw new SignaRegraNegocioException("Já existe este Material de Peação cadastrado para este fornecedor");
                }

                materialPeacao.TabelaTarifaMaterialId = _materialPeacaoDAO.Insert(_mapper.Map<MaterialPeacaoEntity>(materialPeacao));

                if (materialPeacao.TabelaTarifaMaterialId.IsZeroOrNull())
                {
                    throw new SignaRegraNegocioException("Erro na inserção do Material de Peação");
                }
            }
            else
            {
                _materialPeacaoDAO.Update(_mapper.Map<MaterialPeacaoEntity>(materialPeacao));
            }

            _materialPeacaoDAO.GravarHistorico(materialPeacao.TabelaTarifaMaterialId, Global.UsuarioId);

            return materialPeacao.TabelaTarifaMaterialId;
        }

        public void Delete(int tabelaTarifaMaterialId)
        {
            _materialPeacaoDAO.Delete(tabelaTarifaMaterialId);
            _materialPeacaoDAO.GravarHistorico(tabelaTarifaMaterialId, Global.UsuarioId);
        }

        public void DeleteAll(int tabelaPrecoFornecedorId)
        {
            _materialPeacaoDAO.GravarHistoricoAll(tabelaPrecoFornecedorId);
            _materialPeacaoDAO.DeleteAll(tabelaPrecoFornecedorId);
        }

        public IEnumerable<MaterialPeacaoModel> ConsultarHistorico(int tabelaTarifaMaterialId)
        {
            return _mapper.Map<IEnumerable<MaterialPeacaoModel>>(_materialPeacaoDAO.ConsultarHistorico(tabelaTarifaMaterialId));
        }

        public IEnumerable<MaterialPeacaoModel> ConsultarHistoricoExclusao(int tabelaPrecoFornecedorId)
        {
            return _mapper.Map<IEnumerable<MaterialPeacaoModel>>(_materialPeacaoDAO.ConsultarHistoricoExclusao(tabelaPrecoFornecedorId));
        }
    }
}