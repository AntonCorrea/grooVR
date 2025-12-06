using UnityEngine;
using UnityEngine.Video;

public class Sphere360Video : MonoBehaviour
{
    public VideoClip[] videos;
    VideoPlayer videoPlayer;

    private void OnEnable()
    {
        GameManager.Instance.currentSphere360Video = this;
        videoPlayer = GetComponent<VideoPlayer>();
    }

    private void OnDisable()
    {
        GameManager.Instance.currentSphere360Video = null;
    }

    public void PlayVideo(int index)
    {
        videoPlayer.clip = videos[index];
        videoPlayer.Play();
    }
}

