using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class Menu : MonoBehaviour
{
    public GameObject UpgradeMenuObject;
    public GameObject ManagementMenuObject;
    public GameObject AdvertismentMenuObject;
    public GameObject CurrentMenu;
    public GameObject BurgerMenu;
    public List<GameObject> StatScreens;
    private int currentScreenIndex = 0;

    // Below are upgrade menu
    void SwitchMenu(GameObject newMenu)
    {
        if (CurrentMenu != null)
        {
            CurrentMenu.SetActive(false);
        }

        newMenu.SetActive(true);
        CurrentMenu = newMenu;
    }
    void UpdateStatScreen()
    {
        SwitchMenu(StatScreens[currentScreenIndex]);
    }
    // ... (other methods remain unchanged)

    public void NextStatScreen()
    {
        currentScreenIndex = (currentScreenIndex + 1);
        UpdateStatScreen();
        Debug.Log($"Switched to stat screen {currentScreenIndex}");
    }

    public void PreviousStatScreen()
    {
        currentScreenIndex = (currentScreenIndex - 1);
        UpdateStatScreen();
    }
    public void OpenStatScreen(int index)
    {
        if (index >= 0)
        {
            currentScreenIndex = index;
            UpdateStatScreen();
        }
        else
        {
            Debug.LogError($"Invalid stat screen index: {index}");
        }
    }
    public void UpgradeMenu()
    {
        SwitchMenu(UpgradeMenuObject);
    }

    public void ExitUpgradeMenu()
    {
        CurrentMenu.SetActive(false);
        CurrentMenu = null;
    }

    public void ManagementMenu()
    {
        SwitchMenu(ManagementMenuObject);
    }

    public void AdvertiseMenu()
    {
        SwitchMenu(AdvertismentMenuObject);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Next Scene");
    }
    public void QuitGame()
    {
        UnityEngine.Application.Quit();
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


    //Burger MENU STUFF

    public void BurgerButton()
    {
        if (BurgerMenu.activeSelf)
        {
            BurgerMenu.SetActive(false);

        }
        else
        {
            BurgerMenu.SetActive(true);

        }
    }

    public void OnBurgerButton(InputValue Button)
    {
        if (BurgerMenu.activeSelf)
        {
            BurgerMenu.SetActive(false);

        }
        else
        {
            BurgerMenu.SetActive(true);

        }

    }

}
