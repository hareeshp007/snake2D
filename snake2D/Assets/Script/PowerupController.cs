using UnityEngine;


public class PowerupController : MonoBehaviour
{
    public Vector2Int spawnAreaMin = new Vector2Int(-17, -9);
    public Vector2Int spawnAreaMax = new Vector2Int(17, 9);
    [SerializeField]
    public float PowerUpMoveTimer;
    public float PowerUpMoveTimerMax = 5f;
    public float FoodDeadMoveTimer;
    public float FoodDeadMoveTimerMax = 10f;
    public GameObject PowerUpPrefab;
    public GameObject SpeedDownPrefab;
    public GameObject FoodDeadPrefab;
    public GameObject ShieldPrefab;
    // Start is called before the first frame update
    public void SpawnPowerUps()
    {
        PowerUpMoveTimer += Time.deltaTime;
        if (PowerUpMoveTimer >= PowerUpMoveTimerMax)
        {
            PowerUpMoveTimer -= PowerUpMoveTimerMax;
            RandomizePosition(PowerUpPrefab);
            RandomizePosition(SpeedDownPrefab);
        }
    }
    public void SpawnDeadFood()
    {
        FoodDeadMoveTimer += Time.deltaTime;
        if (FoodDeadMoveTimer >= FoodDeadMoveTimerMax)
        {
            FoodDeadMoveTimer -= FoodDeadMoveTimerMax;
            RandomizePosition(FoodDeadPrefab);
            RandomizePosition(ShieldPrefab);
        }
    }

    private void RandomizePosition(GameObject Prefab)
    {
        float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float y = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector3 pos = new Vector3(x, y, 0);
        Debug.Log(pos);
        GameObject powerup = Instantiate(Prefab, pos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        SpawnPowerUps();
        SpawnDeadFood();
    }
}
