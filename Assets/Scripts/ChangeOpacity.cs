using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeOpacity : MonoBehaviour
{
    public List<Image> imageList;

    public void ChangeImageOpacity(float toOpacity)
    {
        foreach (Image image in imageList)
        {
            // Get the current color of the image
            Color currentColor = image.color;

            // Set the new alpha value (opacity)
            currentColor.a = toOpacity;

            // Apply the new color to the image
            image.color = currentColor;
        }
    }
}