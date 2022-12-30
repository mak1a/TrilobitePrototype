using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CsvToSentence")]
public class CsvToSentence : ScriptableObject
{
    [Header("CSVファイルを挿入")]
    public TextAsset csvText;
}
