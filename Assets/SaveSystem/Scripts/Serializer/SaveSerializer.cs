using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SaveSerializer : ScriptableObject
{
    [SerializeField] private string key;

    public string Key => key;

    public abstract string Serialize(object data);
    public abstract T Deserialize<T>(string serializedString);
}
