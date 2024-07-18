using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningAlignment : MonoBehaviour
{

    public GameObject mainCamera;
    public GameObject foot;
    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        animator.SetFloat("hmdRotation", Mathf.DeltaAngle(mainCamera.transform.rotation.y, foot.transform.rotation.y));
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
