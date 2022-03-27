using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    [SerializeField] private Transform[] spawns;

    [SerializeField] private GameObject enemyPrefab;

    public float spawnTime = 10f;

    private float passedTime = 0;
    public float timeBetweenIncrease = 60f;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    var enemy = Instantiate(enemyPrefab, new Vector3(20, 0.5f, 2), default);
        //    var direction = -Vector3.right;
        //    enemy.GetComponent<Enemy>().Move(direction);
        //    enemy.transform.LookAt(new Vector3(1, 0, enemy.transform.position.z));
        //}

        //if (spawnTime < 4)
        //    return;

        //passedTime += Time.deltaTime;

        //if (passedTime > timeBetweenIncrease)
        //{
        //    passedTime = 0;
        //    spawnTime--;
        //}
    }

    public void StartSpawning()
    {
        //StartCoroutine(EnemySpawn());
    }

    IEnumerator EnemySpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);

            var currentSpawn = spawns[Random.Range(0, spawns.Length)];
            var enemy = Instantiate(enemyPrefab, currentSpawn.transform.position, default);
            float randomAddjust = Random.Range(-3, 3);

            Vector3 direction;

            // left or right
            if (Mathf.Abs(currentSpawn.position.x) > Grid.instance.gridXLenght)
            {
                enemy.transform.position += new Vector3(0, 0, randomAddjust);
                direction = -Vector3.right * Mathf.Sign(currentSpawn.position.x);
                enemy.transform.LookAt(new Vector3(1, 0, enemy.transform.position.z));
            }
            else // front or back
            {
                enemy.transform.position += new Vector3(randomAddjust, 0, 0);
                direction = -Vector3.forward * Mathf.Sign(currentSpawn.position.z);
                enemy.transform.LookAt(new Vector3(enemy.transform.position.x, 0, 1));
            }

            enemy.GetComponent<Enemy>().Move(direction);
        }
    }

}
