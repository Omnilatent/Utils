using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Omnilatent.Utils
{
    public class DebugSwitchActive : MonoBehaviour
    {
        public enum Modes
        {
            ShowOnlyInDebugMode = 0,
            ShowInDebugBuild = 2
        }

        public Modes mode;

        void Awake()
        {
            CheckCanShow();
        }

        public bool CheckCanShow()
        {
            bool canShow = true;
            switch (mode)
            {
                default:
                case Modes.ShowOnlyInDebugMode:
                    canShow = DebugManager.IsDebugMode();
                    break;
                case Modes.ShowInDebugBuild:
                    canShow = Debug.isDebugBuild;
                    break;
            }
            gameObject.SetActive(canShow);
            return canShow;
        }

        private void OnValidate()
        {
            if (!Enum.IsDefined(typeof(Modes), mode))
            {
                Debug.LogError($"Field [mode] of {gameObject.name} has invalid value [{mode}]");
            }
        }
    }
}