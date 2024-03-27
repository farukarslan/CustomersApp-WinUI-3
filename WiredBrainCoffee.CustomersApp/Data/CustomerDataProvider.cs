using System.Collections.Generic;
using System.Threading.Tasks;
using WiredBrainCoffee.CustomersApp.Model;

namespace WiredBrainCoffee.CustomersApp.Data
{
    public interface ICustomerDataProvieder
    {
        Task<IEnumerable<Customer>?> GetAllAsync();
    }

    public class CustomerDataProvider : ICustomerDataProvieder
    {
        public async Task<IEnumerable<Customer>?> GetAllAsync()
        {
            await Task.Delay(100);

            return new List<Customer> {
                new() {Id=1,FirstName="Julia",LastName="Developer", IsDeveloper=true},
                new() {Id=2,FirstName="Alex",LastName="Rider"},
                new() {Id=3,FirstName="Ömer Faruk",LastName="Arslan", IsDeveloper=true},
                new() {Id=4,FirstName="Anna",LastName="Rockstar"},
                new() {Id=5,FirstName="Sara",LastName="Metroid"},
                new() {Id=6,FirstName="Ben",LastName="Ronaldo"}
            };
        }
    }
}
