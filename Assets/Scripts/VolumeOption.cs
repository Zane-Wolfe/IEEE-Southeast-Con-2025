using UnityEngine;
using UnityEngine.Audio;

public class Volume : MonoBehaviour
{
    public AudioMixer mixer;

    public void setMasterVolume(float v) {
        mixer.SetFloat("MasterVolume", v);
    }
}
