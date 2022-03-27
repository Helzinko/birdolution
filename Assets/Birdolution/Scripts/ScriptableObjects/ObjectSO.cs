using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectSO", menuName = "ScriptableObjects/ObjectSO", order = 1)]
public class ObjectSO : ScriptableObject
{
    public GameType.ObjectType type;
    public int price;
    public GameObject prefab;
}