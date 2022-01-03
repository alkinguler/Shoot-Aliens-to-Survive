using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GunScript : MonoBehaviour
{

    //animation
    Animator gunAnimator;

    //for movement velocity
    public CharacterController Character;

    //for raycast ignoring
    public LayerMask IgnoreMe;
    
    //impact of bullet
    public GameObject impactMetalEffect;
    public GameObject impactGroundEffect;
    public GameObject impactEnemyEffect;

    //gun stat
    public float damage = 10f;
    public float range = 100f;

    //ammunation
    public float ammoInGun = 30;
    public float ammoInPocket = 120;
    public float ammoMax = 30;
    private float addableAmmo;
    public float reloadCoolDown = 2;
    public float reloadTimer;

    public Text ammoCounter;
    public Text pocketAmmoCounter;

    //firing speed
    public float gunTimer;
    public float gunCoolDown = 0.08f;

    //ammo check
    public bool canFire;

    //for raycast and muzzleflash
    public Camera fpsCam;
    public ParticleSystem ammoFlash;

    //audio
    AudioSource audioSource;
    public AudioClip fireSound;
    public AudioClip reloadSound;
    public AudioClip emptyGunSound;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gunAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        //for animation
        gunAnimator.SetFloat("speed", Character.velocity.magnitude);

        //gunAnimator.SetBool("isGrounded", Character.isGrounded); 

        //ammunation
        ammoCounter.text = ammoInGun.ToString();
        pocketAmmoCounter.text = ammoInPocket.ToString();

        if (ammoInPocket > 30)
            addableAmmo = ammoMax - ammoInGun;
        else
            addableAmmo = ammoInPocket;


        //reload
        if (Input.GetKeyDown(KeyCode.R) && addableAmmo > 0 && ammoInPocket > 0)
        {
            if (Time.time > reloadTimer)
            {
                StartCoroutine(Reload());
                reloadTimer = Time.time + reloadCoolDown;
                canFire = false;
            }
        }

        //firing

        if (Input.GetKey(KeyCode.Mouse0) && canFire && Time.time > gunTimer && ammoInGun > 0 && Pausemenu.IsPaused == false)
        {
            Shoot();
            ammoInGun--;    //decreasing ammo
            gunTimer = Time.time + gunCoolDown;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && ammoInGun == 0 && Pausemenu.IsPaused == false)
        {
            audioSource.clip = emptyGunSound;
            audioSource.Play();
        }
        
    }

    void Shoot()
    {
        ammoFlash.Play();
        audioSource.Play();

        audioSource.clip = fireSound;

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, ~IgnoreMe))
        {
            Debug.Log(hit.transform.name);


            //run the fire animation when shoot
            gunAnimator.Play("fire", -1, 0f);

            //for impact of bullet
            if(hit.transform.tag == "Metal")
            { 
                Instantiate(impactMetalEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else if(hit.transform.tag == "Ground")
            {
                Instantiate(impactGroundEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else if(hit.transform.tag == "Enemy")
            {
                Instantiate(impactEnemyEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }

    IEnumerator Reload()
    {
        gunAnimator.SetBool("isReloading", true);
        

        yield return new WaitForSeconds(0.3f);
        gunAnimator.SetBool("isReloading", false);

        audioSource.clip = reloadSound;
        audioSource.Play();

        yield return new WaitForSeconds(1.4f);
        ammoInGun += addableAmmo;
        ammoInPocket -= addableAmmo;
        canFire = true;

    }
}
