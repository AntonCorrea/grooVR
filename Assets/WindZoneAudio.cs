using UnityEngine;
using UnityEngine.Audio;

public class WindZoneAudio : MonoBehaviour
{
    public AudioSource windSource;
    public AudioLowPassFilter lowPass;

    public float timeCutOff = 5f;

    public float outdoorVolume = 1f;
    public float indoorVolume = 0.3f;

    public float outdoorCutoff = 22000f;
    public float indoorCutoff = 1000f;

    bool isInside = true;

    private void Start()
    {
        windSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        float targetVolume = isInside ? indoorVolume : outdoorVolume;
        float targetCutoff = isInside ? indoorCutoff : outdoorCutoff;

        windSource.volume = Mathf.Lerp(
            windSource.volume,
            targetVolume,
            Time.deltaTime * timeCutOff
        );

        lowPass.cutoffFrequency = Mathf.Lerp(
            lowPass.cutoffFrequency,
            targetCutoff,
            Time.deltaTime * timeCutOff
        );
    }

    public void SetInside(bool inside)
    {
        isInside = inside;
    }
}