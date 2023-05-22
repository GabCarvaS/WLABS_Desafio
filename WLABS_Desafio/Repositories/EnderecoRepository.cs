using DesafioWLABS.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;
using WLABS_Desafio.Interfaces;
using WLABS_Desafio.Models;

namespace WLABS_Desafio.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        LogRepository _repository = new LogRepository();

        /**
         * Centraliza a lógica dos requests
         *
         * @param cep CEP para o qual se deseja obter o endereço.
         * @return Uma string contendo a resposta da requisição em formato JSON.
         */
        public async Task<string> makeRequest(string cep)
        {
            string resposta = "";

            try
            {
                while (string.IsNullOrEmpty(resposta))
                {
                    // Tentar a primeira API
                    try
                    {
                        resposta = await getViaCepEndereco(cep);
                    }
                    catch (Exception ex)
                    {
                        // Tratar o caso de falha na requisição, quando o site estiver offline ou inacessível
                        ApiLog log = new()
                        {
                            date = DateTime.Now,
                            message = "Erro -> " + ex.Message
                        };
                        await _repository.createlog(log);
                    }

                    // Se a primeira API não responder corretamente, tentar a segunda API
                    if (string.IsNullOrEmpty(resposta))
                    {
                        try
                        {
                            resposta = await getApiCepEndereco(cep);
                        }
                        catch (Exception ex)
                        {
                            // Registrar a exceção no log
                            ApiLog log = new()
                            {
                                date = DateTime.Now,
                                message = "Erro -> " + ex.Message
                            };
                            await _repository.createlog(log);
                        }
                    }

                    // Se a segunda API não responder corretamente, tentar a terceira API
                    if (string.IsNullOrEmpty(resposta))
                    {
                        try
                        {
                            resposta = await getAwesomeApiEndereco(cep);
                        }
                        catch (Exception ex)
                        {
                            // Registrar a exceção no log
                            ApiLog log = new()
                            {
                                date = DateTime.Now,
                                message = "Erro -> " + ex.Message
                            };
                            await _repository.createlog(log);
                        }
                    }

                    // Se todas as tentativas falharem
                    if (string.IsNullOrEmpty(resposta))
                    {
                        resposta = $"Nenhum dado encontrado para o cep {cep}";
                    }
                }
                // Retornar a resposta para o controller              
                return resposta;
            }
            // Exceção não esperada
            catch (Exception ex)
            {
                // Registrar a exceção no log
                ApiLog log = new()
                {
                    date = DateTime.Now,
                    message = "Erro ao processar a requisição: " + ex.Message
                };

                await _repository.createlog(log);

                resposta = "Erro ao processar a requisição";

                // Retornar a resposta para o controller              
                return resposta;
            }
        }


        /**
         * Faz uma requisição à primeira API de endereços (ViaCEP) com base no CEP fornecido.
         *
         * @param cep CEP para o qual se deseja obter o endereço.
         * @return Uma string contendo a resposta da requisição em formato JSON.
         */
        public async Task<string> getViaCepEndereco(string cep)
        {
            try
            {
                var url = $"https://viacep.com.br/ws/{cep}/json/";

                string jsonString = "";
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("utf-8"));

                    var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, url));
                    if (response.IsSuccessStatusCode)
                    {
                        var responseApi = await response.Content.ReadFromJsonAsync<EnderecoViaCEP>();
                        if (responseApi == null || responseApi.cep != null)
                        {
                            // Serializar o objeto em uma string JSON
                            jsonString = JsonSerializer.Serialize(responseApi, new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
                        }
                    }
                }
                return jsonString;
            }
            catch (Exception e)
            {
                throw new Exception($"func: getViaCepEndereco | msg: {e.Message}");
            }
        }

        /**
         * Faz uma requisição à segunda API de endereços (ApiCEP) com base no CEP fornecido.
         *
         * @param cep CEP para o qual se deseja obter o endereço.
         * @return Uma string contendo a resposta da requisição em formato JSON.
         */
        public async Task<string> getApiCepEndereco(string cep)
        {
            try
            {
                var formatedCep = FormatarCEP(cep);

                var url = $"https://cdn.apicep.com/file/apicep/{formatedCep}.json";

                string jsonString = "";
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("utf-8"));

                    var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, url));
                    if (response.IsSuccessStatusCode)
                    {
                        var responseApi = await response.Content.ReadFromJsonAsync<EnderecoApiCep>();
                        if (responseApi == null || responseApi.Code != null)
                        {
                            // Serializar o objeto em uma string JSON
                            jsonString = JsonSerializer.Serialize(responseApi, new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
                        }
                    };
                }
                return jsonString;
            }
            catch (Exception e)
            {
                throw new Exception($"func: getApiCepEndereco | msg: {e.Message}");
            }
        }

        /**
         * Faz uma requisição à terceira API de endereços (AwesomeApi) com base no CEP fornecido.
         *
         * @param cep CEP para o qual se deseja obter o endereço.
         * @return Uma string contendo a resposta da requisição em formato JSON.
         */
        public async Task<string> getAwesomeApiEndereco(string cep)
        {
            try
            {
                var url = $"https://cep.awesomeapi.com.br/json/{cep}";

                string jsonString = "";
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("utf-8"));

                    var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, url));
                    if (response.IsSuccessStatusCode)
                    {
                        var responseApi = await response.Content.ReadFromJsonAsync<EnderecoAwesomeApi>();
                        if (responseApi == null || responseApi.Cep != null)
                        {
                            // Serializar o objeto em uma string JSON
                            jsonString = JsonSerializer.Serialize(responseApi, new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
                        }
                    }
                }
                return jsonString;
            }
            catch (Exception e)
            {
                throw new Exception($"func: getAwesomeApiEndereco | msg: {e.Message}");
            }
        }

        /**
         * Formata o CEP com o formato correto.
         *
         * @param cep CEP a ser formatado.
         * @return O CEP formatado.
         */
        private string FormatarCEP(string cep)
        {
            if (cep.Length == 8)
            {
                // Formato xxxxx-xxx
                return cep.Insert(5, "-");
            }
            else
            {
                // Se o CEP não tiver 8 caracteres, retorna o CEP original
                return cep;
            }
        }
    }
}
