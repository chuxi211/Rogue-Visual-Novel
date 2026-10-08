using System.IO;
using System.Text;
using System;
using UnityEngine;
namespace Running
{
    public class SavedInfoSerializer
    {
        private string SavedInfoDire;
        private string SavedInfoName;
        private string AutoSavedInfoName;
        public SavedInfoSerializer(string savedInfoDire, string savedInfoName, string autoSavedInfoName)
        {
            SavedInfoDire = savedInfoDire;
            SavedInfoName = savedInfoName;
            AutoSavedInfoName = autoSavedInfoName;
        }
        public SavedErrorCode TrySerialize(SavedInfo savedInfo,int index,SaveMode saveMode)
        {
            if (savedInfo == null || !savedInfo.IsValid)
            {
                return SavedErrorCode.InvalidData;
            }
            if (saveMode == SaveMode.Manual && index < 0)
            {
                return SavedErrorCode.InvalidSlot;
            }
            if (saveMode != SaveMode.Auto && saveMode != SaveMode.Manual)
            {
                return SavedErrorCode.InvalidData;
            }
            if (string.IsNullOrWhiteSpace(SavedInfoDire))
            {
                return SavedErrorCode.InvalidPath;
            }
            string json;
            try
            {
                json = JsonUtility.ToJson(savedInfo);
            }
            catch (ArgumentException)
            {
                return SavedErrorCode.InvalidData;
            }
            try
            {
                string fileName = saveMode == SaveMode.Auto ? AutoSavedInfoName : SavedInfoName;
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    return SavedErrorCode.InvalidPath;
                }
                string path = saveMode == SaveMode.Auto
                    ? Path.Combine(SavedInfoDire, $"{fileName}.json")
                    : Path.Combine(SavedInfoDire, $"{fileName}_{index}.json");
                return SafeWriteAllText(path, json);
            }
            catch (ArgumentException)
            {
                return SavedErrorCode.InvalidPath;
            }
            catch (NotSupportedException)
            {
                return SavedErrorCode.InvalidPath;
            }
        }
        private SavedErrorCode SafeWriteAllText(string path, string text)
        {
            string directory = Path.GetDirectoryName(path);
            string tmp = Path.Combine(
          directory,
          $"{Path.GetFileName(path)}.{Guid.NewGuid():N}.tmp");
            try
            {
                Directory.CreateDirectory(directory);
                FileStream stream = new FileStream(tmp, FileMode.CreateNew, FileAccess.Write, FileShare.None);
                using (StreamWriter writer = new StreamWriter(stream, new UTF8Encoding(false)))
                {
                    writer.Write(text);
                    writer.Flush();
                }
                if (File.Exists(path))
                {
                    File.Replace(tmp, path, null);
                }
                else
                {
                    File.Move(tmp, path);
                }
                return SavedErrorCode.Success;
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
            catch (NotSupportedException)
            {
                return SavedErrorCode.IOError;
            }
            finally
            {
                if (File.Exists(tmp))
                {
                    try
                    {
                        File.Delete(tmp);
                    }
                    catch (Exception exception)
                    {
                        Debug.LogWarning(
                            $"Failed to delete temporary setting file " +
                            $"'{tmp}': {exception.Message}");
                    }
                }
            }
        }
    }
}
