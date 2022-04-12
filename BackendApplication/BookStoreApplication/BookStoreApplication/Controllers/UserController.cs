using CommonLayer.UserModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Interfaces;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRL userRL;

        public UserController(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        [HttpPost("signup")]
        public IActionResult UserSignup(UserEntities model)
        {
            try
            {
                if (model == null)
                {
                    return NotFound(new { Success = false, message = "Details Missing" });
                }
                SignupResponseModel user = userRL.UserSignup(model);
                return Ok(new { Success = true, message = "Signup Successfull ", user });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginModel model)
        {
            try
            {
                LoginResponseModel credentials = userRL.Login(model);
                if (credentials != null)
                {
                    return Ok(new { Success = true, message = "Login Successful", credentials });
                }
                return NotFound(new { Success = false, message = "Email or Password Not Found" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
