using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCounterUID : MonoBehaviour
{
    public GameObject thePlayer;

    public Text killCountTxt;
    // Start is called before the first frame update
    void Start()
    {
        killCountTxt = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (thePlayer.GetComponent<PlayerStats>().killCount == 0 && DeathMenu.IsDead == false)
        {
            killCountTxt.text = " ";
        }
        else if (DeathMenu.IsDead == false)
        {
            killCountTxt.text = "" + thePlayer.GetComponent<PlayerStats>().killCount;
        }
        else
        {
            killCountTxt.text = "" + thePlayer.GetComponent<PlayerStats>().killCount;
        }
    }
}
