using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // event - easy mode button clicked
    public void OnEasyModeButtonClicked()
    {
        SceneManager.LoadScene(1);
    }

    // event - hardcore mode button clicked
    public void OnHardcoreModeButtonClicked()
    {
        SceneManager.LoadScene(2);
    }

    // event - exit button clicked
    public void OnExitButtonClicked()
    {
        Application.Quit();
    }
}
