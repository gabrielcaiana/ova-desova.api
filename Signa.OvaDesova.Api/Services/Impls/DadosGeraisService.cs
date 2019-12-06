using Signa.Library;
using Signa.Library.Exceptions;
using Signa.Library.Extensions;
using Signa.OvaDesova.Api.Data.Interface;
using Signa.OvaDesova.Api.Data.Repository;
using Signa.OvaDesova.Api.Domain.Models;
using Signa.OvaDesova.Api.Services.Interfaces;

namespace Signa.OvaDesova.Api.Services.Impls
{
    class DadosGeraisService : IDadosGeraisService
    {
        IDadosGeraisRepository _dadosGerais;

        public DadosGeraisService()
        {
            _dadosGerais = new DadosGeraisRepository();
        }

        public DadosGeraisModel ConsultarDadosGerais(int tabelaPrecoFornecedorId)
        {
            return _dadosGerais.ConsultarDadosGerais(tabelaPrecoFornecedorId);
        }

        public int Save(DadosGeraisModel dadosGerais)
        {
            if (dadosGerais.FornecedorId.IsZeroOrNull())
            {
                dadosGerais.FornecedorId = null;
            }
            
            if (dadosGerais.TabelaPrecoFornecedorId.IsZeroOrNull())
            {
                if (_dadosGerais.VerificarDuplicidade(dadosGerais))
                {
                    throw new SignaRegraNegocioException("Já existe Cadastro de Tarifa para este fornecedor e validade.");
                }

                dadosGerais.TabelaPrecoFornecedorId = _dadosGerais.Insert(dadosGerais);

                if (dadosGerais.TabelaPrecoFornecedorId.IsZeroOrNull())
                {
                    throw new SignaRegraNegocioException("Erro na inserção dos Dados Gerais.");
                }
            }
            else
            {
                _dadosGerais.Update(dadosGerais);
            }

            _dadosGerais.GravarHistorico(dadosGerais.TabelaPrecoFornecedorId, Global.UsuarioId);

            return dadosGerais.TabelaPrecoFornecedorId;
        }

        public void Delete(int tabelaPrecoFornecedorId)
        {
            _dadosGerais.Delete(tabelaPrecoFornecedorId);
            _dadosGerais.GravarHistorico(tabelaPrecoFornecedorId, Global.UsuarioId);
        }

        public DadosGeraisModel ConsultarHistorico(int tabelaPrecoFornecedorId)
        {
            return _dadosGerais.ConsultarHistorico(tabelaPrecoFornecedorId);
        }
    }
}