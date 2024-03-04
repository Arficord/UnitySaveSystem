using System.Collections;
using System.Collections.Generic;
using Arficord.SavingSystem.Serializers;
using UnityEngine;

namespace Arficord.SavingSystem.Savers
{
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
}