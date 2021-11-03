using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Gront
{
    public class ShowDirectInfo : MonoBehaviour
    {
        [SerializeField] Canvas m_Canvas;
        [SerializeField] TextMeshProUGUI m_Text;
        [SerializeField] Canvas canvasPrefab;
        string currentText;

        private void Awake()
        {
            if (m_Canvas == null)
            {
                m_Canvas = Instantiate(canvasPrefab, transform);
            }
            if (m_Text == null) { m_Text = GetComponentInChildren<TextMeshProUGUI>(); }
        }

        public void SetText<T>(T obj)
        {
            SetText(obj.ToString());
        }

        public void SetText(string str)
        {
            if (!string.Equals(currentText, str))
                m_Text.text = currentText = str;
        }
    }
}