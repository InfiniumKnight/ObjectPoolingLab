using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float lifeTime = 3;
    public ObjectPoolManager ObjectPool;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Target")
        {
            TargetBehavior target = other.gameObject.GetComponent<TargetBehavior>();
            target.TargetHit();
            ObjectPool.ReturnObject(gameObject);
        }
    }
}
