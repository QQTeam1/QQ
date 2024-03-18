using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private CheckpointManager checkpoint;
    public GameObject button;

    void Start()
    {
        checkpoint = GameObject.Find("Hero").GetComponent<CheckpointManager>();
        button.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Hero")
        {
            button.SetActive(true);
        }
    }

    public void OnButtonClick()
    {
        if (checkpoint != null)
        {
            checkpoint.SetCheckpoint(transform.position);
        }
        //checkpoint.SetCheckpoint(transform.position);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Hero")
        {
            button.SetActive(false);
        }
    }
}
