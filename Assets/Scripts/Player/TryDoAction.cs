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
    float time;
    bool mouseUp;
    void Start()
    {
        tileManager = GameObject.FindGameObjectWithTag("TileManager").GetComponent<TileManager>();
        destroyObject = GetComponent<DestroyObject>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        attackSpeed = 0.8f;
        time = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && tileManager.GetComponent<MouseHoverScript>().isActiveArea)
        {
            point = tileManager.baseMap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            tileInfo = tileManager.GetTileInfoAtPoint();
            CheckAttackAnimations(time);
            TryDestroyOrAttackObject();
        }
        else
        {
            //player.animator.SetState(CharacterState.Idle);
            StopDestroyingObject();
        }
    }

    void CheckAttackAnimations(float time)
    {
        if (time <= 0)
        {
            time = attackSpeed;
            player.animator.Attack();
        }
        else if (time >= 0)
        {
            time -= Time.deltaTime;
        }
        else if (time < 0)
        {
            player.animator.Attack();
            time = attackSpeed;
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
