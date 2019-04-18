using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.MongoRepository
{
    public class MongoDbRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {

        private IMongoDatabase database;
        private IMongoCollection<TEntity> collection;


        public MongoDbRepository()
        {
            GetDatabase();
            GetCollection();
        }
        public TEntity Insert(TEntity entity)
        {
            entity.Id = ObjectId.GenerateNewId().ToString();
            collection.InsertOne(entity);
            return entity;
        }
        public ReplaceOneResult Update(TEntity entity)
        {
            return collection.ReplaceOne(book => book.Id == entity.Id, entity);
        }
        public DeleteResult Delete(TEntity entity)
        {
            DeleteResult result = collection.DeleteOne(Builders<TEntity>.Filter.Eq("_id", entity.Id));
            return result;
        }
        public IList<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate)
        {
            return collection
                .AsQueryable<TEntity>()
                    .Where(predicate.Compile())
                        .ToList();
        }
        public TEntity Get(Expression<Func<TEntity,bool>> predicate)
        {
            return collection
               .AsQueryable<TEntity>()
                   .FirstOrDefault(predicate);
        }
        public TEntity GetById(string id)
        {
            return collection
               .AsQueryable<TEntity>()
                   .SingleOrDefault(x => x.Id == id);
        }
        public IList<TEntity> GetAll()
        {
            return collection.Find(x => true).ToList();
        }
        #region Private Helper Methods
        private void GetDatabase()
        {
            var client = new MongoClient(GetConnectionString());
            database = client.GetDatabase("EmailDB");

        }
        private string GetConnectionString()
        {
            return MongoDbConnectionString.cstr;
        }
        private string GetDatabaseName()
        {
            return "EmailDB";
        }
        private void GetCollection()
        {
            collection = database
                .GetCollection<TEntity>(typeof(TEntity).Name);
        }




        #endregion
    }
}
