using UnityEngine;

public class Volume : MonoBehaviour
{
    public GameData gameData;
    public void setMasterVolume(float v) {
        AudioListener.volume = v;
        gameData.volume = v;
        PlayerPrefs.SetFloat("Volume", v);
    }
}
