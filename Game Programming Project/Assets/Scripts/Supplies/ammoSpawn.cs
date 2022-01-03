using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoSpawn : MonoBehaviour
{
    public GameObject thePlayer, theBox;
    private float xPos,yPos,zPos;
    public int ammoSpawnRange, maxAmmoBoxSpawnCount;
    public int currentAmmoBoxCount;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAmmoBox());
    }

    IEnumerator SpawnAmmoBox() 
    {

        while (true)
        {
            if(currentAmmoBoxCount < maxAmmoBoxSpawnCount)
            {
                xPos = Random.Range(thePlayer.transform.position.x - ammoSpawnRange, thePlayer.transform.position.x + ammoSpawnRange);
                zPos = Random.Range(thePlayer.transform.position.z - ammoSpawnRange, thePlayer.transform.position.z + ammoSpawnRange);
                yPos = Terrain.activeTerrain.SampleHeight(new Vector3(xPos, 0f, zPos));
                Instantiate(theBox, new Vector3(xPos, yPos + 0f, zPos), Quaternion.identity);
                yield return new WaitForSeconds(1);
                currentAmmoBoxCount += 1;
            }
            else yield return new WaitForSeconds(1);
        }

  

    }



}
