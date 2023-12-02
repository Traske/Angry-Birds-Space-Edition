using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    // Gravitationskonstanten
    public float gravitationalConstant = 1f;

    private void FixedUpdate()
    {
        ApplyGravity();
    }

    void ApplyGravity()
    {
        // Hitta alla kroppar
        GameObject[] objectsWithRigidbody = GameObject.FindGameObjectsWithTag("GravityObject");

        foreach (GameObject obj in objectsWithRigidbody)
        {
            if (obj != gameObject) // Undvik att påverka sig själv
            {
                Rigidbody2D objRigidbody = obj.GetComponent<Rigidbody2D>();
                Rigidbody2D thisRigidbody = GetComponent<Rigidbody2D>();

                if (objRigidbody != null && thisRigidbody != null)
                {
                    // Beräkna avståndet mellan objekten
                    float distance = Vector2.Distance(obj.transform.position, transform.position);

                    // Beräkna kraften enligt Newtons gravitationslag
                    float forceMagnitude = (gravitationalConstant * objRigidbody.mass * thisRigidbody.mass) / Mathf.Pow(distance, 2);

                    // Beräkna kraftvektorn
                    Vector2 forceDirection = (obj.transform.position - transform.position).normalized;
                    Vector2 force = forceDirection * forceMagnitude;

                    // Debug.Log för att övervaka avstånd och kraft
                    Debug.Log($"Distance between {gameObject.name} and {obj.name}: {distance}");
                    Debug.Log($"Force applied to {obj.name}: Magnitude = {forceMagnitude}, Direction = {forceDirection}");

                    // Applicera kraften på objektet
                    objRigidbody.AddForce(force);
                }
            }
        }
    }
}
