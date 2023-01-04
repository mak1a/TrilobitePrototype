using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Adventure
{
    public enum Human
    {
        Sharin1,
        Sharin2,
        Sharin3,
        Mak1a1,
        Mak1a2,
        Mak1a3,
        None,
    }

    [System.Serializable]
    public class HumanData
    {
        [field: SerializeField, Header("�l���̎��")]
        public Human HumanType { get; private set; } = Human.None;

        [field: SerializeField, Header("�l���̉摜")]
        public Sprite HumanImage { get; private set; }
    }

    [System.Serializable]
    public class HumanDataList
    {
        [field: SerializeField, Header("HumanData�̃��X�g")]
        public List<HumanData> HumanList { get; private set; } = new List<HumanData>();

        public Sprite Find(Human human)
        {
            foreach (var data in HumanList)
            {
                if (data.HumanType != human)
                {
                    continue;
                }

                return data.HumanImage;
            }

            return null;
        }
    }
}