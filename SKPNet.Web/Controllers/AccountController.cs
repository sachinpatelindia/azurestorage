using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SKPNet.Entities;
using SKPNet.Security.DataValidation;
using SKPNet.Storage.Operations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SKPNet.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITableStorage<Member> _memberStorage;
        private readonly IEncryptor _encryptor;
        public AccountController(ITableStorage<Member> memberStorage, IEncryptor encryptor)
        {
            _memberStorage = memberStorage;
            _encryptor = encryptor;
        }
        // GET: api/<AccountController>
        [HttpGet]
        public IEnumerable<Member> Get()
        {
            string password = "Sachin@123";
            var saltKey = _encryptor.CreateSaltKey(16);
            string newPassword = _encryptor.CreatePasswordHash(password,saltKey,"SHA1");
            _memberStorage.Insert(new Member("skp@example.com","87908666",DateTime.Now,newPassword,saltKey));
            return _memberStorage.GetAll<Member>(nameof(Member), "");
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AccountController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
