using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Core.BusinesLogic;
using Sat.Recruitment.Api.Core.Exceptions;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Request = Sat.Recruitment.Api.Core.Dto.CreateUserRequest;
using Response = Sat.Recruitment.Api.Core.Dto.CreateUserResponse;

namespace Sat.Recruitment.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly UserLogic _userLogic;
        public UsersController(UserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        [HttpPost]
        [Route("/create-user")]
        public async Task<Response> CreateUser(Request request)
        {
            string msg = "";
            try
            {
                await _userLogic.CreateUser(request);

                msg = "User Created";
                Debug.WriteLine(msg);
                return new Response()
                {
                    IsSuccess = true,
                    Errors = msg
                };
            }
            catch (DataUserException ex)
            {
                msg = $"Incorrect data user:  {ex.Message}";
            }
            catch (DuplucateUserException)
            {
                msg = "The user is duplicated";
            }
            catch (Exception)
            {
                msg = "Unexpected Error Creating User";
            }

            Debug.WriteLine(msg);
            return new Response()
            {
                IsSuccess = false,
                Errors = msg
            };

        }
    }
}
