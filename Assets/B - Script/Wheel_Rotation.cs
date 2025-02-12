using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    public List<Transform> wheels; // Lista kó³ do przypisania w inspektorze
    public float rotationSpeed = 300f; // Prêdkoœæ obrotu kó³

    void Update()
    {
        foreach (Transform wheel in wheels)
        {
            if (wheel != null)
            {
                wheel.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
            }
        }
    }
}
