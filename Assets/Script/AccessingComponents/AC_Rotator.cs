using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC_Rotator : MonoBehaviour
{
    public GameObject Setting;

    public float PickUpXRotation = 15;
    public float PickUpYRotation = 30;
    public float PickUpZRotation = 45;


    void Start()
    {
        Setting = GameObject.FindGameObjectWithTag("Setting");
        PickUpXRotation = Setting.GetComponent<AC_SettingController>().PickUpXRotation;
        PickUpYRotation = Setting.GetComponent<AC_SettingController>().PickUpYRotation;
        PickUpZRotation = Setting.GetComponent<AC_SettingController>().PickUpZRotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the object on X, Y, and Z axes by specified amounts, adjusted for frame rate.
        transform.Rotate(new Vector3(PickUpXRotation, PickUpYRotation, PickUpZRotation) * Time.deltaTime);
    }

}