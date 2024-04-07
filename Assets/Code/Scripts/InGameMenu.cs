using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{

    [SerializeField] private GameObject PauseMenu;

    public void Pause() {
        Time.timeScale = 0;
    }

    public void Home() {
        SceneManager.LoadSceneAsync(0);
    }

    public void Mute() {

    }

    public void Resume() {
        Time.timeScale = 1;
    }

}
