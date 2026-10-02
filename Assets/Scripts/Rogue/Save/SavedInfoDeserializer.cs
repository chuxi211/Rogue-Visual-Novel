using System;
using System.IO;
using UnityEngine;
namespace Running
{
    public class SavedInfoDeserializer
    {
        private string SavedInfoDire;
        private string SavedInfoName;
        private string AutoSavedInfoName;
        public SavedInfoDeserializer(string savedInfoDire, string savedInfoName, string autoSavedInfoName)
        {
            SavedInfoDire = savedInfoDire;
            SavedInfoName = savedInfoName;
            AutoSavedInfoName = autoSavedInfoName;
        }
        public SavedErrorCode TryDeserialise(int index, out SavedInfo savedInfo)
        {
            string path = Path.Combine(SavedInfoDire, $"{SavedInfoName}_{index}.json");
            if (!File.Exists(path))
            {
                savedInfo = null;
                return SavedErrorCode.FileNotFound;
            }
            try
            {
                string jsonFile = File.ReadAllText(path);
                savedInfo = JsonUtility.FromJson<SavedInfo>(jsonFile);
                return SavedErrorCode.Success;
            }
            catch (UnauthorizedAccessException)
            {
                Debug.LogError($"FatalError: Unauthorize to read file:{path}");
                savedInfo = null;
                return SavedErrorCode.UnauthorizedAccess;
            }
            catch (IOException)
            {
                Debug.LogError($"FatalError: IO error when reading file:{path}");
                savedInfo = null;
                return SavedErrorCode.IOError;
            }
        }
    }
}