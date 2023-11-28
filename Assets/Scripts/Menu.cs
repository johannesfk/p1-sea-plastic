using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{  
    public GameObject UpgradeMenuObject;
    public GameObject ManagementMenuObject;
    public GameObject AdvertismentMenuObject;
    public GameObject CurrentMenu;
    public GameObject CurrentRegion;
    public GameObject GlobalRegionObject;
    public GameObject Region1Object;
    public GameObject Region2Object;
    public GameObject Region3Object;
    public GameObject Region4Object;
    public GameObject Region5Object;
    public GameObject Region6Object;
    public GameObject BurgerMenu;

    public void OpenGlobalStatScreen()
    {
        if (CurrentRegion != null)
        { 
            CurrentRegion.SetActive(false); 
        }
        GlobalRegionObject.SetActive(true);
        CurrentRegion = GlobalRegionObject;
    }
    public void OpenRegion1()
    {
        CurrentMenu.SetActive(false);
        Region1Object.SetActive(true);
        CurrentRegion = Region1Object;
    }
    public void OpenRegion2()
    {
        CurrentMenu.SetActive(false);
        Region2Object.SetActive(true);
        CurrentRegion = Region2Object;
    }
    public void OpenRegion3()
    {
        CurrentMenu.SetActive(false);
        Region3Object.SetActive(true);
        CurrentRegion = Region3Object;
    }
    public void OpenRegion4()
    {
        CurrentMenu.SetActive(false);
        Region4Object.SetActive(true);
        CurrentRegion = Region4Object;
    }
    public void OpenRegion5()
    {
        CurrentMenu.SetActive(false);
        Region5Object.SetActive(true);
        CurrentRegion = Region5Object;
    }
    public void OpenRegion6()
    {
        CurrentMenu.SetActive(false);
        Region6Object.SetActive(true);
        CurrentRegion = Region6Object;
    }
    public void ExitStatScreen()
    {
        CurrentRegion.SetActive(false);
        CurrentRegion = null;
    }

    // Below are upgrade menu
    public void OpenUpgradeMenu()
    {
        if (CurrentMenu != null)
        {
            CurrentMenu.SetActive(false);
        }

        UpgradeMenuObject.SetActive(true);
        CurrentMenu = UpgradeMenuObject;

    }

    public void ExitUpgradeMenu()
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


    //Burger MENU STUFF

    public void BurgerButton()
    {
        BurgerMenu.SetActive(true);
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
