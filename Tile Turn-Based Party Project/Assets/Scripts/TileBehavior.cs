using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class TileBehavior : MonoBehaviour {
    #region Instance Variables
    bool highlighted;
    Character myUnit;
    public int xPosition;
    public int yPosition;
    public string tileType;

    public TileBehavior Left;
    public TileBehavior Right;
    public TileBehavior Up;
    public TileBehavior Down;

    [SerializeField]
    GameObject tileHighlighter;
    Animator tileHighlighterAnimator;
    public float playerOpacity;
    public float enemyOpacity;
    #endregion

    #region Initialization
    void Awake() {
        //tileHighlighter.transform.position = transform.position;
        //tileHighlighterAnimator = tileHighlighter.GetComponent<Animator>();
        //setHighlightOpacity(playerOpacity);
    }

    #endregion

    #region Opacity
    private void setHighlightOpacity(float opacity) {
        Color c = tileHighlighter.GetComponent<Renderer>().material.color;
        c.a = opacity;
        tileHighlighter.GetComponent<Renderer>().material.color = c;
    }
    #endregion

    #region Unit Functions
    public void PlaceUnit(Character unit) {
        myUnit = unit;
        myUnit.transform.position = transform.position - new Vector3(0, 0, 0);
        myUnit.RecalculateDepth();
        myUnit.OccupiedTile = this;
    }

    public bool HasUnit() {
        return myUnit != null;
    }

    public Character GetUnit() {
        return myUnit;
    }

    public void ClearUnit() {
        myUnit = null;
    }
    #endregion

    #region Variable Functions
    public int GetXPosition() {
        return xPosition;
    }

    public int GetYPosition() {
        return yPosition;
    }
    #endregion

}
