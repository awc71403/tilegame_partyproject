using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public TileBehavior UpRight;
    public TileBehavior UpLeft;
    public TileBehavior DownRight;
    public TileBehavior DownLeft;

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

    #region Movement Functions

    // Recursive helper function to calculate the steps to take to get from tile A to tile B
    public static List<string> CalculateMovement(List<string> movement, TileBehavior currentTile, TileBehavior goalTile, float tileSize, float moveEnergy)
    {
       

        // If you're out of energy, it's an invalid path.
        if (moveEnergy < 0)
        {
            return null;
        }

        List<List<string>> validPaths = new List<List<string>>();

        // Check for all adjacent tiles:
        TileBehavior[] neighbors = { currentTile.Left, currentTile.Up, currentTile.Right, currentTile.Down};

        // If you're there, return the movement path.
        if (neighbors.Contains(goalTile))
        {
            return movement;
        }

        foreach (TileBehavior neighbor in neighbors)
        {
          
            
            if (neighbor != null && neighbor.tileType != "wall")
            {
                Character otherTileUnit = neighbor.myUnit;
                if (otherTileUnit == null)
                {
                    List<string> newMovement = new List<string>(movement.ToArray());
                    if (neighbor.Equals(currentTile.Right))
                    {
                        newMovement.Add("right");
                    }
                    if (neighbor.Equals(currentTile.Left))
                    {
                        newMovement.Add("left");
                    }
                    if (neighbor.Equals(currentTile.Up))
                    {
                        newMovement.Add("up");
                    }
                    if (neighbor.Equals(currentTile.Down))
                    {
                        newMovement.Add("down");
                    }
                    List<string> path = CalculateMovement(newMovement, neighbor, goalTile, tileSize, moveEnergy - 1);
                    if (path != null)
                    {
                        validPaths.Add(path);
                    }
                }
            }
        }

        // Return the shortest valid path
        if (validPaths.Count != 0)
        {
            List<string> shortestList = validPaths[0];
            foreach (List<string> path in validPaths)
            {
                if (path.Count < shortestList.Count)
                {
                    shortestList = path;
                }
            }
            return shortestList;
        }

        // If there are no valid paths from this point, return null
        return null;
    }
    #endregion

    public abstract void Effect();
}
