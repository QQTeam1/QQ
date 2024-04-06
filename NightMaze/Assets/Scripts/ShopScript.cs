using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ShopScript : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject subPanel;
    [SerializeField] private CoinManager coinManager;
    [SerializeField] private Light2D lightBulb;
    [SerializeField] private MovingWithJoystick hero;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            button.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            button.SetActive(false);
        }
    }
    public void OnButtonClick()
    {
        panel.SetActive(true);
    }
    public void OnButtonClickJumpBust()
    {
        if (coinManager.numberOfCoins >= 3)
        {
            coinManager.numberOfCoins -= 3;
            hero.maxNumberOfJumps = 4;
        }
        else
        {
            subPanel.SetActive(true);
        }
    }
    public void OnButtonClickSpeedBust()
    {
        if (coinManager.numberOfCoins >= 3)
        {
            coinManager.numberOfCoins -= 3;
            hero.moveSpeed = 4.5f;
        }
        else
        {
            subPanel.SetActive(true);
        }
    }
    public void OnButtonLightBust()
    {
        if (coinManager.numberOfCoins >= 3)
        {
            coinManager.numberOfCoins -= 3;
            lightBulb.pointLightInnerRadius = 1.5f;
            lightBulb.pointLightOuterRadius = 3f;
        }
        else
        {
            subPanel.SetActive(true);
        }
    }
    public void OnPanelExitPressed()
    {
        panel.SetActive(false);
        subPanel.SetActive(false);
    }
    public void OnSubPanelExitPressed()
    {
        subPanel.SetActive(false);
    }
    void Start()
    {
        button.SetActive(false);
        panel.SetActive(false);
        subPanel.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
