using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace SKPNet.Entities
{
    public class Member:TableEntity
    {
        public Member()
        {

        }
        public Member(string email, string MobileNumber, DateTime createDate,string password, string passwordSalt)
        {
            PartitionKey = email;
            RowKey = Guid.NewGuid().ToString();
            this.Email = email;
            this.MobileNumber = MobileNumber;
            this.CreateDate = createDate;
            this.Password = password;
            this.PasswordSalt = passwordSalt;
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string MobileNumber { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
