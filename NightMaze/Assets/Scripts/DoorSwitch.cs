using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject button;
    [SerializeField] private SpriteRenderer currantSprite;
    [SerializeField] private Sprite newSprite;
    [SerializeField] private bool isFirstUnlock;
    [SerializeField] private bool isSecondUnlock;
    [SerializeField] ShopScript shopScript;
    [SerializeField] NPCHelpScript[] NPCs;

    [SerializeField] private GameObject anotherObj;
    [SerializeField] private GameObject anotherObj2;
    [SerializeField] private int addObj;

    public int currentNPCActive;
    private bool wasUnlocked = false;

    private void Start()
    {
        currentNPCActive = 0;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !wasUnlocked)
        {
            button.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !wasUnlocked)
        {
            button.SetActive(false);
        }
    }
    public void OnButtonClick()
    {
        if (!wasUnlocked)
        {
            Destroy(door);
            wasUnlocked = true;
            button.SetActive(false);
            currantSprite.sprite = newSprite;

            if(addObj == 1)
            {
                anotherObj.SetActive(true);
            }
            if (addObj == 2)
            {
                anotherObj.SetActive(true);
                anotherObj2.SetActive(true);
            }
        }
        if (isFirstUnlock || isSecondUnlock)
        {
            shopScript.maximumLevelOfBoosts++;
            SwitchToNextNPC();
        }
    }
    public void SwitchToNextNPC()
    {
        NPCs[currentNPCActive].gameObject.SetActive(false);
        currentNPCActive++;
        NPCs[currentNPCActive].gameObject.SetActive(true);
    }
}