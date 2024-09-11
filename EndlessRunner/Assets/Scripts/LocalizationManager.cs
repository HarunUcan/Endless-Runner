using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance { get; private set; } // singleton yapýsý için instance oluþturulur
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
        yield return LocalizationSettings.InitializationOperation; // sistem dilinin yüklenmesini bekler
        SetLanguage((int)PlayerStats.language); // 0=Ýngilizce, 1=Türkçe 
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
