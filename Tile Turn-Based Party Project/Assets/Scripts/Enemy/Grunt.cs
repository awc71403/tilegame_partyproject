using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : Character
{
    public int myHealth = 3;
    public int myValue = 3;
    public int myDamage = 3;

    public override void Ability1()
    {
        PlayerManager.singleton.GetCharacter().TakeDamage((int)(myDamage + myDamage * (GameManager.difficulty - 1) * 1.25));
    }

    public override void Ability2()
    {

        throw new System.NotImplementedException();
    }

    public override void Ability3()
    {
        throw new System.NotImplementedException();
    }

    public override void Ability4()
    {
        throw new System.NotImplementedException();
    }

    public override void DisplayStats()
    {
        throw new System.NotImplementedException();
    }

    public override void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    void Awake()
    {
        totalHealth = (int)(myHealth + myHealth * (GameManager.difficulty - 1) * 1.25);
        value = (int)(myValue + myValue * (GameManager.difficulty - 1) * 1.25);
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
