using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Tap : TapCollider
{
    //Index
    public int Index;

    //Judgeクラス
    public Box_Judge JudgeClass;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        AudioManager.Instance.SoundSE("TapButton");

        //ボタンを後ろに移動
        this.gameObject.transform.Translate(new Vector3(0, 0, -0.013f));
        Invoke(nameof(AfterPush), 0.1f);

        //答え合わせ
        JudgeClass.Judge(Index);
    }

    //ボタンを元に戻す
    private void AfterPush()
    {
        this.gameObject.transform.Translate(new Vector3(0, 0, 0.013f));
    }
}
