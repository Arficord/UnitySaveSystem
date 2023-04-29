using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
