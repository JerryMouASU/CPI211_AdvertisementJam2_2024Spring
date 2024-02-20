using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public static bool paused;
    //public GameObject buttonToHide;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
       // buttonToHide.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(paused)
            {
                resumeGame();
            }else
            {
                pauseGame();
            }
        }

        
    }

    public void pauseGame()
    {
       // buttonToHide.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; 
        paused = true;
    }

    public void resumeGame()
    {
       // buttonToHide.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    public void resetLevel()
    {
        //buttonToHide.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
