using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] AudioManager audioManager;
    [Header("UI Parts")]
    [SerializeField] TextMeshProUGUI totalPointsText; 
    [SerializeField] Slider slider;  
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject settingsPanel;
    GameData gameData;
    private void Awake() {
        gameData = SavingSystem.Load();
        audioManager = FindObjectOfType<AudioManager>();
        RefreshData();
        
    }
    private void RefreshData()
    {
        totalPointsText.text = gameData.totalPoints.ToString();
        audioManager.GetComponent<AudioSource>().volume = gameData.musicLoudness;
        slider.value = gameData.musicLoudness;
    }
    public void SetMusicLoudness()
    {
        //Debug.Log("Slider value " + slider.value);
        audioManager.GetComponent<AudioSource>().volume = slider.value;
        
        
    }
    public void SaveData()
    {
        float volumeValue = audioManager.GetComponent<AudioSource>().volume;
        // rounding float value to 2 decimal points
        volumeValue = Mathf.Round(volumeValue*100.0f) * 0.01f;
        // Debug.Log(volumeValue);
        gameData.musicLoudness = volumeValue;
        SavingSystem.Save(gameData);
    }
    public void ProcessSettingsPanel()
    {
        mainMenuPanel.SetActive(!mainMenuPanel.activeSelf);
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }
    
}
