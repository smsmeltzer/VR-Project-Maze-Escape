using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 start_pos;
    void Start()
    {
        start_pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = start_pos + new Vector3(0, Mathf.Sin(Time.time)/10, 0);
        transform.Rotate(transform.up, Mathf.Sin(Time.deltaTime) * 10);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<MovementManager>().increase_speed();
            gameObject.SetActive(false);
        }
    }
}
