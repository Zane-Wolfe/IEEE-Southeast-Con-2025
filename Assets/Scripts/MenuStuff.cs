using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuStuff : MonoBehaviour
{
    public void playTheGame() {
        SceneManager.LoadScene("MainScene");
    }

    public void quitTheGame() {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void openTheOptions() {
        SceneManager.LoadScene("Options");
    }

    public void openTheCredits() {
        SceneManager.LoadScene("Credits");
    }

    public void openTheMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
