using System;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Code
{
    public class Gun : MonoBehaviour
    {
        private const float FireCooldown = 1.25f;
        private float _lastfire;

        public void FireNormal()
        {
            float time = Time.time;
            if (time < _lastfire + FireCooldown)
            {
                return;
            }
            _lastfire = time;

            NormalTower.normalBullets.ForceSpawn(
                transform.position + transform.up * 0.1f + transform.forward * 0.7f,
                transform.rotation,
                transform.forward * 25f);
        }

        public void FireShock()
        {
            float time = Time.time;
            if (time < _lastfire + FireCooldown + 1f)
            {
                return;
            }
            _lastfire = time;

            ShockTower.shockBullets.ForceSpawn(
                transform.position + transform.up * 0.1f + transform.forward * 0.7f,
                transform.rotation,
                transform.forward * 25f);
        }
    }


}



