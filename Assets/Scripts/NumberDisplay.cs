using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberDisplay : MonoBehaviour
{
    public Text numberText;
    private int number = 0;

    void Start()
    {
        numberText.text = number.ToString();
    }
    public void ChangeNumber(int newNumber)
    {
        number = newNumber;
        numberText.text = number.ToString();
    }
}
