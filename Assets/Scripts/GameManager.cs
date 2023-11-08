using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    bool GameHasEnded = false;

    public GameObject GameOverBackground;
    public void GameOver()
    {
        if (GameHasEnded == false)
        {
            GameHasEnded = true;
            GameOverBackground.SetActive(true);
            Debug.Log("Game Over");
        }
    }
    //Instead of using a button to end game use the following for same effect
    //FindObjectOfType<GameManager>().GameOver();
    public void RestartGame()
    {
        if (GameHasEnded == true) 
        {
            GameHasEnded = false;
            GameOverBackground.SetActive(false);
            Debug.Log("New Game");
        }
    }
}
