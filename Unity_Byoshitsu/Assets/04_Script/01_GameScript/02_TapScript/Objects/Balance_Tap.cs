using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance_Tap : TapCollider
{
    //天秤No (0:並行,1:左傾き,2:右傾き)
    public int TenbinNo;
    //皿の位置(L or R)
    public string LR;

    //Judgeクラス
    public Balance_Judge JudgeClass;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        BlockPanel.Instance.ShowBlock();
        JudgeClass.Judge(TenbinNo, LR);

    }
}
