using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class ArcadeGameModeManager : MonoBehaviour
{

    public static ArcadeGameModeManager instance;
    [SerializeField]
    private GameObject scorePopCanvas;
    private float score;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {

            //if not, set instance to this
            instance = this;
        }
        //If instance already exists and it's not this:
        else if (instance != this)
        {

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }
        // //Sets this to not be destroyed when reloading scene
        // DontDestroyOnLoad(gameObject);
    }


    private enum hitRing { Center, MidRing, OutsideRing };
    //private hitRing hitRings;
    public void Score(float distanceFromCenter, Transform targetTransform)
    {
        hitRing currentHitRing = hitRing.OutsideRing;
        float shotScore = PointCalculation(distanceFromCenter, ref currentHitRing);
        score += shotScore;
        StartCoroutine(PointSpawn(targetTransform, shotScore.ToString(), currentHitRing));
        UpdateUI();
    }

    float baseScoreMultiplier = 100;
    float centerHitMax = 0.2f;
    float centerHitMultiplier = 2;
    float midRingHitMax = 0.4f;
    float midRingHitMultiplier = 1.4f;

    float PointCalculation(float distanceFromCenter, ref hitRing spotBallhit)
    {

        if (distanceFromCenter < centerHitMax)
        {
            spotBallhit = hitRing.Center;
            // return Mathf.Round((0.1f / distanceFromCenter) * baseScoreMultiplier * centerHitMultiplier);
            return Mathf.Round(baseScoreMultiplier * centerHitMultiplier);
        }
        else if (distanceFromCenter < midRingHitMax)
        {
            spotBallhit = hitRing.MidRing;
            // return Mathf.Round((0.1f / distanceFromCenter) * baseScoreMultiplier * midRingHitMultiplier);
            return Mathf.Round(baseScoreMultiplier * midRingHitMultiplier);

        }
        else
        {
            spotBallhit = hitRing.OutsideRing;
            // return Mathf.Round((0.1f / distanceFromCenter) * baseScoreMultiplier);
            return Mathf.Round(baseScoreMultiplier);

        }

    }

    [SerializeField]
    private Text scoreUI;

    void UpdateUI()
    {
        scoreUI.text = "Score: " + score;
    }

    Vector3 spawnOffset = new Vector3(0, 0, -0.5f);
    IEnumerator PointSpawn(Transform targetObject, string popText, hitRing spotBallhit)
    {
        GameObject spawnedScore = Instantiate(scorePopCanvas, targetObject.position, Quaternion.identity);
        spawnedScore.transform.LookAt(spawnedScore.transform.position * 2 - Camera.main.transform.position);
        //spawnedScore.transform.position += new Vector3(target.x,spawnOffset * targetObject.forward;
        Text scorePopText = spawnedScore.transform.GetChild(0).GetComponent<Text>();
        scorePopText.text = popText;

        if (spotBallhit == hitRing.Center)
        {
            spawnedScore.transform.localScale *= 1.5f;
            scorePopText.color = Color.red;
        }
        else if (spotBallhit == hitRing.MidRing)
        {
            spawnedScore.transform.localScale *= 1.2f;
            scorePopText.color = Color.yellow;
        }


        spawnedScore.transform.DOMoveY(spawnedScore.transform.position.y + 1, 1)
        .SetEase(Ease.OutCirc);
        scorePopText.DOFade(0, 1)
        .OnComplete(delegate { Destroy(spawnedScore); });

        yield return null;

    }

}
