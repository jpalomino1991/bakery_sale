using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Sale.Persistence.Adapter.Dto
{
    public class SaleDto
    {
        public int Product_Id { get; set; }
        public int Quantity { get; set; }
        public string Invoice { get; set; }
        public string QRCode { get; set; }
        public string User { get; set; }
        public string Date { get; set; }
    }
}
