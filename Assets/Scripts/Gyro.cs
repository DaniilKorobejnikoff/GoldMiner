using UnityEngine;

public class Gyro : MonoBehaviour {
    public float moveSpeed = 5f;
    public float maxHorizontalAngle = 30f;
    public float smoothingFactor = 0.1f;

    private Vector3 initialAcceleration;

    void Start() {
        initialAcceleration = Input.acceleration;
    }

    public float CalculateHorizontalInput() {
        Vector3 currentAcceleration = Input.acceleration - initialAcceleration;
        float maxAngleInRadians = maxHorizontalAngle * Mathf.Deg2Rad;
        Vector3 horizontalAcceleration = new Vector3(currentAcceleration.x, 0f, currentAcceleration.z);
        float angle = Mathf.Atan2(horizontalAcceleration.x, horizontalAcceleration.magnitude);
        float normalizedAngle = Mathf.Clamp(angle / maxAngleInRadians, -1f, 1f);
        float smoothedInput = Mathf.Lerp(0f, normalizedAngle, smoothingFactor);

        float moveDirection = Mathf.Clamp(smoothedInput, -1f, 1f) * moveSpeed;
        Debug.Log(moveDirection);
        if (moveDirection>0.2f) {
            return 1;
            //return moveDirection;
        }
        if (moveDirection < -0.2f) {
            return -1;
            //return moveDirection;
        }
        else {
            return 0;
        }
    }
}
