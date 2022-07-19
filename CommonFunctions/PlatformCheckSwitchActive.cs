using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Omnilatent.Utils
{
    public class PlatformCheckSwitchActive : MonoBehaviour
    {
        [SerializeField] List<RuntimePlatform> deactivePlatforms;
        bool active = true;

        private void Start()
        {
            for (int i = 0; i < deactivePlatforms.Count; i++)
            {
                if (Application.platform == deactivePlatforms[i])
                {
                    DeactivateGameObject();
                    break;
                }
#if UNITY_IOS //treat RuntimePlatform.IPhonePlayer as both iPhone and iPad
                if (deactivePlatforms[i] == RuntimePlatform.IPhonePlayer)
                {
                    DeactivateGameObject();
                    break;
                }
#endif
            }
        }

        void DeactivateGameObject()
        {
            active = false;
            gameObject.SetActive(active);
        }
    }
}