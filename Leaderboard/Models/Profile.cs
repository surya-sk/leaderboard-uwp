using Windows.Foundation;
using Windows.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;

namespace Leaderboard.Models
{
    class Profile
    {
        private static Profile instance = new Profile();
        private ObservableCollection<Game> GamesList = null;
        ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;
        StorageFolder roamingFolder = ApplicationData.Current.RoamingFolder;
        string fileName = "games.txt";

        private Profile()
        {

        }

        public static Profile GetInstance()
        {
            return instance;
        }

        void InitHandlers()
        {
            ApplicationData.Current.DataChanged += new TypedEventHandler<ApplicationData, object>(DataChangeHandler);
        }

        void DataChangeHandler(ApplicationData appData, object o)
        {
            SaveSettings(GamesList);
        }

        public async void WriteProfile()
        {
            string json = JsonConvert.SerializeObject(GamesList);
            StorageFile storageFile = await roamingFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(storageFile, json);
        }

        public void SaveSettings(ObservableCollection<Game> gamesList)
        {
            GamesList = gamesList;
        }

        public async void ReadProfile()
        {
            try
            {
                StorageFile storageFile = await roamingFolder.GetFileAsync(fileName);
                string json = await FileIO.ReadTextAsync(storageFile);
                GamesList = JsonConvert.DeserializeObject<ObservableCollection<Game>>(json);
            }
            catch
            {
                GamesList = new ObservableCollection<Game>();
            }
            
        }

        public ObservableCollection<Game> GetGamesList()
        {
            return GamesList;
        }
    }
}
