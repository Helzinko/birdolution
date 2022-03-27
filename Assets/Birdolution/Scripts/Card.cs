using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeReference] private Image animalImage;
    [SerializeReference] private TextMeshProUGUI levelText;
    [SerializeReference] private TextMeshProUGUI coinsPerSecondText;
    [SerializeReference] private TextMeshProUGUI canKillEnemyText;

    public void OkayButton()
    {
        PauseMenu.instance.Resume();
        gameObject.SetActive(false);
    }

    public void SetupCard(Sprite animalImage, string levelText, string coinsPerSecondText, bool canKillEnemy)
    {
        this.animalImage.sprite = animalImage;
        this.levelText.text = "Level: " + levelText;
        this.coinsPerSecondText.text = "COINS PER SECOND: " + coinsPerSecondText;

        if (canKillEnemy)
            this.canKillEnemyText.text = "CAN KILL ENEMY: YES";
        else this.canKillEnemyText.text = "CAN KILL ENEMY: NO";
    }
}
