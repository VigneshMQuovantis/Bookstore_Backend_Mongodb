using CommonLayer.AddressModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Interfaces;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRL addressRL;

        public AddressController(IAddressRL addressRL)
        {
            this.addressRL = addressRL;
        }


        [Authorize]
        [HttpPut]
        public IActionResult AddAddress(AddAddressModel model)
        {
            try
            {
                string jwtUserId = User.Claims.FirstOrDefault(e => e.Type == "userId").Value;
                AddressResponseModel address = addressRL.AddAddress(model, jwtUserId);
                if (address != null)
                {
                    return Ok(new { Success = true, message = "Address Added Successfully", address });
                }
                return NotFound(new { Success = false, message = "Not able to add adress since typeId is wrong" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
