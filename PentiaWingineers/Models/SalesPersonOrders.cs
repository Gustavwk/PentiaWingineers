namespace PentiaWingineers.Models
{
    public class SalesPersonOrders
    {
        public SalesPerson salesPerson { get; set; }
        public List<Order> orders { get; set; }

        public SalesPersonOrders(SalesPerson salesPerson, List<Order> orders)
        {
            this.orders = orders;
            this.salesPerson = salesPerson;
        }
    }
}
