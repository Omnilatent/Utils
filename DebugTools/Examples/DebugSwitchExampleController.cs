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
            DebugManager.ForceDebugMode = !DebugManager.ForceDebugMode;
            UnityEngine.SceneManagement.SceneManager.LoadScene(DEBUGSWITCHEXAMPLE_SCENE_NAME);
        }

        public void TestThrowException()
        {
            throw new System.Exception("Test");
        }
    }
}