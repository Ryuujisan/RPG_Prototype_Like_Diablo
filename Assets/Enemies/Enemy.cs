using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    float maxHealtPoint = 100f;

    [SerializeField]
    float radiusAttack = 4f;

    [SerializeField]
    float chaseRadius = 6f;

    [SerializeField]
    float damagePerShot = 9f;

    [SerializeField]
    float shotPerSecend = 0.5f;

    [SerializeField]
    Vector3 verticalAimOffset = Vector3.up;

    [SerializeField]
    private GameObject ProjectileToUse;

    [SerializeField]
    private GameObject projectailSocket;

    [SerializeField]
    AICharacterControl ai;

    private ThirdPersonCharacter thirPersonCharacter = null;

    private GameObject player;

    float currentHealtPoint = 100f;
    private bool isAttaking = false;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        thirPersonCharacter = GetComponent<ThirdPersonCharacter>();
        currentHealtPoint = maxHealtPoint;
    }


    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if(distanceToPlayer <= radiusAttack && !isAttaking)
        {
            isAttaking = true;
            InvokeRepeating("SpawnProjectail", 0f, shotPerSecend);
        }

        if (distanceToPlayer >= radiusAttack)
        {
            isAttaking = false;
            CancelInvoke();
        }



        if (distanceToPlayer <= chaseRadius)
        {
            ai.target = player.transform ;
        }
        else
        {
            ai.target = transform;
        }
    }

    private void SpawnProjectail()
    {
        GameObject newProjectail = Instantiate(ProjectileToUse, projectailSocket.transform.position, Quaternion.identity) as GameObject;
        var projectailComponent = newProjectail.GetComponent<Projectile>().damageCount = damagePerShot;

        Vector3 dir = (player.transform.position + verticalAimOffset - projectailSocket.transform.position).normalized;
        newProjectail.GetComponent<Rigidbody>().velocity = dir * projectailComponent;
    }

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

        if(currentHealtPoint <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;

        //Draw attack
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusAttack);

        //Chase radius
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }

}
