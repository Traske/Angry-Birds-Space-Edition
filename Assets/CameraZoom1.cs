using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Detta skript placeras på kamerans spelobjekt
public class CameraController : MonoBehaviour
{
    new Camera camera; // Use the 'new' keyword here

    // Start is called before the first frame update
    void Start()
    {
        // Hämtar Camera-komponenten från kamerans spelobjekt
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            camera.orthographicSize--;
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            camera.orthographicSize++;
        }
    }
}
