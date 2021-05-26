using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithAspNet5.Business;
using RestWithAspNet5.Data.VO;

namespace RestWithAspNet5.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : Controller
    {
        private ILoginBusiness _loginBusiness;

        public AuthController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }


        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] UserVO userVO)
        {
            if (userVO == null)
            {
                return BadRequest("Invalid client request");
            }

            var token = _loginBusiness.ValidateCredentials(userVO);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }


        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenVO tokenVO)
        {
            if (tokenVO == null)
            {
                return BadRequest("Invalid client request");
            }

            var token = _loginBusiness.ValidateCredentials(tokenVO);

            if (token == null)
            {
                return BadRequest("Invalid client request");
            }

            return Ok(token);
        }

        [HttpGet]
        [Route("revoke")]
        [Authorize("Bearer")]
        public IActionResult Revoke()
        {
            
            var userName = User.Identity.Name; //não precisa passar parametro, por causa do Bearer o framework consegue identificar quem está logado
            var result = _loginBusiness.RevokeToken(userName);

            if (!result)
            {
                return BadRequest("Invalid client request");
            }

            return NoContent();
        }
    }
}
