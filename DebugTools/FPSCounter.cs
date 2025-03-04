﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Omnilatent.Utils
{
    public class FPSCounter : MonoBehaviour
    {
        [SerializeField] Text fps;
        int m_frameCounter = 0;
        float m_timeCounter = 0.0f;
        float m_lastFramerate = 0.0f;
        public float m_refreshTime = 0.5f;
        [SerializeField] bool showFps = true;

        public void ToggleFPS(bool on)
        {
            showFps = on;
            fps.text = string.Empty;
        }

        void Update()
        {
            if (showFps)
            {
                if (m_timeCounter < m_refreshTime)
                {
                    m_timeCounter += Time.deltaTime;
                    m_frameCounter++;
                }
                else
                {
                    //This code will break if you set your m_refreshTime to 0, which makes no sense.
                    m_lastFramerate = (float)m_frameCounter / m_timeCounter;
                    m_frameCounter = 0;
                    m_timeCounter = 0.0f;
                }
                fps.text = string.Format("FPS: {0:0.#}", m_lastFramerate);
            }
        }
    }
}
