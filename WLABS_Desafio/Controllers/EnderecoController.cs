using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
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
            try
            {
                var aux = await _enderecoRepository.getApiCepEndereco(cep);
                Console.WriteLine(aux.ToJson());
                return Ok(aux);
            }
            catch (KeyNotFoundException ex)
            {
                return StatusCode(404, $"{ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Ocorreu um erro."); //Rever
            }
        }
    }
}
