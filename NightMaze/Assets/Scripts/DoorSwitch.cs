using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    public GameObject door; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 
        {
            door.SetActive(false);
        }
    }
}