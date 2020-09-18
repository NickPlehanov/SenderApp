using Newtonsoft.Json;
using SenderCoordsApp.Helpers;
using SenderCoordsApp.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

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

        private RelayCommand _GetImei;
        public RelayCommand GetImei {
            get => _GetImei ?? (_GetImei = new RelayCommand(async obj => {
            Content = DependencyService.Get<IDevice>().GetIdentifier();
                Coords coords = new Coords() {
                    coo_imei = DependencyService.Get<IDevice>().GetIdentifier(),
                    coo_RecordID = Guid.NewGuid(),
                    coo_latitude = "55.186391",
                    coo_longitude = "61.334740"
                };
                var json = JsonConvert.SerializeObject(coords);
                var data = new StringContent(json, Encoding.ASCII, "application/json");

                HttpClient client = new HttpClient();
                //HttpRequestMessage request = new HttpRequestMessage {
                //    RequestUri = new Uri("http://193.138.130.98:7419/Coords/PostCoords?emei=" + DependencyService.Get<IDevice>().GetIdentifier()
                //    + "&latitude=" + "55.186391" + "&longitude=" + "61.334740" + ""),
                //    Method = HttpMethod.Post
                //};
            HttpResponseMessage response = await client.PostAsync("http://193.138.130.98:7419/api/Coords", data);
                string result = response.Content.ReadAsStringAsync().Result;
                //if (response.StatusCode == HttpStatusCode.OK) {
                //    HttpContent responseContent = response.Content;
                //    var res = await responseContent.ReadAsStringAsync();
                //}
            }));          
}
    }
}
