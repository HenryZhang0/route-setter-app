using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void PlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame ()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void NavigateToMainMenu ()
    {
        SceneManager.LoadScene(0);
    }

    public void NavigateToGallery ()
    {
        SceneManager.LoadScene(2);
    }

    public void NavigateToMyClimbs()
    {
        SceneManager.LoadScene(3);
    }

    public void NavigateToMyFriendsClimbs()
    {
        SceneManager.LoadScene(4);
    }
}
