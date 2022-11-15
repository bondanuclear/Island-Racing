using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private void Awake() {
        GameObject[] musicObjects = GameObject.FindGameObjectsWithTag("Music");
        if(musicObjects.Length > 1)
        {
            //Debug.Log(musicObjects.Length + "length of music objects array");
            Destroy(this.gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }
}
