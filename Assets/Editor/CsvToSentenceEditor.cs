using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(CsvToSentence))]
public class CsvToSentenceEditor : Editor
{
    private const string OUTPUT_DIR = "Assets/Sentences";

    public override void OnInspectorGUI()
    {
        var csvToSentence = target as CsvToSentence;
        DrawDefaultInspector();

        if (GUILayout.Button("生成"))
        {
            CreateSentence(csvToSentence);
        }
    }

    // Sentenceクラスを作成し、SentenceControllerに追加する
    private void CreateSentence(CsvToSentence csvToSentence)
    {
        var scenarioManager = GameObject.FindWithTag("ScenarioManager")?.GetComponent<Adventure.ScenarioManager>();
        if (!scenarioManager)
        {
            Debug.LogWarning("ScenarioManagerがヒエラルキー上に存在しません");
            return;
        }

        var sentenceController = scenarioManager.SentenceController;
        sentenceController.ClearSentences();

        var csvTextAsset = csvToSentence.csvText;

        if (!csvTextAsset)
        {
            Debug.LogWarning("CSVが読み込めません");
            return;
        }

        var csvText = csvTextAsset.text;
        var csvRow = csvText.Split('\n');
        var name = csvTextAsset.name;

        int positionIndex = 0;

        int createCount = 0;
        // Sentenceの生成
        foreach (var (row, index) in csvRow.Select((value, idx) => (value, idx)))
        {
            if (index == 0)
            {
                continue;
            }

            var sentence = CreateInstance<Adventure.Sentence>();

            var csvCol = row.Split(',');
            if (csvCol.Length < 10)
            {
                break;
            }

            // 名前を代入
            sentence.Name = csvCol[0].Trim();

            // テキストを代入
            var text = csvCol[1].Trim();
            if (text == "")
            {
                break;
            }

            sentence.Text = text;

            for (int csvIndex = 0; csvIndex < 4; csvIndex++)
            {
                bool isEndLoop = false;
                var humanType = csvCol[csvIndex * 2 + 2].Trim();
                switch (humanType)
                {
                case "Sharin1":
                    sentence.People.Add(Adventure.Human.Sharin1);
                    break;
                case "Sharin2":
                    sentence.People.Add(Adventure.Human.Sharin2);
                    break;
                case "Sharin3":
                    sentence.People.Add(Adventure.Human.Sharin3);
                    break;
                case "Mak1a1":
                    sentence.People.Add(Adventure.Human.Mak1a1);
                    break;
                case "Mak1a2":
                    sentence.People.Add(Adventure.Human.Mak1a2);
                    break;
                case "Mak1a3":
                    sentence.People.Add(Adventure.Human.Mak1a3);
                    break;
                case "":
                    isEndLoop = true;
                    break;
                default:
                    sentence.People.Add(Adventure.Human.None);
                    break;
                }

                // ループ終了判定
                if (isEndLoop)
                {
                    break;
                }

                // 場所
                if (int.TryParse(csvCol[csvIndex * 2 + 3].Trim(), out int pos))
                {
                    sentence.PositionIndexes.Add(pos);
                    positionIndex = pos;
                }
                else
                {
                    sentence.PositionIndexes.Add(positionIndex);
                }
            }

            sentence.name = $"{name}（{createCount}）";

            sentenceController.AddSentence(sentence);
            createCount++;
        }

        Debug.Log($"CSV読み込み完了 : {createCount}行");
    }
}
