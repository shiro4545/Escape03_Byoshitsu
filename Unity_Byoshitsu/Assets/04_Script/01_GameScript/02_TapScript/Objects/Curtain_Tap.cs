using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtain_Tap : TapCollider
{
    //カーテンNo
    public int Index;
    //開閉状態(0:close,1:open)
    public int _status;

    //Judgeクラス
    public Curtain_Judge JudgeClass;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        JudgeClass.OpenClose(Index, _status);
    }
}
