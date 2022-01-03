using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static bool SecondRun = false;


    public void PlayGame()
    {
        
        if (Pausemenu.IsPaused == true)
        {
            Pausemenu.IsPaused = false;
            SecondRun = true;
        }
    
        if (DeathMenu.IsDead == true)
        {
            DeathMenu.IsDead = false;
            SecondRun = true;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame() 
    {
        Debug.Log("QUIT!");
        Application.Quit();    
    }
}
