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
            public List<LogType> logTypesIncluded;
            public DebugModeFlag logFlag = DebugModeFlag.Release;
            public DebugModeFlag showToastFlag = DebugModeFlag.DevBuild;

            public LogSetting()
            {
                logTypesIncluded = new List<LogType>()
                    {
                        LogType.Error, LogType.Exception
                    };
            }

            public LogSetting(List<LogType> logTypesIncluded, DebugModeFlag logFlag, DebugModeFlag showToastFlag)
            {
                this.logTypesIncluded = logTypesIncluded;
                this.logFlag = logFlag;
                this.showToastFlag = showToastFlag;
            }
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
            if (LogTypeIsIncluded(type))
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
            if (LogTypeIsIncluded(type))
            {
                ToastMessage.ShowMessage($"{logString}");
            }
        }

        static string CombineLogString(string logString, string stackTrace, LogType type)
        {
            return $"{DateTime.Now:u}: {logString}\nLog type: {type}.\nStack trace:\n{stackTrace}\n";
        }

        static bool LogTypeIsIncluded(LogType type)
        {
            return LogTypesIncluded.Contains(type);
        }
    }
}