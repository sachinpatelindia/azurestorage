using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace SKPNet.Storage.Common
{
    /// <summary>
    /// Create table and connection
    /// </summary>
    public class StorageAccountConnection
    {
        private readonly string _storageAccountConnectionString;
        private CloudStorageAccount cloudStorageAccount;
        public StorageAccountConnection(string storageAccountConnectionString)
        {
            _storageAccountConnectionString = storageAccountConnectionString;
        }

        /// <summary>
        /// Create storage account
        /// </summary>
        /// <returns></returns>
        public CloudStorageAccount CreateStorageAccount()
        {
            try
            {
                cloudStorageAccount = CloudStorageAccount.Parse(_storageAccountConnectionString);
            }
            catch(FormatException ex)
            {
                //Logging info
            }

            return cloudStorageAccount;
        }
    }
}
