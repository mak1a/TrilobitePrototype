using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Adventure
{
    public class Sentence : ScriptableObject
    {
        [field: SerializeField, Header("���O")]
        public string Name { get; set; } = "";

        [field: SerializeField, Multiline, Header("���͂̃e�L�X�g")]
        public string Text { get; set; } = "";

        [field: SerializeField, Header("����������I����܂ł̎���")]
        public float Time { get; set; } = 0.5f;

        [field: SerializeField, Header("�\������l���摜")]
        public List<Human> People { get; set; } = new List<Human>();

        [field: SerializeField, Header("�摜��\������ꏊ")]
        public List<int> PositionIndexes { get; set; } = new List<int>();
    }
}
