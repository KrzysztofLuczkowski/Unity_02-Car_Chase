using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public GameObject roadPrefab;
    public Transform player;

    private float nextSpawnZ = 250f; // Pierwsza droga pojawia siê tutaj
    private float spawnStep = 50f; // Odleg³oœæ miêdzy drogami

    void Update()
    {
        if (player.position.z >= nextSpawnZ - 200f) // Sprawdzamy, czy gracz przeszed³ X jednostek
        {
            SpawnRoad();
        }
    }

    private void SpawnRoad()
    {
        Vector3 spawnPosition = new Vector3(-25.12f, 2.5f, nextSpawnZ);
        Quaternion spawnRotation = Quaternion.Euler(90f, 90f, 0f);

        Instantiate(roadPrefab, spawnPosition, spawnRotation);
        nextSpawnZ += spawnStep; // Przygotowanie na kolejny segment
    }
}

