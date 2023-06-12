using UnityEngine;

public class FoodController : MonoBehaviour
{
    public Vector2Int spawnAreaMin=new Vector2Int(-17,-9);
    public Vector2Int spawnAreaMax = new Vector2Int(17, 9);
    [SerializeField]
    public float FoodMoveTimer;
    public float FoodMoveTimerMax=5f;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        HandlePositioning();
    }
    public void RandomizePosition()
    {
        float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float y = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        this.transform.position=new Vector3(Mathf.Round(x), Mathf.Round(y),0);
        
    }
    public void HandlePositioning()
    {
        FoodMoveTimer += Time.deltaTime;
        if (FoodMoveTimer >= FoodMoveTimerMax)
        {
            FoodMoveTimer -= FoodMoveTimerMax;
            RandomizePosition();
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered1");
        
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            RandomizePosition();
            FoodMoveTimer = 0;
            playerController.grow();
        }
        if(collision.tag== "Player_segment")
        {
            Debug.Log("Playersegment");
            RandomizePosition();
            FoodMoveTimer = 0;
        }
    }

}
