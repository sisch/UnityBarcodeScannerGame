using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Shelf : MonoBehaviour
{

    public int shelfNumber;
    //public int[] shelfNumbers
    public Text LabelText;
    //public List<Text> labels;


    public Light shelfLight;
    public Color correctColor;
    public Color emptyColor;
    public Color wrongColor;


    public string shelfName;
    public bool isEmpty;
	// Use this for initialization
    void Start ()
	{
	    emptyShelf();
        LabelText.text = (shelfNumber+1).ToString();
	}

    void OnTriggerEnter(Collider other)
    {
        isEmpty = false;
        if (!other.CompareTag("Package"))
        {
            Debug.LogFormat("Nasty you! You tried to place object {0} in slot {1}", other.gameObject.name, shelfNumber+1);
            shelfLight.color = wrongColor;
            return;
        }
        Package p = other.GetComponent<Package>();
        if (p.targetShelfNumber == this.shelfNumber)
        {
            Debug.Log("Placed package in correct slot");
            CorrectPackage(other.gameObject);
        }
        else
        {
            shelfLight.color = wrongColor;
            Debug.Log("Placed package in wrong slot");
        }
    }

    void OnTriggerExit(Collider other)
    {
        emptyShelf();
    }

    void CorrectPackage(GameObject package)
    {
        Destroy(package, 0.5f);
        Invoke("emptyShelf", 0.5f);
        shelfLight.color = correctColor;
    }

    void emptyShelf()
    {
        isEmpty = true;
        shelfLight.color = emptyColor;
    }
}
