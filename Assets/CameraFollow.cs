using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//Placera detta skript på kamerans spelobjekt!
public class FollowBox : MonoBehaviour
{
    GameObject Box;

    // Start is called before the first frame update
    void Start()
    {
        //Förutsätter att spelobjektet för den låda man kan skjuta iväg heter "Box"
        Box = GameObject.Find("skjutandebox");
    }

    // Update is called once per frame
    void Update()
    {
        //Observera att här behöver vi även definiera en Z-position eftersom kameran inte finns vid 0 på Z-axeln utan by default på -10
        //Enklast är att bara låta Z-psoitionen vara samma som tidigare
        transform.position = new Vector3(Box.transform.position.x, Box.transform.position.y, transform.position.z);
    }
}
