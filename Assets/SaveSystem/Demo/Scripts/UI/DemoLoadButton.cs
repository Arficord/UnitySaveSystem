using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DemoLoadButton : MonoBehaviour
{
    [SerializeField] private SaveSystem saveSystem;
    [SerializeField] private SaveStore saveStore;
    
    private Button _button;
    
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    { 
        saveSystem.Load(saveStore);
    }

    private void OnDestroy()
    {
        if (_button != null)
        {
            _button.onClick.RemoveListener(OnButtonClick);   
        }
    }
}
