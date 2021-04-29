﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cat : Character
{

    private int[] baseClassStats = { 5, 5, 5, 0}; // Attack, AP, CD, Damage Reduction
    private int[] abilityCDs = { 2, 2, 4, 2 };
    private int[] currentCDs = { 0, 0, 0, 0 };
    private int[] abilityDur = { 0, 0, 0, 0 }; // Only for Ability 2, but may be used more in future

    void Awake()
    {
        totalHealth = currentHealth = 100;
        curStatArr = baseClassStats;
        baseStats = baseClassStats;
        characterName = "cat";
        abilityCooldowns = abilityCDs;
        currentCooldowns = currentCDs;
        abilityDurations = abilityDur;
    }

    void Start()
    {
        base.Start();
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
    //Claw Strike
    public override void Ability1()
    {
        //get target
        GameManager.actionInProcess = true;
        Debug.Log("Ability1");
        Debug.Log("Cooldown : " + currentCooldowns[0]);
        if (currentCooldowns[0] > 0)
        {
            GameManager.actionInProcess = false;
            return;
        }
        TileBehavior targetTile = GetTarget();
        HitEnemy(targetTile, curStatArr[1] + 2);
        //activate cooldown
        updateCooldowns();
        currentCooldowns[0] += abilityCooldowns[0];
        Debug.Log("Cooldown After: " + currentCooldowns[0]);

        GameManager.actionInProcess = false;
    }

    //Cat Cry NOT FINISHED
    public override void Ability2()
    {
        GameManager.actionInProcess = true;
        Debug.Log("Ability2");
        Debug.Log("Cooldown : " + currentCooldowns[1]);
        if (currentCooldowns[1] > 0)
        {
            GameManager.actionInProcess = false;
            return;
        }
        abilityDurations[1] += 3;
        //Need to add health regen
        updateCooldowns();
        currentCooldowns[1] += abilityCooldowns[1];
        Debug.Log("Cooldown After: " + currentCooldowns[1]);
        GameManager.actionInProcess = false;
    }

    //Dragon Kick
    public override void Ability3()
    {
        GameManager.actionInProcess = true;
        Debug.Log("Ability3");
        Debug.Log("Cooldown : " + currentCooldowns[2]);
        if (currentCooldowns[2] > 0) {
            GameManager.actionInProcess = false;
            return;
        }

        TileBehavior targetTile = GetTarget();
        if (targetTile != null && !HitEnemy(targetTile, curStatArr[1] + 2) && !targetTile.HasUnit() && targetTile.tileType != "wall")
        {
            Debug.Log("about to attack move");
            GetComponent<PlayerManager>().StartMoveDuringAttackAnimation();
            targetTile = GetTarget();
            HitEnemy(targetTile, curStatArr[1]);
        }
       
        updateCooldowns();
        currentCooldowns[2] += abilityCooldowns[2];
        Debug.Log("Cooldown After: " + currentCooldowns[2]);
        GameManager.actionInProcess = false;
    }

    //Head Smash
    public override void Ability4()
    {
        GameManager.actionInProcess = true;
        Debug.Log("Ability4");
        Debug.Log("Cooldown : " + currentCooldowns[3]);
        if (currentCooldowns[3] > 0)
        {
            GameManager.actionInProcess = false;
            return;
        }

        TileBehavior targetTile = GetTarget();
        while (targetTile != null && !HitEnemy(targetTile, curStatArr[1]-1) && targetTile.tileType != "wall")
        {
            targetTile = GetTarget(targetTile);
        }
        updateCooldowns();
        currentCooldowns[3] += abilityCooldowns[3];
        Debug.Log("Cooldown After: " + currentCooldowns[3]);
        GameManager.actionInProcess = false;
    }
    #endregion
}
