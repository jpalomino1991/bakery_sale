using Bakery.Sale.DomainApi.Model;
using Bakery.Sale.DomainApi.Port;
using Bakery.Sale.Persistence.Adapter.Dto;
using Bakery.Sale.Persistence.Adapter.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bakery.Sale.RestAdapter.Controllers.v1
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly IRequestSale<SaleEntity> _SaleService;
        public SaleController(IRequestSale<SaleEntity> SaleService)
        {
            _SaleService = SaleService;
        }

        // GET: api/<SaleController>
        [HttpGet]
        public IActionResult Get()
        {
            List<SaleReadDto> saleRead = new List<SaleReadDto>();

            var results = _SaleService.GetSale();
            foreach (var result in results)
            {
                var sale = new SaleReadDto {
                    Id = result.Id,
                    Invoice = result.Invoice,
                    Date = result.Date,
                    Product_Id = result.Product_Id,
                    QRCode = result.QRCode,
                    Quantity = result.Quantity,
                    User = result.UserName
                };

                saleRead.Add(sale);
            }
            return Ok(saleRead);
        }

        // GET api/<SaleController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _SaleService.GetSaleById(id);
            if (result == null) return NotFound();
            var sale = new SaleReadDto
            {
                Id = result.Id,
                Invoice = result.Invoice,
                Date = result.Date,
                Product_Id = result.Product_Id,
                QRCode = result.QRCode,
                Quantity = result.Quantity,
                User = result.UserName
            };

            return Ok(sale);
        }

        // POST api/<SaleController>
        [HttpPost]
        public IActionResult Post([FromBody] SaleRegisterDto Sale)
        {
            if (Sale.Product_Id.Count <= 0)
            {
                return BadRequest(false);
            }

            int iterator = 0;
            foreach (var product in Sale.Product_Id)
            {
                var sale = new SaleEntity
                {
                    Date = DateTime.UtcNow,
                    Invoice = Sale.Invoice,
                    Product_Id = product,
                    QRCode = Sale.QRCode,
                    Quantity = Sale.Quantity[iterator],
                };
                if (User != null)
                    sale.UserName = User.Identity.Name;
                 _SaleService.AddSaleAsync(sale);
                iterator++;
            }
            return Ok(true);
        }

        // PUT api/<SaleController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] SaleDto Sale)
        {
            StatusDto status = new StatusDto();
            var result = _SaleService.GetSaleById(id);
            if (result is null)
            {
                status = new StatusDto { IsSuccess = false, Message = $"Sale {id} is not valid" };
                return BadRequest(status);
            }

            var sale = new SaleEntity
            {
                Date = DateTime.Now,
                Invoice = Sale.Invoice,
                Product_Id = Sale.Product_Id,
                QRCode = Sale.QRCode,
                Quantity = Sale.Quantity,
                UserName = Sale.User
            };

            _SaleService.UpdateSale(id, sale);

            return Ok(status = new StatusDto { IsSuccess = true, Message = $"" });
        }

        // DELETE api/<SaleController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            StatusDto status = new StatusDto();
            var result = _SaleService.GetSaleById(id);
            if (result is null)
            {
                status = new StatusDto { IsSuccess = false, Message = $"Sale {id} is not valid" };
                return BadRequest(status);
            }

            _SaleService.RemoveSaleById(id);

            return Ok(status = new StatusDto { IsSuccess = true, Message = $"" });
        }
    }
}

