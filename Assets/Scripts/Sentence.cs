using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Adventure
{
    public class Sentence : ScriptableObject
    {
        [field: SerializeField, Multiline, Header("���͂̃e�L�X�g")]
        public string Text { get; set; } = "";

        [field: SerializeField, Header("����������I����܂ł̎���")]
        public float Time { get; set; } = 0.5f;
    }
}
