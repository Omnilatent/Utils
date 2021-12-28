using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Omnilatent.Utils
{
    public class DebugManager : MonoBehaviour
    {
        [SerializeField] ErrorDialog debugDialog;
        static ErrorDialog existDebugDialog;
        public static string CurrentStageName;
        public static int currentSeed;
        public static bool showErrorMessageInRelease = false;

        static bool debugModeActive = false;
        public static bool DebugModeActive
        {
            get => debugModeActive; set
            {
                debugModeActive = value;
                onDebugModeChange?.Invoke(debugModeActive);
            }
        }

        public delegate void OnDebugModeChange(bool value);
        public static OnDebugModeChange onDebugModeChange;

        public static DebugManager instance;
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
            CommonFunctions.SetRootThenDontDestroy(this.gameObject);
        }

        public static bool IsDebugMode()
        {
#if DEBUG_MODE
            return true;
#else 
            if (Debug.isDebugBuild && debugModeActive)
                return true;
            return false;
#endif
        }

        public static string LogCurrentGameDetail()
        {
            var str = string.Format($"{CurrentStageName}-{currentSeed}");
#if DEBUG_LOG
            Debug.Log(str);
            //UniClipboard.SetText(str);
#endif
            return str;
        }

#if DEBUG_LOG
        void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }
#endif

        public static void AddLogMessageListener()
        {
            Application.logMessageReceived -= HandleLog;
            Application.logMessageReceived += HandleLog;
        }

        public static void RemoveMessageListener()
        {
            Application.logMessageReceived -= HandleLog;
        }

        static void HandleLog(string logString, string stackTrace, LogType type)
        {
#if DEBUG_LOG
            if (type == LogType.Error || type == LogType.Exception)
            {
                string msg = logString + "\n" + stackTrace;
                ShowErrorMessage(msg, type.ToString());

                //these codes will log a dummy exception regardless of what happen, be careful
                /*FirebaseManager.LogCrashlytics(User.DataToString());
                FirebaseManager.LogCrashlytics(msg);
                FirebaseManager.LogException(new System.Exception(msg));*/
            }
#endif
        }

        public static void ShowErrorMessage(string message, string value = "", string type = "Error")
        {
#if DEBUG_LOG
            ShowErrorMessage(string.Format("{0} <color=blue>{1}</color>", message, value), type);
#endif
        }

        public static void ShowErrorMessage(string message, string type = "Error")
        {
#if DEBUG_LOG
            if (Debug.isDebugBuild || showErrorMessageInRelease)
            {
                //Show popup detailing error info
                if (existDebugDialog == null)
                {
                    existDebugDialog = Instantiate(instance.debugDialog);
                }
                existDebugDialog.AddText($"{LogCurrentGameDetail()}-{type}:{message}");
                //SS.View.Manager.Add(PopupController.POPUP_SCENE_NAME, new PopupData(PopupType.OK, "Error",
                //    string.Format("<color=#C00>{0}</color>: {1}", type, message)));
            }
#endif
        }
    }
}