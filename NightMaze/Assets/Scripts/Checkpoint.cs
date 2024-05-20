using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private CheckpointManager checkpoint;
    [SerializeField] GameObject checkpointLight;
    [SerializeField] GameObject candledCheckpoint;
    [SerializeField] GameObject uncandledCheckpoint;
    [SerializeField] GameObject button;
    [SerializeField] AudioClip lightingSound;

    [SerializeField] AudioSource audioSource;

    void Start()
    {
        checkpoint = GameObject.Find("Hero").GetComponent<CheckpointManager>();
        button.SetActive(false);
        UpdateCheckpointStatus();
    }

    private void Update()
    {
        UpdateCheckpointStatus();
    }

    private void UpdateCheckpointStatus()
    {
        if (checkpoint.RespawnPoint == transform.position)
        {
            checkpointLight.SetActive(true);
            candledCheckpoint.SetActive(true);
            //candledCheckpoint.GetComponent<Animator>().SetTrigger("appear");
            uncandledCheckpoint.SetActive(false);
        }
        else
        {
            checkpointLight.SetActive(false);
            candledCheckpoint.SetActive(false);
            uncandledCheckpoint.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Hero" && checkpoint.RespawnPoint != transform.position)
        {
            button.SetActive(true);
        }
    }

    public void OnButtonClick()
    {
        if (checkpoint != null)
        {
            checkpoint.SetCheckpoint(transform.position);
            //checkpoint.GetComponent<Animator>().SetTrigger("appear");
            button.SetActive(false);
            checkpointLight.SetActive(true);

            if (audioSource != null && lightingSound != null)
            {
                audioSource.PlayOneShot(lightingSound);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Hero")
        {
            button.SetActive(false);
        }
    }
}