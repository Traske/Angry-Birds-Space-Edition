using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonOrbit : MonoBehaviour
{
    // Gravitationskonstanten
    public float gravitationalConstant = 0.1f;

    // Referens till jorden
    public GameObject earth;

    // Startmetoden körs när spelet startar
    void Start()
    {
        // Kontrollera att referensen till jorden är tilldelad
        if (earth != null)
        {
            // Hitta Rigidbody2D-komponenterna för månen och jorden
            Rigidbody2D moonRigidbody = GetComponent<Rigidbody2D>();
            Rigidbody2D earthRigidbody = earth.GetComponent<Rigidbody2D>();

            // Kontrollera att komponenterna är tilldelade
            if (moonRigidbody != null && earthRigidbody != null)
            {
                // Beräkna avståndet mellan månen och jorden
                float distance = Vector2.Distance(transform.position, earth.transform.position);

                // Beräkna kraften enligt Newtons gravitationslag för en cirkulär omloppsbana
                float forceMagnitude = Mathf.Sqrt((gravitationalConstant * earthRigidbody.mass) / distance);

                // Beräkna kraftvektorn (riktningen är vinkelrät mot avståndet)
                Vector2 forceDirection = (transform.position - earth.transform.position).normalized;
                Vector2 force = forceDirection * forceMagnitude;

                // Applicera impulskraften på månen
                moonRigidbody.AddForce(force, ForceMode2D.Impulse);
            }
            else
            {
                Debug.LogError("Rigidbody2D component is missing for Moon or Earth.");
            }
        }
        else
        {
            Debug.LogError("Earth reference is not assigned.");
        }
    }
}
