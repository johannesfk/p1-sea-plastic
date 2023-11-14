using UnityEngine;

public class OpacityManager : MonoBehaviour
{
    public ChangeOpacity opacityController;

    void Start()
    {
        opacityController.ChangeIndividualImageOpacity(0, 0.5f);
        opacityController.ChangeIndividualImageOpacity(4, 0.5f);
            
    }
}