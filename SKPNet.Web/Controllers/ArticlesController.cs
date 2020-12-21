using Microsoft.AspNetCore.Mvc;
using SKPNet.Entities;
using SKPNet.Storage.Operations;
using System.Collections.Generic;
using System;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SKPNet.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly ITableStorage<Article> _article;
        public ArticlesController(ITableStorage<Article> article)
        {
            _article = article;
        }
        // GET: api/<ArticlesController>
        [HttpGet]
        public IEnumerable<Article> Get()
        {
            return  _article.GetAll<Article>(nameof(Article), "");
            
        }

        // GET api/<ArticlesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ArticlesController>
        [HttpPost]
        public void Post([FromBody] Article article)
        {
            var initeart = new Article(article.Title,article.Content,DateTime.Now, "sachin");
            _article.Insert(initeart);
        }

        // PUT api/<ArticlesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ArticlesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
