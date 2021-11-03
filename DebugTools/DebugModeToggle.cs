using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Omnilatent.Utils
{
    public class DebugModeToggle : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(ToggleDebugMode);
        }

        public void ToggleDebugMode()
        {
            DebugManager.DebugModeActive = !DebugManager.DebugModeActive;
        }
    }
}