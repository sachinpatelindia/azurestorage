using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace SKPNet.Entities
{
    /// <summary>
    /// Article Entity
    /// </summary>
    public partial class Article:TableEntity
    {
        public Article()
        {

        }

        public Article(string title, string content, DateTime createDate,string createdBy)
        {
            PartitionKey = title;
            RowKey =  Guid.NewGuid().ToString();
            this.Title = title;
            this.Content = content;
            this.CreateDate = createDate;
            this.CreatedBy = createdBy;
        }

        public string Title { get; set; }
        public string Content { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
