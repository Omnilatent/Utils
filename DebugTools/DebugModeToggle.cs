using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Omnilatent.Utils
{
    public class DebugModeToggle : MonoBehaviour
    {
        public enum InteractType { Manual = 0, Button = 1, Toggle = 2 }

        [Tooltip("Which component to automatically add listener to on Start")]
        [SerializeField] InteractType uiType = InteractType.Button;

        // Start is called before the first frame update
        void Start()
        {
            switch (uiType)
            {
                case InteractType.Button:
                    var button = GetComponent<Button>();
                    if (button != null)
                    {
                        button.onClick.AddListener(ToggleDebugMode);
                    }
                    break;
                case InteractType.Toggle:
                    var toggle = GetComponent<Toggle>();
                    if (toggle != null)
                    {
                        toggle.SetIsOnWithoutNotify(DebugManager.DebugModeActive);
                        toggle.onValueChanged.AddListener(ToggleDebugMode);
                    }
                    break;
            }
        }

        public void ToggleDebugMode()
        {
            DebugManager.DebugModeActive = !DebugManager.DebugModeActive;
        }

        public void ToggleDebugMode(bool active)
        {
            DebugManager.DebugModeActive = active;
        }
    }
}