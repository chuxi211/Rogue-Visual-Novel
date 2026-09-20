using Setting.Running;
using Setting.Save;
using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Setting
{
    public class SettingSerializer
    {
        private string UserSettingDire;
        private string UserSettingName;
        public SettingSerializer(string userSettingDire, string userSettingName)
        {
            UserSettingDire = userSettingDire;
            UserSettingName = userSettingName;
        }

        //Transfor SettingData to SavedSetting and
        //Serailize to Json
        public bool TrySerializeSetting(SettingData settingData)
        {
            string jsonPath = Path.Combine(Application.persistentDataPath, UserSettingDire, UserSettingName);
            string dire = Path.GetDirectoryName(jsonPath);
            SavedSetting savedSetting = new SavedSetting(settingData);
            string jsonFile = JsonUtility.ToJson(savedSetting);
            return SafeWriteAllText(jsonPath, jsonFile);
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