using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{

    public float health = 50f;
    public GameObject theEnemy;
    public bool isDead;
    public AlienSpawner spawner;
    public GameObject spawnerObject;
    public GameObject thePlayer;
    public PlayerStats killCounter;
    public AlienSpawner theEnemySpawnCount;
    public int prevKillCounter = 0;


    private void Start()
    {

        spawner = spawnerObject.GetComponent<AlienSpawner>();
        killCounter = thePlayer.GetComponent<PlayerStats>();
        
      

    }

    private void Awake()
    {
        
        if (prevKillCounter != killCounter.killCount && killCounter.killCount % 5 == 0)
        {
            Debug.Log(prevKillCounter + "|||||||||" + killCounter.killCount);
            spawner.spawnCount += 1;
            prevKillCounter = killCounter.killCount;


        }
    }

    private void Update()
    {





    }
    public void TakeDamage(float damage)
    {

        health -= damage;
        if (health <= 0f)
        {
            isDead = true;
            Die(1f);
        }

        if (health == 0)
        {
            spawner.enemyCount -= 1;
            killCounter.killCount += 1;
        }
    }



    public void Wave(float secs)
    {
    }

    public void Die(float secs)
    {

        Destroy(gameObject, secs);
    }

    
}