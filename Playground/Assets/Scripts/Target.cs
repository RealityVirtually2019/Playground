using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
      ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // What happens when the axe collides
    void OnCollisionEnter(Collision col)
    {
      if (col.gameObject.CompareTag ("Axe"))
      {
        Debug.Log ("HIT!");
        //What happens to the axe?
        //col.gameObject.SetActive (false); //make axe disappear
      }

    }
}
