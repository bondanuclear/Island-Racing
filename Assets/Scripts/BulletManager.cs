using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] float speedOfProjectile = 5;
    [SerializeField] float timeBeforeDisappear = 6f;
    [SerializeField] float hitForce = 100f;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable() {
        timer = 0;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > timeBeforeDisappear)
        {
            //Debug.Log("timer gone off");
            gameObject.SetActive(false);
            
        }
        transform.Translate(Vector3.forward * speedOfProjectile * Time.deltaTime);
        //Debug.Log("script is working");
    }
    private void OnCollisionEnter(Collision other) {
        //other.transform.GetComponent<Rigidbody>().AddForce(Vector3.forward * hitForce, ForceMode.Impulse);
        // add another turret that smokes the vision of the player
        // continue to work on the values of explosion or refactor this part of the code
        // completely
        other.rigidbody.AddExplosionForce(hitForce, transform.position, 4f);
        gameObject.SetActive(false);
        //Debug.Log($"i hit {other.transform.name}");
    }
}
