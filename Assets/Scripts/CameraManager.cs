using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    [SerializeField] float timeBetweenMoves = 0.9f;
    new Camera camera;
    [SerializeField] Transform cameraSpotsParent;
    [SerializeField] List<Vector3> cameraSpots = new List<Vector3>();
    // static bool canClick = true;
    // public static bool CanClick {get {return canClick;}}
    static int currentIndex = 0;
    public static int CurrentIndex {get{return currentIndex;}}
    
    public List<GameObject> descriptionTexts = new List<GameObject>();
    public List<TextMeshProUGUI> carUnlockText = new List<TextMeshProUGUI>();
    public List<GameObject> characteristicsTexts = new List<GameObject>();
    private void Awake() {
        if(instance != null) return;
        else instance = this;
        currentIndex = 0;
        
        
        foreach(Transform spot in cameraSpotsParent)
        {
            cameraSpots.Add(spot.transform.position);
            
        }
        camera = Camera.main;
        camera.transform.position = cameraSpots[currentIndex];
    }
    private void Start() {
        
        
    }
    // public void MoveCamera(int shift)
    // {
    //     if(canClick)
    //     {
    //         int newIndex = currentIndex + shift;
            
            
    //         newIndex %= cameraSpots.Count;
    //         if(newIndex < 0)
    //         {
    //             newIndex = cameraSpots.Count - 1;
    //         }
    //         currentIndex = newIndex;
            
    //         StartCoroutine(MoveToAnotherPoint(newIndex));
    //     }
       
        
    // }
    
    // private IEnumerator MoveToAnotherPoint(int index)
    // {
    //     Debug.Log("Coroutine was called ");
    //     float passedTime = 0;
    
    //     //canClick = false;
        
    //     while(passedTime <= timeBetweenMoves)
    //     {
    //         passedTime += Time.deltaTime;
    //         Debug.Log(passedTime + " passed time ");
    //         camera.transform.position = Vector3.Lerp(camera.transform.position, cameraSpots[index], passedTime/timeBetweenMoves);
    //         yield return null;
    //     }    
    //     //canClick = true;
    //     yield return null;
    // }
    #region swipeControl
    private Vector2 startingTouchPosition;
    private Vector2 endTouchPosition;
    [SerializeField] float swipeRange = 100f;
    [SerializeField ] bool stopTouch = false;
    private void Update() {
        
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Began)
        {
            startingTouchPosition = Input.GetTouch(0).position;
        }
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Moved )
        {
            endTouchPosition = Input.GetTouch(0).position;
            Vector2 distance = (endTouchPosition - startingTouchPosition);
            if(!stopTouch)
            {
                if (distance.x < -swipeRange)
                {
                    NextCarIndex();
                    stopTouch = true;
                }
                if (distance.x > swipeRange)
                {
                    PreviousCarIndex();
                    stopTouch = true;
                }
            }    
            
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Ended)
        {   
            stopTouch = false;
        }
    }

    private void PreviousCarIndex()
    {
        
        int newIndex = currentIndex - 1;
        descriptionTexts[currentIndex].SetActive(false);
        characteristicsTexts[currentIndex].SetActive(false);
        newIndex %= cameraSpots.Count;
        if (newIndex < 0)
        {
            newIndex = cameraSpots.Count - 1;
        }
        descriptionTexts[newIndex].SetActive(true);
        characteristicsTexts[newIndex].SetActive(true);
        currentIndex = newIndex;
        camera.transform.position = cameraSpots[newIndex];
        //camera.transform.position = Vector3.Lerp(camera.transform.position, cameraSpots[newIndex], 0.125f);
    }

    private void NextCarIndex()
    {
        int newIndex = currentIndex + 1;
        newIndex %= cameraSpots.Count;
        descriptionTexts[currentIndex].SetActive(false);
        characteristicsTexts[currentIndex].SetActive(false);
        descriptionTexts[newIndex].SetActive(true);
        characteristicsTexts[newIndex].SetActive(true);
        currentIndex = newIndex;
        camera.transform.position = cameraSpots[newIndex];
        
    }
    #endregion
}
