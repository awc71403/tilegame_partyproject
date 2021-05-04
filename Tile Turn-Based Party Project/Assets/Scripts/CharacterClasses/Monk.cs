﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Monk : Character
{

    public Sprite[] abilityImages;

    [SerializeField]
    private AudioClip[] abilitySounds; //index of abilitysounds corresponds to abililty # that makes the sound - 1

    private int[] baseClassStats = { 3, 5, 5, 0, 0}; // Attack, AP, CD, Damage Reduction, Health
    private int[] abilityCDs = { 8, 8, 12, 12 };
    private int[] currentCDs = { 0, 0, 0, 0 };
    private int[] abilityDur = { 0, 0, 0, 0 }; // Only for Ability 2, but may be used more in future

    void Awake()
    {
        totalHealth = currentHealth = 100;
        curStatArr = baseClassStats;
        baseStats = baseClassStats;
        characterName = "monk";
        totalHealth = currentHealth = 100 + 10 * curStatArr[4];
        abilityCooldowns = abilityCDs;
        currentCooldowns = currentCDs;
        abilityDurations = abilityDur;
        UIManager.singleton.SetAbilityImages(abilityImages);
    }

    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
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
        GameManager.actionInProcess = true;
        Debug.Log("Ability1");
        Debug.Log("Cooldown : " + currentCooldowns[0]);
        if (currentCooldowns[0] > 0)
        {
            GameManager.actionInProcess = false;
            return;
        }
        TileBehavior targetTile = GetTarget();
        if (targetTile == null)
        {
            return;
        }
        HitEnemy(targetTile, curStatArr[1]);

        //Make sound
        MakeAbilitySound(abilitySounds[0]);

        //activate cooldown
        updateCooldowns();
        currentCooldowns[0] += abilityCooldowns[0];
        Debug.Log("Cooldown After: " + currentCooldowns[0]);

        GameManager.actionInProcess = false;
    }

    //Armor Dillo
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
        curStatArr[3] = 20;
        abilityDurations[1] += 4;

        //Make sound
        MakeAbilitySound(abilitySounds[1]);


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
        //Need to fix
        TileBehavior targetTile = GetTarget();
        if (targetTile == null) {
            return;
        }
        TileBehavior LeftTile = targetTile.Left;
        TileBehavior RightTile = targetTile.Right;
        HitEnemy(targetTile, curStatArr[1]);
        HitEnemy(LeftTile, curStatArr[1]);
        HitEnemy(RightTile, curStatArr[1]);

        //Make sound
        MakeAbilitySound(abilitySounds[2]);

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
        if (targetTile == null)
        {
            return;
        }
        if (HitEnemy(targetTile, curStatArr[0] + curStatArr[1]))
        {
            TakeDamage(curStatArr[1] / 5);
            Debug.Log("Recoil: " + curStatArr[1] / 5);
        }

        //Make sound
        MakeAbilitySound(abilitySounds[3]);

        updateCooldowns();
        currentCooldowns[3] += abilityCooldowns[3];
        Debug.Log("Cooldown After: " + currentCooldowns[3]);
        GameManager.actionInProcess = false;
    }
    #endregion
}
