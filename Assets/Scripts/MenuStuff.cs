using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuStuff : MonoBehaviour
{
    public GameObject main;
    public GameObject credits;
    public GameObject options;

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
        main.SetActive(false);
        credits.SetActive(false);
        options.SetActive(true);
    }

    public void openTheCredits() {
        main.SetActive(false);
        credits.SetActive(true);
        options.SetActive(false);
    }

    public void openTheMainMenu() {
        main.SetActive(true);
        credits.SetActive(false);
        options.SetActive(false);
    }
}
