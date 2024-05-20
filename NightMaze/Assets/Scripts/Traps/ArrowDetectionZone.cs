using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    [SerializeField] string tagTarget = "Player";
    [SerializeField] public  List<Collider2D> detectedObjs = new List<Collider2D>();
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == tagTarget)
        {
            detectedObjs.Add(other);
        }
    }

    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == tagTarget)
        {
            detectedObjs.Remove(other);
        }
    }
   
}
