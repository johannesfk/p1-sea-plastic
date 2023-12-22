using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIClickThroughFix : MonoBehaviour
{
    void OnEnable()
    {
        HexInteraction.instance.uiActive = true;
    }

    // Update is called once per frame
    void OnDisable()
    {
        HexInteraction.instance.uiActive = false;
    }
}
