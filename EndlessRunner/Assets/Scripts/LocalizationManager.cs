using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance { get; private set; } // singleton yap�s� i�in instance olu�turulur
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private IEnumerator Start()
    {
        JsonSaveLoad jsonSaveLoad = new JsonSaveLoad();
        jsonSaveLoad.LoadData();
        yield return LocalizationSettings.InitializationOperation; // sistem dilinin y�klenmesini bekler
        SetLanguage((int)PlayerStats.language); // 0=�ngilizce, 1=T�rk�e 
    }
    public void SetLanguage(int language)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[language];
        PlayerStats.language = (Language)language;
        JsonSaveLoad jsonSaveLoad = new JsonSaveLoad();
        jsonSaveLoad.SaveData();
    }
}

public enum Language
{
    English,
    Turkish
}
