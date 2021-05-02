﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour {

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
    public int level = 1;
    public int experience = 0;
    public int experienceThreshold = 1;
    public int skillPoint = 0;
    public int value = 0; // exp value on death
<<<<<<< Updated upstream

=======
    public int money;
>>>>>>> Stashed changes
    public bool isPlayer;
    public bool isCharacter;
    public int charValue;

    public TileBehavior occupiedTile;

    public enum Direction {RIGHT, LEFT, UP, DOWN};
    public Direction myDirection;
    // Sprite Rendering
    private SpriteRenderer myRenderer;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;
    public PlayerManager playerManager;

    [SerializeField]
    private Text DamageTextPrefab;
    public Text damageText;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private AudioClip[] stepSounds;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip regularPunchSound;
    [SerializeField]
    private AudioClip glassStep;

    public abstract void TakeDamage(int damage);
    public abstract void Ability1();
    public abstract void Ability2();
    public abstract void Ability3();
    public abstract void Ability4();
    public abstract void DisplayStats();

    // Movement Bounce Animation
    float totalStretch = 0.3f;
    float totalSquish = 0.3f;
    


    #region Initialization
    public void Start() {
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default");
        audioSource = GetComponent<AudioSource>();
        SetHPFull();
        myDirection = Character.Direction.RIGHT;
<<<<<<< Updated upstream
=======
        money = 0;
>>>>>>> Stashed changes
        playerManager = GetComponent<PlayerManager>();
    }
    #endregion

    public void Update()
    {
        if (experience > experienceThreshold) {
            experience -= experienceThreshold;
            experienceThreshold++;
            skillPoint++;
        }
    }

    #region Getter and Setter
    public string Name {
        get { return characterName; }
    }

    public int GetHP {
        get { return currentHealth; }
    }

    public void SetHPFull() {
        currentHealth = totalHealth;
    }
<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
    public int[] GetCopyStats() {
        int[] copy = new int[curStatArr.Length];
        for (int i = 0; i < curStatArr.Length; i++) {
            copy[i] = curStatArr[i];
        }
        return copy;
    }

    public void ModifyStats(int[] stats) {
        if (stats.Length == curStatArr.Length) {
            for (int i = 0; i < stats.Length; i++) {
                curStatArr[i] = stats[i];
            }
        }
    }

    public virtual int Attack {
        get { return curStatArr[0]; }
    }

    public int AbilityDmg {
        get { return curStatArr[1]; }
    }

    public int CooldownReduction {
        get { return curStatArr[2]; }
    }

    public int DamageReduction {
        get { return curStatArr[3]; }
    }

    public int Health
    {
        get { return curStatArr[4]; }
    }

    public int[] GetCurrentCD {
        get { return currentCooldowns; }
    }

    public int Level {
        get { return level; }
    }

    public int Experience {
        get { return experience; }
    }

    public string GetDirectionString()
    {
        if (myDirection == Direction.DOWN)
        {
            return "down";
        }
        else if (myDirection == Direction.LEFT)
        {
            return "left";
        }
        else if (myDirection == Direction.UP)
        {
            return "up";
        }
        else 
        {
            return "right";
        }
    }

    public void RecalculateDepth() {
        transform.position = new Vector3(transform.position.x, transform.position.y, 10);
    }

    public TileBehavior OccupiedTile {
        get { return occupiedTile; }
        set { occupiedTile = value; }
    }
    #endregion

    #region Sprite
    void WhiteSprite() {
        myRenderer.material.shader = shaderGUItext;
        myRenderer.color = Color.white;
    }

    void NormalSprite() {
        myRenderer.material.shader = shaderSpritesDefault;
        myRenderer.color = Color.white;
    }
    #endregion

    #region Animation
    public void StartHurtAnimation() {
        StartCoroutine("HurtAnimation");
    }

    IEnumerator HurtAnimation() {
        // Go white
        WhiteSprite();

        //Create Damage Text
        print("damage text created");
        // damageText = Instantiate(DamageTextPrefab);
        Vector3 textPositionOffset = new Vector3(0, 1.25f, 0);
        // damageText.transform.position = Camera.main.WorldToScreenPoint(transform.position + textPositionOffset);
        //damageText.GetComponent<DamageTextBehavior>().SetDamage(damage);

        // Shaking
        Vector3 defaultPosition = transform.position;
        System.Random r = new System.Random();
        for (int i = 0; i < 5; i++) {
            double horizontalOffset = r.NextDouble() * 0.2 - 0.1f;
            Vector3 vectorOffset = new Vector3((float)horizontalOffset, 0, 0);
            transform.position += vectorOffset;
            yield return new WaitForSeconds(0.025f);
            transform.position = defaultPosition;
        }

        // Go normal
        NormalSprite();
    }

    public void setFlip(bool direction)
    {
        gameObject.GetComponent<SpriteRenderer>().flipX = direction;
    }

    IEnumerator DeathAnimation() {
        // loop over 0.5 second backwards
        print("death time");
        for (float i = 0.25f; i >= 0; i -= Time.deltaTime) {
            // set color with i as alpha
            myRenderer.color = new Color(1, 1, 1, i);
            transform.localScale = new Vector3(1.5f - i, 1.5f - i, 1);
            yield return null;
        }

        myRenderer.color = new Color(1, 1, 1, 1);
        transform.localScale = new Vector3(1, 1, 1);
        gameObject.SetActive(false);
    }

    public void StartBounceAnimation() {
        StartCoroutine("BounceAnimation");
    }

    IEnumerator BounceAnimation() {
        int frames = 3;
        //Vector3 originalPosition = transform.position;
        float stretch = totalStretch;
        float squish = totalSquish;
        for (int i = frames; i > 0; i--) {
            transform.localScale = new Vector3(1 + stretch, 1 - squish, 1);
            yield return new WaitForSeconds(0.01f);
            stretch /= 2.5f;
            squish /= 2.5f;
        }
        transform.localScale = new Vector3(1, 1, 1);

        // Play random step sound
        System.Random r = new System.Random();
        int stepNum = r.Next(0, stepSounds.Length);
        //audioSource.clip = stepSounds[stepNum];
        //audioSource.Play();
    }


    #endregion

    #region Stats
    public void ResetStats() {
        foreach (int i in curStatArr) {
            curStatArr[i] = baseStats[i];
        }
        currentHealth = totalHealth;
    }

    public void ResetStat(int stat) {
        curStatArr[stat] = baseStats[stat];
    }

    public void ResetHealth() {
        currentHealth = totalHealth;
    }

    public bool HPDamage(int damage) { // Returns True on Kill / False otherwise
        currentHealth -= damage;
        if (currentHealth > 0) {
            StartCoroutine("HurtAnimation", damage);
            return false;
        }
        else {
            occupiedTile.ClearUnit();
            StartCoroutine("DeathAnimation");
            occupiedTile.ClearUnit();
            return true;
        }

    }

    public bool HitEnemy(TileBehavior tile, int dmg)
    {
        MakeAbilitySound(regularPunchSound);
        Debug.Log("attempted hit");
        Debug.Log("Tile coordinates : " + tile.xPosition + " " + tile.yPosition);
        if (validTarget(tile))
        {
            Debug.Log("target hit");
            Character enemy = tile.GetUnit();
            if (enemy.HPDamage(dmg))
            {
                experience += enemy.value;
<<<<<<< Updated upstream
=======
                money += UnityEngine.Random.Range(0, 5);
                Debug.Log("money: " + money);
>>>>>>> Stashed changes
                Debug.Log("experience : " + (experience));
            }
            return true;
        }
        return false;
    }

    public void updateCooldowns() {
        if (currentCooldowns == null) {
            return;
        }
        for (int i = 0; i < currentCooldowns.Length; i++) {
            if (currentCooldowns[i] > 0) currentCooldowns[i] -= 1;
            if (abilityDurations[i] > 0) abilityDurations[i] -= 1;
        }
        UIManager.singleton.UpdateCD();
    }

    public void levelUp(int stat) {
        ///Temp measure
        skillPoint--;
        if (stat == 4) {
            currentHealth += 10;
            totalHealth += 10;
            UIManager.singleton.HealthUI();
        }
        curStatArr[stat] += 1; // This will be a percentage later
        //baseStats[stat] += 1;
        curStatArr[stat] = baseStats[stat] > curStatArr[stat] ? baseStats[stat] : curStatArr[stat];
        UIManager.singleton.UpdateLevelUI();
    }
    #endregion

    #region Attacks
    public TileBehavior GetTarget() {
        TileBehavior target;
        if (myDirection.Equals(Character.Direction.RIGHT)) {
            target = occupiedTile.Right;
        }
        else if (myDirection.Equals(Character.Direction.UP)) {
            target = occupiedTile.Up;
        }
        else if (myDirection.Equals(Character.Direction.LEFT)) {
            target = occupiedTile.Left;
        }
        else {
            target = occupiedTile.Down;
        }
        return target;
    }

    public TileBehavior GetTarget(TileBehavior tile)
    {
        TileBehavior target;
        if (myDirection.Equals(Character.Direction.RIGHT))
        {
            target = tile.Right;
        }
        else if (myDirection.Equals(Character.Direction.UP))
        {
            target = tile.Up;
        }
        else if (myDirection.Equals(Character.Direction.LEFT))
        {
            target = tile.Left;
        }
        else
        {
            target = tile.Down;
        }
        return target;
    }

    public bool validTarget(TileBehavior tile) {
        if (tile == null)
        {
            return false;
        }
        Debug.Log("target exists : " + tile.HasUnit());
        Character target = tile.GetUnit();
        return target != null && target != this;
    }

    public void AttackEnemy() {
        GameManager.actionInProcess = true;
        MakeAbilitySound(regularPunchSound);
        int damage = curStatArr[0];
        TileBehavior target = GetTarget();
        if (target != null && target.HasUnit() && target.GetUnit() != this) {
            Debug.Log("Attacked");
            Character enemy = target.GetUnit();
            if (enemy.HPDamage(curStatArr[1]))
            {
                experience += enemy.value;
<<<<<<< Updated upstream
=======
                money += UnityEngine.Random.Range(0, enemy.value);

                Debug.Log("money :" + money);
>>>>>>> Stashed changes
                Debug.Log("experience : " + (experience));
            }
        }
        updateCooldowns();
        GameManager.actionInProcess = false;
        return;
    }
    #endregion

    #region otherActions
    public Character saveCharacter()
    {
        TileBehavior target = GetTarget();
        if (validTarget(target) && target.GetUnit().isCharacter)
        {
            return target.GetUnit();
        }
        return null;
    }

    public void MakeStepSound()
    {
        //Play a random step sound from the given sounds
        int step = UnityEngine.Random.Range(0, stepSounds.Length);

        audioSource.pitch = UnityEngine.Random.Range(0.8f, 1.1f);
        audioSource.volume = UnityEngine.Random.Range(0.1f, 0.2f);
        audioSource.PlayOneShot(stepSounds[step]);
    }

    public void MakeGlassSound()
    {
        //Play a random step sound from the given sounds
        audioSource.pitch = UnityEngine.Random.Range(0.8f, 1.1f);
        audioSource.volume = UnityEngine.Random.Range(0.1f, 0.2f);
        audioSource.PlayOneShot(glassStep);
    }

    public void MakeAbilitySound(AudioClip sound)
    {
        audioSource.pitch = 1;
        audioSource.volume = 1;
        audioSource.PlayOneShot(sound);
    }
    #endregion
}

