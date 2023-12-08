using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    // Gravitationskonstanten
    public float gravitationalConstant = 0.1f;

    // Initial impuls f�r omloppsbana
    public float initialOrbitImpulse = 0.1f;

    private void Start()
    {
        ApplyInitialOrbitImpulse();
    }

    private void FixedUpdate()
    {
        ApplyGravity();
    }

    void ApplyGravity()
    {
        // Hitta alla kroppar med Rigidbody2D och tagen "GravityObject"
        GameObject[] objectsWithRigidbody = GameObject.FindGameObjectsWithTag("GravityObject");

        foreach (GameObject obj in objectsWithRigidbody)
        {
            // Undvik att p�verka sig sj�lv
            if (obj != gameObject)
            {
                Rigidbody2D objRigidbody = obj.GetComponent<Rigidbody2D>();
                Rigidbody2D thisRigidbody = GetComponent<Rigidbody2D>();

                if (objRigidbody != null && thisRigidbody != null)
                {
                    // Ber�kna avst�ndet mellan objekten
                    float distance = Vector2.Distance(obj.transform.position, transform.position);

                    // Ber�kna kraften enligt Newtons gravitationslag
                    float forceMagnitude = (gravitationalConstant * objRigidbody.mass * thisRigidbody.mass) / Mathf.Pow(distance, 2);

                    // Ber�kna kraftriktningen (korrigerad rad)
                    Vector2 forceDirection = (transform.position - obj.transform.position).normalized;

                    // Ber�kna kraftvektorn
                    Vector2 force = forceDirection * forceMagnitude;

                    // Applicera kraften p� objektet
                    objRigidbody.AddForce(force);
                }
            }
        }
    }

    void ApplyInitialOrbitImpulse()
    {
        // Kontrollera om objektet �r m�nen genom namn
        if (gameObject.name == "Moon")
        {
            // Hitta riktningen fr�n m�nen till jorden
            Vector2 orbitDirection = (Vector2.zero - (Vector2)transform.position).normalized;

            // Applicera impuls vinkelr�tt mot omloppsriktningen
            Rigidbody2D moonRigidbody = GetComponent<Rigidbody2D>();
            moonRigidbody.AddForce(Vector2.Perpendicular(orbitDirection) * initialOrbitImpulse, ForceMode2D.Impulse);
        }
    }
}