using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastShoot {
    public static void Shoot(Vector3 shootPosition, Vector3 shootDirection) {
        bool raycastHit = Physics.Raycast(shootPosition, shootDirection, out RaycastHit hitObject);
        if (raycastHit) {
            Debug.Log(hitObject.collider.gameObject);
            Debug.DrawLine(shootPosition, shootPosition + shootDirection * hitObject.distance, Color.red, 2f);
        }
    }
}
