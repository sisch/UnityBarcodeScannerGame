using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Camera playerCamera;
    private GameObject player;

    private void Start()
    {
        // Mouse Capture anschalten
        Cursor.lockState = CursorLockMode.Locked;
        // Hier wird nochmal ein Verweis auf das aktuelle gameObject in player gespeichert
        // damit weiter unten im Code klar ist, dass es sich um den Player handelt. Rein kosmetisch.
        player = gameObject;
    }

    // In jedem Frame gibt es zwei Dinge zu tun. 
    // 1. Die Kameradrehung anhand der Mausbewegung aktualisieren
    // 2. Die Playerposition auf Basis der Bewegungstasten Inputs aktualisieren
    private void Update()
    {
        mouseLook();
        // move erhält hier als Argumente die beiden Bewegungsachsen (jeweils Werte zwischen -1 und 1,
        // die üblicherweise auf die Pfeiltasten oder WASD gebunden sind.
        move(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
    }


    private void mouseLook()
    {
        // Speichere die aktuelle Camera Rotation und die Mausbewegung in X und Y Richtung seit dem letzten Frame in je eine Variable
        Quaternion camRotation = playerCamera.transform.localRotation;
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        // Die neue Kamerarotation ist ein kombiniertes Quaternion aus der Drehung um die Y-Achse (Wanken),
        // der eigentlichen Kameradrehung
        // und der Drehung um die X-Achse (Neigen)
        playerCamera.transform.localRotation =
            Quaternion.Euler(0, x, 0) *
            camRotation *
            Quaternion.Euler(-y, 0, 0);
    }

    private void move(float forward, float right)
    {
        // Um die relative Bewegung des Player zu berechnen wird ein neuer Vektor 0, 0, 0 angelegt
        // dann wird die Forward Richtung mit dem Wert des User-Inputs multipliziert draufgerechnet
        // dann wird die Right Richtung mit dem Wert des User-Inputs multipliziert draufgerechnet
        // Schließlich wird die deltaTime einmultipliziert, um zu verhindern, dass die Bewegung in verschiedenen
        // Frames verschieden schnell läuft. moveSpeed wird einmultiliziert, damit die Laufgeschwindigkeit
        // Vom Editor aus einstellbar ist.
        Vector3 newPosition = Vector3.zero;
        newPosition += transform.forward * forward;
        newPosition += transform.right * right;
        newPosition *= moveSpeed * Time.deltaTime;
        // ACHTUNG, HIER HATTE ICH ETWAS VERGESSEN
        // Damit die Steuerung nicht auf den Weltachsen basiert, sondern auf dem Player, muss die CameraRotation eingerechnet werden.
        newPosition = playerCamera.transform.localRotation * newPosition;

        // Die neue relative Bewegung wird dann der aktuellen Position hinzugefügt.
        transform.position = transform.position + newPosition;
    }
}