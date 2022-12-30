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

        int createCount = 0;
        // Sentenceの生成
        foreach (var (row, index) in csvRow.Select((value, idx) => (value, idx)))
        {
            var sentence = CreateInstance<Adventure.Sentence>();

            var csvCol = row.Split(',');
            if (csvCol.Length == 0)
            {
                break;
            }

            // テキストを代入
            var text = csvCol[0].Trim();
            if (text == "")
            {
                break;
            }

            sentence.Text = text;

            if (csvCol.Length < 2)
            {
                sentence.Time = 0.5f;
                continue;
            }

            // 時間を代入
            if (float.TryParse(csvCol[1].Trim(), out float time))
            {
                sentence.Time = time;
            }
            else
            {
                sentence.Time = 0.5f;
            }

            sentence.name = $"{name}（{createCount}）";

            sentenceController.AddSentence(sentence);
            createCount++;
        }

        Debug.Log($"CSV読み込み完了 : {createCount}行");
    }
}
