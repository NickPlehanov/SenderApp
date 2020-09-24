using Android;
using Android.Content.PM;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using CoreLocation;
using Newtonsoft.Json;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using SenderCoordsApp.Helpers;
using SenderCoordsApp.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Permission = Plugin.Permissions.Abstractions.Permission;

namespace SenderCoordsApp.ViewModel {
    class MainPageViewModel : BaseViewModel {
        private string _Content;
        public string Content {
            get => _Content;
            set {
                _Content = value;
                OnPropertyChanged("Content");
            }
        }

        private string _Laltitude;
        public string Latitude {
            get => _Laltitude;
            set {
                _Laltitude = value;
            }
        }

        private string _Longitude;
        public string Longitude {
            get => _Longitude;
            set {
                _Longitude = value;
            }
        }

        private string GetImei() {
            return DependencyService.Get<IDevice>().GetIdentifier();
        }
        private async System.Threading.Tasks.Task GetCoordsAsync() {
            //using CLLocationManager locationManager = new CLLocationManager();
            //locationManager.LocationsUpdated += async delegate (object sender, CLLocationsUpdatedEventArgs e) {
            //    foreach (CLLocation l in e.Locations) {
            //        Latitude = l.Coordinate.Latitude.ToString();
            //        Longitude = l.Coordinate.Longitude.ToString();
            //        //Console.WriteLine(l.Coordinate.Latitude.ToString() + ", " + l.Coordinate.Longitude.ToString());                   
            //    }
            //};
            //locationManager.StartUpdatingLocation();

            Location location = await Geolocation.GetLastKnownLocationAsync();
            if (location != null) {
                Latitude = location.Latitude.ToString();
                Longitude = location.Longitude.ToString();
            }
        }

        private RelayCommand _Post;
        public RelayCommand Post {
            get => _Post ?? (_Post = new RelayCommand(async obj => {
                await GetCoordsAsync();
                using HttpClient client = new HttpClient();
                //TODO: На продакшен менять адрес !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                string baseAddress = "https://b21b1b60496c.ngrok.io/api/Coords";
                //string baseAddress = "http://193.138.130.98:7419/api/Coords";
                var data = JsonConvert.SerializeObject(new Coords() { CooImei = GetImei(), CooLatitude = Latitude, CooLongitude = Longitude, CooRecordId = Guid.NewGuid() });
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(baseAddress, content);
                Content = $"JSON => {data}\n\n RESPONSE => {response.Content.ReadAsStringAsync().Result}";
            }));
        }

        private RelayCommand _Get;

        public MainPageViewModel() {
        }

        public RelayCommand Get {
            get => _Get ?? (_Get = new RelayCommand(async obj => {
                using HttpClient client = new HttpClient();
                string baseAddress = "https://b21b1b60496c.ngrok.io/api/Coords";
                HttpResponseMessage response = await client.GetAsync(baseAddress);
                Content = $"RESPONSE => {response.Content.ReadAsStringAsync().Result}";
            }));
        }
    }
}
