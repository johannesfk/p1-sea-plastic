using UnityEngine;
using UnityEngine.UI;

public class GameObjectCycler : MonoBehaviour
{
    public GameObject[] gameObjects;  // Array to store your GameObjects
    private int currentIndex = 0;     // Index to keep track of the current GameObject

    void Start()
    {
        // Make sure you have assigned your GameObjects in the Unity Inspector
        HideAllGameObjects();
    }
    void HideAllGameObjects()
    {
        // Hide all GameObjects
        foreach (GameObject go in gameObjects)
        {
            go.SetActive(false);
        }
    }

    public void ShowNextGameObject()
    {
        // Hide the current GameObject
        gameObjects[currentIndex].SetActive(false);

        // Increment the index and wrap around if needed
        currentIndex = (currentIndex + 1) % gameObjects.Length;

        // Show the new current GameObject
        gameObjects[currentIndex].SetActive(true);
    }

    public void ShowPreviousGameObject()
    {
        // Hide the current GameObject
        gameObjects[currentIndex].SetActive(false);

        // Decrement the index and wrap around if needed
        currentIndex = (currentIndex - 1 + gameObjects.Length) % gameObjects.Length;

        // Show the new current GameObject
        gameObjects[currentIndex].SetActive(true);
    }
    public void ShowSpecificGameObject(int index)
    {
        // Hide the current GameObject
        gameObjects[currentIndex].SetActive(false);

        // Set the specified index and wrap around if needed
        currentIndex = (index + gameObjects.Length) % gameObjects.Length;

        // Show the new current GameObject
        gameObjects[currentIndex].SetActive(true);
    }
}