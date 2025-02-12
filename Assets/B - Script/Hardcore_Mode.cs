using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton dla ³atwego dostêpu
    public bool hardcoreMode = false;   // Tryb hardcore

    void Awake()
    {
        // Upewniamy siê, ¿e jest tylko jedna instancja GameManagera
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    // Funkcja do ustawienia trybu Hardcore
    public void SetHardcoreMode(bool value)
    {
        hardcoreMode = value;
    }
}
