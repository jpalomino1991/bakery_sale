using Azure.Messaging.ServiceBus;
using Bakery.Commons.Bakery.Commons.Domain.Port;
using Bakery.Sale.DomainApi.Model;
using Bakery.Sale.DomainApi.Port;
using Bakery.Sale.Persistence.Adapter.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.Sale.Domain
{
    public interface IProcessQueue
    {
        Task Initialize();
    }

    public class ProcessQueue : IProcessQueue
    {
        private readonly IRequestDeal<Deal> _requestDeal;
        private readonly IServiceBusHelper _serviceBusHelper;

        public ProcessQueue(IRequestDeal<Deal> requestDeal, IServiceBusHelper serviceBusHelper)
        {
            _requestDeal = requestDeal;
            _serviceBusHelper = serviceBusHelper;
        }

        public async Task Initialize()
        {
            var d = _requestDeal.GetDeals();
            await _serviceBusHelper.ProcessAsync(MessageHandler);
        }

        private async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("HexaArchConnInMemoryDb")
                .Options;
            var context = new ApplicationDbContext(options);
            context.Deals.Add(new Deal 
            {
                Id = 1,
                Name = "ABC",
                Description = "ABC deal 123"
            });
            context.SaveChanges();

            await args.CompleteMessageAsync(args.Message);
        }
    }
}
