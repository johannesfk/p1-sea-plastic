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

    public GameObject CurrentRegion;
    public GameObject GlobalRegionObject;
    public GameObject Region1Object;
    public GameObject Region2Object;
    public GameObject Region3Object;
    public GameObject Region4Object;
    public GameObject Region5Object;
    public GameObject Region6Object;

    
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
    void SwitchRegion(GameObject newRegion)
    {
        if (CurrentRegion != null)
        {
            CurrentRegion.SetActive(false);
        }

        newRegion.SetActive(true);
        CurrentRegion = newRegion;
    }
    public void OpenGlobalRegion()
    {
        SwitchRegion(GlobalRegionObject);
    }
    public void OpenRegion1()
    {
        SwitchRegion(Region1Object);
    }
    public void OpenRegion2()
    {
        SwitchRegion(Region2Object);
    }

    public void OpenRegion3()
    {
        SwitchRegion(Region3Object);
    }

    public void OpenRegion4()
    {
        SwitchRegion(Region4Object);
    }

    public void OpenRegion5()
    {
        SwitchRegion(Region5Object);
    }

    public void OpenRegion6()
    {
        SwitchRegion(Region6Object);
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
