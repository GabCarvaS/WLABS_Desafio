using DesafioWLABS.Models;
using MongoDB.Bson;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Web.Helpers;
using WLABS_Desafio.Interfaces;
using WLABS_Desafio.Models;

namespace WLABS_Desafio.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        public async Task<string> makeRequest(string cep)
        {
            try
            {
                string resposta = null;

                // Tentar a primeira API
                resposta = await getViaCepEndereco(cep);

                // Se a primeira API não responder, tentar a segunda API
                if (resposta == null)
                    resposta = await getApiCepEndereco(cep);

                // Se a segunda API não responder, tentar a terceira API
                if (resposta == null)
                    resposta = await getAwesomeApiEndereco(cep);

                // Retornar a resposta para o controller
                return resposta;
            }
            catch()
            {
                throw;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> getViaCepEndereco(string cep)
        {
            try
            {
                var viaCepUri = $"https://viacep.com.br/ws/{cep}/json/"; 

                var request = new HttpRequestMessage(HttpMethod.Get, viaCepUri);

                string jsonString = "";
                using (var client = new HttpClient())
                {
                    var responseBrasilApi = await client.SendAsync(request);
                    var contentResp = await responseBrasilApi.Content.ReadAsStringAsync();
                    var objResponse = JsonSerializer.Deserialize<EnderecoViaCEP>(contentResp);

                    if (responseBrasilApi.StatusCode != HttpStatusCode.OK || objResponse.cep == null)
                    {
                        var data = new
                        {
                            code = 404,
                            data = objResponse
                        };
                        // Serializar o objeto em uma string JSON
                        jsonString = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                    }
                    else
                    {
                        var data = new
                        {
                            code = responseBrasilApi.StatusCode,
                            data = objResponse
                        };
                        // Serializar o objeto em uma string JSON
                        jsonString = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                    }
                }
                return jsonString;               
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<string> getAwesomeApiEndereco(string cep)
        {
            try
            {
                var awesomeApiCepUri = $"https://cep.awesomeapi.com.br/json/{cep}";

                var request = new HttpRequestMessage(HttpMethod.Get, awesomeApiCepUri);

                string jsonString = "";
                using (var client = new HttpClient())
                {
                    var responseBrasilApi = await client.SendAsync(request);
                    var contentResp = await responseBrasilApi.Content.ReadAsStringAsync();
                    var objResponse = JsonSerializer.Deserialize<EnderecoAwesomeApi>(contentResp);

                    if (responseBrasilApi.StatusCode != HttpStatusCode.OK || objResponse.cep == null)
                    {
                        throw new Exception("erro");
                    }

                    var data = new
                    {
                        code = responseBrasilApi.StatusCode,
                        data = objResponse
                    };

                    // Serializar o objeto em uma string JSON
                    jsonString = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });

                   // Console.WriteLine(jsonString);
                }

                return jsonString;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<string> getApiCepEndereco(string cep)
        {
            try
            {
                var formatedCep = FormatarCEP(cep);
                var apiCepUri = $"https://cdn.apicep.com/file/apicep/{formatedCep}.json";

                var request = new HttpRequestMessage(HttpMethod.Get, apiCepUri);

                string jsonString = "";
                using (var client = new HttpClient())
                {
                    var responseBrasilApi = await client.SendAsync(request);
                    var contentResp = await responseBrasilApi.Content.ReadAsStringAsync();
                    var objResponse = JsonSerializer.Deserialize<EnderecoApiCep>(contentResp);

                    if (responseBrasilApi.StatusCode != HttpStatusCode.OK || objResponse.cep == null)
                    {
                        throw new Exception("erro");
                    }

                    var data = new
                    {
                        code = responseBrasilApi.StatusCode,
                        data = objResponse
                    };

                    // Serializar o objeto em uma string JSON
                    jsonString = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });

                    //Console.WriteLine(jsonString);
                }

                return jsonString;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private string FormatarCEP(string cep)
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
