using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PistolScript: MonoBehaviour
{

    //animation
    Animator pistolAnimator;

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
    public float ammoInGun = 12;
    public char ammoInPocket = '-';
    public float ammoMax = 12;
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
        pistolAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        //for animation
        pistolAnimator.SetFloat("speed", Character.velocity.magnitude);

        //ammunation
        ammoCounter.text = ammoInGun.ToString();
        pocketAmmoCounter.text = ammoInPocket.ToString();


        addableAmmo = ammoMax - ammoInGun;

        //reload
        if (Input.GetKeyDown(KeyCode.R) && addableAmmo > 0)
        {
            if (Time.time > reloadTimer)
            {
                StartCoroutine(Reload());
                reloadTimer = Time.time + reloadCoolDown;
                canFire = false;
            }
        }

        //firing
        if (Input.GetKeyDown(KeyCode.Mouse0) && canFire && Time.time > gunTimer && ammoInGun > 0)
        {
            Shoot();
            ammoInGun--;    //decreasing ammo
            gunTimer = Time.time + gunCoolDown;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && ammoInGun == 0)
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
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, ~IgnoreMe))
        {
            Debug.Log(hit.transform.name);


            //run the fire animation when shoot
            pistolAnimator.Play("firePistol", -1, 0f);

            //for impact of bullet
            if (hit.transform.tag == "Metal")
            {
                Instantiate(impactMetalEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else if (hit.transform.tag == "Ground")
            {
                Instantiate(impactGroundEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else if (hit.transform.tag == "Enemy")
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
        pistolAnimator.SetBool("isReloading", true);


        yield return new WaitForSeconds(0.3f);
        pistolAnimator.SetBool("isReloading", false);

        audioSource.clip = reloadSound;
        audioSource.Play();

        yield return new WaitForSeconds(1.4f);
        ammoInGun += addableAmmo;
        canFire = true;

    }
}
