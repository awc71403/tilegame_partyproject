using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTile : TileBehavior
{

    public override void Effect() {
        GetUnit().HPDamage(10);
    }
}
