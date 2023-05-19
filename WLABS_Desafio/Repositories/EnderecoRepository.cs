using DesafioWLABS.Models;
using MongoDB.Bson;
using System.Dynamic;
using System.Text.Json;
using WLABS_Desafio.Interfaces;
using WLABS_Desafio.Models;

namespace WLABS_Desafio.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        public async Task<ApiResponse> getApiCepEndereco(string cep)
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

        public Task<ApiResponse> getAwesomeApiEndereco(string cep)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> getViaCepEndereco(string cep)
        {
            throw new NotImplementedException();
        }
    }
}
