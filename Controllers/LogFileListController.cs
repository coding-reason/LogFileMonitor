using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogFileMonitor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogFileListController : ControllerBase
    {
        // TODO add a way to update the logfile list dynamically And possible to save new configuration.
        // GET: api/LogFileList
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/LogFileList/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/LogFileList
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/LogFileList/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
