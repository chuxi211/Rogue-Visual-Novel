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
            string path = string.Empty;
            if(saveMode==SaveMode.Auto)
            {
                path = Path.Combine(SavedInfoDire, $"{AutoSavedInfoName}.json");
            }
            if(saveMode== SaveMode.Manual)
            {
                path = Path.Combine(SavedInfoDire, $"{SavedInfoName}_{index}.json");
            }
            string jsonFile = JsonUtility.ToJson(savedInfo);
            if(!SafeWriteAllText(path, jsonFile))
            {
                return SavedErrorCode.IOError;
            }
            return SavedErrorCode.Success;
        }
        private bool SafeWriteAllText(string path, string text)
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
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                Debug.LogError($"FatalError: Unauthorize to write file:{path}");
                return false;
            }
            catch (IOException)
            {
                Debug.LogError("FatalError: ");
                return false;
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