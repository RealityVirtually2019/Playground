using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openSocialCanvas : MonoBehaviour
{
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitTime());
    }
    
    private IEnumerator waitTime()
    {
            yield return new WaitForSeconds(45);
            canvas.SetActive(true);
    }
}
