using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OnMouseOverExample : MonoBehaviour
{
    public TextMeshProUGUI uiPickDropThrow;
    public ObjectScript objScript;

    void OnMouseOver()
    {
        uiPickDropThrow.text = "Press E to Pick Up Object";
        if (objScript.heldObjPrefab != null)
        {
            uiPickDropThrow.text = "Press E to Drop the Object. Press Q to YEET the object.";
        }
        Debug.Log("Mouse is over GameObject.");
    }

    void OnMouseExit()
    {
        uiPickDropThrow.text = " ";
        Debug.Log("Mouse is no longer on GameObject.");
    }
}
