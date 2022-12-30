using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

namespace Adventure
{
    public class ScenarioManager : MonoBehaviour
    {
        [SerializeField, Header("SentenceController��ݒ�")]
        SentenceController m_sentenceController = new SentenceController();
        public SentenceController SentenceController
        {
            get
            {
                return m_sentenceController;
            }
        }

        private void Awake()
        {
            m_sentenceController.Initialize();
        }
    }
}
