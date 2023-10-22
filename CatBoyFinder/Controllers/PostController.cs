using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatBoyCommon.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatBoyFinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        // GET: api/Post
        [HttpGet]
        public IEnumerable<Post> Get()
        {
            return new Post[] { new Post() };
        }

        // GET: api/Post/5
        [HttpGet("{id}", Name = "GetPost")]
        public Post Get(int id)
        {
            return new Post();
        }

        // POST: api/Post
        [HttpPost]
        public void Post([FromBody] Post value)
        {
        }

        // PUT: api/Post/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Post value)
        {
        }

        // DELETE: api/Post/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
