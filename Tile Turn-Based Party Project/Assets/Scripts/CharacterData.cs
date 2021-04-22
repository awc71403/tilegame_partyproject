using System;
[Serializable]
public class CharacterData
{
    public int[] curStatArr;
    public int[] baseStats;
    public int[] abilityCooldowns;
    public int[] currentCooldowns;
    public int[] abilityDurations;
    // 0 - attack
    // 1 - ability damage
    // 2 - cooldown reduction
    // 3 - damage reduction - skills mainly

    public string characterName;
    public int totalHealth;
    public int currentHealth;
    public int level;
    public int experience;
    public int value; // exp value on death

    public bool isPlayer;
    public bool isCharacter;
    public int charValue;

    public CharacterData()
    {

    }
}
