using PentiaWingineers.Models;

public class OrderOverviewObject{
    public OrderOverviewObject(List<Order> orders, string mounth, string year)
    {
        this.orders = orders;
        this.mounth = mounth;
        this.year = year;
    }
    public List<Order> orders { get; set; }
    public string mounth { get; set; }
    public string year {get; set;}
    public OrderOverviewObject(string mounth, string year)
    {
        this.year = year;
        this.mounth = mounth;
        this.orders = new List<Order>();
        
    }
}