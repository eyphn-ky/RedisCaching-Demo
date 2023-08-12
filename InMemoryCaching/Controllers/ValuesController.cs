using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCaching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly IMemoryCache _memoryCache;
        public ValuesController(IMemoryCache memoryCache)
        {
              _memoryCache = memoryCache;
        }

        [HttpGet("set/{value}")]
        public void Set(string value)
        {
            _memoryCache.Set("name",value);
        }

        [HttpGet("get")]
        public string Get()
        {
            if(_memoryCache.TryGetValue<string>("name", out string name)) //varsa al. out keywprd ile içerdeli verinin referansını aldık.
            {
                return name.Substring(3);
            }
            return "";
            //return _memoryCache.Get<string>("name");
        }
        [HttpGet("setDate")]
        public void SetDate()
        {
            _memoryCache.Set<DateTime>("date", DateTime.Now,options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            });

        }
        [HttpGet("getDate")]
        public DateTime GetDate()
        {
            return _memoryCache.Get<DateTime>("date");
        }
    }
}
