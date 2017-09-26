using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField]
    private int enemyLayer = 9;

    [SerializeField]
    private float maxHealtPoint = 100f;

    [Range(0, 100)]
    [SerializeField]
    private float damage = 5f;

    [Range(0, 100)]
    [SerializeField]
    private float attackRate = 1.2f;

    [Range(0, 100)]
    [SerializeField]
    private float attackRange;

    private GameObject currentTargeting;

    private float currentHealtPoint = 100f;

    private CameraRaycaster cameraRaycaster = null;

    private float lastTimeHit = 0f;

    private void Start()
    {
        cameraRaycaster = FindObjectOfType<CameraRaycaster>();
        cameraRaycaster.notifyMouseClickObservers += OnClickMause;
    }

    public float healthAsPercentage
    {
        get
        {
            return currentHealtPoint / maxHealtPoint;
        }
    }

    public void OnClickMause(RaycastHit raycastHit, int layerHit)
    {
        if(layerHit == enemyLayer)
        {
            currentTargeting = raycastHit.collider.gameObject;
            var enemy = currentTargeting.GetComponent<Enemy>();

            if ((enemy.transform.position - transform.position).magnitude > attackRange) return;

            if (Time.time - lastTimeHit > attackRate)
            {
                enemy.TakeDamage(damage);
                lastTimeHit = Time.time;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealtPoint = Mathf.Clamp(currentHealtPoint - damage, 0f, maxHealtPoint);
    }
}
