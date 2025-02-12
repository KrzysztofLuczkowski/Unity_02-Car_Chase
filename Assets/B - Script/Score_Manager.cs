using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_Manager : MonoBehaviour
{
    public static Score_Manager instance;
    public int score_Count = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Add_Point()
    {
        score_Count++;
    }
}
