using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    public static bool IsDead = false;

    public GameObject DeadMenuUI;
    public GameObject CounterUI;
    public GameObject AmmoUI;
    public GameObject HealthUI;
    public GameObject StaminaUI;
    public GameObject CrosshairUI;

    // Update is called once per frame
    void Update()
    {
        if (IsDead == true)
        {
            Pausemenu.NeedCursor = true;
            DeadMenuUI.SetActive(true);
            CounterUI.SetActive(false);
            AmmoUI.SetActive(false);
            HealthUI.SetActive(false);
            StaminaUI.SetActive(false);
            CrosshairUI.SetActive(false);
            Time.timeScale = 0f;
        }
    }
    public void Respawn()
    {
        Time.timeScale = 1f;
        Application.LoadLevel(Application.loadedLevel);
        Pausemenu.NeedCursor = false;
        DeadMenuUI.SetActive(false);
        CounterUI.SetActive(true);
        AmmoUI.SetActive(true);
        HealthUI.SetActive(true);
        StaminaUI.SetActive(true);
        CrosshairUI.SetActive(true);
        IsDead = false;
    }

}
