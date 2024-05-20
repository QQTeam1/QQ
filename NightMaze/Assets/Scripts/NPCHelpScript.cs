using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCHelpScript : MonoBehaviour
{
    [SerializeField] private GameObject textPanel;
    [SerializeField] private GameObject imagePanel;
    [SerializeField] private string[] textPasses;
    [SerializeField] private bool[] containImage;
    [SerializeField] private Sprite[] images;
    [SerializeField] private float timeBetweenSymbols;
    [SerializeField] private GameObject mainButton;
    [SerializeField] private TMP_Text textOnTextPanel;
    [SerializeField] private TMP_Text textOnImagePanel;
    [SerializeField] private Image imageOnPanel;

    public int index;
    public int imageIndex;

    void Start()
    {
        index = 0;
        imageIndex = 0;
        //textPasses = new string[] { "C:\\Users\\Admin\\Desktop\\second_try\\NightMaze\\Assets\\TextFiles\\1.1.txt", "C:\\Users\\Admin\\Desktop\\second_try\\NightMaze\\Assets\\TextFiles\\1.2.txt"};
    }
    public void ShowButtonClick()
    {
        ShowPanelWithText();
    }
    private void ShowPanelWithText()
    {
        StopAllCoroutines();
        if (containImage[index])
        {
            imagePanel.SetActive(true);
            textPanel.SetActive(false);
            textOnImagePanel.text = "";
            imageOnPanel.sprite = images[imageIndex];
            StartCoroutine(AddTextWithDelay(File.ReadAllText(textPasses[index]), textOnImagePanel));
        }
        else
        {
            imagePanel.SetActive(false);
            textPanel.SetActive(true);
            textOnTextPanel.text = "";
            StartCoroutine(AddTextWithDelay(File.ReadAllText(textPasses[index]), textOnTextPanel));
        }
    }
    private IEnumerator AddTextWithDelay(string text, TMP_Text panelText)
    {
        foreach (var item in text.ToCharArray())
        {
            panelText.text += item;
            yield return new WaitForSeconds(timeBetweenSymbols); 
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mainButton.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mainButton.SetActive(false);
        }
    }
    public void CrossButtonClick()
    {
        textPanel.SetActive(false);
        imagePanel.SetActive(false);
        index = 0;
        imageIndex = 0;
    }
    public void RightArrowButtonClick()
    {
        if (index + 1 < textPasses.Length)
        {
            if (containImage[index] && imageIndex + 1 < images.Length)
            {
                imageIndex++;
            }
            index++;
            ShowPanelWithText();
        }
    }
    public void LeftArrowButtonClick()
    {
        if (index > 0)
        {
            if (containImage[index] && imageIndex > 0)
            {
                imageIndex--;
            }
            index--;
            ShowPanelWithText();
        }
    }
    void Update()
    {
        
    }
}
