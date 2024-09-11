using System.Collections.Generic;

// JsonSaveLoad s�n�f� ile kaydedilecek verileri serialize etmeye uygun hale d�n��t�rmek i�in bu s�n�f� olu�turduk.
public class CharacterStats
{
    public int coinCount;
    public int highScore;
    public List<int> BoughtCharacters = new List<int>();
    public int selectedCharacterIndex;
    public float musicVolume;
    public float sfxVolume;
    public bool isMusicOn;
    public bool isSfxOn;
    public Language language;


    public CharacterStats()
    {
        this.coinCount = PlayerStats.coinCount;
        this.highScore = PlayerStats.highScore;
        this.BoughtCharacters = PlayerStats.BoughtCharacters;
        this.selectedCharacterIndex = PlayerStats.selectedCharacterIndex;
        this.musicVolume = PlayerStats.musicVolume;
        this.sfxVolume = PlayerStats.sfxVolume;
        this.isMusicOn = PlayerStats.isMusicOn;
        this.isSfxOn = PlayerStats.isSfxOn;
        this.language = PlayerStats.language;
    }
}

