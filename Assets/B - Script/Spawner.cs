using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class VehicleSpawner : MonoBehaviour
{
    [Header("Prefaby pojazdów")]
    public GameObject[] vehiclePrefabs; // Prefaby pojazdów – losujemy spoœród nich

    [Header("Referencje")]
    public Transform player;  // Referencja do gracza, aby pobieraæ jego pozycjê oraz prêdkoœæ

    [Header("Parametry spawnienia")]
    public float spawnY = 0f;         // Wysokoœæ spawnu
    public float spawnOffsetZ = 200f;    // Odleg³oœæ spawnu na osi Z wzglêdem gracza
    public float spawnDistance = 12f;    // Odleg³oœæ miêdzy spawnami (u¿ywana do obliczenia interwa³u)

    private float spawnTimerOurSide = 0f;       // Timer dla naszej strony drogi
    private float spawnTimerOppositeSide = 0f;  // Timer dla pojazdów z naprzeciwka
    private PlayerController playerController;  // Aby pobieraæ aktualn¹ prêdkoœæ gracza

    // Definicje pasów – ka¿da grupa zawiera 3 mo¿liwe pozycje X
    // Nasza strona drogi
    private float[][] ourLanes = new float[][] {
        new float[] { -4f, -3.5f, -3f },      // Pas pierwszy
        new float[] { -1.6f, -1.2f, -0.8f },    // Pas drugi
        new float[] { 0.8f, 1.2f, 1.6f },       // Pas trzeci
        new float[] { 4f, 3.5f, 3f }            // Pas czwarty
    };

    // Strona pojazdów nadje¿d¿aj¹cych z naprzeciwka
    private float[][] oppositeLanes = new float[][] {
        new float[] { -14.1f, -13.6f, -13.1f }, // Pas pierwszy
        new float[] { -11.7f, -11.3f, -10.9f },  // Pas drugi
        new float[] { -9.4f, -9f, -8.4f },        // Pas trzeci
        new float[] { -7.1f, -6.6f, -6.1f }       // Pas czwarty
    };

    void Start()
    {
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
    }

    void Update()
    {
        if (playerController == null)
            return;

        // Obliczamy interwa³ spawnu na podstawie prêdkoœci gracza:
        float spawnInterval = spawnDistance / playerController.currentSpeed;

        // Interwa³ dla pojazdów z naszej strony (standardowy)
        float spawnIntervalOurSide = spawnInterval;

        // Interwa³ dla pojazdów z naprzeciwka (dwa razy czêœciej)
        float spawnIntervalOppositeSide = spawnInterval / 2;

        //Debug.Log(spawnIntervalOurSide);
        //Debug.Log(spawnIntervalOppositeSide);
        // Aktualizujemy timery
        spawnTimerOurSide += Time.deltaTime;
        spawnTimerOppositeSide += Time.deltaTime;

        // Spawnowanie pojazdów po naszej stronie
        if (spawnTimerOurSide >= spawnIntervalOurSide)
        {
            SpawnVehicles(true);  // True – dla naszej strony
            spawnTimerOurSide = 0f;  // Resetujemy timer
        }

        // Spawnowanie pojazdów z naprzeciwka
        if (spawnTimerOppositeSide >= spawnIntervalOppositeSide)
        {
            SpawnVehicles(false);  // False – dla pojazdów z naprzeciwka
            spawnTimerOppositeSide = 0f;  // Resetujemy timer
        }
    }

    void SpawnVehicles(bool isOurSide)
    {
        List<int> laneIndices = new List<int>() { 0, 1, 2, 3 };
        ShuffleList(laneIndices);

        // Spawnowanie pojazdów
        for (int i = 0; i < 2; i++)  // Pojazdy zawsze po 2
        {
            int laneIndex = laneIndices[i];
            float[] lanePositions = isOurSide ? ourLanes[laneIndex] : oppositeLanes[laneIndex];
            float randomX = lanePositions[Random.Range(0, lanePositions.Length)];
            Vector3 spawnPosition = new Vector3(randomX, spawnY, player.position.z + spawnOffsetZ);

            // Obrót w zale¿noœci od strony – 180° dla naprzeciwka
            Quaternion spawnRotation = isOurSide ? Quaternion.Euler(0f, 0f, 0f) : Quaternion.Euler(0f, 180f, 0f);

            // Losujemy prefab
            int prefabIndex = Random.Range(0, vehiclePrefabs.Length);
            Instantiate(vehiclePrefabs[prefabIndex], spawnPosition, spawnRotation);
        }
    }

    // Pomocnicza funkcja do tasowania listy
    void ShuffleList(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
