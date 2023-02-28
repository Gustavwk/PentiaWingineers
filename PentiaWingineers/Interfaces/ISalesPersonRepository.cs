using PentiaWingineers.Models;

namespace PentiaWingineers.Interfaces
{
    public interface ISalesPersonRepository
    {
        IEnumerable<SalesPerson> GetAllSalesPersons();
        SalesPerson GetSalesPersonById(int id);
        void AddSalesPerson(SalesPerson salesPerson);
        void DeleteSalesPerson(int id);
        Task<List<SalesPerson>?> fetchData();
        void updateSalesPersonTable(List<SalesPerson> salesPersons);
        public int GetSalesCountFromSalesPerson(int salesPersonId);
        public void deleteAllSalesPersons();
    }
}
