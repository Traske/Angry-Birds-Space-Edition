using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//Placera detta skript p� kamerans spelobjekt!
public class FollowBox : MonoBehaviour
{
    GameObject Box;

    // Start is called before the first frame update
    void Start()
    {
        //F�ruts�tter att spelobjektet f�r den l�da man kan skjuta iv�g heter "Box"
        Box = GameObject.Find("skjutandebox");
    }

    // Update is called once per frame
    void Update()
    {
        //Observera att h�r beh�ver vi �ven definiera en Z-position eftersom kameran inte finns vid 0 p� Z-axeln utan by default p� -10
        //Enklast �r att bara l�ta Z-psoitionen vara samma som tidigare
        transform.position = new Vector3(Box.transform.position.x, Box.transform.position.y, transform.position.z);
    }
}
