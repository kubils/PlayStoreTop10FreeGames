using Domain;
using Domain.IRepositories;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class BaseMongoRepository
    {
        protected  IMongoDatabase MongoDatabase { get;}
        
        protected BaseMongoRepository(IMongoDbSettings mongoDbSettings)
        {
            MongoClient mongoClient= new MongoClient(mongoDbSettings.ConnectionString);
            MongoDatabase = mongoClient.GetDatabase(mongoDbSettings.DatabaseName);
        }
    }
}