using UnityEngine;
using UnityEngine.Audio;

public class Audio : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void volumenAudio(float sliderVolumen)
    {
        audioMixer.SetFloat("volumen", Mathf.Log10(sliderVolumen)*20);
    }

}
