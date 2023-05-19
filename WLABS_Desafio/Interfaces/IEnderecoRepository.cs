using DesafioWLABS.Models;
using WLABS_Desafio.Models;

namespace WLABS_Desafio.Interfaces
{
    public interface IEnderecoRepository
    {
        public Task<ApiResponse> getViaCepEndereco(string cep);
        public Task<ApiResponse> getApiCepEndereco(string cep);
        public Task<ApiResponse> getAwesomeApiEndereco(string cep);
    }
}
