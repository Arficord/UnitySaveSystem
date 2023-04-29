using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Saver : MonoBehaviour
{
    [SerializeField] private string key;

    public string Key
    {
        get => key;
        set => key = value;
    }

    public abstract string RecordData(SaveSerializer serializer);
    public abstract void ApplyData(SaveSerializer serializer, string serializedString);
}
