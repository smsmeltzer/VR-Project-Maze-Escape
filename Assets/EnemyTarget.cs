using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] EnemyAI enemyAI;
    [SerializeField] UIManager myUI;

    private void Start()
    {
        transform.position = player.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (myUI.gameOver.enabled) return;
        if (other.gameObject.tag == "Enemy")
        {
            transform.position = player.position;
            enemyAI.UpdateAI();
        }
    }
}
