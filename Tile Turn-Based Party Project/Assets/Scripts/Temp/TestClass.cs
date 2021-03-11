using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClass : Character
{
    //strength, speed, knowledge, will
    private int[] Stats = { 5, 5, 2, 4 };


    void Awake() {
        totalHealth = 35;
        currentHealth = totalHealth;
        curStatArr = Stats;
        name = "Test";
    }

    // Update is called once per frame
    void Update() {

    }

    public override void TakeDamage(int damage, int stat) {
        print(curStatArr.Length);
        curStatArr[stat] -= damage;
    }

    public override void DisplayStats() {
        //open menu for character, display stats, etc.
    }
}
