using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arficord.SavingSystem.Stores
{
    public abstract class SaveStore : ScriptableObject
    {
        [SerializeField] private string key;

        public string Key => key;

        public abstract void StoreSaveString(string saveString);
        public abstract string GetSaveString();
        public abstract void DeleteSave();
    }
}