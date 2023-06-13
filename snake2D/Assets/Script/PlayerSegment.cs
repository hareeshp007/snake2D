using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSegment : MonoBehaviour
{
    


    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>() != null)
        {
            PlayerController controller = collision.gameObject.GetComponent<PlayerController>();
            if (!controller.GetShield())
            {
                controller.Death();
                
                Debug.Log("Game Over");
            }
            else
            {
                controller.SetShield(false);
                Debug.Log("shield is destroyed");
            }
            
        }
        Debug.Log("Trigger exit");
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Trigger Stay");
    }
}
