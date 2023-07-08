using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine;

public class SkelotonEnermyScript : EnermyBaseScript
{
    public GameObject coin;
    public float coinDropRate;
    public GameObject bone;
    public float boneDropRate;
    private void Awake()
    {
        GetComponent<BehaviorTree>().SetVariable("player", (SharedGameObject)GameObject.Find("HeroKnight"));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerScript player = other.GetComponent<PlayerScript>();
            if (Random.value<=criticalRate)
            {
                float damage = attackPower * (1 + criticalDamage);
                player.ChangeHealth(-damage);
                MessageBoxScript.Instance.ChangeMessage("你受到了" + damage + "点伤害");
            }
            else
            {
                float damage = attackPower;
                player.ChangeHealth(-damage);
                MessageBoxScript.Instance.ChangeMessage("你受到了" + damage + "点伤害");
            }
        }
    }
    protected override void OnDestroy()
    {
        if (!gameObject.scene.isLoaded)
        {
            return;
        }
        for (int i = 0; i < (int)coinDropRate+(Random.value<=(coinDropRate%1)?1:0); i++)
        {
            GameObject coinItem= Instantiate(coin);
            Vector3 targetPos=new Vector3(Random.Range(-range,range),Random.Range(0,range),0)+transform.position;
            coinItem.transform.position=targetPos;
        }
        for (int i = 0; i < (int)boneDropRate + (Random.value <= (boneDropRate%1) ? 1 : 0); i++)
        {
            GameObject boneItem = Instantiate(bone);
            Vector3 targetPos = new Vector3(Random.Range(-range, range), Random.Range(0, range), 0)+transform.position;
            boneItem.transform.position = targetPos;
        }
    }
}
