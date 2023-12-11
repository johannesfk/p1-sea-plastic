using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIClickThroughFix : MonoBehaviour
{    
    void OnEnable()
    {
       HexGrid.instance.uiActive = true;
    }

    // Update is called once per frame
    void OnDisable()
    {
        HexGrid.instance.uiActive = false;
    }
}
