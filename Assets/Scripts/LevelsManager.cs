using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelsManager : MonoBehaviour
{
    
    public void LoadLevel(int indexOfLevel)
    {
        SceneManager.LoadScene(indexOfLevel);
    }
    

}
