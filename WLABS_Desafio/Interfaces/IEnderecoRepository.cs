using DesafioWLABS.Models;
using WLABS_Desafio.Models;

namespace WLABS_Desafio.Interfaces
{
    /**
     * Interface para o EderecoRepository
    */
    public interface IEnderecoRepository
    {
        public Task<string> makeRequest(string cep);
        public Task<string> getViaCepEndereco(string cep);
        public Task<string> getApiCepEndereco(string cep);
        public Task<string> getAwesomeApiEndereco(string cep);
    }
}
