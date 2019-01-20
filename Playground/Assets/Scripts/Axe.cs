namespace VRTK.Examples
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Axe : VRTK_InteractableListener
    {

        private VRTK_InteractableObject interactableObject;
        private Rigidbody rb;
        public bool scored;
        float destroyTime = 5;
        // Use this for initialization

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        protected virtual void OnEnable()
        {
            interactableObject = GetComponent<VRTK_InteractableObject>();
            EnableListeners();
        }

        protected virtual void OnDisable()
        {

            DisableListeners();
        }

        void Update()
        {

        }

        protected override bool SetupListeners(bool throwError)
        {
            interactableObject = (interactableObject != null ? interactableObject : GetComponentInParent<VRTK_InteractableObject>());
            if (interactableObject != null)
            {

                interactableObject.SubscribeToInteractionEvent(VRTK_InteractableObject.InteractionType.Grab, OnObjectGrab);
                interactableObject.SubscribeToInteractionEvent(VRTK_InteractableObject.InteractionType.Ungrab, OnObjectUnGrab);

                return true;
            }
            else if (throwError)
            {
                VRTK_Logger.Error(VRTK_Logger.GetCommonMessage(VRTK_Logger.CommonMessageKeys.REQUIRED_COMPONENT_MISSING_FROM_GAMEOBJECT, "VRTK_InteractObjectHighlighter", "VRTK_InteractableObject", "the same or parent"));
            }
            return false;
        }

        protected override void TearDownListeners()
        {
            if (interactableObject != null)
            {
                interactableObject.UnsubscribeFromInteractionEvent(VRTK_InteractableObject.InteractionType.Grab, OnObjectGrab);
                interactableObject.UnsubscribeFromInteractionEvent(VRTK_InteractableObject.InteractionType.Ungrab, OnObjectUnGrab);
            }
        }
        protected virtual void OnObjectGrab(object sender, InteractableObjectEventArgs e)
        {
            faceVelocity = false;
        }

        [SerializeField]
        Vector3 ballSpiralVelocity = new Vector3(0, 0, 1);
        [SerializeField]
        float maxSpiralSpeed;
        protected virtual void OnObjectUnGrab(object sender, InteractableObjectEventArgs e)
        {
            StartCoroutine(BallReleaseBehaviour());
            Destroy(gameObject, destroyTime);
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Ground")
            {
                faceVelocity = false;
            }
        }

        bool faceVelocity;
        Quaternion initialRotation = Quaternion.Euler(0, 90, 0);
        Quaternion calculatedRot;
        
        IEnumerator BallReleaseBehaviour()
        {
            //HARD THROWN\\
            
            //if the balls velocity magnitude is higher then 80
            //and throwing at an angle less then 20 degrees 
            //also throwing at an angle more then -40 degrees
            //then make throw a hard straight throw 

            //Could be optimized by storing velocity at first instead of checking it multiple times
            if (rb.velocity.sqrMagnitude > 80 && rb.velocity.normalized.y < 0.1f && rb.velocity.normalized.y > -0.4f)
            {
                print(rb.velocity.sqrMagnitude);
                //change position of where ball was let go
                transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y + 0.1f, transform.position.z);
                //tilt ball velocity up
                rb.velocity = new Vector3(rb.velocity.x * (1 + Mathf.Abs(rb.velocity.y / 6)), 3f, rb.velocity.z * (1 + Mathf.Abs(rb.velocity.y / 6)));
            }

            //make ball face the direction of velocity
            //need to implement ball spin depending on velocity
            faceVelocity = true;
            while (faceVelocity)
            {
                yield return new WaitForEndOfFrame();
                calculatedRot = Quaternion.LookRotation(rb.velocity) * initialRotation;
                transform.localRotation = calculatedRot;
            }
                yield return null;


        }

    }
}
