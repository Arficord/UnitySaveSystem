using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arficord.SavingSystem.Serializers
{
    [CreateAssetMenu(fileName = "JSONSaveSerializer", menuName = "SaveSystem/Serializers/JSONSaveSerializer",
        order = 1)]
    public class JSONSaveSerializer : SaveSerializer
    {
        public override string Serialize(object data)
        {
            return JsonUtility.ToJson(data);
        }

        public override bool TryDeserialize<T>(out T result, string serializedString)
        {
            result = JsonUtility.FromJson<T>(serializedString);
            return result != null;
        }
    }
}