using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VehicleController : MonoBehaviour
{
    public float speed = 10f;  // Prêdkoœæ pojazdu
    public bool isOppositeVehicle = false; // Czy pojazd jedzie w przeciwn¹ stronê?

    private Transform player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        float yRotation = transform.eulerAngles.y;
        if (Mathf.Abs(Mathf.DeltaAngle(yRotation, 180f)) < 1f)
        {
            isOppositeVehicle = true;
        }
    }

    void Update()
    {
        if (isOppositeVehicle)
        {
            transform.position -= Vector3.forward * speed * Time.deltaTime; // Jedzie do ty³u (na -Z)
        }
        else
        {
            transform.position += Vector3.forward * speed * Time.deltaTime; // Jedzie do przodu (+Z)
        }

        // Sprawdzenie, czy pojazd jest za graczem (Z - 5)
        if (player != null && transform.position.z < player.position.z - 5f)
        {
            HandleVehicleDestruction();
        }
    }

    void HandleVehicleDestruction()
    {
        bool hardcoreMode = GameManager.instance.hardcoreMode;

        
        if (isOppositeVehicle && hardcoreMode)
        {
           
            Destroy(gameObject);
            Score_Manager.instance.Add_Point();
            Score_Manager.instance.Add_Point();
        }
        else if (isOppositeVehicle && !hardcoreMode)
        {
            Destroy(gameObject);
        }
        else if (!isOppositeVehicle && hardcoreMode)
        {
            Destroy(gameObject);
        }
        else if (!isOppositeVehicle && !hardcoreMode)
        {
            Destroy(gameObject);
            Score_Manager.instance.Add_Point();
        }
    }
}