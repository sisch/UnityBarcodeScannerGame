using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScannerScript : MonoBehaviour
{
    public float timeout;
    public Transform scanner;
    public Text scannerDisplay;
    public float lineRendererDistance;
    private LineRenderer laser;
    private float timePassed;
    private bool canScan;
    private Valve.VR.InteractionSystem.Hand scannerHand;

    void Start ()
	{
	    // Leg die LineRenderer Komponente unter laser ab und setze die zweite Koordinate vom LineRenderer auf die 
        // einstellbare Distanz, konkreter auf (0,0,Distanz). Die erste Koordinate (Index 0) ist bei (0,0,0)
	    

        GameObject handRight = GameObject.Find("Hand1");
	    if (handRight != null)
	    {
	        scannerHand = handRight.GetComponent<Valve.VR.InteractionSystem.Hand>();
	        scanner = scannerHand.transform;
	    }
	    laser = scanner.GetComponent<LineRenderer>();
	    laser.SetPosition(1, scanner.forward * lineRendererDistance);
        canScan = true;
	    scannerDisplay.text = "-";
	}
    
    void Update ()
    {
        timePassed += Time.deltaTime;
        if (timePassed > timeout)
        {
            // nachdem Timeout wird der Laser und das Scannen wieder aktiviert
            canScan = true;
            laser.enabled = true;
        }
        // Wenn der Scanner bereit ist und der User klickt (Fire1-Taste drückt) soll gescannt werden.
        if (canScan && Input.GetButtonDown("Fire1"))
	    {
	        scan();
	        return;
	    }

        // Für VR
        if (canScan && scannerHand != null && scannerHand.GetStandardInteractionButtonDown())
        {
            Debug.Log("Trigger pressed");
            scan();
        }

        
    }

    void scan()
    {
        // Wenn ein Scanvorgang startet, deaktiviere den Scanner und starte den Timer neu.
        canScan = false;
        timePassed = 0;
        laser.enabled = false;

        // Bereite eine Variable vor, um herauszufinden, ob der Laserstrahl ein Objekt im Raum trifft.
        RaycastHit hit;
        if (!Physics.Raycast(scanner.position, scanner.forward, out hit, lineRendererDistance))
        {
            // Wenn KEIN Objekt getroffen wird, wird - auf dem Scanner angezeigt und die scan()-Methode bricht ab.
            scannerDisplay.text = "-";
            return;
        }
        // Wenn EIN Objekt getroffen wird, geht es hier weiter und das Objekt steht in der Variable hit

        // Wenn das getroffene Objekt ein Paket ist, wird die Zielnummer vom Paket geholt und auf dem Scanner
        // angezeigt. Anderenfalls wird - angezeigt.
        if (hit.transform.gameObject.CompareTag("Package"))
            {

                Package p = hit.transform.GetComponent<Package>();
                scannerDisplay.text = (p.targetShelfNumber + 1).ToString();
            }
            else
            {
                scannerDisplay.text = "-";
            }
        }
}
