using DesafioWLABS.Models;
using MongoDB.Bson;
using System.Dynamic;
using System.Net;
using System.Text.Json;
using System.Web.Helpers;
using WLABS_Desafio.Interfaces;
using WLABS_Desafio.Models;

namespace WLABS_Desafio.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        public async Task<string> getApiCepEndereco(string cep)
        {
            try
            {
                var formatedCep = FormatarCEP(cep);
                var viaCepUri = $"https://viacep.com.br/ws/{cep}/json/";
                var apiCepUri = $"https://cdn.apicep.com/file/apicep/{formatedCep}.json";
                var awesomeApiCepUri = $"https://cep.awesomeapi.com.br/json/{cep}";

                var request = new HttpRequestMessage(HttpMethod.Get, viaCepUri);

                string jsonString = "";
                using (var client = new HttpClient())
                {
                    var responseBrasilApi = await client.SendAsync(request);
                    var contentResp = await responseBrasilApi.Content.ReadAsStringAsync();
                    var objResponse = JsonSerializer.Deserialize<EnderecoViaCEP>(contentResp);

                    var data = new
                    {
                        code = responseBrasilApi.StatusCode,
                        data = objResponse
                    };

                    // Serializar o objeto em uma string JSON
                    jsonString = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });

                    Console.WriteLine(jsonString);
                }

                return jsonString;               
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<ApiResponse> getAwesomeApiEndereco(string cep)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://viacep.com.br/ws/{cep}/json/");

                var response = new ApiResponse();
                using (var client = new HttpClient())
                {
                    var responseBrasilApi = await client.SendAsync(request);
                    var contentResp = await responseBrasilApi.Content.ReadAsStringAsync();
                    var objResponse = JsonSerializer.Deserialize<EnderecoViaCEP>(contentResp);

                    response.StatusCode = responseBrasilApi.StatusCode;
                    response.Data = objResponse.ToJson();

                }
                Console.WriteLine(response.ToJson());

                return response;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public Task<ApiResponse> getViaCepEndereco(string cep)
        {
            throw new NotImplementedException();
        }

        public string FormatarCEP(string cep)
        {
            if (cep.Length == 8)
            {
                return cep.Insert(5, "-");
            }
            else
            {
                // Se o CEP não tiver 8 caracteres, pode-se optar por retornar o CEP original ou lançar uma exceção, dependendo dos requisitos do seu programa.
                return cep;
            }
        }

    }
}
