using FluentValidation;
using Signa.Default.Template.Domain.Validators;
using Signa.Library;
using Signa.Library.Exceptions;
using Signa.Library.Extensions;
using Signa.Library.Helpers;
using Signa.OvaDesova.Api.Data.Interface;
using Signa.OvaDesova.Api.Data.Repository;
using Signa.OvaDesova.Api.Domain.Entities;
using Signa.OvaDesova.Api.Domain.Models;
using Signa.OvaDesova.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Signa.OvaDesova.Api.Services.Impls
{
    class OvaDesovaService : IOvaDesovaService
    {
        IOvaDesovaRepository _pessoa;
        PessoaValidator _pessoaValidator;

        public OvaDesovaService()
        {
            _pessoa = new OvaDesovaRepository();
            _pessoaValidator = new PessoaValidator();
        }

        public OvaDesovaModel Atualizar(OvaDesovaModel Pessoa)
        {
            _pessoaValidator.ValidateAndThrow(Pessoa);

            if (_pessoa.Update(new OvaDesovaEntity(Pessoa.PessoaId, Pessoa.PessoaNome, Pessoa.PessoaNomeFantasia, Pessoa.PessoaCnpjCpf, Pessoa.PessoaEmail)) == 0)
            {
                throw new SignaNoRowAffectedException("Nenhum registro afetado");
            }
            CommonHelper.GravarLogSistema(41, 2, Global.UsuarioId, Pessoa.PessoaId, 0, Global.FuncaoId);

            var EntidadeConsulta = _pessoa.GetById(Pessoa.PessoaId);

            return new OvaDesovaModel(EntidadeConsulta.PessoaId, EntidadeConsulta.Nome, EntidadeConsulta.NomeFantasia, EntidadeConsulta.CnpjCpf, EntidadeConsulta.Email);
        }

        public OvaDesovaModel Consultar(int id)
        {
            if (id.IsZeroOrNull())
            {
                throw new SignaRegraNegocioException("Informe um id para consulta");
            }

            var Pessoa = _pessoa.GetById(id);

            if (Pessoa == null)
            {
                throw new SignaRegraNegocioException("Pessoa não encontrada");
            }

            return new OvaDesovaModel(Pessoa.PessoaId, Pessoa.Nome, Pessoa.NomeFantasia, Pessoa.CnpjCpf, Pessoa.Email);
        }

        public void Excluir(int id)
        {
            if (id.IsZeroOrNull())
            {
                throw new SignaRegraNegocioException("Informe um Id para Excluir");
            }

            if (_pessoa.Delete(id) == 0)
            {
                throw new SignaNoRowAffectedException("Nenhum registro afetado");
            }

            CommonHelper.GravarLogSistema(41, 3, Global.UsuarioId, id, 0, Global.FuncaoId);
        }

        public OvaDesovaModel Gravar(OvaDesovaModel Pessoa)
        {
            _pessoaValidator.ValidateAndThrow(Pessoa);

            var id = _pessoa.Insert(new OvaDesovaEntity(Pessoa.PessoaId, Pessoa.PessoaNome, Pessoa.PessoaNomeFantasia, Pessoa.PessoaCnpjCpf, Pessoa.PessoaEmail));

            if (id.IsZeroOrNull())
            {
                throw new ApplicationException("Erro na inserção de pessoa");
            }
            CommonHelper.GravarLogSistema(41, 1, Global.UsuarioId, id, 0, Global.FuncaoId);

            var Entidade = _pessoa.GetById(id);
            return new OvaDesovaModel(Entidade.PessoaId, Entidade.Nome, Entidade.NomeFantasia, Entidade.CnpjCpf, Entidade.Email);
        }

        public IEnumerable<OvaDesovaModel> Listar()
        {
            return _pessoa.GetAll().Select(x => new OvaDesovaModel(x.PessoaId, x.Nome, x.NomeFantasia, x.CnpjCpf, x.Email));
        }
    }
}