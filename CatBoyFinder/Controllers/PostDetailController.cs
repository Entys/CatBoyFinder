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
    public class PostDetailController : ControllerBase
    {
        // GET: api/PostDetail
        [HttpGet]
        public IEnumerable<PostDetails> Get()
        {
            return CatBoyContext.instance.PostDetails.GetAll();
        }

        // GET: api/PostDetail/5
        [HttpGet("{id}", Name = "GetPostDetail")]
        public PostDetails Get(int id)
        {
            return CatBoyContext.instance.PostDetails.GetById(id);
        }

        // POST: api/PostDetail
        [HttpPost]
        public void Post([FromBody] PostDetails value)
        {
            CatBoyContext.instance.PostDetails.Add(ref value);
        }

        // PUT: api/PostDetail/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] PostDetails value)
        {
            value.PostId = id;
            CatBoyContext.instance.PostDetails.Update(value);
        }

        // DELETE: api/PostDetail/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            CatBoyContext.instance.PostDetails.Delete(id);
        }
    }
}
