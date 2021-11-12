using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/EnemyPrefab"),new Vector3(player.transform.position.x + Random.Range(-20f,20f), player.transform.position.y + Random.Range(-20f, 20f), 0), Quaternion.identity);
        }
    }
}
