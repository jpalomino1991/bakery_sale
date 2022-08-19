using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.Sale.DomainApi.Model
{
    [Table("SaleEntity")]
    public class SaleEntity 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int Product_Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Invoice { get; set; }
        [Required]
        public string QRCode { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
