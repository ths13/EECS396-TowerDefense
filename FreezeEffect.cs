using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Code;
using UnityEngine;
using UnityEngine.AI;

public class FreezeEffect : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    string enemyType;
    LayerMask layermask;
    float originalSpeed;
    float freezeDamage;

    void Start()
    {
        freezeDamage = .01f;
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        originalSpeed = navMeshAgent.speed;
        enemyType = gameObject.tag;
        layermask = 1 << LayerMask.NameToLayer("FreezeTower");
    }

    void Update()
    {
        Vector3 originPos = transform.position;
        Collider[] collHits = Physics.OverlapBox(originPos, new Vector3(1.5f, 1.5f, 1.5f),new Quaternion(), layermask);

        float speed = originalSpeed;
        float damage = freezeDamage;

        foreach(var freezeTower in collHits)
        {
            speed = speed / 3;
        }
        damage = damage * collHits.Length;

        navMeshAgent.speed = speed;


        if (enemyType == "NormalEnemy")
        {
            gameObject.GetComponent<NormalEnemy>().DealDamage(damage);
        }
        else if (enemyType == "FastEnemy")
        {
            gameObject.GetComponent<FastEnemy>().DealDamage(damage);
        }
        else if (enemyType == "StrongEnemy")
        {
             gameObject.GetComponent<StrongEnemy>().DealDamage(damage);
        }
    }

}
