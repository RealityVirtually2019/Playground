using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeGenerator : MonoBehaviour
{
    public gameObject axe;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Generate a new axe game object
    void Spawn()
    {
      Instantiate(axe);
    }



}
