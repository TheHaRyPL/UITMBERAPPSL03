using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UITMBER.Models.Application;
using UITMBER.Models.Car;
using Xamarin.Forms;
using UITMBER.Services.Application;
using UITMBER.Services.Car;
using System.Diagnostics;
using System.Threading.Tasks;

using System.IO;
using System.Linq;

namespace UITMBER.ViewModels
{
    public class SendApplicationViewModel : BaseViewModel
    {
        public IApplicationService _applicationService = DependencyService.Get<IApplicationService>();
        public ICarService _carService = DependencyService.Get<ICarService>();
        public ObservableCollection<CarDto> CarList { get; }
        public SendApplicationRequest application { get; set; }


        public SendApplicationViewModel()
        {
            Title = "Send New Application";
            getCarList();

            CarList = new ObservableCollection<CarDto>();

            SendNewApplicationCommand = new Command(send);
        }
        public Command SendNewApplicationCommand { get; }

        /// <summary>
        /// Pobieram liste samochodów tego użytkownika
        /// </summary>
        public async void getCarList()
        {
            try
            {
                CarList.Clear();
                var cars =  await _carService.GetMyCars();
                foreach(var car in cars)
                {
                    CarList.Add(car);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public async void send()
        {
            if (string.IsNullOrEmpty(driverLicenceNo) || string.IsNullOrEmpty(imageSource) || string.IsNullOrEmpty(carPlate)) 
            {
                await Application.Current.MainPage.DisplayAlert("Błąd", "The required fields have not been completed", "OK");
            }
            else
            {
                //Sprawdzam czy podany przez użytkownika numer rejestracyjny pojazdu istnieje
                //w liście z pobranymi samochodami użytkownika i wybieram numer id tego samochodu
                var x = CarList.FirstOrDefault(z => z.PlateNo == carPlate);
                if (x != null)
                {
                    application.CarId = x.Id;

                    //Wybieram ID zalogowanego użytkownika z wyszukanego po numerze rejstracyjnym(który wprowadził użytkownik) samochodu
                    //Zakładam ze numer rejstracyjny a zarazem samochód jest przypisany do tego użytkownika i będzie to dobre Id użytkownika
                    //jest to słabe i wadliwe rozwazanie ale nie mam skąd pobrac Id zalogowanego uzytkownika
                    //w AuthenticationService zapisujemy w Settings imie, nazwisko, zdjęcie, oraz token dostępu zalogowanego użytkownika.
                    //Może warto pomyśleć nad tym aby pobierać z API również Id użytkownika który sie zalogował i również zapisać to w Settings
                    application.UserId = x.UserId;

                    application.DriverLicenceNo = driverLicenceNo;
                    application.Date = DateTime.Now;
                    application.DriverLicencePhoto = imageSource;

                    await _applicationService.SendApplicationAsync(application);
                    await Application.Current.MainPage.DisplayAlert("Save", "The application has been sent", "OK");
                    await Task.Delay(400);

                    //Docelowo strona do wysyłania aplikacji będzie dostępna ze strony w której są wyświetlane wszystkie aplikacje wiec
                    //po klikniecie Send i poprawnym wysłaniu, użytkownik zostanie przeniesiony z powrotem do strony ze wszystkimi jego aplikacjami;
                    //Aktualnie po kliknięciu w przycisk wyrzuca błąd
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Błąd", "A car with such registration numbers was not found", "OK");
                }

            }
            
        }

        public string driverLicenceNo = "";
        public string imageSource = "";
        public string carPlate = "";

        public string DriverLicenceNo
        {
            get
            {
                return driverLicenceNo;
            }
            set
            {
                driverLicenceNo = value;
                OnPropertyChanged();
            }
        }

        public string DriverLicencePhoto
        {
            get
            {
                return imageSource;
            }
            set
            {
                imageSource = value;
                OnPropertyChanged();
            }
        }

        public string CarPlate
        {
            get 
            {
                return carPlate;
            }
            set
            {
                carPlate = value;
                OnPropertyChanged();
            }
        }
    }
}
