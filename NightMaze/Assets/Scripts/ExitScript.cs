using UnityEngine;
using UnityEngine.Video;

public class ExitScript : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject rawImage;
    [SerializeField] private VideoPlayer player;
    // Start is called before the first frame update
    void Start()
    {
        player.loopPointReached += VideoFinished;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rawImage.SetActive(true);
            player.Play();
        }
    }
    private void VideoFinished(VideoPlayer vp)
    {
        rawImage.SetActive(false);
        ShowPanelWithComponents();
    }
    private void ShowPanelWithComponents()
    {
        panel.SetActive(true);
    }
}
