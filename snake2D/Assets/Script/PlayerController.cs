using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 1f;
    public float RotationSpeed = 90f;
    public Vector2Int GridWrapOffset = new Vector2Int(0, 0);
    public Transform SegmentPrefab;
    public int initialsize;
    public bool IsShieldActive;
    public bool IsPlayer1;
    public TutorialManager TutorialManager;
    public ScoreController scoreController;

    [SerializeField]
    private Vector2Int gridMoveDirection = Vector2Int.right;
    [SerializeField]
    private float gridMoveTimer;
    [SerializeField]
    private float gridMoveTimerMax;
    [SerializeField]
    private Camera gameCamera;

    [SerializeField]
    private List<Transform> segments = new List<Transform>();

    private void Awake()
    {
        ResetSize();
    }
    private void Start()
    {
        if(SceneManager.GetActiveScene().name=="Single Player")
        {
            scoreController.setIsPlayerCoOp(false);
        }else if(SceneManager.GetActiveScene().name == "Co Op")
        {
            scoreController.setIsPlayerCoOp(true);
        }
        gridMoveTimerMax = 1f / MoveSpeed;
        gridMoveTimer = gridMoveTimerMax;
        gameCamera=Camera.main;

        
    }

    private void Update()
    {
        HandleInput();
        HandleMovement();
        WrapAroundScreen();
        scoreController.ChangeColor(IsShieldActive,IsPlayer1);
    }

    private void HandleInput()
    {
        if (IsPlayer1)
        {
            if ((Input.GetKeyDown(KeyCode.UpArrow)) && gridMoveDirection != Vector2Int.down)
            {
                gridMoveDirection = Vector2Int.up;
            }
            else if ((Input.GetKeyDown(KeyCode.DownArrow)) && gridMoveDirection != Vector2Int.up)
            {
                gridMoveDirection = Vector2Int.down;
            }
            else if ((Input.GetKeyDown(KeyCode.LeftArrow)) && gridMoveDirection != Vector2Int.right)
            {
                gridMoveDirection = Vector2Int.left;
            }
            else if ((Input.GetKeyDown(KeyCode.RightArrow)) && gridMoveDirection != Vector2Int.left)
            {
                gridMoveDirection = Vector2Int.right;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W) && gridMoveDirection != Vector2Int.down)
            {
                gridMoveDirection = Vector2Int.up;
            }
            else if (Input.GetKeyDown(KeyCode.S) && gridMoveDirection != Vector2Int.up)
            {
                gridMoveDirection = Vector2Int.down;
            }
            else if (Input.GetKeyDown(KeyCode.A) && gridMoveDirection != Vector2Int.right)
            {
                gridMoveDirection = Vector2Int.left;
            }
            else if (Input.GetKeyDown(KeyCode.D) && gridMoveDirection != Vector2Int.left)
            {
                gridMoveDirection = Vector2Int.right;
            }
        }
        
    }
    private void HandleMovement()
    {
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= gridMoveTimerMax)
        {
            gridMoveTimer -= gridMoveTimerMax;
            for(int i=segments.Count-1; i>0; i--)
            {
                segments[i].position = segments[i - 1].position;
            }
            this.transform.position = new Vector3(
           Mathf.Round(this.transform.position.x) + gridMoveDirection.x,
           Mathf.Round(this.transform.position.y) + gridMoveDirection.y,
           0.0f);
            //transform.position += new Vector3(gridMoveDirection.x, gridMoveDirection.y, 0f);
        }
    }
    private void WrapAroundScreen()
    {
        Vector3 screenPos = gameCamera.WorldToScreenPoint(transform.position);

        if (screenPos.x < 0 - GridWrapOffset.x && gridMoveDirection != Vector2Int.right)
        {
            screenPos.x = Mathf.Round(Screen.width + GridWrapOffset.x);
        }
        else if (screenPos.x > Screen.width + GridWrapOffset.x && gridMoveDirection != Vector2Int.left)
        {
            
            screenPos.x = Mathf.Round(0 - GridWrapOffset.x);
        }

        if (screenPos.y < 0 - GridWrapOffset.y && gridMoveDirection != Vector2Int.up)
        {
            screenPos.y = Mathf.Round(Screen.height + GridWrapOffset.y);
        }
        else if (screenPos.y > Screen.height + GridWrapOffset.y && gridMoveDirection != Vector2Int.down)
        {
            screenPos.y =Mathf.Round(0 - GridWrapOffset.y);
        }
        
        transform.position = gameCamera.ScreenToWorldPoint(screenPos);
    }
    public void grow()
    {
        SoundManager.Instance.Play(Sounds.collect);
        
        if (!IsPlayer1)
        {
            scoreController.ScoreCounter1();
        }
        else
        {
            scoreController.ScoreCounter();
        }
        Transform segment = Instantiate(this.SegmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }
    private void ResetSize()
    {
        segments.Add(this.transform);
        for(int i = 1; i < initialsize; i++)
        {
            Vector3 pos = new Vector3(Mathf.Round(this.transform.position.x - 1), Mathf.Round(this.transform.position.y),0.0f);
            segments.Add(Instantiate(this.SegmentPrefab,pos,Quaternion.identity));
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player_segment")
        {
            Debug.Log("Game Over");
        }
    }
    public void Death()
    {
        Debug.Log("player has died");
        SoundManager.Instance.Play(Sounds.PlayerDied);
        TutorialManager.EnableTutorial();
        this.enabled = false;
    }
    public bool GetShield()
    {
        return IsShieldActive;
    }
    public void SetShield(bool Active)
    {
        SoundManager.Instance.Play(Sounds.collect);
        IsShieldActive = Active;
    }

    public void SpeedUp()
    {
        SoundManager.Instance.Play(Sounds.collect);
        MoveSpeed += 2;
        gridMoveTimerMax = 1f / MoveSpeed;
        gridMoveTimer = gridMoveTimerMax;
    }

    public void SpeedDown()
    {
        SoundManager.Instance.Play(Sounds.collect);
        MoveSpeed -= 2;
        gridMoveTimerMax = 1f / MoveSpeed;
        gridMoveTimer = gridMoveTimerMax;
    }

    public void MassBurner()
    {
        if (IsPlayer1)
        {
            scoreController.ScoreDown();
        }else if (!IsPlayer1)
        {
            scoreController.ScoreDown1();
        }
        SoundManager.Instance.Play(Sounds.collect);
        PlayerSegment lastsegment = segments[segments.Count - 1].gameObject.GetComponent<PlayerSegment>();
        if (lastsegment != null)
        {
            lastsegment.Destroy();
        }
        segments.RemoveAt(segments.Count - 1);
        
    }
}
