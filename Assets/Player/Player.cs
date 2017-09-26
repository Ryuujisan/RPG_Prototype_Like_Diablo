using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
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

    public void TakeDamage(float damage)
    {
        currentHealtPoint = Mathf.Clamp(currentHealtPoint - damage, 0f, maxHealtPoint);
    }
}
