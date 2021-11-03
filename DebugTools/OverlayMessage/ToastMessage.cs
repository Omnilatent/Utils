using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Omnilatent.Utils
{
    public class ToastMessage : MonoBehaviour
    {
        Button button;
        [SerializeField] Text text;

        void Awake()
        {
            button = GetComponentInChildren<Button>();
            button?.onClick.AddListener(OnOkButtonClick);
            DontDestroyOnLoad(gameObject);
        }

        public void Setup(string str, float destroyAfterSec)
        {
            text.text = str;
            Destroy(gameObject, destroyAfterSec);
        }

        public void OnOkButtonClick()
        {
            Destroy(gameObject);
        }

        public static void ShowMessage(string msg, float destroyAfterSec = 5f)
        {
            ToastMessage messagePrefab = Resources.Load<ToastMessage>("ToastMessageCanvas");
            if (messagePrefab != null)
            {
                ToastMessage messagePopup = Instantiate(messagePrefab);
                messagePopup.Setup(msg, destroyAfterSec);
            }
            else
            {
                Debug.LogError($"[No message prefab found] {msg}");
            }
        }
    }
}