using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class revealBeacon : MonoBehaviour
{
    const int MAX_TIME = 10;

    [SerializeField] GameObject beacon;
    private float timer = 0.0f;
    private bool beaconActive = false;

    private Vector3 start_pos;

    // Start is called before the first frame update
    void Start()
    {
        start_pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = start_pos + new Vector3(0, Mathf.Sin(Time.time) / 10, 0);
        transform.Rotate(transform.up, Mathf.Sin(Time.deltaTime) * 10);

        if (beaconActive)
        {
            timer += Time.deltaTime;
            if (timer > MAX_TIME)
            {
                beaconActive = false;
                beacon.SetActive(false);
                timer = 0.0f;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            beacon.SetActive(true);
            beaconActive = true;
            GetComponent<Collider>().enabled = false;
            GetComponentInChildren<MeshRenderer>().enabled = false;
        }
    }
}
