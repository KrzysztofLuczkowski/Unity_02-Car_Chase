using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Referencja do gracza
    private float offsetZ;   // Pocz¹tkowy dystans miêdzy kamer¹ a graczem

    void Start()
    {
        // Zapisujemy pocz¹tkowy dystans miêdzy kamer¹ a graczem
        offsetZ = transform.position.z - player.position.z;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Ustawiamy now¹ pozycjê kamery - zmienia siê tylko Z
            transform.position = new Vector3(transform.position.x, transform.position.y, player.position.z + offsetZ);
        }
    }
}
