using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    [Serializable]
    public class SaveSystemSaveData
    {
        [Serializable]
        public class SaveRecord
        {
            public string SaverKey;
            public string Data;
        }
        
        public SaveRecord[] Records;
    }
    
    [SerializeField] private SaveSerializer[] serializers;
    [SerializeField] private SaveStore[] stores;
    [SerializeField] private Saver[] savers;

    private readonly Dictionary<string, Saver> _saversDictionary = new Dictionary<string, Saver>();
    private readonly SaveDataFormat _saveDataFormat = new SaveDataFormat();

    private void Awake()
    {
        for (int i = 0; i < savers.Length; i++)
        {
            var saver = savers[i];
            if (_saversDictionary.ContainsKey(saver.Key))
            {
                Debug.LogError($"Saver key [{saver.Key}] already present in savers dictionary. Check if save system contains savers with identical keys.");
                continue;
            }
            _saversDictionary.Add(saver.Key, saver);
        }
    }

    public void Save(string serializerKey, string storeKey)
    {
        var serializer = GetSaveSerializer(serializerKey);
        if (serializer == null)
        {
            Debug.LogError($"Can't find save serializer with [{serializerKey}] key");
            return;
        }
        
        var store = GetSaveStore(storeKey);
        if (store == null)
        {
            Debug.LogError($"Can't find save serializer with [{storeKey}] key");
            return;
        }

        Save(serializer, store);
    }
    
    public void Save(SaveSerializer serializer, SaveStore store)
    {
        var save = RecordSaveData(serializer);
        StoreSaveData(store, serializer, save);
    }

    public void Load(string storeKey)
    {
        var store = GetSaveStore(storeKey);
        if (store == null)
        {
            Debug.LogError($"Can't find save serializer with [{storeKey}] key");
            return;
        }

        Load(store);
    }

    public void Load(SaveStore store)
    {
        var saveData = CollectSaveData(store, out var saveSerializer);
        ApplySaveData(saveData, saveSerializer);
    }

    private SaveSystemSaveData RecordSaveData(SaveSerializer serializer)
    {
        var saveData = new SaveSystemSaveData()
        {
            Records = GetSaveRecords(serializer)
        };

        return saveData;
    }

    public void ApplySaveData(SaveSystemSaveData saveData, SaveSerializer saveSerializer)
    {
        var records = saveData.Records;
        for (int i = 0; i < records.Length; i++)
        {
            var record = records[i];
            var saverKey = record.SaverKey;
            if (_saversDictionary.ContainsKey(saverKey))
            {
                _saversDictionary[saverKey].ApplyData(saveSerializer, record.Data);
            }
        }
    }

    private void StoreSaveData(SaveStore store, SaveSerializer serializer, SaveSystemSaveData save)
    {
        var saveDataString = serializer.Serialize(save);
        var saveString = _saveDataFormat.FormatApply(serializer.Key, saveDataString);
        store.StoreSaveString(saveString);
    }
    
    //TODO: don't like "Collect" word in this context. Replace to something better.
    private SaveSystemSaveData CollectSaveData(SaveStore store, out SaveSerializer saveSerializer)
    {
        var saveString = store.GetSaveString();
        var saveDataString = _saveDataFormat.GetData(saveString);
        
        saveSerializer = GetSaveSerializer(saveDataString.SerializerKey);

        if (saveSerializer == null)
        {
            Debug.LogError($"Store save data contains save serializer key [{saveDataString.SerializerKey}] which is not presented in Save System");
            return null;
        }

        return saveSerializer.Deserialize<SaveSystemSaveData>(saveDataString.SaveData);
    }

    private SaveSystemSaveData.SaveRecord[] GetSaveRecords(SaveSerializer serializer)
    {
        var saversKeys = _saversDictionary.Keys;
        var records = new SaveSystemSaveData.SaveRecord[saversKeys.Count];

        int recordsCount = 0;
        foreach (var saverKey in saversKeys)
        {
            var saver = _saversDictionary[saverKey];
            var record = new SaveSystemSaveData.SaveRecord()
            {
                SaverKey = saver.Key,
                Data = saver.RecordData(serializer)
            };
            records[recordsCount] = record;
            recordsCount++;
        }

        return records;
    }

    private SaveSerializer GetSaveSerializer(string key)
    {
        foreach (var serializer in serializers)
        {
            if (serializer.Key == key)
            {
                return serializer;
            }
        }

        return null;
    }
    
    private SaveStore GetSaveStore(string key)
    {
        foreach (var store in stores)
        {
            if (store.Key == key)
            {
                return store;
            }
        }

        return null;
    }
}
