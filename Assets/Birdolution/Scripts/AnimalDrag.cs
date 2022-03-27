using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalDrag : MonoBehaviour
{
    public bool dragging = false;

    [SerializeField] float gridSnapSize = 5;
    [SerializeField] float fixedHieght = 5;

    private void Update()
    {
        if (PauseMenu.instance.isPaused) return;

        if (dragging)//_drag == null &&
        {
            Transform draggingObject = transform;

            Plane plane = new Plane(Vector3.up, Vector3.up * fixedHieght); // ground plane

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float distance; // the distance from the ray origin to the ray intersection of the plane
            if (plane.Raycast(ray, out distance))
            {
                Vector3 rayPoint = ray.GetPoint(distance);
                Vector3 snappedRayPoint = rayPoint;
                snappedRayPoint.x = (Mathf.RoundToInt(rayPoint.x / gridSnapSize) * gridSnapSize);
                snappedRayPoint.z = (Mathf.RoundToInt(rayPoint.z / gridSnapSize) * gridSnapSize);
                draggingObject.position = snappedRayPoint;
            }
        }
    }
    void OnMouseDown()
    {
        if (PauseMenu.instance.isPaused) return;

        dragging = true;
    }

    void OnMouseUp()
    {
        if (PauseMenu.instance.isPaused) return;

        dragging = false;

        if (isTouchingAnotherAnimal())
            return;

        if (!IsGrounded())
        {
            GetComponent<Animal>().Kill();
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, transform.localScale.y + 0.1f, GameManager.instance.groundMask);
    }

    private bool isTouchingAnotherAnimal()
    {
        var touchingObjects = Physics.OverlapSphere(transform.position, transform.localScale.z);

        foreach(var obj in touchingObjects)
        {
            var animal = obj.GetComponent<Animal>();
            var thisAnimal = GetComponent<Animal>();

            if(animal && thisAnimal)
            {
                if (animal != thisAnimal && animal.GetLevel() == thisAnimal.GetLevel())
                {
                    var thisAnimalLevel = thisAnimal.GetLevel();

                    var animalToSpawn = AnimalSpawner.instance.GetAnimal(thisAnimalLevel++);
                    var animalPrefab = Instantiate(animalToSpawn.prefab, obj.transform.position, default);
                    animalPrefab.GetComponent<Animal>().SetupAnimal(animalToSpawn.level, animalToSpawn.cointPerSecond, animalToSpawn.canKillEnemy);

                    AnimalSpawner.instance.CheckRecord(thisAnimal.GetLevel(), animalToSpawn);

                    Destroy(Instantiate(AnimalSpawner.instance.mergeParticle, transform.position, default), 1f);

                    Destroy(obj.gameObject);
                    Destroy(gameObject);

                    SoundManager.instance.PlayEffect(GameType.SoundTypes.bird_merge);

                    return true;
                }
            }
        }

        return false;
    }
}
