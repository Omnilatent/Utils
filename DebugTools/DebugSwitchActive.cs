using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Omnilatent.Utils
{
    public class DebugSwitchActive : MonoBehaviour
    {
        [System.Obsolete("Use DebugModeFlag instead.")]
        public enum Modes
        {
            ShowOnlyInDebugMode = 0,
            ShowInDebugBuild = 2
        }

        public DebugModeFlag activateCondition = DebugModeFlag.NotSet; //todo: use this instead of Modes

        [Tooltip("mode is deprecated. Use activateCondition instead.")]
        [System.Obsolete("Use activateCondition instead.")]
        [SerializeField] private Modes mode;

        void Awake()
        {
            CheckCanShow();
        }

        public bool CheckCanShow()
        {
            bool canShow = DebugManager.CheckDebugFlag(activateCondition);
            gameObject.SetActive(canShow);
            return canShow;
        }

        private void OnValidate()
        {
            /*if (!Enum.IsDefined(typeof(Modes), mode))
            {
                Debug.LogError($"Field [mode] of {gameObject.name} has invalid value [{mode}]");
            }*/

            if (activateCondition == DebugModeFlag.NotSet)
            {
                Debug.LogError($"Field [activateCondition] of {gameObject.name} need to be set value.");
            }
        }
    }
}