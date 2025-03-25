using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace Game.Scripts.MenuComponents.ShopComponents.Data
{
    public class IDataLocalSaver : IDataSaver
    {
        private const string FileNamePattern = "PlayerSave*.json";
        private const string CurrentFileName = "PlayerSave_Current.json";
        private const int CurrentVersion = 3;

        private readonly IPersistentData _persistentData;

        public IDataLocalSaver(IPersistentData persistentData) => _persistentData = persistentData;

        private string SavePath => Application.persistentDataPath;
        private string CurrentFilePath => Path.Combine(SavePath, CurrentFileName);

        public bool TryLoad()
        {
            string[] files = Directory.GetFiles(SavePath, FileNamePattern);

            if(files.Length > 0)
            {
                PlayerSaveData loadedData = null;

                foreach(string file in files)
                {
                    try
                    {
                        string json = File.ReadAllText(file);
                        loadedData = JsonConvert.DeserializeObject<PlayerSaveData>(json);

                        if(loadedData != null)
                        {
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new AggregateException($"File loading error {file}: {ex}");
                    }
                }

                if(loadedData == null)
                {
                    return false;
                }

                if(loadedData.Version < CurrentVersion)
                {
                    loadedData = MigrateData(loadedData);

                    Save(loadedData);
                }

                _persistentData.PlayerData = loadedData.Data;

                CleanupOldFiles();

                return true;
            }

            return false;
        }

        public void Save()
        {
            PlayerSaveData saveData = new PlayerSaveData() { Version = CurrentVersion, Data = _persistentData.PlayerData };
            string json = JsonConvert.SerializeObject(saveData, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            File.WriteAllText(CurrentFilePath, json);
        }
        
        private void Save(PlayerSaveData saveData)
        {
            string json = JsonConvert.SerializeObject(saveData, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

            File.WriteAllText(CurrentFilePath, json);
        }
        
        private PlayerSaveData MigrateData(PlayerSaveData oldData)
        {
            if(oldData.Version == 1)
            {
                oldData.Version = 2;
            }
            
            if(oldData.Version == 2)
            {
                oldData.Version = 3;
            }
            
            return oldData;
        }
        
        private void CleanupOldFiles()
        {
            string[] files = Directory.GetFiles(SavePath, FileNamePattern);

            foreach(string file in files)
            {
                if(!file.EndsWith(CurrentFileName))
                {
                    File.Delete(file);
                }
            }
        }
    }
}