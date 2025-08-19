using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SOUIIntUpdate : MonoBehaviour
{
    public SOInt SOInt;
    public TMP_Text uiTextValue;

    void Update()
    {
        uiTextValue.text = SOInt.value.ToString();
    }
}
