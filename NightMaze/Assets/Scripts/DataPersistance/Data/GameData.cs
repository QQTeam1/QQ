using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[System.Serializable]
public class GameData
{
    public Vector3 respawnPos;

    public GameData()
    {
        respawnPos = Vector3.zero;
    }

}
