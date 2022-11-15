using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GarageLocationManager : MonoBehaviour
{
    GameData gameData;
    [SerializeField] List<Image> imagesList = new List<Image>();
    [SerializeField] Color colorClosed;
    [SerializeField] List<GameObject> cars = new List<GameObject>();
    [SerializeField] Image chooseText;
    [SerializeField] Color chooseTextColor;
    [SerializeField] string carToSave = "RockBuggy";
    int currentCarIndex;
    int previousCarIndex;
    bool[] canChooseCar = new bool[4];
    private void Awake() {
       gameData = SavingSystem.Load();
       currentCarIndex = gameData.savedCarIndex;
       for(int i = 0; i < canChooseCar.Length; i++)
       {
            if(i == currentCarIndex) canChooseCar[i] = false;
            else canChooseCar[i] = true;
       }
       cars[currentCarIndex].SetActive(true);
    }
    
    void Start()
    {
       bool[] openedCharacters = gameData.charactersUnlocked;
       for(int i = 0; i < openedCharacters.Length; i++)
       {
            if(openedCharacters[i] == false)
            {
                imagesList[i].GetComponent<EventTrigger>().enabled = false;
                imagesList[i].GetComponentInChildren<TextMeshProUGUI>().color = colorClosed;
            }
       }
       
    }
    public void SpawnCar(int number)
    {
        previousCarIndex = currentCarIndex;
        cars[currentCarIndex].SetActive(false);
        cars[number].SetActive(true);
        currentCarIndex = number;
    }
    public void ChooseCar()
    {
        canChooseCar[GetFalseComponentIndex()] = true;
        canChooseCar[currentCarIndex] = false;
        carToSave = cars[currentCarIndex].GetComponent<Car>().carType.ToString();
        gameData.carToLoad = carToSave;
        gameData.savedCarIndex = currentCarIndex;
        gameData.currentCarMotorForce = cars[currentCarIndex].GetComponent<Car>().motorForce;
        gameData.currentCarBrakeForce = cars[currentCarIndex].GetComponent<Car>().brakeForce;
        
        SavingSystem.Save(gameData);        
    }
    int GetFalseComponentIndex()
    {
        for (int i = 0; i < canChooseCar.Length; i++)
        {
            if (canChooseCar[i]) continue;
            else return i;
        }
        return 0;
    }
    
    void Update()
    {
        if (canChooseCar[currentCarIndex] == false) // cars[currentCarIndex].activeSelf && 
        {
            chooseText.GetComponentInChildren<TextMeshProUGUI>().color = chooseTextColor;
            chooseText.GetComponent<EventTrigger>().enabled = false;
        }
        else
        {
            chooseText.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            chooseText.GetComponent<EventTrigger>().enabled = true;
        }
    }
    
}
