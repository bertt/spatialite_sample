using Microsoft.Data.Sqlite;
using System;
using System.ComponentModel;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace spatialite_sample_xamarin_forms
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            lblTest.Text = "hohoho";

            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "test-2.3.sqlite");

            string connectString = "Data Source=" + path;
            var connection = new SqliteConnection(connectString);

            connection.EnableExtensions(true);

            // todo: Load mod_spatialite, but how...
            //#if Windows
            //  SpatialiteLoader.Load(connection);
            //#elif OSX || Linux || Android
            //#endif
            // nextline does not work:
            // connection.LoadExtension("mod_spatialite");

            connection.Open();
            // todo: Get this query working: SELECT Name, ST_ASBinary(geometry) FROM Towns;
            var sql = "SELECT Name FROM Towns";
            var command = new SqliteCommand(sql, connection);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var name = reader.GetString(0);
            }

            connection.Close();
            connection.Dispose();
        }
    }
}
