using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    private Player player;
    void Awake()
    {
        player = Player.Instance;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == 5)
        {
            player.Grounded = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 5)
        {
            player.Grounded = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 5)
        {
            player.Grounded = true;
        }
    }
}
