using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Omnilatent.Utils
{
    public class ErrorLogger
    {
        public class LogSetting
        {
            public List<LogType> logTypesIncluded = null;
            public DebugModeFlag logFlag = DebugModeFlag.Release;
            public DebugModeFlag showToastFlag = DebugModeFlag.DevBuild;
        }

        static LogSetting logSetting;
        static List<LogType> LogTypesIncluded { get => logSetting.logTypesIncluded; set => logSetting.logTypesIncluded = value; }

        [System.Obsolete("Use Initialize(LogSetting) instead")]
        public static void Initialize(List<LogType> logTypesToInclude, bool onlyLogInDebugBuild = true)
        {
            Initialize();
        }

        public static void Initialize(LogSetting p_LogSetting = null)
        {
            logSetting = p_LogSetting;
            if (logSetting == null) { logSetting = new LogSetting(); }

            if (LogTypesIncluded == null)
            {
                ErrorLogger.LogTypesIncluded = new List<LogType>()
                    {
                        LogType.Error, LogType.Exception
                    };
            }

            if (DebugManager.CheckDebugFlag(logSetting.logFlag))
            {
                Application.logMessageReceived += HandleLog;
            }

            if (DebugManager.CheckDebugFlag(logSetting.showToastFlag))
            {
                Application.logMessageReceived += ShowToast;
            }
        }

        static void HandleLog(string logString, string stackTrace, LogType type)
        {
            bool isValidLogType = false;
            //Check if this message should be logged
            for (int i = 0; i < LogTypesIncluded.Count; i++)
            {
                if (type == LogTypesIncluded[i])
                {
                    isValidLogType = true;
                    break;
                }
            }
            if (isValidLogType)
            {
                string msg = CombineLogString(logString, stackTrace, type);
                LogToTextFile(msg);
            }
        }

        public static void LogToTextFile(string msg)
        {
            DateTime now = DateTime.Now;
            string filePath = $"{Application.persistentDataPath}/log_{now:yyyyMMdd}.txt";
            using (StreamWriter w = File.AppendText(filePath))
            {
                w.WriteLine(msg);
            }
            Debug.Log($"Message logged to {filePath}");
        }

        static void ShowToast(string logString, string stackTrace, LogType type)
        {
            ToastMessage.ShowMessage(CombineLogString(logString, stackTrace, type));
        }

        static string CombineLogString(string logString, string stackTrace, LogType type)
        {
            return $"{DateTime.Now:u}: {logString}\nLog type: {type}.\nStack trace:\n{stackTrace}\n";
        }
    }
}