namespace Signa.OvaDesova.Api.Domain.Entities
{
    public class OvaDesovaEntity
    {
        public int PessoaId { get; }
        public string Nome { get; }
        public string NomeFantasia { get; }
        public string CnpjCpf { get; }
        public string Email { get; }

        public OvaDesovaEntity()
        {

        }

        public OvaDesovaEntity(int pessoaId, string nome, string nomeFantasia, string cnpjCpf, string email)
        {
            PessoaId = pessoaId;
            Nome = nome;
            NomeFantasia = nomeFantasia;
            CnpjCpf = cnpjCpf;
            Email = email;
        }
    }
}