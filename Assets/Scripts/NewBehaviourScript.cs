using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpacityManager : MonoBehaviour
{
    ChangeImageOpacity opacityController; // Reference to the ChangeImageOpacity script

    void Start()
    {
        // Get reference to the ChangeImageOpacity script attached to the same GameObject
        opacityController = GetComponent<ChangeImageOpacity>();

        // Example usage:
        // Set opacity of the first image in the list to 0.5
        opacityController.SetImageOpacity(opacityController.myImages[0], 0.5f);

        // Get the opacity value of the second image in the list
        float opacity = opacityController.GetImageOpacity(opacityController.myImages[1]);
        Debug.Log("Opacity: " + opacity);
    }

}
