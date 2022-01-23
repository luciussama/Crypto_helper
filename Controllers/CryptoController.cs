using LT.Dominio.Infra;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crypto_helper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        [HttpGet]
        public ActionResult ValidarCriptografia(string chave, string dadoEntrada, bool criptografar = false, bool descriptografar = false)
        {
            try
            {
                if (criptografar)
                    return Ok(CriptografiaDados.Criptografar(chave, dadoEntrada));
                if (descriptografar)
                    return Ok(CriptografiaDados.Descriptografar(chave, dadoEntrada));
                if (criptografar && descriptografar)
                    return BadRequest("Escolha apenas uma das opções");
                else
                return BadRequest("Deve ser escolhido se é para criptografar ou descriptografar");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
