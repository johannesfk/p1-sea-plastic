using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{  
    public GameObject UpgradeMenuObject;
    public GameObject ManagementMenuObject;
    public GameObject AdvertismentMenuObject;
    public GameObject CurrentMenu;
    public void OpenMenu()
    {   
        if (CurrentMenu != null)
        {
            CurrentMenu.SetActive(false);  
        }

        UpgradeMenuObject.SetActive(true);
        CurrentMenu = UpgradeMenuObject;

    }
    public void ExitMenu()
    {
        CurrentMenu.SetActive(false);
        CurrentMenu = null;
    }

    public void ManagementMenu()
    {
        CurrentMenu.SetActive(false);

        ManagementMenuObject.SetActive(true);

        CurrentMenu = ManagementMenuObject;

    }

    public void AdvertiseMenu()
    {
        CurrentMenu.SetActive(false);

        AdvertismentMenuObject.SetActive(true);

        CurrentMenu = AdvertismentMenuObject;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Next Scene");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
    public void RestartButton()
    {
        SceneManager.LoadScene("Game Screen");
        FindObjectOfType<GameManager>().RestartGame();
        Debug.Log("Reloading New Game");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Debug.Log("Loading Menu");
    }
}
