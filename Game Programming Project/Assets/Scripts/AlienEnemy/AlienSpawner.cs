using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    public GameObject theEnemy;
    public GameObject thePlayer;
    public int xPos;
    public int zPos;
    public int enemyCount;
    float yVal;
    public int spawnRange;
    public int spawnCount;
    private bool isOnCapacity;


    private void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {

        while (true)
        {
            if (enemyCount < spawnCount)
            {
                xPos = Random.Range((int)thePlayer.transform.position.x - spawnRange, (int)thePlayer.transform.position.x + spawnRange);
                zPos = Random.Range((int)thePlayer.transform.position.z - spawnRange, (int)thePlayer.transform.position.z + spawnRange);
                yVal = Terrain.activeTerrain.SampleHeight(new Vector3(xPos, 0, zPos));
                Instantiate(theEnemy, new Vector3(xPos, yVal + 5f, zPos), Quaternion.identity);
                yield return new WaitForSeconds(1);
                enemyCount += 1;
            }

            else yield return new WaitForSeconds(1);
        }
    }
}
