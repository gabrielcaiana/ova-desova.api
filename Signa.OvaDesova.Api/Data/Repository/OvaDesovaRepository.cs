using Signa.Library.Helpers;
using Signa.OvaDesova.Api.Data.Interface;
using Signa.OvaDesova.Api.Domain.Entities;
using System.Collections.Generic;
using System.Data;

namespace Signa.OvaDesova.Api.Data.Repository
{
    class OvaDesovaRepository : RepositoryBase, IOvaDesovaRepository
    {
        public IEnumerable<OvaDesovaEntity> GetAll()
        {
            var sql = "" +
                "Select Top 50 " + _n +
                    "Pessoa_Id, " + _n +
                    "Nome, " + _n +
                    "Nome_Fantasia, " + _n +
                    "CnpjCpf = IsNull(Pj_Cgc, Pf_Cpf), " + _n +
                    "Email " + _n +
                 "From Pessoa " + _n +
                 "Where Tab_Status_Id = 1";

            return RepositoryHelper.Query<OvaDesovaEntity>(sql, null, CommandType.Text);
        }

        public OvaDesovaEntity GetById(int id)
        {
            var sql = "Sp_Ecr_Con_Pessoa_Template";
            var param = new { Pessoa_Id = id };

            return RepositoryHelper.QueryFirstOrDefault<OvaDesovaEntity>(sql, param, CommandType.StoredProcedure);
        }

        public int Insert(OvaDesovaEntity pessoa)
        {
            var sql = "Sp_Ecr_Inc_Pessoa_Template";
            var param = new
            {
                pessoa.Nome,
                pessoa.NomeFantasia,
                pessoa.CnpjCpf,
                pessoa.Email
            };

            return RepositoryHelper.QueryFirstOrDefault<int>(sql, param, CommandType.StoredProcedure);
        }

        public dynamic Update(OvaDesovaEntity pessoa)
        {
            var sql = "Sp_Ecr_Atu_Pessoa_Template";
            var param = new
            {
                pessoa.PessoaId,
                pessoa.Nome,
                pessoa.NomeFantasia,
                pessoa.CnpjCpf,
                pessoa.Email
            };

            return RepositoryHelper.Execute(sql, param, CommandType.StoredProcedure);
        }

        public dynamic Delete(int pessoaId)
        {
            var sql = "Update Pessoa Set Tab_Status_Id = 2 Where Pessoa_Id = @Id";
            var param = new { Id = pessoaId };

            return RepositoryHelper.Execute(sql, param, CommandType.Text);
        }
    }
}