using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Adventure
{
    public class Sentence : ScriptableObject
    {
        [field: SerializeField, Multiline, Header("文章のテキスト")]
        public string Text { get; set; } = "";

        [field: SerializeField, Header("文字が流れ終えるまでの時間")]
        public float Time { get; set; } = 0.5f;
    }
}
