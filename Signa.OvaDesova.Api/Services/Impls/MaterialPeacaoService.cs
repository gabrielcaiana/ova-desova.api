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
    class MaterialPeacaoService : IMaterialPeacaoService
    {
        IMaterialPeacaoRepository _materialPeacao;

        public MaterialPeacaoService()
        {
            _materialPeacao = new MaterialPeacaoRepository();
        }

        public IEnumerable<MaterialPeacaoModel> ConsultarMaterialPeacao(int tabelaPrecoFornecedorId)
        {
            return _materialPeacao.ConsultarMaterialPeacao(tabelaPrecoFornecedorId);
        }

        public int Save(MaterialPeacaoModel materialPeacao)
        {
            if (materialPeacao.TabelaTarifaMaterialId.IsZeroOrNull())
            {
                if (_materialPeacao.VerificarDuplicidade(materialPeacao))
                {
                    throw new SignaRegraNegocioException("Já existe este Material de Peação cadastrado para este fornecedor.");
                }

                materialPeacao.TabelaTarifaMaterialId = _materialPeacao.Insert(materialPeacao);

                if (materialPeacao.TabelaTarifaMaterialId.IsZeroOrNull())
                {
                    throw new SignaRegraNegocioException("Erro na inserção do Material de Peação.");
                }
            }
            else
            {
                _materialPeacao.Update(materialPeacao);
            }

            _materialPeacao.GravarHistorico(materialPeacao.TabelaTarifaMaterialId, Global.UsuarioId);

            return materialPeacao.TabelaTarifaMaterialId;
        }

        public void Delete(int tabelaTarifaMaterialId)
        {
            _materialPeacao.Delete(tabelaTarifaMaterialId);
            _materialPeacao.GravarHistorico(tabelaTarifaMaterialId, Global.UsuarioId);
        }
    }
}