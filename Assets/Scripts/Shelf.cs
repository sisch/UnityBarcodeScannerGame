using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Shelf : MonoBehaviour
{

    // Ein Shelf soll seine Nummer (einstellbar) kennen und sein Label (um die Nummer anzuzeigen)
    public int shelfNumber;
    public Text LabelText;

    // Ein Shelf soll außerdem ein Licht haben und drei Zustandsfarben
    // Blau: Warte auf Input
    // Rot: Input ungültig
    // Grün: Input gültig
    public Light shelfLight;
    public Color correctColor;
    public Color emptyColor;
    public Color wrongColor;


    public string shelfName;
    public bool isEmpty;

    void Start ()
	{
        // Mit leerem Shelf starten.
        // Und die Anzeige entsprechend der shelfNumber ändern.
	    emptyShelf();
        LabelText.text = (shelfNumber+1).ToString();
	}

    void OnTriggerEnter(Collider other)
    {
        // Wenn irgendein Objekt in das Fach gelegt wird, wird der Status isEmpty auf falsch gesetzt
        isEmpty = false;

        // Wenn der hinzugefügte Gegenstand kein Package ist (anhand des Tags)
        if (!other.CompareTag("Package"))
        {
            Debug.LogFormat("Nasty you! You tried to place object {0} in slot {1}", other.gameObject.name, shelfNumber+1);
            shelfLight.color = wrongColor;
            return;
        }

        // Hier der Code wird (wg. return) nur ausgeführt, wenn es ein Package ist.
        // Hier wird dann geprüft, ob es das richtige Package für das Fach ist oder nicht.
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
        // Wenn ein Package in das korrekte Fach gelegt wurde, soll das gameObject des Package nach einer halben Sekunde zerstört werden.
        Destroy(package, 0.5f);
        // Außerdem soll mit der gleichen Verzögerung auch der isEmpty-Status wieder angepasst werden.
        // Invoke ruft Methoden über den Namen auf und akzeptiert wie Destroy() einen Delay als zweites Argument
        Invoke("emptyShelf", 0.5f);
        shelfLight.color = correctColor;
    }

    void emptyShelf()
    {
        isEmpty = true;
        shelfLight.color = emptyColor;
    }
}
