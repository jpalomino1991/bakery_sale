using System.ComponentModel.DataAnnotations;

namespace Bakery.Sale.DomainApi
{
    public class BaseEntity<TKey>
    {
        [Key]
        public TKey Id { get; set; }
    }
}
