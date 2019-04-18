using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB.GenericRepository.MongoRepository
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        TEntity Insert(TEntity entity);
        ReplaceOneResult Update(TEntity entity);
        DeleteResult Delete(TEntity entity);
        IList<TEntity>
            SearchFor(Expression<Func<TEntity, bool>> predicate);
        IList<TEntity> GetAll();
        TEntity GetById(string id);
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
    }
}
