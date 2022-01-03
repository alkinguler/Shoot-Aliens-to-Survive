using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoPick : MonoBehaviour
{
    public GameObject gun, ammoSpawnerEvent;
    private GunScript gunRef;
    AudioSource audioSource;
    public AudioClip pickAmmoAudio;
    private int boxAmmo;
    ammoSpawn ammoSpawner;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        gunRef = gun.GetComponent<GunScript>();
        ammoSpawner = ammoSpawnerEvent.GetComponent<ammoSpawn>();

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            gunRef.ammoInPocket += 30;
        }   
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            audioSource.clip = pickAmmoAudio;
            audioSource.Play();
            StartCoroutine(DelayAndDestroy(0.2f));
            ammoSpawner.currentAmmoBoxCount -= 1;
        }
    }

    IEnumerator DelayAndDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
