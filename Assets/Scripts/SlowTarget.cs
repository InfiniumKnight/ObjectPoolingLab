using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTarget : TargetBehavior
{
    void Start()
    {
        TargetSpeed = 2f;
        TargetHealth = 5f;
        TargetScoreWorth = 10;

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void TargetHit()
    {
        base.TargetHit();
    }
}
