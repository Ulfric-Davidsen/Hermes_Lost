using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 2f;
    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        if(period <= Mathf.Epsilon) {return;} // Mathf.Epsilon is the smallest number that can be a float. Use this to prevent conflicts when using floats and comparing to 0.

        float cycles = Time.time / period; // Continually growing over time
        
        const float tau = Mathf.PI * 2; // Constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); // Going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f; // Recalculated to go from 0 to 1 so its cleaner

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
