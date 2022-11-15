using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCollider : MonoBehaviour
{
    
    void Awake()
    {
        transform.GetComponent<TreeCollider>().enabled = false;
        transform.GetComponent<TreeCollider>().enabled = true;
    }

    
}
