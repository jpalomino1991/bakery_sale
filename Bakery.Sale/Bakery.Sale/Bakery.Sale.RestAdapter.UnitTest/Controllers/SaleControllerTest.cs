using Bakery.Sale.DomainApi.Model;
using Bakery.Sale.DomainApi.Port;
using Bakery.Sale.Persistence.Adapter.Dto;
using Bakery.Sale.RestAdapter.Controllers.v1;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.Sale.RestAdapter.UnitTest.Controllers
{
    public class SaleControllerTest
    {
        private SaleController _controller;
        private Mock<IRequestSale<SaleEntity>> _requestSaleMock;

        [SetUp]
        public void Setup()
        {
            _requestSaleMock = new Mock<IRequestSale<SaleEntity>>();
            _controller = new SaleController(_requestSaleMock.Object);
        }

        [Test]
        public void GetAllBakeryTestOkResult()
        {
            _requestSaleMock.Setup(mock => mock.GetSale())
            .Returns(InitInventorieList());
            var response = _controller.Get();
            Assert.IsInstanceOf<OkObjectResult>(response);
            var result = (OkObjectResult)response;
            Assert.IsNotNull(result);
            var sales = result.Value as List<SaleReadDto>;
            Assert.IsNotNull(sales);
            Assert.AreEqual(2, sales.Count);
            Assert.AreEqual(1, sales[0].Id);
            Assert.AreEqual(1, sales[0].Quantity);
            Assert.AreEqual("TestUser1", sales[0].User);
        }


        [Test]
        public void GetAllSaleByIdTestOkResult()
        {
            _requestSaleMock.Setup(mock => mock.GetSaleById(It.IsAny<int>()))
            .Returns(GetSale());
            var response = _controller.Get(1);
            Assert.IsInstanceOf<OkObjectResult>(response);
            var result = (OkObjectResult)response;
            Assert.IsNotNull(result);
            var sale = result.Value as SaleReadDto;
            Assert.IsNotNull(sale);
            Assert.AreEqual(1, sale.Id);
            Assert.AreEqual(1, sale.Quantity);
            Assert.AreEqual("TestUser1", sale.User);
        }

        [Test]
        public async Task AddSaleTestOkResult()
        {
            var Sale = GetSalePost();
            _requestSaleMock.Setup(mock => mock.AddSaleAsync(It.IsAny<SaleEntity>()))
            .Returns(Task.FromResult(GetSale()));

            var response = _controller.Post(Sale);
            Assert.IsInstanceOf<OkObjectResult>(response);
            var result = (OkObjectResult)response;
            Assert.IsNotNull(result);
            Assert.IsTrue(Convert.ToBoolean(result.Value));
        }

        [Test]
        public void DeleteSaleTestNoValue()
        {
            _requestSaleMock.Setup(mock => mock.RemoveSaleById(It.IsAny<int>()))
            .Returns((SaleEntity)null);

            var response = _controller.Delete(10);

            Assert.IsInstanceOf<BadRequestObjectResult>(response);
        }

        [Test]
        public void EditSaleTestNoValue()
        {
            var sale = GetSaleDto();
            _requestSaleMock.Setup(mock => mock.UpdateSale(It.IsAny<int>(),It.IsAny<SaleEntity>()))
            .Returns((SaleEntity)null);

            var response = _controller.Put(15,sale);

            Assert.IsInstanceOf<BadRequestObjectResult>(response);
        }

        private List<SaleEntity> InitInventorieList()
        {
            var Sale = new List<SaleEntity>
            {
                new SaleEntity
                {
                    Id = 1,
                    Product_Id = 1,
                    Quantity = 1,
                    Invoice = "DHFB82",
                    Date = DateTime.Now,
                    UserName = "TestUser1",
                    QRCode = "FHK003"
                },
                new SaleEntity
                {
                    Id = 2,
                    Product_Id = 3,
                    Quantity = 12,
                    Invoice = "DHFK03",
                    Date = DateTime.Now,
                    UserName = "TestUser2",
                    QRCode = "GTS124"
                }
            };

            return Sale;
        }

        


        private SaleEntity GetSale()
        {
            return
                new SaleEntity
                {
                    Id = 1,
                    Product_Id = 1,
                    Quantity = 1,
                    Invoice = "DHFB82",
                    Date = DateTime.Now,
                    UserName = "TestUser1",
                    QRCode = "FHK003"
                };
        }

        private SaleDto GetSaleDto()
        {
            return
                new SaleDto
                {

                    Product_Id = 1,
                    Quantity = 1,
                    Invoice = "DHFB82",
                    User= "TestUser1",
                    QRCode = "FHK003"
                };
        }

        private SaleRegisterDto GetSalePost()
        {
            List<int> Ids = new List<int>();
            Ids.Add(1);
            Ids.Add(2);
            List<int> Quantyties = new List<int>();
            Quantyties.Add(2);
            Quantyties.Add(6);
            return
                new SaleRegisterDto
                {
                    Product_Id = Ids,
                    Quantity = Quantyties,
                    Invoice = "DHFB82",
                    User = "TestUser1",
                    QRCode = "FHK003"
                };
        }



    }
}
