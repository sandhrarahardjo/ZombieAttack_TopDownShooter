using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private bool canSpawn = true;
    [SerializeField] private float spawnDistance = 10f; // Distance from the camera boundary to spawn enemies

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (true)
        {
            yield return wait;
            int rand = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyToSpawn = enemyPrefabs[rand];
            
            Vector3 spawnPosition = GetRandomSpawnPosition();
            Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 cameraBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 cameraTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        float xMin = cameraBottomLeft.x - spawnDistance;
        float xMax = cameraTopRight.x + spawnDistance;
        float yMin = cameraBottomLeft.y - spawnDistance;
        float yMax = cameraTopRight.y + spawnDistance;

        float spawnX = Random.Range(xMin, xMax);
        float spawnY = Random.Range(yMin, yMax);

        return new Vector3(spawnX, spawnY, 0);
    }
}
