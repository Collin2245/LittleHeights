using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAICollision : MonoBehaviour
{
    private Collider2D enemyAICollider;
    public bool isNearPlayer;

    // Start is called before the first frame update
    void Start()
    {
        enemyAICollider = this.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit player");
            isNearPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Left player");
            isNearPlayer = false;
        }
    }

}
