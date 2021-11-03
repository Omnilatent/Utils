using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Omnilatent.Utils
{
    public class ErrorDialog : MonoBehaviour
    {
        [SerializeField] Text errorText;

        private void Start()
        {
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }

        public void SetText(string text) { errorText.text = text; }

        public void AddText(string text) { errorText.text += text; }

        public void Close()
        {
            Destroy(gameObject);
        }
    }
}
