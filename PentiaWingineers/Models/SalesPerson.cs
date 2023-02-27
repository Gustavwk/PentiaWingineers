using System.ComponentModel.DataAnnotations;

namespace PentiaWingineers.Models
{
    public class SalesPerson
    {
        public SalesPerson()
        {
            
        }

        public SalesPerson(int id, string name, string hireDate, string address, string city, string zipCode)
        {
            this.id = id;
            this.name = name;
            this.hireDate = hireDate;
            this.address = address;
            this.city = city;
            this.zipCode = zipCode;
        }
        public int id { get; set; }
        public string name { get; set; }
        public string hireDate { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string zipCode { get; set; }
    }
}
