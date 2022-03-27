using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "AnimalSO", menuName = "ScriptableObjects/AnimalSO", order = 1)]
public class AnimalSO : ScriptableObject
{
    public int level;
    public int cointPerSecond;
    public GameObject prefab;
    public bool canKillEnemy;
    public Sprite birdImage;
}