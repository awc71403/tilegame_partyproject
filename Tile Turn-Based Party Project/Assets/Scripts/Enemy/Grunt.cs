using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : Character
{

    public int MeleeDamage = 3;
    public override void Ability1()
    {
        PlayerManager.singleton.GetCharacter().TakeDamage(MeleeDamage);
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
