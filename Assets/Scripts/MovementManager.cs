using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Unity.XR.CoreUtils;
using UnityEditor.XR.LegacyInputHelpers;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.VisualScripting;

public class MovementManager : MonoBehaviour
{
    const float BASE_MOVE_SPEED = 5.0f;
    const int SPEED_TIME = 10;

    [SerializeField] Transform HeadHeight;
    [SerializeField] GameObject XrOrigin;
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator animator;
    [SerializeField] UIManager myUI;

    private AudioSource myAudio;

    private GameObject child;
    private float xInput;
    private float yInput;
    private float movementSpeed = BASE_MOVE_SPEED;
    private Vector3 moveAmount;
    private Vector3 smoothMoveVelocity;
    private Vector3 headOffset;

    private InputData inputData;
    private Transform XRrig;
    private Transform cameraOffset;

    private bool speedBoostActive = false;
    private float timer = 0.0f;

    /*private XRController rightController;
    private XRController leftController;
    private XRRayInteractor rayInteractor;
    private GameObject leftRayInteractor;
    private GameObject rightRayInteractor;*/

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        child = transform.GetChild(0).gameObject;

        XRrig = XrOrigin.transform;
        inputData = XrOrigin.GetComponent<InputData>();

        

        /*rightController = XrOrigin.transform.Find("Camera Offset").Find("Right Controller").GetComponent<XRController>();
        leftController = XrOrigin.transform.Find("Camera Offset").Find("Left Controller").GetComponent<XRController>();
        rayInteractor = XrOrigin.transform.Find("Camera Offset").Find("Main Camera").GetComponent<XRRayInteractor>();
        leftRayInteractor = XrOrigin.transform.Find("Camera Offset").Find("Left Controller").Find("Ray Interactor").gameObject;
        rightRayInteractor = XrOrigin.transform.Find("Camera Offset").Find("Right Controller").Find("Ray Interactor").gameObject;*/

        cam = Camera.main;
        myAudio = GetComponent<AudioSource>();
        myAudio.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(speedBoostActive)
        {
            timer += Time.deltaTime;
            if (timer > SPEED_TIME)
            {
                speedBoostActive = false;
                movementSpeed = BASE_MOVE_SPEED;
                timer = 0.0f;
            }
        }

        XRrig.position = transform.position;
        XrOrigin.GetComponent<XROrigin>().MoveCameraToWorldLocation(HeadHeight.position);
        // Get camera rotation
        Vector3 lookDirection = cam.transform.forward;
        lookDirection.y = 0; // Keep the player upright

        transform.forward = lookDirection;

        if (inputData.rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 movement))
        {
            Vector3 moveDir = new Vector3(movement.x, 0, movement.y).normalized;
            Vector3 targetMoveAmount = moveDir * movementSpeed;
            moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);

            float speed = targetMoveAmount.magnitude;
            animator.SetFloat("Speed", speed);
            animator.SetFloat("xInput", movement.x);
            animator.SetFloat("yInput", movement.y);

            if (speed > .01)
            {
                myAudio.enabled = true;
            }
            else
            {
                myAudio.enabled = false;
            }
        }

    }

    private void FixedUpdate()
    {
        if (myUI.gameOver.enabled) return;
        rb.MovePosition(rb.position + rb.transform.TransformVector(moveAmount) * Time.fixedDeltaTime);
    }

    public void increase_speed()
    {
        speedBoostActive = true;
        timer = 0;
        movementSpeed = BASE_MOVE_SPEED * 2;
        myUI.enable_speed_UI();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            myUI.game_over();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Exit")
        {
            myUI.game_won();
        }
    }
}
