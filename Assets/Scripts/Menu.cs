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
    public GameObject SettingMenu;

    public GameObject CurrentRegion;
    public GameObject GlobalRegionObject;
    public GameObject Region1Object;
    public GameObject Region2Object;
    public GameObject Region3Object;
    public GameObject Region4Object;
    public GameObject Region5Object;
    public GameObject Region6Object;
    void SwitchMenu(GameObject newMenu)
    {
        if (CurrentMenu != null)
        {
            CurrentMenu.SetActive(false);
        }

        newMenu.SetActive(true);
        CurrentMenu = newMenu;
    }
    void SwitchRegion(GameObject newRegion)
    {
        if (newRegion == null)
        {
            Debug.LogError($"Trying to switch to a null region! CurrentRegion: {CurrentRegion?.name}");
            return;
        }

        Debug.Log($"Switching to region: {newRegion.name}");

        if (CurrentRegion != null)
        {
            CurrentRegion.SetActive(false);
            Debug.Log($"Deactivating current region: {CurrentRegion.name}");
        }

        newRegion.SetActive(true);
        CurrentRegion = newRegion;

        Debug.Log($"Activating new region: {newRegion.name}");
    }
    public void OpenGlobalRegion()
    {
        SwitchRegion(GlobalRegionObject);
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

    //Settings MENU STUFF
    public void SettingsMenu()
    {
        if (SettingMenu.activeSelf)
        {
            SettingMenu.SetActive(false);

        }
        else
        {
            SettingMenu.SetActive(true);

        }
    }



}
