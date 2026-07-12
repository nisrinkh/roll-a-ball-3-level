using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTiltGround : MonoBehaviour
{
    [Header("Tilt Settings")]
    public float tiltSpeed = 30f;   // Kecepatan goyangan
    public float maxTilt = 20f;     // Derajat kemiringan maksimum
    private float tiltAngle = 0f;

    void Update()
    {
        // Gunakan gelombang sinus agar goyangan lembut dan berulang
        tiltAngle = Mathf.Sin(Time.time * tiltSpeed * Mathf.Deg2Rad) * maxTilt;

        // Terapkan rotasi ke sumbu Z (miring kanan-kiri)
        transform.rotation = Quaternion.Euler(0f, 0f, tiltAngle);
    }
}
