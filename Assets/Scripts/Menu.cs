/*
 * Author: Emanuel Misztal
 * 2019
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // event - easy mode button clicked
    public void OnEasyModeButtonClicked()
    {
        SceneManager.LoadScene(1); // load first scene (easy mode)
    }

    // event - hardcore mode button clicked
    public void OnHardcoreModeButtonClicked()
    {
        SceneManager.LoadScene(2); // load second scene (hardcore mode)
    }

    // event - exit button clicked
    public void OnExitButtonClicked()
    {
        Application.Quit(); // exit game
    }
}
