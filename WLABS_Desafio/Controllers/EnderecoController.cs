using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.X86;
using WLABS_Desafio.Interfaces;
using WLABS_Desafio.Models;
using WLABS_Desafio.Repositories;

namespace WLABS_Desafio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoRepository _enderecoRepository = new EnderecoRepository();

        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<string>>> searchByKey(string cep)
        {
            if(!ValidarCEP(cep)) return StatusCode(404, $"CEP Inválido");

            try
            {
                var res = await _enderecoRepository.makeRequest(cep);               
                 
                return Ok(res);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Não foi possivel realizar a solicitação.");
            }
        }
        private bool ValidarCEP(string cep)
        {
            // Remover caracteres não numéricos do CEP
            cep = new string(cep.Where(char.IsDigit).ToArray());

            // Verificar se o CEP possui 8 dígitos
            if (cep.Length != 8)
            {
                return false;
            }

            return true;
        }
    }
}
