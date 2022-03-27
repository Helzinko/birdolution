using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandCube : Cube
{
    public GameObject placingIndicator;

    private void Start()
    {
        ShowPlacingIndication(false);
    }

    public void ShowPlacingIndication(bool active)
    {
        placingIndicator.SetActive(active);
    }

}
