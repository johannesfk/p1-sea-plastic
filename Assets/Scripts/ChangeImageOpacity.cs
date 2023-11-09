using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ChangeImageOpacity : MonoBehaviour
{
    public List<Image> myImages; // List to hold multiple Image components
    private Dictionary<Image, float> opacityValues = new Dictionary<Image, float>(); // Map Image components to opacity values

    void Start()
    {
        foreach (Image img in myImages)
        {
            opacityValues[img] = img.color.a; // Store initial opacity values for each Image
        }
    }

    // Set the opacity of a specific image
    public void SetImageOpacity(Image image, float opacityValue)
    {
        if (opacityValues.ContainsKey(image))
        {
            Color imageColor = image.color;
            imageColor.a = opacityValue;
            image.color = imageColor;
            opacityValues[image] = opacityValue; // Update the opacity value for the image
        }
    }

    // Get the current opacity value of a specific image
    public float GetImageOpacity(Image image)
    {
        if (opacityValues.ContainsKey(image))
        {
            return opacityValues[image];
        }
        return 0.0f; // If the image is not found, return 0 opacity
    }
}