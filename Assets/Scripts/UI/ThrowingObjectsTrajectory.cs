using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// TODO: Refactor
public class ThrowingObjectsTrajectory : MonoBehaviour{
    [SerializeField] private PlayerInput input;
    private LineRenderer lineRenderer;
    private bool isEnabled;

    private void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        isEnabled = false;
        input.GetPlayersActions().Aim.performed += DrawingHandler;  
    }

    private void DrawingHandler(InputAction.CallbackContext context) {
        isEnabled = !isEnabled;
    }

    private void Update() {
        DrawProjection();
    }

    private void DrawProjection() {
        Player player = input.GetPlayer();
        (float, float, Vector3) throwingObjectData = player.GetThrowingObjectData();
        if (isEnabled && throwingObjectData != (0f, 0f, Vector3.zero)) {
            Transform releasePosition = player.GetMountingPoint();
            int linePoints = 25; 
            float timeBetweenPoints = 0.1f;
            
            lineRenderer.enabled = true;
            lineRenderer.positionCount = Mathf.CeilToInt(linePoints / timeBetweenPoints);
            Vector3 startPosition = releasePosition.position;
            // (Momentum * Direction) / Mass
            Vector3 startVelocity = throwingObjectData.Item2 * throwingObjectData.Item3 / throwingObjectData.Item1;

            int i = 0;
            lineRenderer.SetPosition(i, startPosition);
            for (float time = timeBetweenPoints; time < linePoints; time += timeBetweenPoints) {
                i++;
                Vector3 point = startPosition + time * startVelocity; 
                point.y = startPosition.y + startVelocity.y * time + (Physics.gravity.y / 2f * time * time);
                lineRenderer.SetPosition(i, point);
            }
        } else {
            lineRenderer.enabled = false;
        }
    }
}
