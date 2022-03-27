using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10.0f;

    private Vector3 gameCenterPos = Vector3.zero;

    private void Start()
    {
        transform.position = new Vector3(Grid.instance.gridXLenght / 2, 0, Grid.instance.gridZLenght / 2);
    }

    private void LateUpdate()
    {
        var inputRot = Input.GetAxis("Horizontal");

        if(inputRot != 0)
            transform.Rotate(0, rotationSpeed * -inputRot * Time.deltaTime, 0);
    }
}
