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
using System.Windows.Input;
using Xamarin.Essentials;

namespace UITMBER.ViewModels
{
    public class SendApplicationViewModel : BaseViewModel
    {
        public IApplicationService _applicationService = DependencyService.Get<IApplicationService>();
        public ICarService _carService = DependencyService.Get<ICarService>();
        public ObservableCollection<CarDto> CarList { get; set; }
        public SendApplicationRequest application { get; set; }


        public SendApplicationViewModel()
        {
            Title = "Send New Application";

            application = new SendApplicationRequest();
            CarList = new ObservableCollection<CarDto>();

            SendNewApplicationCommand = new Command(Send);
            CancelCommand = new Command(Cancel);
        }
        public Command SendNewApplicationCommand { get; }

        public Command CancelCommand { get; }

        public ICommand TakePicture => new Command(async () => await TakePictureAsync());

        private async Task TakePictureAsync()
        {
            try
            {
                if (!MediaPicker.IsCaptureSupported)
                {
                    //Nie mozna zrobic zdjecia
                    return;
                }


                var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions()
                {
                    Title = "MyDriverLicense"
                });


                if (photo == null)
                    return;

                var base64 = string.Empty;

                using (var stream = await photo.OpenReadAsync())
                {
                    byte[] photoData = new byte[stream.Length];

                    stream.Read(photoData, 0, Convert.ToInt32(stream.Length));


                    base64 = Convert.ToBase64String(photoData);

                    DriverLicencePhoto = base64;
                }



                ImageSource = Xamarin.Forms.ImageSource.FromStream(
           () => new MemoryStream(Convert.FromBase64String(base64)));


            }
            catch (Exception ex)
            {


            }
        }

        public void Cleaning()
        {
            ImageSource = null;
            DriverLicenceNo = "";
            DriverLicencePhoto = "";
            CarPlate = "";
        }

        public async void Cancel()
        {
            Cleaning();
            await Shell.Current.GoToAsync($"//{nameof(Views.MainPage)}");
        }

        /// <summary>
        /// Pobieram liste samochodów tego użytkownika
        /// </summary>
        public async Task GetCarList()
        {


            try
            {
                CarList.Clear();
                var cars = await _carService.GetMyCars();

                //Mock Data
                //var cars = new List<CarDto>()
                //{
                //    new CarDto {Id=1, UserId=1, Model="Fiat", PlateNo="RZ123" },
                //    new CarDto {Id=2, UserId=1, Model="Audi", PlateNo="RLA123" }
                //};

                foreach (var car in cars)
                {
                    CarList.Add(car);
                }
                return;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public async void Send()
        {
            if (string.IsNullOrEmpty(DriverLicenceNo) || string.IsNullOrEmpty(DriverLicencePhoto) || string.IsNullOrEmpty(CarPlate))
            {
                await Application.Current.MainPage.DisplayAlert("Błąd", "The required fields have not been completed", "OK");
            }
            else
            {
                //Sprawdzam czy podany przez użytkownika numer rejestracyjny pojazdu istnieje
                //w liście z pobranymi samochodami użytkownika i wybieram numer id tego samochodu
                await GetCarList();
                var x = CarList.FirstOrDefault(z => z.PlateNo == carPlate);

                if (x != null)
                {
                    application.CarId = x.Id;
                    application.UserId = x.UserId;

                    application.DriverLicenceNo = DriverLicenceNo;
                    application.Date = DateTime.Now;
                    application.DriverLicencePhoto = DriverLicencePhoto;

                    await _applicationService.SendApplicationAsync(application);
                    await Application.Current.MainPage.DisplayAlert("Save", "The application has been sent", "OK");
                    await Task.Delay(400);
                    await Shell.Current.GoToAsync($"//{nameof(Views.MainPage)}");
                    Cleaning();
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

        public ImageSource _imageSource = "";
        public ImageSource ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                _imageSource = value;
                OnPropertyChanged();
            }
        }

    }
}
