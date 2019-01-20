using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullTowards : MonoBehaviour
{
    [SerializeField]
    float timeToFullDirectionChange = 0.1f;
    [SerializeField]
    Transform pullPosition;
    [SerializeField]
    AimAssistSettings aimAssistSettings;

    float objectToPullMagnitude;

    void Start()
    {
        SetupAimAssist();
    }
    public void ChangeDirection(Rigidbody objectToPull)
    {
        objectToPullMagnitude = objectToPull.velocity.magnitude;
        objectToPull.velocity = Vector3.Lerp(objectToPull.velocity, (pullPosition.position - objectToPull.position).normalized * objectToPullMagnitude, (Time.deltaTime / timeToFullDirectionChange) * aimAssistSettings.aimAssistPercentage);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Football" && !aimAssistSettings.disableAimAssist)
        {
            ChangeDirection(other.GetComponent<Rigidbody>());
            //PullTowardsGameObject(other.GetComponent<Rigidbody>());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball" && !aimAssistSettings.disableAimAssist)
        {
            ChangeDirection(other.GetComponent<Rigidbody>());
            //PullTowardsGameObject(other.GetComponent<Rigidbody>());
        }
    }

    //Get this from basketball net spawner later
    float maxDistanceFromPlayer = 15;
    float distanceFromPlayer;
    void SetupAimAssist()
    {
        distanceFromPlayer = Vector3.Distance(transform.position, ArcadeGameModeManager.instance.transform.position);
        float distancePercentage = distanceFromPlayer / maxDistanceFromPlayer;
        gameObject.GetComponent<CapsuleCollider>().height = aimAssistSettings.distanceAimAssistLength.Evaluate(distancePercentage);
        gameObject.GetComponent<CapsuleCollider>().radius = aimAssistSettings.distanceAimAssistRadius.Evaluate(distancePercentage);
        transform.localPosition = new Vector3(transform.localPosition.x, (gameObject.GetComponent<CapsuleCollider>().radius * 2), transform.localPosition.z);
        //transform.GetChild(0).transform.localPosition = new Vector3(transform.GetChild(0).localPosition.x, -gameObject.GetComponent<CapsuleCollider>().radius - 0.1f, transform.GetChild(0).localPosition.z);
        timeToFullDirectionChange = aimAssistSettings.distanceAimAssistStrength.Evaluate(distancePercentage);

        // Look at including x and z leaning
        //transform.LookAt(ArcadeGameModeManager.instance.transform.position);

        // Euler angles are easier to deal with. You could use Quaternions here also
        // C# requires you to set the entire rotation variable. You can't set the individual x and z (UnityScript can), so you make a temp Vec3 and set it back
        // Vector3 eulerAngles = transform.rotation.eulerAngles;
        // eulerAngles.x = 0;
        // eulerAngles.z = 0;

        // // Set the altered rotation back
        // transform.rotation = Quaternion.Euler(eulerAngles);
    }



}
