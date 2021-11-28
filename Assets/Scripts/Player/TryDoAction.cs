using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor4D.Common.CharacterScripts;

public class TryDoAction : MonoBehaviour
{
    // Start is called before the first frame update
    TileManager tileManager;
    DestroyObject destroyObject;
    Vector3Int point;
    TileInfo tileInfo;
    PlayerController player;
    public float attackSpeed;
    float startTime;
    void Start()
    {
        tileManager = GameObject.FindGameObjectWithTag("TileManager").GetComponent<TileManager>();
        destroyObject = GetComponent<DestroyObject>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        attackSpeed = 2.0f;
        startTime = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && tileManager.GetComponent<MouseHoverScript>().isActiveArea)
        {
            point = tileManager.baseMap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            tileInfo = tileManager.GetTileInfoAtPoint();
            if(Input.GetMouseButtonDown(0))
            {
                player.animator.Attack();
            }
            
            TryDestroyOrAttackObject();
        }
        else
        {
            //player.animator.SetState(CharacterState.Idle);
            StopDestroyingObject();
        }
    }

    void TryDestroyOrAttackObject()
    {
        if (tileInfo.isTreeOn)
        {
            //Debug.Log("HitDestructableOb");
            destroyObject.TryDestroyObject(point, "tree");
        }else
        {
            //add logic here for melee combat
            StopDestroyingObject();
        }
    }


    void StopDestroyingObject()
    {
        destroyObject.stopAllCoroutinesPlease();
        destroyObject.playAudio = false;
        destroyObject.resetCounter();
    }
}
