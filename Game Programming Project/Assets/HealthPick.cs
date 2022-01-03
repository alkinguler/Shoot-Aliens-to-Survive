using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPick : MonoBehaviour
{
    public GameObject thePlayer, healthSpawnerEvent;
    private PlayerStats healthRef;
    AudioSource audioSource;
    public AudioClip pickHealthAudio;
    private int boxAmmo;
    HealthSpawn healthSpawner;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        healthRef = thePlayer.GetComponent<PlayerStats>();
        healthSpawner = healthSpawnerEvent.GetComponent<HealthSpawn>();

    }

    // Update is called once per frame
    void Update()
    {

        
       
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            healthRef.IncreaseHealth(10);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            audioSource.clip = pickHealthAudio;
            audioSource.Play();
            StartCoroutine(DelayAndDestroy(0.2f));
            healthSpawner.currentHealthBoxCount -= 1;
        }
    }

    IEnumerator DelayAndDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
