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

        [SerializeField, Header("NameTextを挿入")]
        private Text m_nameText;

        [SerializeField, Header("Textを挿入")]
        private Text m_text;

        [SerializeField, Header("ボタンを挿入")]
        private Button m_buttonNextText;

        [SerializeField, Header("人物の画像を全て挿入")]
        private HumanDataList m_humanData;

        [SerializeField, Header("場所を設定")]
        private List<Image> m_images = new List<Image>();

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

            m_images.ForEach(image => image.gameObject.SetActive(false));

            // ボタンを非表示にする
            m_buttonNextText.gameObject.SetActive(false);

            var sentence = m_sentences[m_sentenceIndex];

            for (int index = 0; index < sentence.People.Count; index++)
            {
                // 画像を表示する
                var image = m_images[sentence.PositionIndexes[index]];
                var sprite = m_humanData.Find(sentence.People[index]);

                if (sprite)
                {
                    image.gameObject.SetActive(true);
                    image.sprite = sprite;
                }
            }

            // 名前を表示する
            m_nameText.text = sentence.Name;

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
