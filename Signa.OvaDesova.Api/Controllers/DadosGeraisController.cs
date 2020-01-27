using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Signa.OvaDesova.Api.Business;
using Signa.OvaDesova.Api.Domain.Models;

namespace Signa.OvaDesova.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Authorize("Bearer")]
    [AllowAnonymous]
    public class DadosGeraisController : Controller
    {
        private readonly DadosGeraisBL _dadosGeraisBLL;

        public DadosGeraisController(DadosGeraisBL dadosGeraisBL)
        {
            _dadosGeraisBLL = dadosGeraisBL;
        }

        [HttpGet]
        [Route("DadosGerais")]
        public ActionResult ConsultarDadosGerais(int tabelaPrecoFornecedorId) => Ok(_dadosGeraisBLL.ConsultarDadosGerais(tabelaPrecoFornecedorId));

        [HttpPost]
        [Route("DadosGerais")]
        public ActionResult Save(DadosGeraisModel dadosGerais) => Ok(_dadosGeraisBLL.Save(dadosGerais));

        [HttpGet]
        [Route("DadosGerais/Historico")]
        public ActionResult ConsultarHistorico(int tabelaPrecoFornecedorId) => Ok(_dadosGeraisBLL.ConsultarHistorico(tabelaPrecoFornecedorId));
    }
}