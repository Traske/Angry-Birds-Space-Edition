using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyImpulseToMoon : MonoBehaviour
{
    public float impulseForce = 10f;

    void Start()
    {
        Rigidbody moonRigidbody = GetComponent<Rigidbody>();
        moonRigidbody.AddForce(Vector2.right * impulseForce, ForceMode.Impulse);
    }
}