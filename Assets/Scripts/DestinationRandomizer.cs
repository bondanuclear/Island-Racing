using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestinationRandomizer : MonoBehaviour
{
    [SerializeField] GameObject destinationGates = null;
    [SerializeField] Button button;
    [SerializeField] int indexOfGate;
    // Start is called before the first frame update
    void Start()
    {
        int numberOfChildren = destinationGates.transform.childCount;
        indexOfGate = Random.Range(0, numberOfChildren);
        for(int i = 0; i < numberOfChildren; i++)
        {
            if(i == indexOfGate)
            {
                destinationGates.transform.GetChild(i).GetComponent<BoxCollider>().enabled = true;
                destinationGates.transform.GetChild(i).GetComponent<DestinationGate>().enabled = true;
                //destinationGates.transform.GetChild(i).GetComponent<DestinationGate>().Points = 9000;
            }
            else
            {
                destinationGates.transform.GetChild(i).GetComponent<BoxCollider>().enabled = false;
                destinationGates.transform.GetChild(i).GetComponent<DestinationGate>().enabled = false;
            }
            button.onClick.AddListener(ClickToWatchAd);
        }
        void ClickToWatchAd()
        {
            destinationGates.transform.GetChild(indexOfGate).GetComponent<DestinationGate>().WatchAdvertisement();
        }
        
    }
   

    
}
