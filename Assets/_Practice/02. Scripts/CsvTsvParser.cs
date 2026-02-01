using System;
using System.Collections.Generic;
using UnityEngine;

public class CsvTsvParser : MonoBehaviour
{
    [Serializable]
    public class CharacterData
    {
        public string CharID;
        public string Name;
        public int HP;
        public int Attack;

        public CharacterData(string CharID, string Name, int HP, int Attack)
        {
            this.CharID = CharID;
            this.Name = Name;
            this.HP = HP;
            this.Attack = Attack;
        }
    }

    [SerializeField] private List<CharacterData> characterDatas = new List<CharacterData>();

    void Start()
    {
        TextAsset dataFile = Resources.Load<TextAsset>("CSVData");
        // TextAsset dataFile = Resources.Load<TextAsset>("TSVData");

        string data = dataFile.text;
        ParsingData(data);
    }

    private void ParsingData(string data)
    {
        string[] rows = data.Split('\n'); // 단락 변경 기준으로 자르기

        foreach (string row in rows)
            Debug.Log(row);

        for (int i = 1; i < rows.Length; i++)
        {
            string row = rows[i].Trim(); // 공백 제거

            string[] col = row.Split(','); // 콤마 기준으로 자르기
            // string[] col = row.Split('\t'); // 탭 기준으로 자르기

            CharacterData characterData = new CharacterData(col[0], col[1], int.Parse(col[2]), int.Parse(col[3]));
            characterDatas.Add(characterData); // 리스트에 데이터 추가
        }
    }
}