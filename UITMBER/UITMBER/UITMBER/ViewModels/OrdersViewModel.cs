using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using UITMBER.Models;
using UITMBER.Models.Orders;
using UITMBER.Views;
using Xamarin.Forms;
using UITMBER.Services.Orders;

namespace UITMBER.ViewModels
{
        public class OrdersViewModel : BaseViewModel
        {
        public IOrdersServices OrderService => DependencyService.Get<IOrdersServices>();
        
        private Order _selectedOrder;

            public ObservableCollection<Order> Orders { get; }
            public Command LoadOrdersCommand { get; }
            public Command AddOrderCommand { get; }
            public Command<Order> OrderTapped { get; }

            public OrdersViewModel()
            {
                Title = "Orders";
                Orders = new ObservableCollection<Order>();
                LoadOrdersCommand = new Command(async () => await ExecuteLoadOrdersCommand());

                OrderTapped = new Command<Order>(OnOrderSelected);
            }

            async Task ExecuteLoadOrdersCommand()
            {
                IsBusy = true;

                try
                {
                    Orders.Clear();
                //var myOrders = await OrderService.GetMyOrders(); //Pobieranie z serwisu nie działa
                var myOrders = new List<Order>() { 
                    new Order { Status = Enums.OrderStatus.Ongoing, Cost = 15.50, Type=Enums.CarType.Standard},
                    new Order { Status = Enums.OrderStatus.Finished, Cost = 55.40, Type=Enums.CarType.Seater7}
                };

                foreach (var myOrder in myOrders)
                    {
                        Orders.Add(myOrder);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    IsBusy = false;
                }
            }

            public void OnAppearing()
            {
                IsBusy = true;
                SelectedOrder = null;
            }

            public Order SelectedOrder
            {
                get => _selectedOrder;
                set
                {
                    SetProperty(ref _selectedOrder, value);
                    OnOrderSelected(value);
                }
            }

            async void OnOrderSelected(Order order)
            {
                if (order == null)
                    return;

                // This will push the OrderDetailPage onto the navigation stack
                await Shell.Current.GoToAsync($"{nameof(OrderDetailPage)}?{nameof(OrderDetailViewModel.OrderId)}={order.Id}");
            }
        }
}
