using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] Transform targetPlayer;
    [SerializeField] float distanceToAttack;
    [SerializeField] bool canAttack = false;
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] Transform rotatingBase = null;
    [SerializeField] float timeBetweenShooting = 2f;
    [SerializeField] Transform bulletSpawnTransform;
    Vector3 lookAtPosition = Vector3.zero;
    float attackTimer = 0;
    Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        
    }
    private void Start() {
        PoolOfObjects.instance.FillThePool();
    }
    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, targetPlayer.position);
        if(distanceToAttack >= distance)
        {
            canAttack = true;
            ProcessAttack();
            RotateTowardsTarget();
        } else
        {
            canAttack = false;
        }
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        if(canAttack)
            attackTimer += Time.deltaTime;
    }

    private void RotateTowardsTarget()
    {
        lookAtPosition = targetPlayer.position + new Vector3(0, 1.2f, 0);
       // Debug.Log(lookAtPosition + "look at position");
        Vector3 direction = (lookAtPosition - rotatingBase.position);
        direction.Normalize();
        //Debug.DrawRay(rotatingBase.position, direction);
        Quaternion tr = Quaternion.LookRotation(direction);
        rotatingBase.localRotation = Quaternion.Slerp(rotatingBase.localRotation, tr, Time.deltaTime * rotationSpeed);
       
        //rotatingBase.LookAt(targetPlayer);
    }

    private void ProcessAttack()
    {
        
        if(!canAttack) return;
        if(attackTimer > timeBetweenShooting)
        {
            Shoot();
            attackTimer = 0;
            //Debug.Log("Can attack " + canAttack);
        }
        
        
        
    }

    private void Shoot()
    {
        GameObject bullet = PoolOfObjects.instance.GetObjectFromPool();
        if(bullet!= null)
        {
            bullet.transform.position = bulletSpawnTransform.position;
            bullet.transform.rotation = rotatingBase.rotation;
            bullet.SetActive(true);
            //Debug.Log(bullet.transform.position);
        }
        

    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceToAttack);
    }
   
}
