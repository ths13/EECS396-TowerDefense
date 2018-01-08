using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// Bullet manager for spawning and tracking all of the game's bullets
/// </summary>
public class BulletManager
{
    private readonly Transform _holder;

    /// <summary>
    /// Bullet prefab. Use GameObject.Instantiate with this to make a new bullet.
    /// </summary>
    private readonly Object _bullet;

    public BulletManager(Transform holder, string bulletType)
    {
        _holder = holder;
        if(bulletType =="NormalBullet")
            _bullet = Resources.Load("NormalBullet");
        else if (bulletType == "ShockBullet")
            _bullet = Resources.Load("ShockBullet");
    }

    public void ForceSpawn(Vector3 pos, Quaternion rotation, Vector3 velocity)
    {
        GameObject bullet = (GameObject)Object.Instantiate(_bullet, pos, rotation);
        bullet.transform.SetParent(_holder);
        bullet.gameObject.GetComponent<Bullet>().Initialize(velocity);

    }
}

