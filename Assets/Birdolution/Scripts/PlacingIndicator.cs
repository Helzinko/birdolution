using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingIndicator : MonoBehaviour
{
    private Renderer rend;

    [SerializeField] private Color baseColor;
    [SerializeField] private Color mouseOverColor;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = baseColor;
    }

    private void OnMouseOver()
    {
        rend.material.color = mouseOverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = baseColor;
    }

    private void OnMouseDown()
    {
        ObjectManager.instance.PlaceObject(transform.parent.gameObject);
    }
}
