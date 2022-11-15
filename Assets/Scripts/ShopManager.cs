using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    const int AMOUNT_OF_CARS = 4;
    [Header("Prices of the cars")]
    [SerializeField] int[] prices = new int[AMOUNT_OF_CARS];

    [SerializeField] TextMeshProUGUI pointsText;
    [SerializeField] bool[] isAquired = new bool[AMOUNT_OF_CARS];
    [Header("Purchase button data")]
    [SerializeField] TextMeshProUGUI purchaseButtonText = null;
    [SerializeField] Color defaultButtonColor;
    [SerializeField] Color aquiredButtonColor;
    GameData gameData;
    private void Awake() {
        
        gameData = SavingSystem.Load();
        for(int i = 0; i < isAquired.Length; i++)
        {
            isAquired[i] = gameData.charactersUnlocked[i];
            CameraManager.instance.carUnlockText[i].text = "Acquired: " + isAquired[i];
        }
    }
    
    public void PurchaseButton()
    {
        if (gameData.totalPoints >= prices[CameraManager.CurrentIndex] && !isAquired[CameraManager.CurrentIndex])
        {
            gameData.totalPoints -= prices[CameraManager.CurrentIndex];
            isAquired[CameraManager.CurrentIndex] = true;
            gameData.charactersUnlocked[CameraManager.CurrentIndex] = true;
            CameraManager.instance.carUnlockText[CameraManager.CurrentIndex].text = "Acquired: true";
            SavingSystem.Save(gameData);
        }
    }
    // Update is called once per frame
    void Update()
    {
        pointsText.text = "Points: " + gameData.totalPoints;
        
        if(isAquired[CameraManager.CurrentIndex])
        {
            purchaseButtonText.color = aquiredButtonColor;
        }
        else
        {
            purchaseButtonText.color = defaultButtonColor;  
        }
        
    }
}
