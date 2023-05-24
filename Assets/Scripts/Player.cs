using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7;
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private float turnSmoothVelocity;

    // Start is called before the first frame update
    void Start()
    {
              
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalDirection = Input.GetAxisRaw("Horizontal");
        float verticalDirection = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection = new Vector3(horizontalDirection, 0, verticalDirection).normalized;
        float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cameraPosition.eulerAngles.y;
        float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, 0.1f);


        if (inputDirection.magnitude != 0)
        {
            transform.eulerAngles = Vector3.up * smoothedAngle;
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
        }
    }
}
