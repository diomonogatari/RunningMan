using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class LightController : MonoBehaviour
{

    public float dayCycleInMinutes = 1;
    private float _rotationSpeed;

    private const float degreesPerSecond = 360 / Constants.TimeUnits.day;

    private float degreeRotation;


    private void Start()
    {
        degreeRotation = (degreesPerSecond * Constants.TimeUnits.day / (dayCycleInMinutes * Constants.TimeUnits.minute))*-1;
    }

    void Update()
    {
        transform.Rotate(new Vector3(degreeRotation,0,0)*Time.deltaTime);
    }
}
