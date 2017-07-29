using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Thinktecture.IdentityModel.Mvc;

namespace Localink.UserCenter.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        
        [ResourceAuthorize("Read", "Values")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [ResourceAuthorize("Read", "Values")]
        public string Get(int id)
        {
            return "value";
        }

        [ResourceAuthorize("Write", "Values")]
        public void Post([FromBody]string value)
        {
        }

        [ResourceAuthorize("Write", "Values")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [ResourceAuthorize("Write", "Values")]
        public void Delete(int id)
        {
        }
    }
}
