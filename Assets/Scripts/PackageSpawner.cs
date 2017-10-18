using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***
 * Der PackageSpawner soll immer dann ein neues Package erzeugen, wenn er selbst keins mehr hat.
 */
public class PackageSpawner : MonoBehaviour
{
    public GameObject packagePrefab;
    public GameObject spawnPoint;
	
	// Update is called once per frame
	void Update () {
	    if (spawnPoint.transform.childCount < 1)
	    {
            //Das neue Package soll in der Hierarchie als Kind vom Spawnpoint und an der Position des SpawnPoints erzeugt werden.
	        GameObject go = Instantiate(packagePrefab);
	        go.transform.SetParent(spawnPoint.transform);
	        go.transform.localPosition = Vector3.zero;

            //Alternative Schreibweise
	        //Instantiate(packagePrefab, spawnPoint.transform.position, Quaternion.identity, spawnPoint.transform);
	    }
	}
}
