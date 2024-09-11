using UnityEngine;
using System.IO;

public class JsonSaveLoad
{

#if UNITY_EDITOR
    public string path = Application.dataPath + "/SaveFolder/PlayerSave.json";
#else
    public string path = Application.dataPath + "/PlayerSave.json";
#endif

    private CharacterStats characterStats = new CharacterStats();
    public void SaveData()
    {
        string data = JsonUtility.ToJson(characterStats);
        File.WriteAllText(path, data);
    }

    public void LoadData()
    {
        if (File.Exists(path)) // E�er daha �nce kaydedilmi� bir dosya varsa
        {
            string savedData = File.ReadAllText(path);

            characterStats = JsonUtility.FromJson<CharacterStats>(savedData);

            PlayerStats.coinCount = characterStats.coinCount;
            PlayerStats.highScore = characterStats.highScore;
            PlayerStats.BoughtCharacters = characterStats.BoughtCharacters;
            PlayerStats.selectedCharacterIndex = characterStats.selectedCharacterIndex;
            PlayerStats.musicVolume = characterStats.musicVolume;
            PlayerStats.sfxVolume = characterStats.sfxVolume;
            PlayerStats.isMusicOn = characterStats.isMusicOn;
            PlayerStats.isSfxOn = characterStats.isSfxOn;
            PlayerStats.language = characterStats.language;
        }
        else // E�er daha �nce kaydedilmi� bir dosya yoksa default de�erleri ata (Daha sonra SaveData() metodu �al��t�r�lmal�)
        {
            PlayerStats.coinCount = 0;
            PlayerStats.highScore = 0;
            PlayerStats.BoughtCharacters.Add(0);
            PlayerStats.selectedCharacterIndex = 0;
            PlayerStats.musicVolume = 1f;
            PlayerStats.sfxVolume = 1f;
            PlayerStats.isMusicOn = true;
            PlayerStats.isSfxOn = true;
            PlayerStats.language = Language.English;
        }
    }
}



