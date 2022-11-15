using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    
    GameData gameData;
    public static bool hasInstantiated = false;
    public static GameObject carInstance;
    public static float motorForce;
    public static float brakeForce;
    private void Awake()
    {
        hasInstantiated = false;
        if(hasInstantiated)
        {
            Debug.LogWarning("Already instantiated");
            return;  
        } 
        else
        {
            gameData = SavingSystem.Load();
            //Debug.Log(gameData.carToLoad);
            carInstance = Instantiate(Resources.Load(gameData.carToLoad, typeof(GameObject)), FindObjectOfType<CarController>().transform) as GameObject;
            motorForce = gameData.currentCarMotorForce;
            brakeForce = gameData.currentCarBrakeForce;
            hasInstantiated = true;
        }
        Destroy(GameObject.FindGameObjectWithTag("Music"));
    }
}
