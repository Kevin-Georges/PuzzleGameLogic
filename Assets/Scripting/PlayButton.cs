using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour 
{

    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void EndGame()
    {
        SceneManager.LoadSceneAsync(3);
    }
    public void QuitGame() 
    {
        Application.Quit();
    }
    
}
