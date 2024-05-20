using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Coin : MonoBehaviour, IDataPersistance
{

    [SerializeField] private string id;
    [SerializeField] private GameObject Coinn;
    bool collected = false;
    [SerializeField] GameObject coin;

    [ContextMenu("Generate id for coin")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    void Awake()
    {
    }

    public void LoadData(GameData data)
    {
        data.coinsCollected.TryGetValue(id, out collected);
        if (collected)
        {
            coin.SetActive(false);
            Destroy(coin);
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.coinsCollected.ContainsKey(id))
        {
            data.coinsCollected.Remove(id);
        }
        print(collected);
        data.coinsCollected.Add(id, collected);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("Coin picked up");
            collected = true;
            coin.SetActive(false);
        }
    }
}
