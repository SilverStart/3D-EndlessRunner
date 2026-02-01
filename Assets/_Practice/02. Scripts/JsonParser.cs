using System;
using System.Collections.Generic;
using UnityEngine;

public class JsonParser : MonoBehaviour
{
    [Serializable]
    public class CharacterListWrapper
    {
        public List<CharacterData> characters;
    }

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
        TextAsset dataFile = Resources.Load<TextAsset>("JSONData");
        string data = dataFile.text;

        ParsingData(data);
    }

    private void ParsingData(string data)
    {
        CharacterListWrapper wrapper = JsonUtility.FromJson<CharacterListWrapper>(data);

        foreach (var characterData in wrapper.characters)
        {
            characterDatas.Add(characterData);
        }
    }
}