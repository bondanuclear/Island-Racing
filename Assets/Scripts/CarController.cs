using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CarController : MonoBehaviour
{
    [SerializeField] float motorForce = 30f;
   [SerializeField] float currentAcceleration = 0;
   public float CurrentAcceleration {get{return currentAcceleration;}}
   [SerializeField] float rotationInput;
   float steeringRotation = 0f;
   [SerializeField] float maxSteeringAngle = 35f;
   float verticalInput;
   float horizontalInput;
   public static bool  isBraking = false;
   float currentBraking = 0;
   public float CurrentBraking {get; set;}
   [SerializeField] float brakingForce = 30f; 

   [Header("Wheel colliders")]
   [SerializeField] List<WheelCollider> wheelColliders = new List<WheelCollider>();
//    [SerializeField] WheelCollider frontLeft;
//    [SerializeField] WheelCollider frontRight;
//    [SerializeField] WheelCollider backLeft;
//    [SerializeField] WheelCollider backRight;

    [Header("Wheel transforms")]
    [SerializeField] List<Transform> wheelTransforms = new List<Transform>();
    // [SerializeField] Transform frontLeftTransform;
    // [SerializeField] Transform frontRightTransform;
    // [SerializeField] Transform backLeftTransform;
    // [SerializeField] Transform backRightTransform;
    [SerializeField] float minAllowedNegativePosition;
    new Rigidbody rigidbody;
    [Header("Lights")]
    [SerializeField] List<GameObject> brakeLights = new List<GameObject>();
    [SerializeField] List<GameObject> rearLights = new List<GameObject>();
    [Header("Skid effects")]
    [SerializeField] List<TrailRenderer> skidEffects = new List<TrailRenderer>();
    [SerializeField] float maxSkidThreshold = 1f;
    // [SerializeField] GameObject rightBrakeLight = null;
    // [SerializeField] GameObject leftBrakeLight = null;
    // [SerializeField] GameObject leftRearLight = null;
    // [SerializeField] GameObject rightRearLight = null;
    
    private void Awake() {
        rigidbody = GetComponentInChildren<Rigidbody>();
        //Debug.Log(rigidbody.centerOfMass);
        rigidbody.centerOfMass = new Vector3(0,1f,0);
        
        
    }
    private void Start() {
        FillData();
    }
    private void FillData()
    {
        motorForce = CarSpawner.motorForce;
        brakingForce = CarSpawner.brakeForce;
        FillMeshesList();
        FillCollidersList();
        FillRearLights();
        FillBrakeLights();
        FillSkidEffects();
    }

    private void FillSkidEffects()
    {
        GameObject skidFX = GameObject.FindWithTag("SkidEffects");
        foreach(Transform effect in skidFX.transform)
        {
            skidEffects.Add(effect.GetComponentInChildren<TrailRenderer>());
        }
    }

    private void FillRearLights()
    {
        GameObject rearLightsParent = GameObject.FindWithTag("RearLights");
        foreach(Transform light in rearLightsParent.transform)
        {
            rearLights.Add(light.gameObject);
        }
    }

    private void FillBrakeLights()
    {
        GameObject brakeLightsParent = GameObject.FindWithTag("BrakeLights");
        foreach (Transform light in brakeLightsParent.transform)
        {
            brakeLights.Add(light.gameObject);
        }
    }

    private void FillCollidersList()
    {
        GameObject meshesObject = GameObject.FindGameObjectWithTag("WheelColliders");
        foreach (Transform item in meshesObject.transform)
        {
            wheelColliders.Add(item.GetComponent<WheelCollider>());
        }
    }

    private void FillMeshesList()
    {
       //Debug.Log(CarSpawner.carInstance.name + " NAME "); 
       GameObject meshesObject = GameObject.FindGameObjectWithTag("WheelMeshes");
       foreach(Transform item in meshesObject.transform)
       {
            wheelTransforms.Add(item);
       }
          
    }

    private void Update() {
        //TickInput();   
        CheckFalling();
        HandleSkids();
        
        //Debug.Log($"{Mathf.Abs(rigidbody.angularVelocity.y)} ANGULAR VELOCITY");
    }

    private void CheckFalling()
    {
        if(transform.position.y < minAllowedNegativePosition)
        {
           PlayerManager.instance.ResetLevel();
        }
    }

    private void FixedUpdate() {
        Accelerate();
        RotateWheels();
        UpdateWheels();
        ProcessBraking();
    }

    private void ProcessBraking()
    {
        // if(isBraking)
        // {
        //     currentBraking = brakingForce;

        // } else currentBraking = 0;
        wheelColliders[0].brakeTorque = currentBraking;
        wheelColliders[1].brakeTorque = currentBraking;
        wheelColliders[2].brakeTorque = currentBraking;
        wheelColliders[3].brakeTorque = currentBraking;

        // frontLeft.brakeTorque = currentBraking;
        // frontRight.brakeTorque = currentBraking;
        // backLeft.brakeTorque = currentBraking;
        // backRight.brakeTorque = currentBraking;
    }

    private void UpdateWheels()
    {
        // UpdateSingleWheel(frontLeft, frontLeftTransform);
        // UpdateSingleWheel(frontRight, frontRightTransform);

        // UpdateSingleWheel(backLeft, backLeftTransform);

        // UpdateSingleWheel(backRight, backRightTransform);
        UpdateSingleWheel(wheelColliders[0], wheelTransforms[0]);
        UpdateSingleWheel(wheelColliders[1], wheelTransforms[1]);
        UpdateSingleWheel(wheelColliders[2], wheelTransforms[2]);
        UpdateSingleWheel(wheelColliders[3], wheelTransforms[3]);

    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion quat;
        wheelCollider.GetWorldPose(out pos, out quat);
        wheelTransform.position = pos;
        wheelTransform.rotation = quat;
        
    }

    private void RotateWheels()
    {
        steeringRotation = rotationInput * maxSteeringAngle;
        //Debug.Log(steeringRotation + " steering rotation ");
        wheelColliders[0].steerAngle = Mathf.Lerp(wheelColliders[0].steerAngle, steeringRotation, 0.125f);
        wheelColliders[1].steerAngle = Mathf.Lerp(wheelColliders[1].steerAngle, steeringRotation, 0.125f);

        // frontLeft.steerAngle = Mathf.Lerp(frontLeft.steerAngle, steeringRotation, 0.125f);
        // frontRight.steerAngle = Mathf.Lerp(frontRight.steerAngle, steeringRotation, 0.125f);
    }
    public void RotateWheelsTouch(int number)
    {
        rotationInput = number;
        //Debug.Log("touched me " + rotationInput);
    }
    private void Accelerate()
    {
       
    //    frontLeft.motorTorque = currentAcceleration * motorForce;
    //    frontRight.motorTorque = currentAcceleration * motorForce;
       wheelColliders[0].motorTorque = currentAcceleration * motorForce;
       wheelColliders[1].motorTorque = currentAcceleration * motorForce;
    }
    public void AccelerateByTouch(int accelerationValue)
    {
        currentAcceleration = accelerationValue;
        
    }
    
    public void BrakeByTouch(float brakeValue)
    {
        currentBraking = brakingForce * brakeValue;
        brakeLights[0].GetComponent<Light>().intensity = 2f * brakeValue;
        brakeLights[1].GetComponent<Light>().intensity = 2f * brakeValue;
        // 
    }
    public void Brake(bool braking)
    {
        isBraking = braking;
    }
    public void TurnRearLights(int value)
    {
        rearLights[0].GetComponent<Light>().intensity = value * 2;
        rearLights[1].GetComponent<Light>().intensity = value * 2;

    }
    // check if angular velocity is too high than leave skid marks on surface
    public void HandleSkids()
    {
        float angular = Mathf.Abs(rigidbody.angularVelocity.y);
        // your angular velocity is too high, or you are braking at high speed while being on the ground
        bool checkAngularVelocity = angular > maxSkidThreshold;
        bool checkBraking = Mathf.Abs(rigidbody.velocity.magnitude) > 8f && isBraking;
        bool areWheelsTouchingGround = wheelColliders[2].isGrounded && wheelColliders[3].isGrounded;
        bool isDrifting = ((checkAngularVelocity) || (checkBraking)) && (areWheelsTouchingGround);
        foreach(var item in skidEffects)
        {
            item.emitting = isDrifting;
        }
    }   
    // #region ResetLevel

    // [SerializeField] Vector3 spawnPoint;
    // [SerializeField] Vector3 startingRotation;
    // public void ResetLevel()
    // {
    //     transform.position = spawnPoint;
    //     transform.rotation = Quaternion.Euler(startingRotation);
    //     foreach(var collider in wheelColliders)
    //     {
    //         collider.motorTorque = 0;
            
    //     }
    //     rigidbody.velocity = Vector3.zero;
        
    // }

    // #endregion
    // private void TickInput()
    // {
    //     verticalInput = Input.GetAxis("Vertical");
    //     horizontalInput = Input.GetAxis("Horizontal");
    //     isBraking = Input.GetKey(KeyCode.Space);
    // }
}
