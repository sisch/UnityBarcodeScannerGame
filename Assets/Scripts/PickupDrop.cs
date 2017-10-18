using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***
 * Dieses Script soll das Aufheben und Ablegen eines Package erlauben.
 * Auslöser soll der definierte Input "Use" sein.
 * Aufheben bedeutet, dass das Objekt relativ zur Kamera positioniert wird und dort bleibt,
 * also auch keine Gravitation mehr hat.
 * 
 * 
 */
public class PickupDrop : MonoBehaviour
{
    public GameObject camera;
    //Das Range Attribut ergänzt im Inspector das Eingabefeld um einen Slider
    [Range(1f, 3f)] public float pickupRange;
    private bool packageInHand;
    private GameObject currentPackage;

	void Start ()
	{
	    packageInHand = false;
	}
	
	void Update () {

        // Die If-Bedingung repariert den Wert von packageInHand, falls das Package in der Hand zerstört wurde.
	    if (currentPackage == null && packageInHand)
	    {
	        packageInHand = false;
	    }

        // Hier wird jedes Frame geschaut, ob UserInput vorliegt (im Beispiel die Taste E gedrückt wurde) 
        // Wenn das der Fall ist, und ein Package in der Hand ist, lass es fallen, sonst
        // wenn kein Package in der Hand ist, heb eins, das in der Nähe ist auf.
	    if (Input.GetButtonDown("Use"))
	    {
	        if (packageInHand)
	        {
	            Drop();
	        }
	        else
	        {
	            Pickup();
	        }
	    }
	}

    // Hebt ein Package in der Nähe (Umkreis wie in pickupRange angegeben)
    void Pickup()
    {
        currentPackage = GameObject.FindGameObjectWithTag("Package");
        Debug.Log(currentPackage);
        float distanceToPlayer = Vector3.Distance(currentPackage.transform.position, camera.transform.position);
        if (distanceToPlayer < pickupRange)
        {
            Debug.Log(distanceToPlayer);
            // Ordne das Package in der Hierarchie der Camera unter
            // setze die relative Position auf 0.3, 0.05, 1.0
            // setze den RigidBody vom Package auf kinematic
            currentPackage.transform.SetParent(camera.transform);
            currentPackage.transform.localPosition = new Vector3(0.3f,0.05f,1f);
            packageInHand = true;
            currentPackage.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    void Drop()
    {
        packageInHand = false;
        // Setze den RigidBody vom Package wieder auf non-kinematic
        // Entferne die Hierarchie Beziehung vom Package -> Package ist dann direktes Kind der Szene
        currentPackage.GetComponent<Rigidbody>().isKinematic = false;
        currentPackage.transform.parent = null;
    }
}
