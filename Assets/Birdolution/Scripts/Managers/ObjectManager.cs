using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance;

    [HideInInspector] public bool isPlacingObject = false;

    private ObjectSO objectForPlacemenet;

    public GameObject placingParticle;

    private void Awake()
    {
        instance = this;
    }

    public void BuyObject(ObjectSO objectToPlace)
    {
        if (isPlacingObject)
        {
            StopPlacingObject();
        }
        else
        {
            if (BankManager.instance.CanBuyObject(objectToPlace.price) && !GameManager.instance.isPlacingLand)
            {
                Grid.instance.TogglePlacingIndicators(true);
                isPlacingObject = true;
                objectForPlacemenet = objectToPlace;
            }
            else
            {
                SoundManager.instance.PlayEffect(GameType.SoundTypes.ui_error);
            }
        }

    }

    public void PlaceObject(GameObject landCube)
    {
        StopPlacingObject();

        if (BankManager.instance.CanBuyObject(objectForPlacemenet.price))
        {
            BankManager.instance.RemoveMoney(objectForPlacemenet.price);

            var previousTilePosition = landCube.GetComponent<LandCube>().transform.position;
            Grid.instance.ReplaceCube((int)previousTilePosition.x, (int)previousTilePosition.z, objectForPlacemenet.prefab);

            SoundManager.instance.PlayEffect(GameType.SoundTypes.place_land);
            Destroy(Instantiate(placingParticle, new Vector3(previousTilePosition.x, 1, previousTilePosition.z), default), 1f);
        }
        else
        {
            SoundManager.instance.PlayEffect(GameType.SoundTypes.ui_error);
        }
    }

    public void StopPlacingObject()
    {
        Grid.instance.TogglePlacingIndicators(false);
        isPlacingObject = false;
    }
}
