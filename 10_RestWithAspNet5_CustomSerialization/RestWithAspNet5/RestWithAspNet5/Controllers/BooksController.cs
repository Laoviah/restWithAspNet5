using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithAspNet5.Business;
using RestWithAspNet5.Data.VO;
using RestWithAspNet5.Model;
using Serilog;

namespace RestWithAspNet5.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BooksController : Controller
    {
        private readonly ILogger<BooksController> _logger;
        private IBooksBusiness _bookBusiness;

        public BooksController(ILogger<BooksController> logger, IBooksBusiness bookBusiness)
        {
            _logger = logger;
            _bookBusiness = bookBusiness;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var person = _bookBusiness.FindById(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpPost]
        public IActionResult Post([FromBody] BookVO bookVO)
        {
            if (bookVO == null)
            {
                return NotFound();
            }

            return Ok(_bookBusiness.Create(bookVO));
        }

        [HttpPut]
        public IActionResult Put([FromBody] BookVO bookVO)
        {
            if (bookVO == null)
            {
                return NotFound();
            }

            return Ok(_bookBusiness.Update(bookVO));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _bookBusiness.Delete(id);


            return NoContent();
        }
    }
}
