using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatBoyCommon;
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
            return CatBoyContext.instance.Posts.GetAll();
        }

        // GET: api/Post/5
        [HttpGet("{id}", Name = "GetPost")]
        public Post Get(int id)
        {
            return CatBoyContext.instance.Posts.GetById(id);
        }

        // POST: api/Post
        [HttpPost]
        public void Post([FromBody] Post value)
        {
            CatBoyContext.instance.Posts.Add(ref value);
        }

        // PUT: api/Post/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Post value)
        {
            value.PostId = id;
            CatBoyContext.instance.Posts.Update(value);
        }

        // DELETE: api/Post/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            CatBoyContext.instance.Posts.Delete(id);
        }
    }
}
