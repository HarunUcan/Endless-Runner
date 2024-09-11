using System.Collections.Generic;

// Kay�tlar y�klendikten sonra her yerden kolayca eri�ebilmek i�in statik bir s�n�f olu�turduk.
public static class PlayerStats
{
    public static int coinCount;
    public static int highScore;
    public static List<int> BoughtCharacters = new List<int>();
    public static int selectedCharacterIndex;
    public static float musicVolume = 1.0f;
    public static float sfxVolume = 1.0f;
    public static bool isMusicOn = true;
    public static bool isSfxOn = true;
    public static Language language = Language.English;
}
