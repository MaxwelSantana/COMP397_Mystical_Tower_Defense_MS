/*
    Author: Maxwel Santana
    Student Number: 301294337
    File: MainMenuManager
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject optionMenu;

    private void Start()
    {
        optionMenu.SetActive(false);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OpenOptions()
    {
        optionMenu.SetActive(true);
    }

    public void CloseOptions()
    {
        optionMenu.SetActive(false);
    }

    public void Exit()
    {
        #if UNITY_EDITOR
                // Application.Quit() does not work in the editor so
                // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                 Application.Quit();
        #endif
    }
}
