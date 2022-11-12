using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBinCoin : AbstractInteractive
{
    protected override void WhenTrigger() {
        if (CoinUI.Instance.coinNum > 0) {
            SoundManager.Instance.PlayThrowCoinInTrashBinSound();
            CoinUI.Instance.DecreaseCoinNum(1);
            TrashBinBar.Instance.DropCoinInTrashBin(1);
        }
    }

    protected override bool TriggerOnlyOnce() {
        return false;
    }
}
