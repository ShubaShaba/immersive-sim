using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// TODO: Refactor
public class ThrowingObjectsTrajectory : MonoBehaviour{
    [SerializeField] private PlayerInput input;
    private LineRenderer lineRenderer;

    /*
      The field linePoints affects the length of a projection, 
      while the timeBetweenPoints determines the amount of "rendering points" between them. 
      Meaning, the bigger the timeBetweenPoints => more smooth line.  
    */
    [Range(1,25)] [SerializeField] private int linePoints = 15;
    [Range(0.1f, 1f)] [SerializeField] private float timeBetweenPoints = 0.1f;
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
            Vector3 startPosition = releasePosition.position;
            Vector3 startVelocity = throwingObjectData.Item2 * throwingObjectData.Item3 / throwingObjectData.Item1; 
            // (Momentum * Direction) / Mass

            lineRenderer.enabled = true;
            lineRenderer.positionCount = Mathf.CeilToInt(linePoints / timeBetweenPoints);
            lineRenderer.SetPosition(0, startPosition);
            int i = 1;
            for (float time = timeBetweenPoints; time < linePoints; time += timeBetweenPoints) {
                // Determine the current projection rendering point based on passed time:
                Vector3 point = startPosition + time * startVelocity;
                point.y = startPosition.y + startVelocity.y * time + (Physics.gravity.y / 2f * time * time);
                lineRenderer.SetPosition(i, point);

                // Checking if projection is hitting something:
                Vector3 lastPosition = lineRenderer.GetPosition(i - 1);
                Vector3 currentDirection = (point - lastPosition).normalized;
                float magnitude = (point - lastPosition).magnitude;
                if (Physics.Raycast(lastPosition, currentDirection, out RaycastHit hit, magnitude)) {
                    lineRenderer.SetPosition(i, hit.point);
                    /*
                        Setting the total position count to current point to remove previous 
                        drawings, which otherwise won't get overridden from previous frame. 
                        And return.
                    */   
                    lineRenderer.positionCount = i + 1; 
                    return;
                }
                i++;
            }
        } else {
            lineRenderer.enabled = false;
        }
    }
}
