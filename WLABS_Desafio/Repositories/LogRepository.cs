using MongoDB.Driver;
using WLABS_Desafio.Interfaces;
using WLABS_Desafio.Models;
using System.Collections;

namespace WLABS_Desafio.Repositories
{
    public class LogRepository : ILogRepository
    {
        private static IMongoClient c = new MongoClient("mongodb://localhost:27017/");
        private static IMongoDatabase db = c.GetDatabase("DesafioWLABS");
        private static IMongoCollection<ApiLog> collection = db.GetCollection<ApiLog>("Log's");

        /**
        * Cria um novo registro de log na coleção do MongoDB.
        *
        * @param log O objeto ApiLog a ser inserido.
        * @return Uma tarefa que representa a operação assíncrona. O resultado da tarefa é o objeto ApiLog inserido.
        * @exception Exception Se ocorrer algum erro durante a inserção do registro de log.
        */
        public async Task<ApiLog> createlog(ApiLog log)
        {
            try
            {
                await collection.InsertOneAsync(log);

                var aux = await collection.Find(x => x.date == log.date).FirstOrDefaultAsync();

                return aux;

            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
