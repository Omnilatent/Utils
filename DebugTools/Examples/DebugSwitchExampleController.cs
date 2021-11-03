using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SS.View;
using Omnilatent.Utils;

namespace Omnilatent.Utils.Example
{
    public class DebugSwitchExampleController : MonoBehaviour
    {
        public const string DEBUGSWITCHEXAMPLE_SCENE_NAME = "DebugSwitchExample";

        private void Start()
        {
            ErrorLogger.Initialize();
        }

        public void ToggleDebugMode()
        {
            DebugManager.DebugModeActive = !DebugManager.DebugModeActive;
            ToastMessage.ShowMessage($"DebugModeActive: {DebugManager.DebugModeActive}");
            UnityEngine.SceneManagement.SceneManager.LoadScene(DEBUGSWITCHEXAMPLE_SCENE_NAME);
        }

        public void TestThrowException()
        {
            throw new System.Exception("Test");
        }
    }
}