using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionUI : MonoBehaviour
{
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;

    public Sprite upright;
    public Sprite upleft;
    public Sprite downright;
    public Sprite downleft;

    public void SwitchDirection(string direction) {
        if (direction.Equals("right")) {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = right;
        }
        else if (direction.Equals("left")) {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = left;
        }
        else if (direction.Equals("up")) {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = up;
        }
        else if (direction.Equals("down")) {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = down;
        }
        else if (direction.Equals("upright")) {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = upright;
        }
        else if (direction.Equals("upleft")) {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = upleft;
        }
        else if (direction.Equals("downright")) {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = downright;
        }
        else {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = downleft;
        }
    }


}
