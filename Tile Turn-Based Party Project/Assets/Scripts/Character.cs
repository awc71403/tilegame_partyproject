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

    protected string characterName;
    public int totalHealth;
    public int currentHealth;
    public int level;
    public int experience;

    public bool isPlayer;

    public TileBehavior occupiedTile;

    public enum Direction {RIGHT, LEFT, UP, DOWN};
    public Direction myDirection;
    // Sprite Rendering
    private SpriteRenderer myRenderer;
    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;

    [SerializeField]
    private Text DamageTextPrefab;
    public Text damageText;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private AudioClip[] stepSounds;
    private AudioSource audioSource;

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
    void Start() {
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = Shader.Find("Sprites/Default");
        audioSource = GetComponent<AudioSource>();
        SetHPFull();
        myDirection = Character.Direction.RIGHT;
    }
    #endregion

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

    public int Level {
        get { return level; }
    }

    public int Experience {
        get { return experience; }
    }

    public void RecalculateDepth() {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
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

    public void HPDamage(int damage) {
        currentHealth -= damage;
        if (currentHealth > 0) {
            StartCoroutine("HurtAnimation", damage);
        }
        else {
            occupiedTile.ClearUnit();
            StartCoroutine("DeathAnimation");
        }
    }

    public void updateCooldowns() {
        if (currentCooldowns == null) {
            return;
        }
        for (int i = 0; i < currentCooldowns.Length; i++) {
            if (currentCooldowns[i] > 0) currentCooldowns[i] -= 1;
            if (abilityDurations[i] > 0) abilityDurations[i] -= 1;
        }
    }

    public void levelUp(int stat) {
        ///Temp measure
        curStatArr[stat] += 1; // This will be a percentage later
        baseStats[stat] += 1;
        curStatArr[stat] = baseStats[stat] > curStatArr[stat] ? baseStats[stat] : curStatArr[stat];
    }
    #endregion

    #region Attacks
    public TileBehavior GetTarget() {
        TileBehavior target;
        if (myDirection.Equals(Character.Direction.RIGHT)) {
            target = occupiedTile.Right;
        }
        else if (myDirection.Equals(Character.Direction.UP)) {
            target = occupiedTile.Left;
        }
        else if (myDirection.Equals(Character.Direction.LEFT)) {
            target = occupiedTile.Up;
        }
        else {
            target = occupiedTile.Down;
        }
            return target;
        }

    public bool validTarget(TileBehavior tile) {
        Debug.Log("target exists : " + tile.HasUnit());
        Character target = tile.GetUnit();
        return tile != null && target != null && target != this;
    }

    public void AttackEnemy() {
        GameManager.actionInProcess = true;
        int damage = curStatArr[0];
        Debug.Log("called attack function");
        Debug.Log(myDirection);
        Debug.Log(myDirection.Equals(Character.Direction.RIGHT));
        TileBehavior target = GetTarget();
        Debug.Log(target.HasUnit());
        if (target != null && target.HasUnit() && target.GetUnit() != this) {
            Debug.Log("Attacked");
            target.GetUnit().HPDamage(damage);
        }
        updateCooldowns();
        GameManager.actionInProcess = false;
        return;
    }
    #endregion
}

