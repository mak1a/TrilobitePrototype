using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Adventure
{
    public class Sentence : ScriptableObject
    {
        [field: SerializeField, Header("名前")]
        public string Name { get; set; } = "";

        [field: SerializeField, Multiline, Header("文章のテキスト")]
        public string Text { get; set; } = "";

        [field: SerializeField, Header("文字が流れ終えるまでの時間")]
        public float Time { get; set; } = 0.5f;

        [field: SerializeField, Header("表示する人物画像")]
        public List<Human> People { get; set; } = new List<Human>();

        [field: SerializeField, Header("画像を表示する場所")]
        public List<int> PositionIndexes { get; set; } = new List<int>();
    }
}
