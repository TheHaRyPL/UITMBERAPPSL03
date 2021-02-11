using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UITMBER.Models.Orders;
using UITMBER.Services.Request;
using Xamarin.Forms;

namespace UITMBER.Services.Orders
{
    public class OrdersServices : IOrdersServices
    {
        public IRequestService _requestService=>DependencyService.Get<IRequestService>();
        public async Task<AcceptOrderResponse> OrderAccept(Order input)
        {
            var uri = $"{Settings.SERVERENDPOINT}/Orders/OrderAccept";

            return await _requestService.PostAsync<Order, AcceptOrderResponse>(uri, input);
        }

        public async Task<RateClientResponse> ClientRate(long idOrder, double driverRate, string info)
        {
            var uri = $"{Settings.SERVERENDPOINT}/Orders/ClientRate?idOrder={idOrder}&driverRate={driverRate}&info={info}";

            return await _requestService.PutAsync<string, RateClientResponse>(uri, "");
        }

        public async Task<List<Order>> GetClientOrderDetails()
        {
            var uri = $"{Settings.SERVERENDPOINT}/Orders/GetClientOrderDetails";


            return await _requestService.GetAsync<List<Order>>(uri);

        }

        public async Task<List<string>> GetLuggageTypes()
        {

            var uri = $"{Settings.SERVERENDPOINT}/Orders/GetLuggageTypes";


            return await _requestService.GetAsync<List<string>>(uri);

        }

        public async Task<double> GetCost(DateTime date, double distance)
        {
            var uri = $"{Settings.SERVERENDPOINT}/Orders/GetCost?date={date}&distance={distance}";


            return await _requestService.GetAsync<double>(uri);

        }

        public async Task<List<Order>> GetMyOrders()
        {
            var uri = $"{Settings.SERVERENDPOINT}/Orders/GetMyOrders";

            return await _requestService.GetAsync<List<Order>>(uri);
        }
        //Czy aby GetCarTypes nie powinno zwracać listy stringów, zamiast Orderów, tak jak w GetLuggageTypes?
        //Odwołanie do UITMBER.Api
        public async Task<List<Order>> GetCarTypes()
        {
            var uri = $"{Settings.SERVERENDPOINT}/Orders/GetCarTypes";

            return await _requestService.GetAsync<List<Order>>(uri);
        }
    }
}
