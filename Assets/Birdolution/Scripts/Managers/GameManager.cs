using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isPlacingLand = false;

    public LayerMask groundMask;
    public LayerMask emptyMask;

    public GameObject boxToSpawn;

    public bool isDraggingAnimal = false;

    public bool canSpawnBox = true;

    [SerializeField] private GameObject boxSpawnParticle;

    [SerializeField] private GameObject gameOverText;

    public int currentBoxLevel = 0;

    public bool playerLost = false;

    public GameObject popupText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SpawnAnimal();

        SoundManager.instance.PlayMusic(GameType.SoundTypes.music);
    }

    public void StartPlacingLand()
    {
        if (isPlacingLand)
        {
            isPlacingLand = !isPlacingLand;
            Grid.instance.ToggleTilePlacingIndicators();
        }
        else
        {
            if (BankManager.instance.CanBuyLand() && !ObjectManager.instance.isPlacingObject)
            {
                isPlacingLand = !isPlacingLand;
                Grid.instance.ToggleTilePlacingIndicators();
            }
            else
            {
                SoundManager.instance.PlayEffect(GameType.SoundTypes.ui_error);
            }
        }
    }

    public void BuyAnimal()
    {
        if (BankManager.instance.CanBuyAnimal() && canSpawnBox)
        {
            SpawnAnimal();
            BankManager.instance.RemoveMoney(BankManager.Prices.animalPrice);
        }
        else
        {
            SoundManager.instance.PlayEffect(GameType.SoundTypes.ui_error);
        }
    }

    public void BuyUpgrade()
    {
        if (BankManager.instance.CanUpgradeBox())
        {
            currentBoxLevel++;
            BankManager.instance.RemoveMoney(BankManager.Prices.boxUpradePrice);

            //ShowPopupTextForBuying(BankManager.Prices.boxUpradePrice);
        }
        else
        {
            SoundManager.instance.PlayEffect(GameType.SoundTypes.ui_error);
        }
    }

    private void Update()
    {
        if(playerLost && Input.GetKeyDown(KeyCode.Return))
        {
            PauseMenu.instance.Restart();
        }
    }

    private void SpawnAnimal()
    {
        var spawnPos = new Vector3(Grid.instance.centerCoord.x, 0.5f, Grid.instance.centerCoord.y);

        Instantiate(boxToSpawn, spawnPos, default);
        Destroy(Instantiate(boxSpawnParticle, spawnPos, default), 1f);
        SoundManager.instance.PlayEffect(GameType.SoundTypes.box_buy);
        canSpawnBox = false;
    }

    [SerializeField] private GameObject StartText;
    [SerializeField] private GameObject RandomTip;
    private bool firstBox = true;
    public void CheckIfFirstBox()
    {
        if (firstBox)
        {
            EnemySpawner.instance.StartSpawning();
            StartText.SetActive(false);
            RandomTip.SetActive(false);
            firstBox = false;
        }
    }

    public void CheckBirdCount(int minus = 0)
    {
        var birds = FindObjectsOfType<Animal>();

        if(birds.Length - minus == 0)
        {
            PauseMenu.instance.gameplayUI.SetActive(false);
            gameOverText.SetActive(true);
            playerLost = true;
            SoundManager.instance.PlayEffect(GameType.SoundTypes.game_over);
        }
    }
}
