using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    public Transform thisObject;
    public Transform target;
    private NavMeshAgent navComponent;
    public Spawn sp;

    internal void Start () {
        target = GameObject.FindGameObjectWithTag("Base").transform;
        navComponent = this.gameObject.GetComponent<NavMeshAgent>();
        sp = FindObjectOfType<Spawn>();

    }

    void Update()
    {
        navComponent.SetDestination(target.position);
        if (sp.GameOver)
        {
            navComponent.isStopped = true;
            gameObject.GetComponent<EnemyMovement>().enabled = false;
        }
    } 
}