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
    [QueryProperty(nameof(OrderId), nameof(OrderId))]
    public class OrderDetailViewModel : BaseViewModel
    {
        public IOrdersServices OrderService => DependencyService.Get<IOrdersServices>();

        private string orderId;
        private double cost;
        private Enums.OrderStatus status;
        private Enums.CarType type;

        public string Id { get; set; }

        public double Cost
        {
            get => cost;
            set => SetProperty(ref cost, value);
        }

        public Enums.OrderStatus Status
        {
            get => status;
            set => SetProperty(ref status, value);
        }

        public Enums.CarType Type
        {
            get => type;
            set => SetProperty(ref type, value);
        }

        public string OrderId
        {
            get
            {
                return orderId;
            }
            set
            {
                orderId = value;
                LoadOrderId(value);
            }
        }

        public async void LoadOrderId(string orderId)
        {
            try
            {
                //var order = await OrderService.GetMyOrders();
                var myOrders = new List<Order>() {
                    new Order { Id = 123, Status = Enums.OrderStatus.Ongoing, Cost = 15.50, Type=Enums.CarType.Standard},
                    new Order { Id = 321, Status = Enums.OrderStatus.Finished, Cost = 55.40, Type=Enums.CarType.Seater7}
                };
                var order = new Order();
                for (int i = 0; i < myOrders.Count; i++)
                    if (myOrders[i].Id.ToString() == orderId)
                    {
                        order = myOrders[i];
                    }

                Id = order.Id.ToString();
                Cost = order.Cost;
                Status = order.Status;
                Type = order.Type;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Order");
            }
        }


    }
}