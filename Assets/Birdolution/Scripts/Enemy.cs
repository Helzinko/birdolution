using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;

    public float enemySpeed = 10f;

    public int animalLevelToLose = 6;

    public int moneyToAdd = 1000;

    private bool canMove = false;
    private Vector3 moveDir;

    private void OnTriggerEnter(Collider other)
    {
        var animal = other.GetComponent<Animal>();

        if (animal)
        {
            if (animal.CanKillEnemy())
            {
                Kill();
            }
            else
            {
                animal.Kill();
            }
        }
        else
        {
            var trap = other.GetComponent<Trap>();

            if (trap)
            {
                trap.PlayTrap();
            }
        }

    }

    public void Move(Vector3 direction)
    {
        rb = GetComponent<Rigidbody>();
        moveDir = direction;
        canMove = true;
        Destroy(gameObject, 15f);
        SoundManager.instance.PlayEffect(GameType.SoundTypes.eagle_spawn);
    }

    private void Update()
    {
        if (!canMove) return;

        transform.position += moveDir * enemySpeed * Time.deltaTime;
    }

    public void Kill()
    {
        Destroy(Instantiate(AnimalSpawner.instance.deathParticlearticle, transform.position, default), 1f);
        BankManager.instance.AddMoney(moneyToAdd);
        SoundManager.instance.PlayEffect(GameType.SoundTypes.bird_hurt);

        GameObject floatingText = Instantiate(GameManager.instance.popupText, transform.position, Quaternion.identity);
        floatingText.GetComponent<PopupText>().displayText = "+" + moneyToAdd;

        Destroy(gameObject);
    }
}
