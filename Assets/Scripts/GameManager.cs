using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject levelsPanel;
    [SerializeField]  Color levelColorBlocked;
    [SerializeField] Color levelColorUnlocked; 
    List<EventTrigger> triggers = new List<EventTrigger>();
    GameData gameData;
    private void Awake()
    {
        FillTriggersList();
        gameData = SavingSystem.Load();
        AudioManager audio = FindObjectOfType<AudioManager>();
        audio.GetComponent<AudioSource>().volume = gameData.musicLoudness;
        
    }

    

    private void FillTriggersList()
    {
        foreach (Transform level in levelsPanel.transform)
        {
            triggers.Add(level.GetComponent<EventTrigger>());
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        bool[] levelsPassed = gameData.levelsPassed;
        //CheckArray(levelsPassed);
        if(levelsPassed.Length != triggers.Count) 
        {
            Debug.LogWarning("Data structures of levels are not the same size");
            return;
        }
        for(int i = 0; i < levelsPassed.Length; i++)
        {
            
            triggers[i].enabled = levelsPassed[i];
            if (levelsPassed[i] == false)
            {
                triggers[i].GetComponent<Image>().color = levelColorBlocked; 
            } else if(levelsPassed[i] == true)
            {
                triggers[i].GetComponent<Image>().color = levelColorUnlocked;

            }
            
        }
        
    }

    // public static void CheckArray(bool[] levels)
    // {
    //     for(int i = 0; i < levels.Length; i++)
    //     {
    //         Debug.Log(levels[i] + " #" + i);
    //     }
    // }
}
