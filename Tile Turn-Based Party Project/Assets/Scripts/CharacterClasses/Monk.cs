using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monk : Character
{

    private int[] baseStats = { 5, 5, 5, 0}; // Attack, AP, CD, Damage Reduction
    private int[] abilityCDs = { 2, 2, 2, 2 };
    private int[] currentCDs = { 0, 0, 0, 0 };
    private int[] abilityDur = { 0, 0, 0, 0 }; // Only for Ability 2, but may be used more in future

    void Awake()
    {
        totalHealth = currentHealth = 100;
        curStatArr = baseStats;
        characterName = "monk";
        abilityCooldowns = abilityCDs;
        currentCooldowns = currentCDs;
        abilityDurations = abilityDur;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void TakeDamage(int damage)
    {
        currentHealth -= (int) (damage * (1 - ((double) curStatArr[3] / 100))); 
        StartHurtAnimation();
    }

    public override void DisplayStats()
    {
        //Not Done
    }

    #region Abilities
    //Mega Punch
    public override void Ability1()
    {
        //get target
        if (currentCooldowns[0] > 0)
        {
            return;
        }
        TileBehavior targetTile = GetTarget();
        //damage calc
        Character target = targetTile.GetUnit();
        //reduce health
        if (targetTile != null && target != null && target != this)
        {
            target.HPDamage(curStatArr[1]);
        }
        //activate cooldown
        currentCooldowns[0] += abilityCooldowns[0];

        //NO ANIMATIONS
    }

    //Armor Dillo
    public override void Ability2()
    {
        //damage mitigation
        if (currentCooldowns[1] > 0)
        {
            return;
        }
        curStatArr[3] = 20;
        abilityDurations[1] += 3;
        currentCooldowns[1] += abilityCooldowns[1];
        //damage reduction 
    }

    //Dragon Kick
    public override void Ability3()
    {
        //find targets in range
        if (currentCooldowns[2] > 0) {
            return;
        }
        TileBehavior targetTile = GetTarget();
        //find enemies in area
        //foreach (TileBehavior tile in targetTiles)
        //{
        //    Character target = tile.GetUnit();
        //    if (tile != null && target != null && target != this)
        //    {
        //        target.HPDamage(curStatArr[1]);
        //    }
        //}
        Character target = targetTile.GetUnit();

        if (targetTile != null && target != null && target != this)
        {
            target.HPDamage(curStatArr[1]);
        }
        currentCooldowns[2] += abilityCooldowns[2];
    }

    //Head Smash
    public override void Ability4()
    {
        //get target
        TileBehavior targetTile = GetTarget();
        if (currentCooldowns[3] > 0)
        {
            return;
        }
        //damage calc
        Character target = targetTile.GetUnit();
        //reduce health
        if (targetTile != null && target != null && target != this)
        {
            target.HPDamage(curStatArr[1]);
            currentHealth -= (curStatArr[1] / 5); // Recoil
        }
        //activate cooldown
        currentCooldowns[3] += abilityCooldowns[3];
        // NO ANIMATIONS
    }
    #endregion
}
