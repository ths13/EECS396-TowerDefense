using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour {

    Vector3 target;
    Spawn spawner;

	void Start () {
        target = new Vector3(150, 60);
        spawner = FindObjectOfType<Spawn>();
    }
	
	void Update () {
        if (!spawner.GameOver)
        {
            if (transform.position == target)
            {
                spawner.ProcessWave(gameObject.tag);
                Destroy(gameObject);
            }
            else
            {
                float speed = 30f;
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target, step);
            }
        }
    }
}
