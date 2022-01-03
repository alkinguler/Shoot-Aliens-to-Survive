using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausemenu : MonoBehaviour
{

    public static bool IsPaused = false;
    public static bool NeedCursor = false;

    public GameObject pauseMenuUI;
    public GameObject CounterUI;
    public GameObject AmmoUI;
    public GameObject HealthUI;
    public GameObject StaminaUI;
    public GameObject CrosshairUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (IsPaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        CounterUI.SetActive(true);
        AmmoUI.SetActive(true);
        HealthUI.SetActive(true);
        StaminaUI.SetActive(true);
        CrosshairUI.SetActive(true);
        Time.timeScale = 1f;
        IsPaused = false;
        NeedCursor = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        CounterUI.SetActive(false);
        AmmoUI.SetActive(false);
        HealthUI.SetActive(false);
        StaminaUI.SetActive(false);
        CrosshairUI.SetActive(false);
        IsPaused = true;
        NeedCursor = true;
    }
    
    public void LoadMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
