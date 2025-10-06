using UnityEngine;
using UnityEngine.Tilemaps;

public class Box : MonoBehaviour
{
    public Sprite normalSprite;
    public Sprite onGoalSprite;
    private SpriteRenderer spriteRenderer;

    public Tilemap goalTilemap;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = normalSprite;

        // Assign your goal tilemap in the Inspector
        //goalTilemap = GameObject.Find("goal_0").GetComponent<Tilemap>();
    }

    public void UpdateSprite(Vector3Int cellPosition)
    {
        if (goalTilemap.HasTile(cellPosition))
        {
            spriteRenderer.sprite = onGoalSprite;
        }
        else
        {
            spriteRenderer.sprite = normalSprite;
        }
    }

    public bool IsOnGoal()
    {
        Vector3Int cellPos = goalTilemap.WorldToCell(transform.position);
        return goalTilemap.HasTile(cellPos);
    }
}
