using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeOpacity : MonoBehaviour
{
    public List<Image> imageList;

    public void ChangeIndividualImageOpacity(int index, float Opacity)
    {
        if (index >= 0 && index < imageList.Count)
        {
            // Get the current color of the specific image
            Color currentColor = imageList[index].color;

            // Set the new alpha value (opacity)
            currentColor.a = Opacity;

            // Apply the new color to the specific image
            imageList[index].color = currentColor;
        }
        else
        {
            Debug.LogError("Invalid index or image not found in the list.");
        }
    }
}