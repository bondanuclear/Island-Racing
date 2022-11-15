using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] float oscillationTime = 2f;
    [SerializeField] Vector3 movementVector;
    [SerializeField][Range(0,1)] float movementFactor;
    [Header("Changes wave from -1;1 to 0;1")]
    [SerializeField] bool changeRange;
    Vector3 startingPosition;
    float tau;
    // Start is called before the first frame update
    void Start()
    {
        tau = (float)Math.PI * 2f;
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Oscillate();
    }

    private void Oscillate()
    {
        float cycles = 0;
        cycles = Time.time / oscillationTime;
        float sinValue = Mathf.Sin(tau * cycles);
        Vector3 offset = Vector3.zero;
        //Debug.Log(sinValue + "sinValue ");
        if(changeRange)
        {
            movementFactor = (sinValue + 1) / 2;
            offset = movementVector * movementFactor;
        }
        else if(!changeRange)
        {
            offset = movementVector * sinValue;
        }
         
        Vector3 sinWave = Vector3.Lerp(transform.position, startingPosition + offset , Time.deltaTime);
        transform.position = sinWave;
    }
}
