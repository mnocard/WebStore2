using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.ServiceHosting.Controllers
{
    [Route("api/values")]
    [ApiController]
    public class ValuesApiController : ControllerBase
    {
        private static readonly List<string> _Values = Enumerable
            .Range(1, 10)
            .Select(i => $"Value {i:00}")
            .ToList();

        [HttpGet]
        public IEnumerable<string> Get() => _Values;

        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            if (id < 0)
                return BadRequest();
            if (id >= _Values.Count)
                return NotFound();

            return _Values[id];
        }

        [HttpPost("add")]
        public ActionResult Post([FromBody]string value)
        {
            if (string.IsNullOrEmpty(value))
                return BadRequest();

            _Values.Add(value);
            return CreatedAtAction(nameof(Get), new { Id = _Values.Count - 1 });
        }

        [HttpPut("edit/{id}")]
        public ActionResult Put(int id, [FromBody] string value)
        {
            if (id < 0 || string.IsNullOrEmpty(value))
                return BadRequest();
            if (id >= _Values.Count)
                return NotFound();

            _Values[id] = value;
            return Ok();
        }

        [HttpDelete("remove/{id}")]
        public ActionResult Delete(int id)
        {
            if (id < 0)
                return BadRequest();
            if (id >= _Values.Count)
                return NotFound();

            _Values.RemoveAt(id);
            return Ok();
        }
    }
}
