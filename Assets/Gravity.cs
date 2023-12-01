using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public Transform[] celestialBodies; // Assign Moon and Earth in the Inspector

    public float gravitationalConstant = 5f;

    void Update()
    {
        ApplyGravity();
    }

    void ApplyGravity()
    {
        foreach (Transform body1 in celestialBodies)
        {
            foreach (Transform body2 in celestialBodies)
            {
                if (body1 != body2)
                {
                    Vector2 direction = body2.position - body1.position;
                    float distance = direction.magnitude;

                    float forceMagnitude = gravitationalConstant * (body1.GetComponent<Rigidbody>().mass * body2.GetComponent<Rigidbody>().mass) / Mathf.Pow(distance, 2);

                    Vector2 force = direction.normalized * forceMagnitude;

                    body1.GetComponent<Rigidbody>().AddForce(force);
                }
            }
        }
    }
}