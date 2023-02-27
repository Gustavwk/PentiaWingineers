using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PentiaWingineers.Models
{
    public class Order
    {
        public Order()
        {
            
        }
        public Order(int id, string orderName, int orderPrice, string orderDate, int salesPersonId)
        {
            this.id = id;
            this.orderName = orderName;
            this.orderPrice = orderPrice;
            this.orderDate = orderDate;
            this.salesPersonId = salesPersonId;
        }

      
        public int id { get; set; }
        public string orderName { get; set; }
        public int orderPrice { get; set; }
        public string orderDate { get; set; }
        public int salesPersonId { get; set; }
    }
}
