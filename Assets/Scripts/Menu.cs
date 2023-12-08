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

    public GameObject GlobalRegionObject;
    void SwitchMenu(GameObject newMenu)
    {
        if (CurrentMenu != null)
        {
            CurrentMenu.SetActive(false);
        }

        newMenu.SetActive(true);
        CurrentMenu = newMenu;
    }
    public void OpenGlobalRegion()
    {
        GlobalRegionObject.SetActive(true);
        Debug.Log("Button Clicked");
    }
    public void ExitRegion()
    {
        GlobalRegionObject.SetActive(false);
        Debug.Log("Button Clicked");
    }
    public void NextRegion()
    {
        PollutionController.instance.currentRegion++;
    }
    public void PreviousRegion()
    {
        PollutionController.instance.currentRegion--;
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
        FindObjectOfType<AudioManager>().Play("SoundFX");
        Debug.Log("Sound LOG");
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
