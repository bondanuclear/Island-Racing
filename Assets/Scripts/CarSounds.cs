using System;

using UnityEngine;
using UnityEngine.UI;

public class CarSounds : MonoBehaviour
{
    [Header("Sound Effects")]
    [SerializeField] AudioSource audioSource;
    AudioSource source;
    [SerializeField] AudioClip[] brakingSounds = new AudioClip[2];
    //[SerializeField] AudioClip motorRevving;
    [SerializeField] float minPitch;
    [SerializeField] float maxPitch;
    [SerializeField] float pitchFromCar = 0;
    bool shouldPlaySound = true;
    [Header("Speed values")]
    [SerializeField] float maxSpeed;
    [SerializeField]  float minSpeed;
    [SerializeField] float currentSpeed = 0;    
    
    new Rigidbody rigidbody;
    CarController carController;
    [Header("Control ground touch")]
    GameObject rayCastStartingPoint;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float distanceToGround = 1f;
    [SerializeField] bool isTouchingGround = true;
    [Header("Volume sliders")]
    [SerializeField] Slider revvingSlider;
    [SerializeField] Slider brakingSlider;
    private void Awake() {
        rigidbody = GetComponent<Rigidbody>();
        carController = GetComponent<CarController>();
        rayCastStartingPoint = GameObject.FindGameObjectWithTag("RayPoint");
        source = GetComponent<AudioSource>();
        layerMask = 1 << 6;
        //brakingSlider = GameObject.FindWithTag("BrakingSlider").GetComponent<Slider>();
        
    }
    private void Start() {
        GameObject revving = GameObject.Find("Revving");
        audioSource = revving.GetComponent<AudioSource>();
        revvingSlider.value = audioSource.volume;
        brakingSlider.value = source.volume;
        brakingSlider.onValueChanged.AddListener(delegate { BrakingSoundChange(); });
    }
    // Update is called once per frame
    void Update()
    {
        EngineSound();
        CheckGroundTouch();
    }

    private void CheckGroundTouch()
    {
        RaycastHit hit;
        if (Physics.Raycast(rayCastStartingPoint.transform.position, Vector3.down, out hit, distanceToGround, layerMask))
        {
            isTouchingGround = true;
        }
        else
        {
            isTouchingGround = false;
        }
    }

    private void EngineSound()
    {
        
        currentSpeed = rigidbody.velocity.magnitude * carController.CurrentAcceleration;
        pitchFromCar = currentSpeed / 50f;
        if(currentSpeed < minSpeed)
        {
            audioSource.pitch = minPitch;
        } else if(currentSpeed > minSpeed && currentSpeed < maxSpeed)
        {
            audioSource.pitch = minPitch + pitchFromCar;
        } else if(currentSpeed > maxSpeed)
        {
            audioSource.pitch = maxPitch;
        }
        // fix: can make sound in the air
        // add empty object and project raycast underneath to check if car touches the ground
        // if true 
        
        if(Mathf.Abs(rigidbody.velocity.magnitude) > 8f && CarController.isBraking && shouldPlaySound && isTouchingGround)
        {
            shouldPlaySound = false;
            Debug.Log("SHOULD PLAY BRAKING SOUND");
            int randomIndex = UnityEngine.Random.Range(0,2);
            source.PlayOneShot(brakingSounds[randomIndex]);
        }

        if ((!CarController.isBraking && !shouldPlaySound) || !isTouchingGround)
        {
            shouldPlaySound = true;
            //Debug.Log("SHOUD STOP PLAYIN");
            source.Stop();
        }
        
        // else
        // {
        //     source.Stop();
        //     shouldPlaySound = true;
        // }
    }
    public void SetRevvingSound()
    {
        audioSource.volume = revvingSlider.value;
    }
    private void BrakingSoundChange()
    {
        source.volume = brakingSlider.value;
    }
}
