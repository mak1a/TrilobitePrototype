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
        [SerializeField, Header("文章を設定")]
        private List<Sentence> m_sentences = new List<Sentence>();

        [SerializeField, Header("Textを挿入")]
        private Text m_text;

        [SerializeField, Header("ボタンを挿入")]
        private Button m_buttonNextText;

        // m_sentenceのindex
        private int m_sentenceIndex = 0;

        public void ClearSentences()
        {
            m_sentences.Clear();
        }

        // 文章を追加する
        public void AddSentence(Sentence sentence)
        {
            m_sentences.Add(sentence);
        }

        public void Initialize()
        {
            m_buttonNextText.onClick.AddListener(ChangeNextSentence);
            m_sentenceIndex = 0;
        }

        // 次の文章に切り替える
        public void ChangeNextSentence()
        {
            // 文章の数をindexが超えた場合、早期リターン
            if (m_sentenceIndex >= m_sentences.Count)
            {
                return;
            }

            // ボタンを非表示にする
            m_buttonNextText.gameObject.SetActive(false);

            var sentence = m_sentences[m_sentenceIndex];

            // 文章を表示する
            m_text.text = "";
            var tween = m_text.DOText(sentence.Text, sentence.Time).SetEase(Ease.Linear);

            // 文章を表示し終えたら、ボタンを表示する
            tween.OnComplete(() =>
            {
                m_buttonNextText.gameObject.SetActive(true);
                m_sentenceIndex++;
            });
        }
    }
}
