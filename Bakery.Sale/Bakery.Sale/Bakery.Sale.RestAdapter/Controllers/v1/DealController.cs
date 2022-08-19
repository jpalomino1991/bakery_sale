using Bakery.Commons.Bakery.Commons.Domain.Model;
using Bakery.Commons.Bakery.Commons.Domain.Port;
using Bakery.Sale.DomainApi.Model;
using Bakery.Sale.DomainApi.Port;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bakery.Sale.RestAdapter.Controllers.v1
{
    [Authorize]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DealController : ControllerBase
    {
        private readonly IRequestDeal<Deal> _requestDeal;
        private readonly IServiceBusHelper _serviceBusHelper;

        public DealController(IRequestDeal<Deal> requestDeal, IServiceBusHelper serviceBusHelper)
        {
            _requestDeal = requestDeal;
            _serviceBusHelper = serviceBusHelper;
        }

        // GET: api/deal
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = _requestDeal.GetDeals();
            await _serviceBusHelper.SendAsync(new ServiceBusMessage<dynamic>
            {
                User = User.Identity.Name,
                Message = new
                {
                    a = 1,
                    b = "ok"
                },
                Operation = ServiceBusOperation.Delete
            });
            return Ok(result);
        }

        // GET: api/deal/1
        [HttpGet]
        [Route("{id}", Name = "GetDeal")]
        public IActionResult Get(int id)
        {
            var result = _requestDeal.GetDeal(id);
            return Ok(result);
        }
    }
}
