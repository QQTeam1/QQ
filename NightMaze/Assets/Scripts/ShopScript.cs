using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ShopScript : MonoBehaviour, IDataPersistance
{
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject speedButton;
    [SerializeField] private GameObject jumpChargingButton;
    [SerializeField] private GameObject jumpCapacityButton;
    [SerializeField] private GameObject bodyLightButton;
    [SerializeField] private GameObject lightLenghtButton;
    [SerializeField] private GameObject additionalLifeButton;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject subPanel;
    [SerializeField] private CoinManager coinManager;
    [SerializeField] private Light2D bodyLight;
    [SerializeField] private Light2D lightBulb;
    [SerializeField] private MovingWithJoystick hero;
    [SerializeField] private int speedPrice;
    [SerializeField] private int jumpChargPrice;
    [SerializeField] private int jumpCapacityPrice;
    [SerializeField] private int bodyLightPrice;
    [SerializeField] private int lightbulbLenghtPrice;
    [SerializeField] private int additionalLifePrice;

    public int maximumLevelOfBoosts = 1;
    private int currentBodyLightBoost;
    private int currentLightLengthBoost;
    private int currentJumpChargingBoost;
    private int currentJumpCapacityBoost;
    private int currentSpeedBoost;
    private int currentHealthBoost;
    private Health heroHealth;

    void Start()
    {
        coinManager = hero.GetComponent<CoinManager>();
        heroHealth = hero.GetComponent<Health>();
        button.SetActive(false);
        CloseWholePanel();
    }
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
        ShowWholePanel();
    }
    public void OnButtonSpeedBoost()
    {
        if (coinManager.numberOfCoins >= speedPrice && currentSpeedBoost + currentHealthBoost < maximumLevelOfBoosts)
        {
            coinManager.numberOfCoins -= speedPrice;
            hero.moveSpeed++;
            currentSpeedBoost++;
            ShowAfterComparingWithCeil(currentSpeedBoost + currentHealthBoost, speedButton, additionalLifeButton);
        }
        else
        {
            subPanel.SetActive(true);
        }
    }
    public void OnButtonhealthBoost()
    {
        if (coinManager.numberOfCoins >= additionalLifePrice && currentSpeedBoost + currentHealthBoost < maximumLevelOfBoosts)
        {
            coinManager.numberOfCoins -= additionalLifePrice;
            heroHealth.currentHealth++;
            currentHealthBoost++;
            print(heroHealth.currentHealth);
            ShowAfterComparingWithCeil(currentSpeedBoost + currentHealthBoost, speedButton, additionalLifeButton);
        }
        else
        {
            subPanel.SetActive(true);
        }
    }
    public void OnButtonClickJumpCapacityBoost()
    {
        if (coinManager.numberOfCoins >= jumpCapacityPrice && currentJumpCapacityBoost + currentJumpChargingBoost < maximumLevelOfBoosts)
        {
            coinManager.numberOfCoins -= jumpCapacityPrice;
            hero.maxNumberOfJumps++;
            currentJumpCapacityBoost++;
            ShowAfterComparingWithCeil(currentJumpCapacityBoost + currentJumpChargingBoost, jumpCapacityButton, jumpChargingButton);
        }
        else
        {
            subPanel.SetActive(true);
        }
    }
    public void OnButtonClickJumpChargingBoost()
    {
        if (coinManager.numberOfCoins >= jumpChargPrice && currentJumpCapacityBoost + currentJumpChargingBoost < maximumLevelOfBoosts)
        {
            coinManager.numberOfCoins -= jumpChargPrice;
            hero.jumpLoadSpeed *= 2;
            currentJumpChargingBoost++;
            ShowAfterComparingWithCeil(currentJumpCapacityBoost + currentJumpChargingBoost, jumpChargingButton, jumpCapacityButton);
        }
        else
        {
            subPanel.SetActive(true);
        }
    }
    public void OnButtonBodyLightBoost()
    {
        if (coinManager.numberOfCoins >= bodyLightPrice && currentLightLengthBoost + currentBodyLightBoost < maximumLevelOfBoosts)
        {
            coinManager.numberOfCoins -= bodyLightPrice;
            bodyLight.pointLightInnerRadius += 0.5f;
            bodyLight.pointLightOuterRadius += 1.5f;
            currentBodyLightBoost++;
            ShowAfterComparingWithCeil(currentBodyLightBoost + currentLightLengthBoost, bodyLightButton, lightLenghtButton);
        }
        else
        {
            subPanel.SetActive(true);
        }
    }
    public void OnButtonLightBulbBoost()
    {
        if (coinManager.numberOfCoins >= lightbulbLenghtPrice && currentLightLengthBoost + currentBodyLightBoost < maximumLevelOfBoosts)
        {
            coinManager.numberOfCoins -= lightbulbLenghtPrice;
            lightBulb.pointLightInnerRadius += 1f;
            currentLightLengthBoost++;
            ShowAfterComparingWithCeil(currentLightLengthBoost + currentBodyLightBoost, lightLenghtButton, bodyLightButton);
        }
        else
        {
            subPanel.SetActive(true);
        }
    }
    public void OnPanelExitPressed()
    {
        CloseWholePanel();
    }
    public void OnSubPanelExitPressed()
    {
        subPanel.SetActive(false);
    }
    void CloseWholePanel()
    {
        panel.SetActive(false);
        subPanel.SetActive(false);
        bodyLightButton.SetActive(false);
        jumpCapacityButton.SetActive(false);
        jumpChargingButton.SetActive(false);
        lightLenghtButton.SetActive(false);
        speedButton.SetActive(false);
    }
    void ShowWholePanel()
    {
        panel.SetActive(true);
        ShowAfterComparingWithCeil(currentSpeedBoost + currentHealthBoost, speedButton, additionalLifeButton);
        ShowAfterComparingWithCeil(currentLightLengthBoost + currentBodyLightBoost, lightLenghtButton, bodyLightButton);
        //ShowAfterComparingWithCeil(currentBodyLightBoost, bodyLightButton);
        ShowAfterComparingWithCeil(currentJumpCapacityBoost + currentJumpChargingBoost, jumpCapacityButton, jumpChargingButton);
        //ShowAfterComparingWithCeil(currentJumpChargingBoost, jumpChargingButton);
    }
    void ShowAfterComparingWithCeil(int currentBoost, GameObject button, GameObject secondButton = null)
    {
        if (currentBoost < maximumLevelOfBoosts && secondButton != null)
        {
            button.SetActive(true);
            secondButton.SetActive(true);
        }
        else if (currentBoost < maximumLevelOfBoosts && secondButton == null)
        {
            button.SetActive(true);
        }
        else if (currentBoost >= maximumLevelOfBoosts && secondButton == null)
        {
            button.SetActive(false);
        }
        else if (currentBoost >= maximumLevelOfBoosts && secondButton != null)
        {
            button.SetActive(false);
            secondButton.SetActive(false);
        }
    }

    public void LoadData(GameData data)
    {
        currentBodyLightBoost = data.currentBodyLightBoost;
        currentLightLengthBoost = data.currentLightLengthBoost;
        currentJumpChargingBoost = data.currentJumpChargingBoost;
        currentJumpCapacityBoost = data.currentJumpCapacityBoost;
        currentSpeedBoost = data.currentSpeedBoost;
        currentHealthBoost = data.currentHealthBoost;
        if (currentJumpCapacityBoost == 1 && hero.maxNumberOfJumps < 3)
        {
            hero.maxNumberOfJumps++;
        }
        // fix
        if (currentHealthBoost == 1 && heroHealth.currentHealth < 3)
        {
            heroHealth.currentHealth++;
        }
        if (currentBodyLightBoost == 1 && bodyLight.pointLightInnerRadius < 1 && bodyLight.pointLightOuterRadius < 3.4)
        {
            bodyLight.pointLightInnerRadius += 0.5f;
            bodyLight.pointLightOuterRadius += 1.5f;
        }
        if (currentLightLengthBoost == 1 && lightBulb.pointLightInnerRadius < 1.5)
        {
            lightBulb.pointLightInnerRadius += 1f;
        }
        if (currentJumpChargingBoost == 1 && hero.jumpLoadSpeed < 0.004)
        {
            hero.jumpLoadSpeed *= 2;
        }
        if (currentSpeedBoost == 1 && hero.moveSpeed < 4)
        {
            hero.moveSpeed++;
        }
    }

    public void SaveData(ref GameData data)
    {
        data.currentBodyLightBoost = this.currentBodyLightBoost;
        data.currentLightLengthBoost = this.currentLightLengthBoost;
        data.currentJumpChargingBoost = this.currentJumpChargingBoost;
        data.currentJumpCapacityBoost = this.currentJumpCapacityBoost;
        data.currentSpeedBoost = this.currentSpeedBoost;
        data.currentHealthBoost = this.currentHealthBoost;
    }
    // Update is called once per frme
    void Update()
    {

    }
}
