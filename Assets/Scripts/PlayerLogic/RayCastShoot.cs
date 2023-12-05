using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastShoot {
    public static void Shoot(Vector3 shootPosition, Vector3 shootDirection) {
        bool raycastHit = Physics.Raycast(shootPosition, shootDirection, out RaycastHit hitObject);
        if (raycastHit && hitObject.transform.TryGetComponent(out ShootingTarget target)) {
            target.OnHit();
            Debug.DrawLine(shootPosition, shootPosition + shootDirection * hitObject.distance, Color.red, 2f);
        } else {
            Debug.DrawLine(shootPosition, shootPosition + shootDirection * 100, Color.yellow, 2f);
        }        
    }
}
