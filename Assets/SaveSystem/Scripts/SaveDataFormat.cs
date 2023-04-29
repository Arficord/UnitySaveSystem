using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveDataFormat
{
    public class SaveStringData
    {
        public string SerializerKey;
        public string SaveData;

        public SaveStringData(string serializerKey, string saveData)
        {
            SerializerKey = serializerKey;
            SaveData = saveData;
        }
    }

    public string FormatApply(string serializerKey, string saveData)
    {
        return $"{serializerKey}\n{saveData}";
    }
    
    public SaveStringData GetData(string formattedString)
    {
        using var reader = new StringReader(formattedString);
        string serializerKey = reader.ReadLine(); 
        string saveData = reader.ReadLine();
        
        return new SaveStringData(serializerKey, saveData);
    }
}
