using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolOfObjects : MonoBehaviour
{
    public static PoolOfObjects instance;
    [SerializeField] int size = 10;
    [SerializeField] GameObject spawnObject;
    private void Awake() {
        if(instance != null) return;
        else 
        {
            instance = this;
        }
    }
    private void Start() {
        
    }
    public void FillThePool()
    {
        for(int i = 0; i < size; i++)
        {
            GameObject poolObject = Instantiate(spawnObject, transform);
            poolObject.SetActive(false);
        }
    }
    public GameObject GetObjectFromPool()
    {
        foreach(Transform item in transform)
        {
           // Debug.Log("iteration");
            if(item.gameObject.activeSelf == false)
            {
                return item.gameObject;
            }
        }
        return null;
    }
}
