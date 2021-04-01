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
        DetectPlayer();
    }

    void DetectPlayer()
    {
        Vector2 direction = player.transform.position - transform.position;
        List<RaycastHit2D> results = new List<RaycastHit2D>();
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        int numHit = Physics2D.Raycast(transform.position, direction, contactFilter2D.NoFilter(), results, viewDistance);
        Debug.DrawRay(transform.position, direction.normalized * viewDistance, Color.red);

        bool blocked = false;
        bool playerVisible = false;

        for (int i = 0; i < numHit; i++)
        {
            GameObject hitObject = results[i].transform.gameObject;
            if (hitObject.GetComponent<PlayerManager>() != null)
            {
                if (!blocked)
                {
                    playerVisible = true;
                }
            }
            else if (hitObject.tag.Equals("Wall"))
            {
                blocked = true;
            }
        }



    }
}
