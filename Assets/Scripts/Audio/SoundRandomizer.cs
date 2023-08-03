using UnityEngine;

public class SoundRandomizer : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private float pitchVariance, volumeVariance, stereoPanVariance;
    [SerializeField] private bool pitchUpOnly;
    private float defaultPitch, defaultVolume, defaultStereoPan;

    private void Start()
    {
        defaultPitch = source.pitch;
        defaultVolume = source.volume;
        defaultStereoPan = source.panStereo;
    }

    public void PlayRandomizedSound()
    {
        if (pitchUpOnly)
            source.pitch = Random.Range(defaultPitch, defaultPitch + pitchVariance);
        else
            source.pitch = Random.Range(defaultPitch - pitchVariance, defaultPitch + pitchVariance);

        source.volume = Random.Range(defaultVolume - volumeVariance, defaultVolume + volumeVariance);
        source.panStereo = Random.Range(defaultStereoPan - stereoPanVariance, defaultStereoPan + stereoPanVariance);
        source.Play();
    }
}
