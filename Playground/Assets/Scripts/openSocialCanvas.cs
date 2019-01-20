using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openSocialCanvas : MonoBehaviour
{

    private IEnumerator time;
    // Start is called before the first frame update
    void Start()
    {
        time = waitTime(45.0f);
        StartCoroutine("waitTime");
    }
    
    private IEnumerator waitTime(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
        }
    }
}
