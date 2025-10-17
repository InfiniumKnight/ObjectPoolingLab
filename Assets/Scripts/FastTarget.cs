using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastTarget : TargetBehavior
{
    // Start is called before the first frame update
    void Start()
    {
        TargetSpeed = 10f;
        TargetHealth = 1;
        TargetScoreWorth = 5;

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
