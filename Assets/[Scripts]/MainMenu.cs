/*
 * Full Name        : Zijie Wang
 * StudentID        : 101283316
 * Date Modified    : October 24, 2021
 * File             : MainMenu
 * Description      : This is the Main Menu Script
 * Revision History : v0.1
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
