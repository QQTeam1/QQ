using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        
    }

    double x;
    void Update()
    {
        //x += 0.001;

        transform.Rotate(0,0,(float)x+ (float)0.1);
    }
}
