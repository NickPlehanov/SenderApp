using SenderCoordsApp.Helpers;

[assembly: Xamarin.Forms.Dependency(typeof(UniqueIdAndroid))]
namespace SenderCoordsApp.Helpers {
    public class UniqueIdAndroid : IDevice {
        //public string GetIdentifier => Android.Provider.Settings.Secure.GetString(Android.App.Application.Context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);

        string IDevice.GetIdentifier() {
            return Android.Provider.Settings.Secure.GetString(Android.App.Application.Context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);
        }
    }
}
