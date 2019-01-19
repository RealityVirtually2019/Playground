using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
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

    void OnCollisionExit()
    {
      //TODO: if trigger is released
      StartCoroutine("Disappear");
    }

    IEnumerator Disappear()
    {
      yield return new WaitForSeconds(5.0f);
    }



}
