using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private GameObject openParticle;

    private void OnMouseDown()
    {
        if (PauseMenu.instance.isPaused) return;

        GameManager.instance.CheckIfFirstBox();

        SoundManager.instance.PlayEffect(GameType.SoundTypes.box_open);

        Destroy(Instantiate(openParticle, transform.position, default), 1f);

        var animalToSpawn = AnimalSpawner.instance.GetAnimal(GameManager.instance.currentBoxLevel);
        var animalPrefab = Instantiate(animalToSpawn.prefab, new Vector3(transform.position.x, 1f, transform.position.z), default);
        animalPrefab.GetComponent<Animal>().SetupAnimal(animalToSpawn.level, animalToSpawn.cointPerSecond, animalToSpawn.canKillEnemy);

        GameManager.instance.canSpawnBox = true;
        Destroy(gameObject);
    }
}
