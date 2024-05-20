using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour, IDataPersistance
{
    public TMP_Text coinText;
    public int numberOfCoins = 0;
    void Update()
    {
        coinText.text = numberOfCoins.ToString();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            numberOfCoins++;
            //Destroy(other.gameObject);
        }
    }

    public void LoadData(GameData data)
    {
        foreach (var pair in data.coinsCollected)
        {
            if (pair.Value)
            {
                numberOfCoins++;
            }
        }
    }

    public void SaveData(ref GameData data)
    {

    }
}
