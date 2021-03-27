using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClass : Character
{
    //strength, speed, knowledge, will
    private int[] Stats = { 5, 5, 2, 4 };


    void Awake()
    {
        totalHealth = 35;
        currentHealth = totalHealth;
        curStatArr = Stats;
        name = "Test";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void TakeDamage(int damage)
    {
        print(curStatArr.Length);
        currentHealth -= damage;
    }

    public override void DisplayStats()
    {
        //open menu for character, display stats, etc.
    }

    public override void Ability1() { return; }
    public override void Ability2() { return; }
    public override void Ability3() { return; }
    public override void Ability4() { return; }
}
