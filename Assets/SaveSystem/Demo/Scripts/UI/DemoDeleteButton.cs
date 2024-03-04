using Arficord.SavingSystem.Stores;
using UnityEngine;
using UnityEngine.UI;

namespace Arficord.SavingSystem.Demo.UI
{
    [RequireComponent(typeof(Button))]
    public class DemoDeleteButton : MonoBehaviour
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
            saveSystem.Delete(saveStore);
        }

        private void OnDestroy()
        {
            if (_button != null)
            {
                _button.onClick.RemoveListener(OnButtonClick);
            }
        }
    }
}