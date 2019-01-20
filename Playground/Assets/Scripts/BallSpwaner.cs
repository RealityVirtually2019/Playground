using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpwaner : MonoBehaviour {

    public GameObject footBallPrefab;
    private bool empty;

	// Use this for initialization
	void Start () {
        empty = true;
    }
	
	// Update is called once per frame
	void Update () {

        if (empty)
        {
            Instantiate(footBallPrefab, transform.position, Quaternion.identity);            
        }
        //Instantiate(basketBall, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
    }

    private void OnTriggerStay(Collider other)
    {
       
            if(other.tag == "Football")
        {
            empty = false;

            }

    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Football")
        {
            empty = true;

        }
    }


}
