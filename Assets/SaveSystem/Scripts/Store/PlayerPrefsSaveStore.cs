using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arficord.SavingSystem.Stores
{
    [CreateAssetMenu(fileName = "PlayerPrefsSaveStore", menuName = "SaveSystem/Stores/PlayerPrefsSaveStore", order = 2)]
    public class PlayerPrefsSaveStore : SaveStore
    {
        [SerializeField] private string playerPrefsKey = "save";

        public override void StoreSaveString(string saveString)
        {
            PlayerPrefs.SetString(playerPrefsKey, saveString);
        }

        public override string GetSaveString()
        {
            //if not present return default value
            return PlayerPrefs.GetString(playerPrefsKey);
        }
        
        public override void DeleteSave()
        {
            if (!PlayerPrefs.HasKey(playerPrefsKey))
            {
                return;
            }
            
            PlayerPrefs.DeleteKey(playerPrefsKey);
            Debug.Log("PlayerPrefsStore: Saves deleted");
        }
    }
}