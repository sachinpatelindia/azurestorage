using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using SKPNet.Storage.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SKPNet.Storage.Operations
{
    /// <summary>
    /// Table storage operations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TableStorage<T> : ITableStorage<T> where T : TableEntity
    {
        private readonly StorageAccountConnection _accountConnection;
        public TableStorage(StorageAccountConnection connection)
        {
            _accountConnection = connection;
        }
        public T Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public T Get(object searchPattern)
        {
            //CloudTable table = CreateTable(tableName);
            //TableQuery<TableEntity> type = new TableQuery<TableEntity>();
            //TableOperation select = TableOperation.Retrieve<T>(partitionKey, rowKey);
            //TableResult tableResult = table.ExecuteAsync(select).Result;
            //T data = tableResult.Result as T;
            return null;
        }

        /// <summary>
        /// Get all records
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tableName"></param>
        /// <param name="partitionKey"></param>
        /// <returns></returns>
        public IEnumerable<T> GetAll<T>(string tableName, string partitionKey="") where T : ITableEntity, new()
        {
            CloudTable table = CreateTable(tableName);
            var query = new TableQuery<T>();//.Where(TableQuery.GenerateFilterCondition(partitionKey, QueryComparisons.Equal, partitionKey));
            var results = table.ExecuteQuerySegmentedAsync<T>(query, null).Result;
            return results;
        }

        /// <summary>
        /// Insert an entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                CloudTable table = CreateTable(entity.GetType().Name);
                TableOperation insertOperation = TableOperation.InsertOrMerge(entity);
                var result = table.ExecuteAsync(insertOperation).Result;
                return result.Result as T;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T Update(T entity)
        {
            throw new NotImplementedException();
        }

        #region Utilites

        /// <summary>
        /// Create a table if not exist
        /// </summary>
        /// <returns></returns>
        private CloudTable CreateTable(string tableName)
        {
            CloudStorageAccount storageAccount = _accountConnection.CreateStorageAccount();
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference(tableName);

            if (!table.CreateIfNotExistsAsync().Result)
            {
                //Table created
            }

            return table;

        }
        #endregion
    }
}
