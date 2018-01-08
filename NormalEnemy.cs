using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NUnit.Framework.Constraints;
using UnityEngine;

public class NormalEnemy : MonoBehaviour {
    public float AttackDamage { get; set; }
    public float HitPoints { get; set; }

    public MoneyManager Money;

    public List<GameObject> closeitems = new List<GameObject>();
    public List<GameObject> hitEnemies = new List<GameObject>();
    public Color CurrentColor;

    internal void Start () {
        AttackDamage = 10f;
        HitPoints = 20f;
        Money = FindObjectOfType<MoneyManager>();

        CurrentColor = transform.gameObject.GetComponent<Renderer>().material.color;

    }

    internal void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Base")
            Die();
        //control for determining how much damage is done to enemy
        else if (other.gameObject.tag == "NormalBullet")
        {
            var damage = other.gameObject.GetComponent<Bullet>().damage;
            DealDamage(damage);
        }

        else if (other.gameObject.tag == "ShockBullet")
        {

            //change value of damage for shock bullets
            var damage = other.gameObject.GetComponent<Bullet>().damage / 2;
            ShockEffect();

            foreach (GameObject enemyhit in hitEnemies)
            {

                if (enemyhit.tag == "NormalEnemy" && enemyhit != null)
                {
                    enemyhit.GetComponent<NormalEnemy>().DealDamage(damage);

                }
                else if (enemyhit.tag == "FastEnemy" && enemyhit != null)
                {
                    enemyhit.GetComponent<FastEnemy>().DealDamage(damage);
                    
                }
                else if (enemyhit.tag == "StrongEnemy" && enemyhit != null)
                {
                    enemyhit.GetComponent<StrongEnemy>().DealDamage(damage);
                }
            }

            StartCoroutine(ChangeColors());
        }
    }


    IEnumerator ChangeColors()
    {
        foreach (GameObject enemyhit in hitEnemies)
        {

            if (enemyhit.tag == "NormalEnemy")
            {
                enemyhit.GetComponent<Renderer>().material.color = Color.yellow;

            }
            else if (enemyhit.tag == "FastEnemy")
            {
                enemyhit.GetComponent<Renderer>().material.color = Color.yellow;
            }
            else if (enemyhit.tag == "StrongEnemy")
            {
                enemyhit.GetComponent<Renderer>().material.color = Color.yellow;
            }
        }

        yield return new WaitForSeconds(0.05f);


        foreach (GameObject enemyhit in GameObject.FindGameObjectsWithTag("NormalEnemy"))
        {

            enemyhit.GetComponent<Renderer>().material.color = enemyhit.GetComponent<NormalEnemy>().CurrentColor;

        }

        foreach (GameObject enemyhit in GameObject.FindGameObjectsWithTag("FastEnemy"))
        {

            enemyhit.GetComponent<Renderer>().material.color = enemyhit.GetComponent<FastEnemy>().CurrentColor;

        }

        foreach (GameObject enemyhit in GameObject.FindGameObjectsWithTag("StrongEnemy"))
        {

            enemyhit.GetComponent<Renderer>().material.color = enemyhit.GetComponent<StrongEnemy>().CurrentColor;

        }


    }



    public void DealDamage(float damageValue)
    {
        HitPoints -= damageValue;
        if (HitPoints <= 0)
        {
            Money.AddMoney(50);
            Die();
        }
    }

    private void ShockEffect()
    {
        hitEnemies = new List<GameObject>();
        bool nearbyEnemies = true;
        Vector3 originPos = transform.position;

        closeitems = new List<GameObject>();

        while (hitEnemies.Count < 3 && nearbyEnemies)
        {
            
            Collider[] collHits = Physics.OverlapBox(originPos, new Vector3(1.5f, 1.5f, 1.5f), new Quaternion());

            foreach (Collider Enemy in collHits)
            {
                if (Enemy.tag == "NormalEnemy" || Enemy.tag == "FastEnemy" || Enemy.tag == "StrongEnemy")
                {
                    closeitems.Add(Enemy.gameObject);
                }
            }

            if (closeitems.Count == 1)
            {
                nearbyEnemies = false;
            }


            GameObject[] gos;
            gos = closeitems.ToArray();

            GameObject closest = gos[0];
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject go in gos)
            {
                if (go != null)
                {
                    Vector3 diff = go.transform.position - position;
                    float curDistance = diff.sqrMagnitude;
                    if (curDistance < distance && !(hitEnemies.Contains(go)))
                    {

                        closest = go;
                        distance = curDistance;
                    }

                }
            }

            hitEnemies.Add(closest);
            originPos = closest.transform.position;

        }
    }




    private void Die()
    {


        Destroy(gameObject);
    }
}