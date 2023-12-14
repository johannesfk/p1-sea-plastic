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
    public GameObject ShopMenuObject;
    public GameObject CurrentMenu;
    public GameObject BurgerMenu;
    public GameObject SettingMenu;
    public GameObject GlobalRegionObject;

    public void OpenGlobalRegion()
    {
        GameManager.instance.Pause();
        GlobalRegionObject.SetActive(true);
        Debug.Log("Button Clicked");
    }
    public void ExitRegion()
    {
        GameManager.instance.Resume();
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
    void SwitchMenu(GameObject newMenu)
    {
        if (CurrentMenu != null)
        {
            CurrentMenu.SetActive(false);
        }

        newMenu.SetActive(true);
        CurrentMenu = newMenu;
    }
    public void UpgradeMenu()
    {
        GameManager.instance.Pause();
        SwitchMenu(UpgradeMenuObject);
    }
    public void ManagementMenu()
    {
        GameManager.instance.Pause();
        SwitchMenu(ShopMenuObject);
    }

    public void ExitUpgradeMenu()
    {
        GameManager.instance.Resume();
        CurrentMenu.SetActive(false);
        CurrentMenu = null;
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
        FindObjectOfType<AudioManager>().Play("SFX");
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
            GameManager.instance.Resume();
            SettingMenu.SetActive(false);
        }
        else
        {
            GameManager.instance.Pause();
            SettingMenu.SetActive(true);
        }
    }
}
