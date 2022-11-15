using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class DestinationGate : MonoBehaviour
{
    [Header("Controls and sounds of the car")]    
    [SerializeField] GameObject currentCar;
    [SerializeField] GameObject revvingObject;
    [Header("Finish results")]
    [SerializeField] int points;
    public int Points {get; set;}
    [SerializeField] int awardPoints = 0;
    [SerializeField] bool hasFinished = false;
    float timePassed = 0;
    GameData gameData;
    [SerializeField] TextMeshProUGUI amountOfPoints;
    [SerializeField] TextMeshProUGUI timeSpent;
    [SerializeField] GameObject panel; 
    [SerializeField] GameObject[] otherUI;
    [SerializeField] GameObject returnButton;
    [SerializeField] Button adButton;
    [Header("Index of the current level")]
    [Range(0,14)][SerializeField] int indexOfLevel;
    private void Awake() {
        Time.timeScale = 1f;
        gameData = SavingSystem.Load();
        currentCar = GameObject.FindWithTag("Player");
        
        //Debug.Log(gameData.totalPoints);
    }
    private void Start() {
        revvingObject = GameObject.FindWithTag("Revving");
    }
    void Update()
    {
        if(!hasFinished)
            timePassed += Time.deltaTime;
           
    }
    // think of a way to end a level
    private void OnTriggerEnter(Collider other)
    {
        
        LevelComplete();
        
    }

    private void LevelComplete()
    {
        DisableUI();
        DisableControlsAndSound();
        hasFinished = true;
        awardPoints = (int)(points / timePassed);
        gameData.totalPoints += awardPoints;
        //Debug.Log("INDEX " + indexOfLevel);
        gameData.levelsPassed[indexOfLevel] = true;
        //GameManager.CheckArray(gameData.levelsPassed);
        SavingSystem.Save(gameData);
        //Debug.Log("time passed" + timePassed);
        StartCoroutine(SumUpResults());
    }

    private void DisableControlsAndSound()
    {
        currentCar.GetComponent<CarController>().enabled = false;
        currentCar.GetComponent<CarSounds>().enabled = false;
        revvingObject.SetActive(false);
    }

    IEnumerator SumUpResults()
    {
        Time.timeScale = 0.4f;
        yield return new WaitForSeconds(1f);
        panel.SetActive(true);
        yield return new WaitForSeconds(1f);
        amountOfPoints.text = "Amount of points: " + awardPoints;
        amountOfPoints.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        timeSpent.text = "Seconds needed: " + String.Format("{0:0.0}", timePassed); 
        timeSpent.gameObject.SetActive(true);
        adButton.gameObject.SetActive(true);
        returnButton.SetActive(true);
    }

    private void DisableUI()
    {
        foreach(var item in otherUI)
        {
            item.SetActive(false);
        }
    }
    // double the points after watching reward ad
    public void DoublePoints()
    {
        gameData.totalPoints += awardPoints;
        Debug.Log(gameData.totalPoints  + " total points after adding " + awardPoints);
        SavingSystem.Save(gameData);
    }
    public void WatchAdvertisement()
    {
        AdManager.instance.ShowAd(this);
        adButton.interactable = false;
    }
}
