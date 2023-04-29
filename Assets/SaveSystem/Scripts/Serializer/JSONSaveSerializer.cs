using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JSONSaveSerializer", menuName = "SaveSystem/Serializers/JSONSaveSerializer", order = 1)]
public class JSONSaveSerializer : SaveSerializer
{
    public override string Serialize(object data)
    {
        return JsonUtility.ToJson(data);
    }

    public override T Deserialize<T>(string serializedString)
    {
        return JsonUtility.FromJson<T>(serializedString);
    }
}
