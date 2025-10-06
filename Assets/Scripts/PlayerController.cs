using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement; // <-- needed for scene reload

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // speed of sliding between tiles
    private bool isMoving;
    private Vector2 targetPos;

    public Sprite guyUp;
    public Sprite guyDown;
    public Sprite guyLeft;
    public Sprite guyRight;
    private SpriteRenderer spriteRenderer;

    public Tilemap wallTilemap; // assign in Inspector
    public Box[] boxes; // assign all boxes in the level or populate via LevelManager

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetPos = transform.position;
    }

    void Update()
    {
        // --- Movement ---
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                TryMove(Vector2.up, guyUp);
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                TryMove(Vector2.down, guyDown);
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                TryMove(Vector2.left, guyLeft);
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                TryMove(Vector2.right, guyRight);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            if ((Vector2)transform.position == targetPos)
                isMoving = false;
        }

        // --- Restart level ---
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    void TryMove(Vector2 dir, Sprite facingSprite)
    {
        Vector2 playerPos = targetPos;
        Vector2 nextPos = playerPos + dir;

        // --- Check wall at next position ---
        Vector3Int wallCell = wallTilemap.WorldToCell(nextPos);
        if (wallTilemap.HasTile(wallCell))
            return;

        // --- Check if a box is at nextPos ---
        Box boxAtNext = GetBoxAtPosition(nextPos);

        if (boxAtNext != null)
        {
            Vector2 boxTargetPos = (Vector2)boxAtNext.transform.position + dir;

            // Check wall behind the box
            Vector3Int boxWallCell = wallTilemap.WorldToCell(boxTargetPos);
            if (wallTilemap.HasTile(boxWallCell))
                return;

            // Check if another box is behind
            if (GetBoxAtPosition(boxTargetPos) != null)
                return;

            // Move box
            boxAtNext.transform.position = boxTargetPos;
            boxAtNext.UpdateSprite(Vector3Int.FloorToInt(boxTargetPos));
        }

        // Move player
        targetPos = nextPos;
        isMoving = true;
        spriteRenderer.sprite = facingSprite;

        // Check level completion
        if (LevelManager.Instance != null)
            LevelManager.Instance.CheckLevelComplete();
    }

    // Helper: returns the box at a given world position, or null
    Box GetBoxAtPosition(Vector2 pos)
    {
        foreach (Box b in boxes)
        {
            if ((Vector2)b.transform.position == pos)
                return b;
        }
        return null;
    }

    // --- Restart level method ---
    void RestartLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }
}
