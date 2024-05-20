using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOffScript : MonoBehaviour
{
    [SerializeField] private bool isForSwitchOff;
    [SerializeField] private bool changeNPC;
    [SerializeField] private GameObject light;
    [SerializeField] private DoorSwitch attachedToNPCs;
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isForSwitchOff)
            {
                light.SetActive(false);
            }
            if(!isForSwitchOff)
            {
                light.SetActive(true);
            }
            if (changeNPC)
            {
                attachedToNPCs.SwitchToNextNPC();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
