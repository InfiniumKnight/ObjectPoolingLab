using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTarget : TargetBehavior
{
    void Start()
    {
        TargetSpeed = 5f;
        TargetHealth = 1f;
        TargetScoreWorth = 1;

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
