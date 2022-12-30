using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Adventure
{
    [System.Serializable]
    public class SentenceController
    {
        [SerializeField, Header("���͂�ݒ�")]
        private List<Sentence> m_sentences = new List<Sentence>();

        [SerializeField, Header("Text��}��")]
        private Text m_text;

        [SerializeField, Header("�{�^����}��")]
        private Button m_buttonNextText;

        // m_sentence��index
        private int m_sentenceIndex = 0;

        public void ClearSentences()
        {
            m_sentences.Clear();
        }

        // ���͂�ǉ�����
        public void AddSentence(Sentence sentence)
        {
            m_sentences.Add(sentence);
        }

        public void Initialize()
        {
            m_buttonNextText.onClick.AddListener(ChangeNextSentence);
            m_sentenceIndex = 0;
        }

        // ���̕��͂ɐ؂�ւ���
        public void ChangeNextSentence()
        {
            // ���͂̐���index���������ꍇ�A�������^�[��
            if (m_sentenceIndex >= m_sentences.Count)
            {
                return;
            }

            // �{�^�����\���ɂ���
            m_buttonNextText.gameObject.SetActive(false);

            var sentence = m_sentences[m_sentenceIndex];

            // ���͂�\������
            m_text.text = "";
            var tween = m_text.DOText(sentence.Text, sentence.Time).SetEase(Ease.Linear);

            // ���͂�\�����I������A�{�^����\������
            tween.OnComplete(() =>
            {
                m_buttonNextText.gameObject.SetActive(true);
                m_sentenceIndex++;
            });
        }
    }
}
