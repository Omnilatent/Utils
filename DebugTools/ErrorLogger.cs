using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Omnilatent.Utils
{
    public class ErrorLogger
    {
        static List<LogType> logTypesIncluded;

        public static void Initialize(List<LogType> logTypesToInclude = null, bool onlyLogInDebugBuild = true)
        {
            bool doLog = true;
            if (onlyLogInDebugBuild)
            {
                doLog = Debug.isDebugBuild;
            }
            if (doLog)
            {
                logTypesIncluded = logTypesToInclude;
                if (logTypesIncluded == null)
                {
                    ErrorLogger.logTypesIncluded = new List<LogType>()
                    {
                        LogType.Error, LogType.Exception
                    };
                }
                Application.logMessageReceived += HandleLog;
            }
        }

        static void HandleLog(string logString, string stackTrace, LogType type)
        {
            bool isValidLogType = false;
            //Check if this message should be logged
            for (int i = 0; i < logTypesIncluded.Count; i++)
            {
                if(type == logTypesIncluded[i])
                {
                    isValidLogType = true;
                    break;
                }
            }
            if (isValidLogType)
            {
                string msg = $"{DateTime.Now:u}: {logString}\nLog type: {type}.\nStack trace:\n{stackTrace}\n";
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
    }
}