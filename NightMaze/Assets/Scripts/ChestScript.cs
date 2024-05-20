using System;
using System.Collections;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private SpriteRenderer currantSprite;
    [SerializeField] private Sprite newSprite;
    [SerializeField] private int coinsInChest;
    [SerializeField] private float oneCoinUpdate;
    private CoinManager coinManager;
    private bool wasOpened = false;
    // Start is called before the first frame update
    private void Start()
    {
        coinManager = GameObject.Find("Hero").GetComponent<CoinManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !wasOpened)
        {
            button.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !wasOpened)
        {
            button.SetActive(false);
        }
    }
    public void OnButtonClick()
    {
        if (!wasOpened)
        {
            StartCoroutine(AddCoinsOverTime(coinsInChest, oneCoinUpdate));
            wasOpened = true;
            button.SetActive(false);
            currantSprite.sprite = newSprite;
        }
    }
    private IEnumerator AddCoinsOverTime(int coinsToAdd, float interval)
    {
        for (int i = 0; i < coinsToAdd; i++)
        {
            coinManager.numberOfCoins += 1;
            yield return new WaitForSeconds(interval);
        }
    }
}
