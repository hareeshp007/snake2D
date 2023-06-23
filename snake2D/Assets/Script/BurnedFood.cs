using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnedFood : MonoBehaviour
{
    public float DestroyDelay;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Death", DestroyDelay);
    }
    public void Death()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.MassBurner();
            Death();
        }
    }
}
