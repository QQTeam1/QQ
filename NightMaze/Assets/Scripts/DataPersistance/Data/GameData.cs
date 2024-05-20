using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[System.Serializable]
public class GameData
{
    public Vector3 respawnPos;
    public SerializableDictionary<string, bool> coinsCollected;
    public int currentBodyLightBoost;
    public int currentLightLengthBoost;
    public int currentJumpChargingBoost;
    public int currentJumpCapacityBoost;
    public int currentSpeedBoost;
    public int currentHealthBoost;
    public int coinsCollectedNumber;
    public int currentHealth;

    public GameData()
    {
        respawnPos = Vector3.zero;
        coinsCollected = new SerializableDictionary<string, bool>();
        currentBodyLightBoost = 0;
        currentLightLengthBoost = 0;
        currentJumpChargingBoost = 0;
        currentJumpCapacityBoost = 0;
        currentSpeedBoost = 0;
        currentHealthBoost = 0;
        coinsCollectedNumber = 0;
        currentHealth = 2;
    }

}
