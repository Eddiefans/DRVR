using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    public Rigidbody target;

    public float maxSpeed = 0.0f; // The maximum speed of the target ** IN KM/H **

    public float minSpeedArrowAngle;
    public float maxSpeedArrowAngle;

    [Header("UI")]
    public Text SpeedLabel; // The label that displays the speed;
    public RectTransform Arrow; // The arrow in the speedometer

    private float speed = 0.0f;
    private void Update()
    {
        // 3.6f to convert in kilometers
        // ** The speed must be clamped by the car controller **
        speed = target.velocity.magnitude * 7f;

        if (SpeedLabel != null)
            SpeedLabel.text = ((int)speed) + " km/h";
        if (Arrow != null)
            Arrow.localEulerAngles =
                new Vector3(0, 0, Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, speed / maxSpeed));
    }
}