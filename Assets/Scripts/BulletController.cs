using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float lifeTime = 3;

    private void Start()
    {
        
    }
    void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime < 0)
        {
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Target")
        {
            other.gameObject.GetComponent<TargetBehavior>().TargetHit();
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }
}
