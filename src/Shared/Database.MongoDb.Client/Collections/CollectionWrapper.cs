using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Database.MongoDb.Client.Collections
{
	public interface ICollectionWrapper<T>
	{
		IQueryable<T> AsLinqQueryable(IMongoCollection<T> mongoCollection);
		IMongoQueryable<T> AsQueryable(IMongoCollection<T> mongoCollection);
		IMongoQueryable<T> AsQueryable(IMongoCollection<T> mongoCollection, Expression<Func<T, bool>> predicate);
	}

	[ExcludeFromCodeCoverage]
	public class CollectionWrapper<T> : ICollectionWrapper<T>
	{
		public IQueryable<T> AsLinqQueryable(IMongoCollection<T> mongoCollection)
		{
			return AsQueryable(mongoCollection);
		}

		public IMongoQueryable<T> AsQueryable(IMongoCollection<T> mongoCollection)
		{
			return mongoCollection.AsQueryable();
		}

		public IMongoQueryable<T> AsQueryable(IMongoCollection<T> mongoCollection, Expression<Func<T, bool>> predicate)
		{
			return mongoCollection.AsQueryable().Where(predicate);
		}
	}
}