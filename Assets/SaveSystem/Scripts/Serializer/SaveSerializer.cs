using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arficord.SavingSystem.Serializers
{
    public abstract class SaveSerializer : ScriptableObject
    {
        [SerializeField] private string key;

        public string Key => key;

        public abstract string Serialize(object data);
        public abstract bool TryDeserialize<T>(out T result, string serializedString);
    }
}