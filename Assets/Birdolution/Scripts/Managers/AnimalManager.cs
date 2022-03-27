using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    public static AnimalManager instance;

    public float movSpeed = 0.3f;
    public float timeBetweenMov = 2.0f;
    public float timeBetweenClicks = 1f;

    private void Awake()
    {
        instance = this;
    }
}
