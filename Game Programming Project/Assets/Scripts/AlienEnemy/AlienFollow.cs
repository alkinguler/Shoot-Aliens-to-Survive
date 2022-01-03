using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienFollow : MonoBehaviour
{
    public GameObject thePlayer;
    public float targetDistance;
    public float allowedRange = 10;
    public GameObject theEnemy;
    public float enemySpeed;
    public int attackTrigger;
    public RaycastHit Shot;
    public PlayerStats healthRef;
    public bool isDead;

    public float initSpeed;
    public int isAttacking;
    
    private void Update()
    {
        isDead = theEnemy.GetComponent<Target>().isDead;
        transform.LookAt(thePlayer.transform);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Shot))
        {

            targetDistance = Shot.distance;
            if (targetDistance < allowedRange)
            {
                

                if (attackTrigger == 0 && !isDead)
                {
                    theEnemy.GetComponent<Animation>().Play("walk");
                    if (Pausemenu.IsPaused || DeathMenu.IsDead)
                    {
                        enemySpeed = 0f;
                    }

                    else 
                    {
                        enemySpeed = initSpeed;
                    }
                    transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, enemySpeed);
                }
            }
        }

        if (attackTrigger == 1 && !isDead) 
        {
            if (isAttacking == 0) StartCoroutine(EnemyDamage());
            theEnemy.GetComponent<Animation>().Play("attack1");
            
            
        }

        if (isDead)
        {
           
            theEnemy.GetComponent<Animation>().Play("die2");
           
        }


    }

    private void Start()
    {
        initSpeed = enemySpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
           
            attackTrigger = 1;
          
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            attackTrigger = 0;
        }
    }

    IEnumerator EnemyDamage()
    {

        healthRef = thePlayer.GetComponent<PlayerStats>();

        isAttacking = 1;
        yield return new WaitForSeconds(0.50f);
        if (attackTrigger == 1)
        {
            healthRef.TakeDamage(10);
        }
        
        yield return new WaitForSeconds(0.4f);
        isAttacking = 0;


    }
}
