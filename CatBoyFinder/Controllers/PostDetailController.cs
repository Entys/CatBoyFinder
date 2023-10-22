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
    public class PostDetailController : ControllerBase
    {
        // GET: api/PostDetail
        [HttpGet]
        public IEnumerable<PostDetails> Get()
        {
            return new PostDetails[] { new PostDetails() };
        }

        // GET: api/PostDetail/5
        [HttpGet("{id}", Name = "GetPostDetail")]
        public PostDetails Get(int id)
        {
            return new PostDetails();
        }

        // POST: api/PostDetail
        [HttpPost]
        public void Post([FromBody] PostDetails value)
        {
        }

        // PUT: api/PostDetail/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] PostDetails value)
        {
        }

        // DELETE: api/PostDetail/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
