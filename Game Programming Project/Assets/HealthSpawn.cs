using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpawn : MonoBehaviour
{
    public GameObject thePlayer, theBox;
    private float xPos, yPos, zPos;
    public int healthSpawnRange, maxHealthBoxSpawnCount;
    public int currentHealthBoxCount;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAmmoBox());
    }

    IEnumerator SpawnAmmoBox()
    {

        while (true)
        {
            if (currentHealthBoxCount < maxHealthBoxSpawnCount)
            {
                xPos = Random.Range(thePlayer.transform.position.x - healthSpawnRange, thePlayer.transform.position.x + healthSpawnRange);
                zPos = Random.Range(thePlayer.transform.position.z - healthSpawnRange, thePlayer.transform.position.z + healthSpawnRange);
                yPos = Terrain.activeTerrain.SampleHeight(new Vector3(xPos, 0f, zPos));
                Instantiate(theBox, new Vector3(xPos, yPos + 0f, zPos), Quaternion.identity);
                yield return new WaitForSeconds(1);
                currentHealthBoxCount += 1;
            }
            else yield return new WaitForSeconds(1);
        }



    }



}
