using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    [SerializeField] GameObject controlPanel;
    [SerializeField] GameObject pausePanel;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null) return;
        else
        {
            instance = this;
        }
    }
    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        controlPanel.SetActive(false);
        pausePanel.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        controlPanel.SetActive(true);
        pausePanel.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
