using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterServer_Tap : TapCollider
{
    //0:Coll,1:Hot
    public int CH;

    public WaterServer_Judge JudgeClass;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        JudgeClass.PourWater(CH);
    }
}
