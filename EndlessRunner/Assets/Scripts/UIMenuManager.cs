using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _buyPanel;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundSlider;

    private bool _isMusicOn = true;
    private bool _isSoundsOn = true;
    [SerializeField] private Sprite _enableToggle;
    [SerializeField] private Sprite _disableToggle;
    [SerializeField] private Button _musicToggle;
    [SerializeField] private Button _soundsToggle;

    [SerializeField] private TMP_Dropdown _languageDropdown;

    [SerializeField] private TMP_Text _bestScoreTxt;
    [SerializeField] private TMP_Text _totalBookTxt;
    [SerializeField] private TMP_Text _characterNameTxt;
    [SerializeField] private TMP_Text _characterPriceTxt;


    private void Start()
    {
        JsonSaveLoad jsonSaveLoad = new JsonSaveLoad();
        jsonSaveLoad.LoadData();
        jsonSaveLoad.SaveData();
        _bestScoreTxt.text = $"{PlayerStats.highScore}";
        _bestScoreTxt.transform.GetChild(0).GetComponent<TMP_Text>().text = _bestScoreTxt.text;
        _totalBookTxt.text = $"{PlayerStats.coinCount}";
        _totalBookTxt.transform.GetChild(0).GetComponent<TMP_Text>().text = _totalBookTxt.text;

        _soundSlider.value = PlayerStats.sfxVolume;
        _musicSlider.value = PlayerStats.musicVolume;
        AudioManager.Instance.ChangeMusicSoundLevel(PlayerStats.musicVolume);
        AudioManager.Instance.ChangeSoundSoundLevel(PlayerStats.sfxVolume);

        if (PlayerStats.isSfxOn)
        {
            _isSoundsOn = true;
            _soundsToggle.image.sprite = _enableToggle;
        }
        else
        {
            _isSoundsOn = false;
            _soundsToggle.image.sprite = _disableToggle;
        }
        if (PlayerStats.isMusicOn)
        {
            _isMusicOn = true;
            _musicToggle.image.sprite = _enableToggle;
        }
        else
        {
            _isMusicOn = false;
            _musicToggle.image.sprite = _disableToggle;
        }
        AudioManager.Instance.SoundsOnOff(PlayerStats.isSfxOn, _soundSlider.value);
        AudioManager.Instance.MusicOnOff(PlayerStats.isMusicOn, _musicSlider.value);

    }
    public void OpenSettingsPanel()
    {
        _settingsPanel.SetActive(true);
        AudioManager.Instance.ButtonClick();
    }
    public void CloseSettingsPanel()
    {
        _settingsPanel.SetActive(false);
        AudioManager.Instance.ButtonClick();
    }
    public void OpenBuyPanel()
    {
        _buyPanel.SetActive(true);
        AudioManager.Instance.ButtonClick();

        CharacterSelectManager.Instance.CheckBoughtCharacter();
        CharacterSelectManager.Instance.CheckSelectedCharacter();

        string name = CharacterSelectManager.Instance.GetCharacterName();
        _characterNameTxt.text = name;
        _characterNameTxt.transform.GetChild(0).GetComponent<TMP_Text>().text = _characterNameTxt.text;
        string price = CharacterSelectManager.Instance.GetCharacterPrice();
        _characterPriceTxt.text = price;
        _characterPriceTxt.transform.GetChild(0).GetComponent<TMP_Text>().text = _characterPriceTxt.text;
    }
    public void CloseBuyPanel()
    {
        _buyPanel.SetActive(false);
        AudioManager.Instance.ButtonClick();
        CharacterSelectManager.Instance.ResetBuyPanel();
    }

    public void ChangeMusicSoundLevel()
    {
        if (_isMusicOn)
            AudioManager.Instance.ChangeMusicSoundLevel(_musicSlider.value);
    }
    public void ChangeSoundSoundLevel()
    {
        if (_isSoundsOn)
            AudioManager.Instance.ChangeSoundSoundLevel(_soundSlider.value);
    }
    public void MusicOnOff()
    {
        _isMusicOn = !_isMusicOn;
        if (_isMusicOn)
            _musicToggle.image.sprite = _enableToggle;
        else
            _musicToggle.image.sprite = _disableToggle;
        AudioManager.Instance.MusicOnOff(_isMusicOn, _musicSlider.value);
        AudioManager.Instance.ButtonClick();
    }
    public void SoundsOnOff()
    {
        _isSoundsOn = !_isSoundsOn;
        if (_isSoundsOn)
            _soundsToggle.image.sprite = _enableToggle;
        else
            _soundsToggle.image.sprite = _disableToggle;
        AudioManager.Instance.SoundsOnOff(_isSoundsOn, _soundSlider.value);
        AudioManager.Instance.ButtonClick();
    }
    public void SetLanguage()
    {
        LocalizationManager.Instance.SetLanguage(_languageDropdown.value);
    }
    public void StartGame()
    {
        AudioManager.Instance.ButtonClick();
        SceneManager.LoadScene("SampleScene");
    }
    public void NextCharacter()
    {
        CharacterSelectManager.Instance.NextCharacter();
        string name = CharacterSelectManager.Instance.GetCharacterName();
        _characterNameTxt.text = name;
        _characterNameTxt.transform.GetChild(0).GetComponent<TMP_Text>().text = _characterNameTxt.text;
        string price = CharacterSelectManager.Instance.GetCharacterPrice();
        _characterPriceTxt.text = price;
        _characterPriceTxt.transform.GetChild(0).GetComponent<TMP_Text>().text = _characterPriceTxt.text;
    }
    public void PreviousCharacter()
    {
        CharacterSelectManager.Instance.PreviousCharacter();
        string name = CharacterSelectManager.Instance.GetCharacterName();
        _characterNameTxt.text = name;
        _characterNameTxt.transform.GetChild(0).GetComponent<TMP_Text>().text = _characterNameTxt.text;
        string price = CharacterSelectManager.Instance.GetCharacterPrice();
        _characterPriceTxt.text = price;
        _characterPriceTxt.transform.GetChild(0).GetComponent<TMP_Text>().text = _characterPriceTxt.text;
    }
    public void BuyCharacter()
    {
        CharacterSelectManager.Instance.BuyCharacter();
        _totalBookTxt.text = $"{PlayerStats.coinCount}";
        _totalBookTxt.transform.GetChild(0).GetComponent<TMP_Text>().text = _totalBookTxt.text;
    }
    public void SelectCharacter()
    {
        CharacterSelectManager.Instance.SelectCharacter();
    }
}
