using Bakery.Sale.DomainApi.Model;
using Bakery.Sale.DomainApi.Port;
using Bakery.Sale.Persistence.Adapter.Dto;
using Bakery.Sale.RestAdapter.Controllers.v1;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Bakery.Sale.RestAdapter.UnitTest.Controllers
{
    public class SaleControllerTest
    {
        private BakeryController _controller;
        private Mock<IRequestSale<SaleEntity>> _requestSaleMock;

        [SetUp]
        public void Setup()
        {
            _requestSaleMock = new Mock<IRequestSale<SaleEntity>>();
            _controller = new BakeryController(_requestSaleMock.Object);
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
            var sales = result.Value as List<SaleEntity>;
            Assert.IsNotNull(sales);
            Assert.AreEqual(2, sales.Count);
            Assert.AreEqual(1, sales[0].Id);
            Assert.AreEqual(1, sales[0].Quantity);
            Assert.AreEqual("TestUser1", sales[0].UserName);
        }

        [Test]
        public void GetAllDealByIdTestOkResult()
        {
            var response = _controller.Get(1);
            Assert.IsInstanceOf<OkObjectResult>(response);
        }

        [Test]
        public void GetAllInventoryByIdTestOkResult()
        {
            _requestSaleMock.Setup(mock => mock.GetSaleById(It.IsAny<int>()))
            .Returns(GetSale());

            var response = _controller.Get(1);

            Assert.IsInstanceOf<OkObjectResult>(response);
            var result = (OkObjectResult)response;
            Assert.IsNotNull(result);
            var sale = result.Value as SaleEntity;
            Assert.IsNotNull(sale);
            Assert.AreEqual(1, sale.Id);
            Assert.AreEqual(1, sale.Quantity);
            Assert.AreEqual("TestUser1", sale.UserName);
        }

        [Test]
        public void AddInventoryTestOkResult()
        {
            var Sale = GetSalePost();
            _requestSaleMock.Setup(mock => mock.AddSale(It.IsAny<SaleEntity>()))
            .Returns(GetSale());

            var response = _controller.Post(Sale);

            Assert.IsInstanceOf<OkObjectResult>(response);
            var result = (OkObjectResult)response;
            Assert.IsNotNull(result);
            var sale = result.Value as SaleEntity;
            Assert.IsNotNull(sale);
            Assert.AreEqual(Sale.Product_Id[0], sale.Product_Id);
            Assert.AreEqual(Sale.User, sale.UserName);
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

        private SaleRegisterDto GetSalePost()
        {
            List<int> Ids = new List<int>();
            Ids.Add(1);
            return
                new SaleRegisterDto
                {
                    Product_Id = Ids,
                    Quantity = 1,
                    Invoice = "DHFB82",
                    User = "TestUser1",
                    QRCode = "FHK003"
                };
        }

    }
}
