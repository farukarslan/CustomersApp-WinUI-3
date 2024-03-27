using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WiredBrainCoffee.CustomersApp.Command;
using WiredBrainCoffee.CustomersApp.Data;
using WiredBrainCoffee.CustomersApp.Model;

namespace WiredBrainCoffee.CustomersApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ICustomerDataProvieder _customerDataProvieder;
        private CustomerItemViewModel? _selectedCustomer;

        public MainViewModel(ICustomerDataProvieder customerDataProvieder) 
        {
            _customerDataProvieder = customerDataProvieder;
            AddCommand = new DelegeteCommand(Add);
            DeleteCommand = new DelegeteCommand(Delete, CanDelete);
        }

        public DelegeteCommand AddCommand { get; }
        public DelegeteCommand DeleteCommand { get; }

        // ObservableCollection class is special collection that notifies the  data binding when items are edit or removed.
        public ObservableCollection<CustomerItemViewModel> Customers { get; } = new();
        public CustomerItemViewModel? SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                if (_selectedCustomer != value)
                {
                    _selectedCustomer = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(IsCustomerSelected));
                    DeleteCommand.RaiseCanExecuteChanged();
                } 
            }
        }
        public bool IsCustomerSelected => SelectedCustomer is not null;

        public async Task LoadAsync()
        {
            if (Customers.Any())
            {
                return;
            }

            var customers = await _customerDataProvieder.GetAllAsync();
            if (customers is not null)
            {
                foreach (var customer in customers)
                {
                    Customers.Add(new CustomerItemViewModel(customer));
                }
            }
        }

        private void Add(object? parameter)
        {
            var customer = new Customer { FirstName = "New" };
            var viewModel = new CustomerItemViewModel(customer);
            Customers.Add(viewModel);
            SelectedCustomer = viewModel;
        }

        private void Delete(object? parameter)
        {
            if (SelectedCustomer is not null)
            {
                Customers.Remove(SelectedCustomer);
                SelectedCustomer = null;
            }
        }

        private bool CanDelete(object? parameter) => SelectedCustomer is not null;
    }
}