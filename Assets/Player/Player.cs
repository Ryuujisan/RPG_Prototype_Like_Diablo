using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float maxHealtPoint = 100f;

    float currentHealtPoint = 100f;

    public float healthAsPercentage
    {
        get
        {
            return currentHealtPoint / maxHealtPoint;
        }
    }

}
