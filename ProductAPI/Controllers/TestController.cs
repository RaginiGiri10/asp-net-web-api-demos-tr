using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ProductAPI.Controllers
{
    public class TestController : ApiController
    {

        [Route("api/strings")]
        public IEnumerable<string> Get()
        {
            return new string[] { "Test1", "Test2" };
        }
    }
}