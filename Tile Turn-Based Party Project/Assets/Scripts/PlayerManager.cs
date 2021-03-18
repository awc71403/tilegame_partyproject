using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private static PlayerManager m_Singleton;

    public static PlayerManager GetSingleton()
    {
        return m_Singleton;
    }

    private Character myCharacter;

    private const float stepDuration = 0.1f;

    // Start is called before the first frame update
    private void Awake()
    {
        if (m_Singleton != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        m_Singleton = this;

        myCharacter = GetComponent<Character>();
    }

    void Update() {
        if (!GameManager.actionInProcess) {
            if (Input.GetKeyDown(KeyCode.W)) {
                StartCoroutine(MoveUnitInDirection("up"));
            }
            else if (Input.GetKeyDown(KeyCode.A)) {
                StartCoroutine(MoveUnitInDirection("left"));
            }
            else if (Input.GetKeyDown(KeyCode.S)) {
                StartCoroutine(MoveUnitInDirection("down"));
            }
            else if (Input.GetKeyDown(KeyCode.D)) {
                StartCoroutine(MoveUnitInDirection("right"));
            }
            else if (Input.GetKeyDown(KeyCode.F)) {
                myCharacter.AttackEnemy(10);
            }
        }
    }

    #region Movement
    IEnumerator MoveUnitInDirection(string direction) {
        // Action in process!
        GameManager.actionInProcess = true;

        float tileSize = GetComponent<SpriteRenderer>().sprite.bounds.size.x;

        // Calculate the steps you need to take

        //Take that step!
        if (direction.Equals("up")) {
            myCharacter.transform.position += new Vector3(0, tileSize);
            if (myCharacter.occupiedTile.Up != null) {
                myCharacter.occupiedTile = myCharacter.occupiedTile.Up;
                myCharacter.myDirection = Character.Direction.UP;
            }
        }
        else if (direction.Equals("right")) {
            myCharacter.transform.position += new Vector3(tileSize, 0);
            if (myCharacter.occupiedTile.Right != null) {
                myCharacter.occupiedTile = myCharacter.occupiedTile.Right;
                myCharacter.myDirection = Character.Direction.RIGHT;
            }
        }
        else if (direction.Equals("down")) {
            myCharacter.transform.position -= new Vector3(0, tileSize);
            if (myCharacter.occupiedTile.Down != null) {
                myCharacter.occupiedTile = myCharacter.occupiedTile.Down;
                myCharacter.myDirection = Character.Direction.DOWN;
            }
        }
        else if (direction.Equals("left")) {
            myCharacter.transform.position -= new Vector3(tileSize, 0);
            if (myCharacter.occupiedTile.Left != null) {
                myCharacter.occupiedTile = myCharacter.occupiedTile.Left;
                myCharacter.myDirection = Character.Direction.LEFT;
            }
        }
        myCharacter.RecalculateDepth();
        myCharacter.StartBounceAnimation();
        yield return new WaitForSeconds(stepDuration);
        myCharacter.occupiedTile.PlaceUnit(myCharacter);

        // Action over!
        GameManager.actionInProcess = false;
    }
    #endregion
}
