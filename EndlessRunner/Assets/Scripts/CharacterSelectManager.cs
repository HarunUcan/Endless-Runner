using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectManager : MonoBehaviour
{
    public static CharacterSelectManager Instance;

    public List<GameObject> Characters;
    private int _selectedCharacterIndex;
    private int _currentCharacterIndex;

    [SerializeField] private GameObject _buyButton;
    [SerializeField] private GameObject _selectButton;
    [SerializeField] private GameObject _priceTxt;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        JsonSaveLoad jsonSaveLoad = new JsonSaveLoad();
        jsonSaveLoad.LoadData();
        jsonSaveLoad.SaveData();
        
    }
    private void Start()
    {

        _selectedCharacterIndex = PlayerStats.selectedCharacterIndex;
        _currentCharacterIndex = _selectedCharacterIndex;
        foreach (var character in Characters)
        {
            if(PlayerStats.BoughtCharacters.Contains(Characters.IndexOf(character))) // eðer BoughtCharacters listesi Characters listesindeki indexi içeriyorsa
                character.GetComponent<Character>().IsPurchased = true;
            else
                character.GetComponent<Character>().IsPurchased = false;

            if (Characters.IndexOf(character) == _selectedCharacterIndex)
                character.GetComponent<Character>().IsSelected = true;
            else
                character.GetComponent<Character>().IsSelected = false;


            character.SetActive(false);

        }
        Characters[_selectedCharacterIndex].SetActive(true);
    }

    public void SelectCharacter(int index)
    {
        if (index == _selectedCharacterIndex)
            return;
        

        Characters[_selectedCharacterIndex].GetComponent<Character>().IsSelected = false;
        Characters[index].GetComponent<Character>().IsSelected = true;
        foreach (var character in Characters)
            character.SetActive(false);
        Characters[index].SetActive(true);
        _selectedCharacterIndex = index;
        PlayerStats.selectedCharacterIndex = _selectedCharacterIndex;
        JsonSaveLoad jsonSaveLoad = new JsonSaveLoad();
        jsonSaveLoad.SaveData();
    }
    public void SelectCharacter()
    {
        SelectCharacter(_currentCharacterIndex);
        _selectButton.SetActive(false);
    }
    public void NextCharacter()
    {
        _currentCharacterIndex++;
        if (_currentCharacterIndex >= Characters.Count)
            _currentCharacterIndex = 0;

        foreach (var character in Characters)
            character.SetActive(false);

        Characters[_currentCharacterIndex].SetActive(true);

        CheckBoughtCharacter();
        CheckSelectedCharacter();
    }
    public void PreviousCharacter()
    {
        _currentCharacterIndex--;
        if (_currentCharacterIndex < 0)
            _currentCharacterIndex = Characters.Count - 1;

        foreach (var character in Characters)
            character.SetActive(false);
        
        Characters[_currentCharacterIndex].SetActive(true);

        CheckBoughtCharacter();
        CheckSelectedCharacter();
    }
    public void BuyCharacter()
    {
        if (PlayerStats.BoughtCharacters.Contains(_currentCharacterIndex))
        {
            SelectCharacter(_currentCharacterIndex);
            return;
        }
        Character character = Characters[_currentCharacterIndex].GetComponent<Character>();
        if (PlayerStats.coinCount >= character.Price)
        {
            PlayerStats.coinCount -= (int)character.Price;
            PlayerStats.BoughtCharacters.Add(_currentCharacterIndex);
            _buyButton.SetActive(false);
            _selectButton.SetActive(true);
            _priceTxt.SetActive(false);
        }
        JsonSaveLoad jsonSaveLoad = new JsonSaveLoad();
        jsonSaveLoad.SaveData();
    }

    public void CheckBoughtCharacter()
    {
        if (PlayerStats.BoughtCharacters.Contains(_currentCharacterIndex)) // eðer BoughtCharacters listesi _currentCharacterIndex içeriyorsa
        {
            _buyButton.SetActive(false);
            _selectButton.SetActive(true);
            _priceTxt.SetActive(false);
        }
        else
        {
            _buyButton.SetActive(true);
            _selectButton.SetActive(false);
            _priceTxt.SetActive(true);
        }
    }
    public void CheckSelectedCharacter()
    {
        if (_currentCharacterIndex == _selectedCharacterIndex)
            _selectButton.SetActive(false);
        else
            _selectButton.SetActive(true);
    }
    public void ResetBuyPanel()
    {
        foreach (var character in Characters)
        {
            if(PlayerStats.selectedCharacterIndex == Characters.IndexOf(character))
            {
                character.SetActive(true);
                _currentCharacterIndex = PlayerStats.selectedCharacterIndex;
            }
            else
                character.SetActive(false);
        }
    }
    public string GetCharacterName()
    {
        return Characters[_currentCharacterIndex].GetComponent<Character>().CharacterName;
    }
    public string GetCharacterPrice()
    {
        return Characters[_currentCharacterIndex].GetComponent<Character>().Price.ToString();
    }
}
