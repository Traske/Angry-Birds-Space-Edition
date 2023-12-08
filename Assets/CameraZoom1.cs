using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Detta skript placeras p� kamerans spelobjekt
public class CameraController : MonoBehaviour
{
    new Camera camera; // Use the 'new' keyword here

    // Start is called before the first frame update
    void Start()
    {
        // H�mtar Camera-komponenten fr�n kamerans spelobjekt
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
