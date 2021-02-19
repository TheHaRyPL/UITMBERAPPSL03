using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using UITMBER.Models.Application;
using UITMBER.Services.Application;
using Xamarin.Forms;

namespace UITMBER.ViewModels
{
    public class MyApplicationViewModel : BaseViewModel
    {
        public IApplicationService _applicationService => DependencyService.Get<IApplicationService>();

        public ObservableCollection<GetMyApplicationsResponse> MyApplicationsResponse { get; }
        public Command LoadApplicationsCommand { get; }

        public MyApplicationViewModel()
        {
            Title = "Application";
            MyApplicationsResponse = new ObservableCollection<GetMyApplicationsResponse>();
            LoadApplicationsCommand = new Command(async () => await ExecuteLoadApplicationsCommand());
        }

        async Task ExecuteLoadApplicationsCommand()
        {
            IsBusy = true;

            try
            {
                MyApplicationsResponse.Clear();
                //var applications = await _applicationService.GetMyApplicationsAsync();
                //Uzywam mockowych danych w celu sprawdzenia działania wyświetlania danych na stronie

                var applications = new List<GetMyApplicationsResponse>() {
                    new GetMyApplicationsResponse { Id = 1, Date = DateTime.Now, Accepted=true, DriverLicenceNo="4124141"},
                    new GetMyApplicationsResponse { Id = 2, Date = DateTime.Now, Accepted=false, DriverLicenceNo="4242424"},
                    new GetMyApplicationsResponse { Id = 3, Date = DateTime.Now, Accepted=true, DriverLicenceNo="4124141"},
                    new GetMyApplicationsResponse { Id = 4, Date = DateTime.Now, Accepted=false, DriverLicenceNo="4242424"}

                };

                foreach (var x in applications) 
                {
                    MyApplicationsResponse.Add(x);
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
        }
    }
}
