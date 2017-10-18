using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Package : MonoBehaviour
{
    //numberOfShelves soll im Editor einstellbar sein, damit man einfach Shelves hinzufügen kann.
    public int numberOfShelves;
    public int targetShelfNumber;

	void Awake ()
	{
        //Beim Erzeugen eines Package soll es sich automatisch ein zufälliges, verfügbares Shelf als Ziel aussuchen.
	    targetShelfNumber = Random.Range(0, numberOfShelves);
	}
}
