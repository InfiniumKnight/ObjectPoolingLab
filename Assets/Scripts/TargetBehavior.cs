using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehavior : MonoBehaviour
{
    public GameManager gameManager;
    public float TargetSpeed;
    public float TargetHealth;
    public int TargetScoreWorth;
    public Vector3 Direction = Vector3.left;

    void Start()
    {

    }

    
    protected virtual void Update()
    {
        transform.Translate(Direction.normalized * TargetSpeed * Time.deltaTime);
    }

    public virtual void TargetHit()
    {
        TargetHealth--;
        
        if (TargetHealth <= 0)
        {
            gameManager.UpdateScore(TargetScoreWorth);
            Destroy(gameObject);
        }
    }
}
