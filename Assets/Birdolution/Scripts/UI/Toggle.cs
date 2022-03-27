using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle : MonoBehaviour
{
    public void ToggleObject(GameObject toggleObject)
    {
        toggleObject.SetActive(!toggleObject.activeInHierarchy);
    }
}
