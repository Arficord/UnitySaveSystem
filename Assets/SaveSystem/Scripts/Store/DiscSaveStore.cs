using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Arficord.SavingSystem.Stores
{
    [CreateAssetMenu(fileName = "DiscSaveStore", menuName = "SaveSystem/Stores/DiscSaveStore", order = 1)]
    public class DiscSaveStore : SaveStore
    {
        private enum PathStart
        {
            DataPath,
            PersistentDataPath,
            Absolute,
        }

        [SerializeField] private PathStart pathStart;
        [SerializeField] private string path;
        [SerializeField] private string fileName;
        [SerializeField] private string fileType;

        private DirectoryInfo _directoryInfo;

        public override void StoreSaveString(string saveString)
        {
            var directoryPath = GetSaveDirectoryPath();

            if (_directoryInfo == null || _directoryInfo.FullName != directoryPath)
            {
                _directoryInfo = new DirectoryInfo(directoryPath);
            }

            if (!_directoryInfo.Exists)
            {
                _directoryInfo.Create();
            }

            var fullPath = $"{directoryPath}/{fileName}.{fileType}";

            File.WriteAllText(fullPath, saveString);

            Debug.Log($"Save file [{fullPath}] was saved with string:\n{saveString}");
        }

        public override string GetSaveString()
        {
            var directoryPath = GetSaveDirectoryPath();

            if (_directoryInfo == null || _directoryInfo.FullName != directoryPath)
            {
                _directoryInfo = new DirectoryInfo(directoryPath);
            }

            if (!_directoryInfo.Exists)
            {
                _directoryInfo.Create();
            }

            var fullPath = $"{directoryPath}/{fileName}.{fileType}";

            if (!File.Exists(fullPath))
            {
                Debug.LogError($"Tried to read data from not existing file.");
                return "";
            }

            return File.ReadAllText(fullPath);
        }

        public override void DeleteSave()
        {
            var directoryPath = GetSaveDirectoryPath();
            
            var fullPath = $"{directoryPath}/{fileName}.{fileType}";

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                Debug.Log("DiscStore: Saves deleted");
            }
        }
        
        private string GetSaveDirectoryPath()
        {
            switch (pathStart)
            {
                case PathStart.DataPath:
                    return Path.Join(Application.dataPath, path);
                case PathStart.PersistentDataPath:
                    return Path.Join(Application.persistentDataPath, path);
                case PathStart.Absolute:
                    return path;
                default:
                    Debug.LogError($"Unknown path start type [{pathStart}]");
                    return path;
            }
        }
    }
}