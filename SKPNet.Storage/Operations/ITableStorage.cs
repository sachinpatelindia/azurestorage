using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;

namespace SKPNet.Storage.Operations
{
    public partial interface ITableStorage<T> where T:TableEntity
    {
        T Insert(T entity);
        T Update(T entity);
        T Delete(T entity);
        T Get(object searchPattern);
        // IEnumerable<T> GetAll(string tableName,string partitionKey="",string rowKey="");
        IEnumerable<T> GetAll<T>(string tableReference, string partitionKey)
      where T : ITableEntity, new();
    }
}
