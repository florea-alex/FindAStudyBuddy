using Microsoft.AspNetCore.Mvc;
using ProiectMDS.Services.EmailService;

namespace ProiectMDS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IEmailServices _emailServices;

        public AuthController(IEmailServices emailServices)
        {
            _emailServices = emailServices;
        }
    }
}
