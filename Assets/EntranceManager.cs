using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntranceManager : MonoBehaviour
{
    public void QuitGame() {
        Application.Quit();
    }

    public void StartGame() {
        Time.timeScale = 1;
        SceneManager.LoadScene("mainscreen");
    }
}
