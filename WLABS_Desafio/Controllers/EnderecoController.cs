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

        /**
         * Obtém o endereço correspondente a um determinado CEP.
         *
         * @param cep CEP para o qual se deseja obter o endereço.
         * @return Uma lista de strings contendo a resposta da requisição em formato JSON.
        */
        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<string>>> searchByKey(string cep)
        {
            // Validar o CEP informado
            if (!ValidarCEP(cep)) return StatusCode(404, $"CEP Inválido");

            try
            {
                // Fazer a requisição para obter o endereço
                var res = await _enderecoRepository.makeRequest(cep);

                return Ok(res);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Não foi possivel realizar a solicitação.");
            }
        }

        /**
         * Valida se um CEP é válido.
         *
         * @param cep CEP a ser validado.
         * @return True se o CEP for válido, False caso contrário.
        */
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
