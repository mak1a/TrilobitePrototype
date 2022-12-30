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

        if (GUILayout.Button("����"))
        {
            CreateSentence(csvToSentence);
        }
    }

    // Sentence�N���X���쐬���ASentenceController�ɒǉ�����
    private void CreateSentence(CsvToSentence csvToSentence)
    {
        var scenarioManager = GameObject.FindWithTag("ScenarioManager")?.GetComponent<Adventure.ScenarioManager>();
        if (!scenarioManager)
        {
            Debug.LogWarning("ScenarioManager���q�G�����L�[��ɑ��݂��܂���");
            return;
        }

        var sentenceController = scenarioManager.SentenceController;
        sentenceController.ClearSentences();

        var csvTextAsset = csvToSentence.csvText;

        if (!csvTextAsset)
        {
            Debug.LogWarning("CSV���ǂݍ��߂܂���");
            return;
        }

        var csvText = csvTextAsset.text;
        var csvRow = csvText.Split('\n');
        var name = csvTextAsset.name;

        int createCount = 0;
        // Sentence�̐���
        foreach (var (row, index) in csvRow.Select((value, idx) => (value, idx)))
        {
            var sentence = CreateInstance<Adventure.Sentence>();

            var csvCol = row.Split(',');
            if (csvCol.Length == 0)
            {
                break;
            }

            // �e�L�X�g����
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

            // ���Ԃ���
            if (float.TryParse(csvCol[1].Trim(), out float time))
            {
                sentence.Time = time;
            }
            else
            {
                sentence.Time = 0.5f;
            }

            sentence.name = $"{name}�i{createCount}�j";

            sentenceController.AddSentence(sentence);
            createCount++;
        }

        Debug.Log($"CSV�ǂݍ��݊��� : {createCount}�s");
    }
}
