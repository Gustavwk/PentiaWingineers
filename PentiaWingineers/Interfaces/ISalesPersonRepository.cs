using PentiaWingineers.Models;

namespace PentiaWingineers.Interfaces
{
    public interface ISalesPersonRepository
    {
        IEnumerable<SalesPerson> GetAllSalesPersons();
        SalesPerson GetSalesPersonById(int id);
        IEnumerable<Order> GetOrdersFromSalesPersonId(int id);
        void AddSalesPerson(SalesPerson salesPerson);
        void UpdateSalesPerson(SalesPerson salesPerson);
        void DeleteSalesPerson(int id);
        Task<List<SalesPerson>?> fetchData();
        void resetSalesTable();
        void updateSalesPersonTable(List<SalesPerson> salesPersons);
    }
}
