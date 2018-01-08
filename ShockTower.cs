using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Code;
using UnityEngine;

public class ShockTower : MonoBehaviour
{
    public static ShockTower shock;
    private Gun _gun;
    public static BulletManager shockBullets;
    public Vector3 lookDirection;
    public float closestDistance = Mathf.Infinity;
    public Spawn sp;

    void Start()
    {
        shock = this;
        _gun = GetComponent<Gun>();
        shockBullets = new BulletManager(GameObject.Find("shockBullets").transform, "ShockBullet");
        sp = FindObjectOfType<Spawn>();
    }

    void Update()
    {

        Vector3 originPos = transform.position;

        RaycastHit[] collHits = Physics.SphereCastAll(originPos, 1f, transform.forward, 0.5f);

        RaycastHit hit;

        if (collHits.Length > 0)
        {
            hit = collHits[0];

            foreach (RaycastHit temp in collHits)
            {
                if ((temp.transform.tag == "NormalEnemy") || (temp.transform.tag == "FastEnemy") || (temp.transform.tag == "StrongEnemy"))
                {
                    if (temp.distance < closestDistance)
                    {
                        closestDistance = temp.distance;
                        hit = temp;
                    }
                }
            }

            if ((hit.transform.tag == "NormalEnemy") || (hit.transform.tag == "FastEnemy") || (hit.transform.tag == "StrongEnemy"))
            {
                lookDirection = hit.point - (transform.position + transform.up * 0.1f);
                Quaternion rotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = rotation;
                _gun.FireShock();
            }
        }

        closestDistance = Mathf.Infinity;

        if (sp.GameOver)
        {
            gameObject.GetComponent<ShockTower>().enabled = false;
        }

    }
}
