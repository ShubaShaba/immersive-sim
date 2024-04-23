using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvStaticObj : MonoBehaviour, ShootingTarget
{
    public void OnHit()
    {
        Debug.Log(gameObject);
    }
}
