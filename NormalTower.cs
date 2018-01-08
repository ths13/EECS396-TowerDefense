using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Code;
using UnityEngine;

public class NormalTower : MonoBehaviour
{
    public static NormalTower norm;
    private Gun _gun;
    public static BulletManager normalBullets;
    public Vector3 lookDirection;
    public float closestDistance = Mathf.Infinity;
    public Spawn sp;

    void Start ()
	{
	    norm = this;
        _gun = GetComponent<Gun>();
        normalBullets = new BulletManager(GameObject.Find("normalBullets").transform, "NormalBullet");
	    sp = FindObjectOfType<Spawn>();
    }

    void Update()
    {

        Vector3 originPos = transform.position;

        RaycastHit[] collHits = Physics.SphereCastAll(originPos, 1f, transform.forward, Mathf.Infinity);

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
                _gun.FireNormal();
            }
        }

        closestDistance = Mathf.Infinity;

        if (sp.GameOver)
        {
            gameObject.GetComponent<NormalTower>().enabled = false;
        }

    }
}
