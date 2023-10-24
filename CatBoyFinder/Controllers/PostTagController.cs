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
    public class PostTagController : ControllerBase
    {
        // GET: api/PostTag
        [HttpGet]
        public IEnumerable<PostTag> Get()
        {
            return CatBoyContext.instance.PostTags.GetAll();
        }

        // GET: api/PostTag/5
        [HttpGet("{id}", Name = "GetPostTag")]
        public PostTag Get(int id)
        {
            return CatBoyContext.instance.PostTags.GetById(id);
        }

        // POST: api/PostTag
        [HttpPost]
        public void Post([FromBody] PostTag value)
        {
            CatBoyContext.instance.PostTags.Add(ref value);
        }

        // PUT: api/PostTag/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] PostTag value)
        {
            value.Id = id;
            CatBoyContext.instance.PostTags.Update(value);
        }

        // DELETE: api/PostTag/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            CatBoyContext.instance.PostTags.Delete(id);
        }
    }
}
