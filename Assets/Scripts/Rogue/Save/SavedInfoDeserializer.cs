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
            savedInfo = null;
            if (index < 0)
            {
                return SavedErrorCode.InvalidSlot;
            }
            if (string.IsNullOrWhiteSpace(SavedInfoDire) || string.IsNullOrWhiteSpace(SavedInfoName))
            {
                return SavedErrorCode.InvalidPath;
            }
            string path;
            try
            {
                path = Path.Combine(SavedInfoDire, $"{SavedInfoName}_{index}.json");
            }
            catch (ArgumentException)
            {
                return SavedErrorCode.InvalidPath;
            }
            catch (NotSupportedException)
            {
                return SavedErrorCode.InvalidPath;
            }
            try
            {
                string jsonFile = File.ReadAllText(path);
                savedInfo = JsonUtility.FromJson<SavedInfo>(jsonFile);
                if (savedInfo == null || !savedInfo.IsValid)
                {
                    savedInfo = null;
                    return SavedErrorCode.InvalidData;
                }
                return SavedErrorCode.Success;
            }
            catch (FileNotFoundException)
            {
                return SavedErrorCode.FileNotFound;
            }
            catch (DirectoryNotFoundException)
            {
                return SavedErrorCode.FileNotFound;
            }
            catch (UnauthorizedAccessException)
            {
                return SavedErrorCode.UnauthorizedAccess;
            }
            catch (System.Security.SecurityException)
            {
                return SavedErrorCode.UnauthorizedAccess;
            }
            catch (IOException)
            {
                return SavedErrorCode.IOError;
            }
            catch (ArgumentException)
            {
                return SavedErrorCode.InvalidData;
            }
            catch (NotSupportedException)
            {
                return SavedErrorCode.InvalidPath;
            }
        }
    }
}
