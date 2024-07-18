using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHeight : MonoBehaviour
{
    [SerializeField] Transform Avatar, PlayerHead, Camera;

    private float ratio;
    // Start is called before the first frame update
    void Start()
    {
        float XRheight = Camera.position.y - Avatar.position.y;
        float Playerheight = PlayerHead.position.y- Avatar.position.y;

        float ratio = XRheight / Playerheight;

        Avatar.localScale = new Vector3(ratio, ratio, ratio);
    }

    
}
