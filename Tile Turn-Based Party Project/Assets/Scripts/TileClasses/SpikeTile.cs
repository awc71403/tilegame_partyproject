using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTile : TileBehavior
{
    public void Start()
    {
        tileType = "glass";
    }

    public override void Effect() {
        PlayerManager.singleton.GetCharacter().TakeDamage(5);
    }
}
