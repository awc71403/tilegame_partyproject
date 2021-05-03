using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealTile : TileBehavior
{
    // Start is called before the first frame update
    void Start()
    {
        tileType = "seal";
    }

    public override void Effect() {
        int half = PlayerManager.singleton.GetCharacter().currentHealth / 2;
        if (half == PlayerManager.singleton.GetCharacter().currentHealth) {
            half = half - 1;
        }
        PlayerManager.singleton.GetCharacter().TakeDamage(half);
    }
}
