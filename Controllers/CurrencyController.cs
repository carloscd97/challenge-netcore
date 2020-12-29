using System.Threading.Tasks;
using BcpChallenge.Services;
using BcpChallenge.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BcpChallenge.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {

        private readonly IChangeService _changeService;  
  
        public CurrencyController(IChangeService changeService)  
        {  
            _changeService = changeService;  
        }

        [HttpGet]
        public IActionResult Get()
        {
            var change = _changeService.GetAll();
            return Ok(change);
        }

        [HttpGet("change")]
        public IActionResult Change(CurrencyExchangeViewModel currencyExchangeViewModel)
        {
            var change = _changeService.CurrencyExchange(currencyExchangeViewModel);
            return Ok(change);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CurrencyViewModel currencyViewModel)
        {
            _changeService.Update(currencyViewModel);
            return Ok("Se actualizó correctamente");
        }

    }
}
