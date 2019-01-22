using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIDoTween : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(tweenDown());
    }

    // Update is called once per frame
    void Update()
    {

    }
    [SerializeField]
    float delay;
    [SerializeField]
    float difference;
    IEnumerator tweenDown()
    {
        yield return new WaitForSeconds(delay);

        transform.GetComponent<RectTransform>().DOAnchorPosY(transform.GetComponent<RectTransform>().anchoredPosition.y - difference, 1f);
        transform.GetComponent<Image>().DOColor(Color.white, 1f);
        yield return new WaitForSeconds(10);
        transform.GetComponent<Image>().DOColor(Color.clear, 1f);


        // transform

    }
}
