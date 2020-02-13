using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.Res;
using System.Threading.Tasks;
using System.IO;

namespace spatialite_sample_xamarin_forms.Droid
{
    [Activity(Label = "spatialite_sample_xamarin_forms", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            DeployDatabaseFromAssetsAsync();
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


        public void DeployDatabaseFromAssetsAsync()
        {
            var databaseName = "test-2.3.sqlite";

            // Android application default folder.
            var appFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            var dbFile = Path.Combine(appFolder, databaseName);

            // Check if the file already exists.
            if (!File.Exists(dbFile))
            {
                using (FileStream writeStream = new FileStream(dbFile, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    // Assets is comming from the current context.
                    Assets.Open(databaseName).CopyTo(writeStream);
                }
            }
        }
    }
}