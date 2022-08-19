using Bakery.Commons.Bakery.Commons.Domain.Model;
using Bakery.Commons.Bakery.Commons.Domain.Port;
using Bakery.Sale.DomainApi.Model;
using Bakery.Sale.DomainApi.Port;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Bakery.Sale.RestAdapter.Controllers.v1
{
    //[Authorize]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DealController : ControllerBase
    {
        private readonly IRequestDeal<Deal> _requestDeal;
        private readonly IServiceBusHelper _serviceBusHelper;
        private readonly IStorageAccountHelper _storageAccountHelper;

        public DealController(IRequestDeal<Deal> requestDeal, IServiceBusHelper serviceBusHelper, IStorageAccountHelper storageAccountHelper)
        {
            _requestDeal = requestDeal;
            _serviceBusHelper = serviceBusHelper;
            _storageAccountHelper = storageAccountHelper;
        }

        // GET: api/deal
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = _requestDeal.GetDeals();
            await _serviceBusHelper.SendAsync(new ServiceBusMessage<InventorySold>
            {
                User = User.Identity.Name,
                Message = new InventorySold
                {
                    ProductId = 2,
                    Quantity = 3
                },
                Operation = ServiceBusOperation.Update
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

        [HttpPost(nameof(Upload))]
        public async Task<IActionResult> Upload(IFormFile file, int productId)
        {
            await _storageAccountHelper.UploadFileAsync(file);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
