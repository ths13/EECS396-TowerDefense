using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rbBullet;
    public float damage;

    public void Initialize(Vector3 velocity)
    {
        GetComponent<Rigidbody>().velocity = velocity;
        damage = 5f;
    }

    internal void OnCollisionEnter(Collision other)
    {

        Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}