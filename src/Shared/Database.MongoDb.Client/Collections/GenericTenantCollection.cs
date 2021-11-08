using System;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Database.MongoDb.Client.Collections
{
	public interface IGenericTenantCollection<T> where T : class
	{
		IMongoCollection<T> GetCollection(bool write);
		IQueryable<T> GetQueryable();
		IMongoQueryable<T> GetFiltered(Expression<Func<T, bool>> predicate);
		void DropCollection();
	}

	public class GenericTenantCollection<T> : IGenericTenantCollection<T> where T : class
	{
		protected readonly IMongoDatabaseFactory MongoDatabaseFactory;
		private readonly ICollectionWrapper<T> _collectionWrapper;
		private readonly string _collectionName;

		public GenericTenantCollection(IMongoDatabaseFactory mongoDatabaseFactory, ICollectionWrapper<T> collectionWrapper, string collectionName)
		{
			MongoDatabaseFactory = mongoDatabaseFactory;
			_collectionWrapper = collectionWrapper;
			_collectionName = collectionName;
		}

		public void DropCollection()
		{
			var database = MongoDatabaseFactory.GetTenantDatabase();
			database.DropCollection(_collectionName);
		}

		public IMongoCollection<T> GetCollection(bool write)
		{
			var database = write ? MongoDatabaseFactory.GetTenantDatabase() : MongoDatabaseFactory.GetTenantDatabase();
			return database.GetCollection<T>(_collectionName);
		}

		public IQueryable<T> GetQueryable()
		{
			var collection = GetCollection(false);

			return _collectionWrapper.AsLinqQueryable(collection);
		}

		public IMongoQueryable<T> GetFiltered(Expression<Func<T, bool>> predicate)
		{
			var collection = GetCollection(false);
			
			return _collectionWrapper.AsQueryable(collection, predicate);
		}
	}

}