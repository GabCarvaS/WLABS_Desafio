using DesafioWLABS.Models;
using WLABS_Desafio.Models;

namespace WLABS_Desafio.Interfaces
{
    /**
     * Interface para o LogRepository
    */
    public interface ILogRepository
    {
        public Task<ApiLog> createlog(ApiLog log);
    }
}
