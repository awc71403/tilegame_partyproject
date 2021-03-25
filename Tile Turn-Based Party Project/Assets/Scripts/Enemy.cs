using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Character myCharacter;
    private GameObject player;

    public float viewDistance;

    void Start()
    {
        player = PlayerManager.GetSingleton().gameObject;
    }

    void Update()
    {
        Vector2 direction = player.transform.position - transform.position;
        List<RaycastHit2D> results = new List<RaycastHit2D>();
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        int numHit = Physics2D.Raycast(transform.position, direction, contactFilter2D.NoFilter(), results, viewDistance);
        Debug.DrawRay(transform.position, direction.normalized * viewDistance, Color.red);
        for(int i = 0; i < numHit; i++)
        {
            if (results[i].transform.gameObject.GetComponent<PlayerManager>() != null)
            {
                Debug.Log("Hit player");
            }
            
        }
    }

    void DetectPlayer()
    {

    }
}
