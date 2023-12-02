using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonOrbit : MonoBehaviour
{
    // Gravitationskonstanten
    public float gravitationalConstant = 0.1f;

    // Referens till jorden
    public GameObject earth;

    // Startmetoden k�rs n�r spelet startar
    void Start()
    {
        // Kontrollera att referensen till jorden �r tilldelad
        if (earth != null)
        {
            // Hitta Rigidbody2D-komponenterna f�r m�nen och jorden
            Rigidbody2D moonRigidbody = GetComponent<Rigidbody2D>();
            Rigidbody2D earthRigidbody = earth.GetComponent<Rigidbody2D>();

            // Kontrollera att komponenterna �r tilldelade
            if (moonRigidbody != null && earthRigidbody != null)
            {
                // Ber�kna avst�ndet mellan m�nen och jorden
                float distance = Vector2.Distance(transform.position, earth.transform.position);

                // Ber�kna kraften enligt Newtons gravitationslag f�r en cirkul�r omloppsbana
                float forceMagnitude = Mathf.Sqrt((gravitationalConstant * earthRigidbody.mass) / distance);

                // Ber�kna kraftvektorn (riktningen �r vinkelr�t mot avst�ndet)
                Vector2 forceDirection = (transform.position - earth.transform.position).normalized;
                Vector2 force = forceDirection * forceMagnitude;

                // Applicera impulskraften p� m�nen
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
