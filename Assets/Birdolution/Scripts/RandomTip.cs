using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomTip : MonoBehaviour
{
    [SerializeField] public string[] tips;

    [SerializeField] public TextMeshProUGUI moneyText;

    void Start()
    {
        moneyText.text = tips[Random.Range(0, tips.Length)];
    }
}
