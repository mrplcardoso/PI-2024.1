using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("orders")]
    public class Order
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [Column("clientId")]
        public int clientId { get; set; }

        [Column("address")]
        public string Address { get; set; }
    }
}