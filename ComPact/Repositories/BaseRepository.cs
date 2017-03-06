﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SQLite.Net.Async;

namespace ComPact
{
	public abstract class BaseRepository<TEntity, TKey> where TEntity : class, new()
	{
		private readonly SQLiteAsyncConnection _connection;

		protected BaseRepository()
		{
			//_connection = //Initialize your connection here

			//_connection.CreateTableAsync<TEntity>().Wait();
		}

		public virtual IEnumerable<TEntity> All()
		{
			return _connection.Table<TEntity>().ToListAsync().Result;
		}

		public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> filter)
		{
			return _connection.Table<TEntity>().ToListAsync().Result.AsQueryable().Where((filter ?? (e => true)));
		}

		public virtual int Count()
		{
			return _connection.Table<TEntity>().CountAsync().Result;
		}

		public virtual async Task<TEntity> Get(TKey key)
		{
			return await _connection.GetAsync<TEntity>(key);
		}

		public virtual async Task InsertOrReplace(TEntity entity)
		{
			await _connection.InsertOrReplaceAsync(entity);
		}

		public virtual async Task InsertOrReplace(IEnumerable<TEntity> entities)
		{
			await _connection.InsertOrReplaceAllAsync(entities);
		}

		public virtual async Task<TEntity> Insert(TEntity entity)
		{
			await _connection.InsertAsync(entity);
			return All().LastOrDefault();
		}

		public virtual async Task Insert(IEnumerable<TEntity> entities)
		{
			await _connection.InsertAllAsync(entities);
		}

		public virtual async Task Update(TEntity entity)
		{
			await _connection.UpdateAsync(entity);
		}

		public virtual async Task Update(IEnumerable<TEntity> entities)
		{
			await _connection.UpdateAllAsync(entities);
		}

		public virtual async Task Delete(TEntity entity)
		{
			await _connection.DeleteAsync(entity);
		}

		public virtual async Task Delete(IEnumerable<TEntity> entities)
		{
			foreach (var entity in entities)
			{
				await Delete(entity);
			}
		}

		public virtual async Task Delete(TKey key)
		{
			await _connection.DeleteAsync(await Get(key));
		}

		public virtual async Task Clear()
		{
			await _connection.DeleteAllAsync<TEntity>();
		}

		public virtual async Task Truncate()
		{
			await _connection.DropTableAsync<TEntity>();
			await _connection.CreateTableAsync<TEntity>();
		}
	}
}