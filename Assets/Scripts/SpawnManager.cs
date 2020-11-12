using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] asteroidPrefabs;
    public GameObject[] bottlePrefabs;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        Invoke("SpawnRandomAsteroid", 66f);
        //Invoke ("SpawnOutOfBoundsRandomAsteroid", 60f);
        Invoke("SpawnRandomBottle", 1f);
    }

    // Update is called once per frame
    // Spawn random bottle
    void SpawnRandomAsteroid()
    {
        if (playerControllerScript.gameOver == false)
        {
            // Generate random Asteroid index
            float randomX = Random.Range(-10f, 10f);
            Vector3 spawnPos = new Vector3(randomX, 0, 30);

            // instantiate Asteroid
            int asteroidIndex = Random.Range(0, asteroidPrefabs.Length);
            Instantiate(asteroidPrefabs[asteroidIndex], spawnPos, asteroidPrefabs[asteroidIndex].transform.rotation);

            float AsteroidTime;
            if (Time.deltaTime <= 160f)
            {
                AsteroidTime = Random.Range(4f, 8f);
            }
            else if (Time.deltaTime <= 250f)
            {
                AsteroidTime = Random.Range(3f, 6f);
            }
            else {
                AsteroidTime = Random.Range(2f, 4.5f);
            }

            Invoke("SpawnRandomAsteroid", AsteroidTime);
        }
    }

    void SpawnRandomBottle()
    {
        if (playerControllerScript.gameOver == false)
        {
            // Generate random bottle index
            float randomX = Random.Range(-5f, 5f);
            Vector3 spawnPos = new Vector3(randomX, 0, 10);

            // instantiate bottle
            int bottleIndex = Random.Range(0, bottlePrefabs.Length);
            Instantiate(bottlePrefabs[bottleIndex], spawnPos, bottlePrefabs[bottleIndex].transform.rotation);

            float bottleTime = Random.Range(4f, 6f);
            Invoke("SpawnRandomBottle", bottleTime);
        }
    }

    /* void SpawnOutOfBoundsRandomAsteroid () {
        if (playerControllerScript.gameOver == false) {
            // Generate random wall index
            float randomminusX = Random.Range (-15f, -7.5f);
            float randomplusX = Random.Range (7.5f, 15f);
            float randomminusY = Random.Range (-15f, -3f);
            float randomplusY = Random.Range (3f, 15f);
            Vector3 spawnPos1 = new Vector3 (randomminusX, randomminusY, 50);
            Vector3 spawnPos2 = new Vector3 (randomminusX, randomplusY, 50);
            Vector3 spawnPos3 = new Vector3 (randomplusX, randomminusY, 50);
            Vector3 spawnPos4 = new Vector3 (randomplusX, randomplusY, 50);

            // instantiate wall
            int asteroidIndex1 = Random.Range (0, asteroidPrefabs.Length);
            int asteroidIndex2 = Random.Range (0, asteroidPrefabs.Length);
            int asteroidIndex3 = Random.Range (0, asteroidPrefabs.Length);
            int asteroidIndex4 = Random.Range (0, asteroidPrefabs.Length);

            Instantiate (asteroidPrefabs[asteroidIndex1], spawnPos1, asteroidPrefabs[asteroidIndex1].transform.rotation);
            Instantiate (asteroidPrefabs[asteroidIndex2], spawnPos2, asteroidPrefabs[asteroidIndex2].transform.rotation);
            Instantiate (asteroidPrefabs[asteroidIndex3], spawnPos3, asteroidPrefabs[asteroidIndex3].transform.rotation);
            Instantiate (asteroidPrefabs[asteroidIndex4], spawnPos4, asteroidPrefabs[asteroidIndex4].transform.rotation);

            Invoke ("SpawnOutOfBoundsRandomAsteroid", 7f);
        }
    } */
}