using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(fileName = "BinarySerializer", menuName = "SaveSystem/Serializers/BinarySerializer", order = 2)]
public class BinarySerializer : SaveSerializer
{
    public override string Serialize(object data)
    {
        //example: https://zetcode.com/csharp/base64/
        var bf = new BinaryFormatter();
        using var ms = new MemoryStream();
        bf.Serialize(ms, data);
        var biteData = ms.ToArray();
        return Convert.ToBase64String(biteData);
    }

    public override T Deserialize<T>(string serializedString)
    {
        var biteData = Convert.FromBase64String(serializedString);
        
        BinaryFormatter binForm = new BinaryFormatter();
        using var memStream = new MemoryStream();
        memStream.Write(biteData, 0, biteData.Length);
        memStream.Seek(0, SeekOrigin.Begin);
        return (T) binForm.Deserialize(memStream);
    }
}
