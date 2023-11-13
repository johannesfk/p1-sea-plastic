using UnityEngine;

public class OpacityManager : MonoBehaviour
{
    public ChangeOpacity opacityController;

    void Start()
    {
        // Assuming you have a reference to the ImageOpacityController
        if (opacityController != null)
        {
            // Call ChangeOpacity for the third image in the list (index 2)
            opacityController.ChangeImageOpacity(0.5f);
        }
    }
}