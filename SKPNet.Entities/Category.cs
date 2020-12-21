using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace SKPNet.Entities
{
    public class Category:TableEntity
    {
        public Category()
        {

        }
        public Category(string title,string name,string shortDescription,string fullDescription,DateTime createdOn,bool isActive, bool isDeleted )
        {
            PartitionKey = name;
            RowKey = Guid.NewGuid().ToString();
            this.Title = title;
            this.Name = name;
            this.ShortDescription = shortDescription;
            this.FullDescription = fullDescription;
            this.CreatedOn = createdOn;
            this.IsActive = isActive;
            this.IsDeleted = isDeleted;
        }

        public string Title { get;  set; }
        public string Name { get;  set; }
        public string ShortDescription { get;  set; }
        public string FullDescription { get;  set; }
        public DateTime CreatedOn { get;  set; }
        public bool IsActive { get;  set; }
        public bool IsDeleted { get;  set; }
    }
}
