using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCube : Cube
{
    private Cube cube;

    void Start()
    {
        cube = GetComponent<Cube>();
        TogglePlacingIndication();
    }

    private void OnMouseDown()
    {
        if (GameManager.instance.isPlacingLand)
        {
            Grid.instance.ReplaceCube(x, z, Grid.instance.GetRandomLand());
            SoundManager.instance.PlayEffect(GameType.SoundTypes.place_land);
            GameManager.instance.StartPlacingLand();
        }
    }

    public void TogglePlacingIndication()
    {
        gameObject.SetActive(GameManager.instance.isPlacingLand);
    }
}
