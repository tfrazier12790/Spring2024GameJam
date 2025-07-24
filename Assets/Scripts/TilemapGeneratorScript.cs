using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Handles procedural tile generation and display based on player position.
/// </summary>

// Stores data for individual tiles
public class TileData
{
    public TileBase tile;
}

// Stores data for a chunk of tiles
public class TileChunk
{
    public const int Size = 16;
    public Vector2Int chunkCoord;
    public TileData[,] tiles = new TileData[Size, Size];
}

public class TilemapGeneratorScript : MonoBehaviour
{
    // Reference to Tilemap component used to render tiles
    Tilemap tilemap;

    // Reference to the player
    Transform player;

    [Header("Tile Settings")]
    [SerializeField] List<TileBase> grassTiles = new List<TileBase>();
    [SerializeField] List<TileBase> deadGrassTiles = new List<TileBase>();

    // Stores all generated world tiles using their position as the key so the same tile can be reused when revisited
    Dictionary<Vector3Int, TileData> worldTiles = new Dictionary<Vector3Int, TileData>();

    // Stores the last known tile position of the player to detect movement
    Vector3Int lastCenter;

    [Header("Perlin Settings")]
    [SerializeField] float noiseScale = 10f;
    [SerializeField] float deadGrassThreshold = 0.5f;
    [SerializeField] Vector2 noiseOffset = new Vector2(100f, 200f);

    private void Start()
    {
        // Store player and tilemap references
        player = GameObject.FindGameObjectWithTag("Player").transform;
        tilemap = GetComponentInChildren<Tilemap>();
    }

    private void Update()
    {
        // Converts the player's world position to tilemap grid coordinates
        Vector3Int currentTile = tilemap.WorldToCell(player.position);

        // Only update tiles if the player has moved to a new tile
        if (currentTile != lastCenter)
        {
            lastCenter = currentTile;
            UpdateVisibleTiles(currentTile);
        }
    }

    // Randomly selects a tile from the available list of tiles
    TileBase GenerateTile(Vector3Int pos)
    {
        float xCoord = (pos.x + noiseOffset.x) / noiseScale;
        float yCoord = (pos.y + noiseOffset.y) / noiseScale;

        float noiseValue = Mathf.PerlinNoise(xCoord, yCoord);

        if (noiseValue > deadGrassThreshold)
        {
            int index = Random.Range(0, deadGrassTiles.Count);
            return deadGrassTiles[index];
        } else
        {
            int index = Random.Range(0, grassTiles.Count);
            return grassTiles[index];
        }
    }

    // Updates the visible area around the player by placing tiles from memory or generating new ones
    void UpdateVisibleTiles(Vector3Int centerTile)
    {
        int viewRadius = 16;

        // Clears currently rendered tiles before redrawing
        tilemap.ClearAllTiles();

        // Loop through each tile in the visible radius
        for (int x = -viewRadius; x <= viewRadius; x++)
        {
            for (int y = -viewRadius; y <= viewRadius; y++)
            {
                Vector3Int pos = new Vector3Int(centerTile.x + x, centerTile.y + y, 0);

                // Check if the tile is already generated; create one if not
                if (!worldTiles.TryGetValue(pos, out TileData data))
                {
                    data = new TileData { tile = GenerateTile(pos) };
                    worldTiles[pos] = data;
                }

                // Set tile on the tilemap
                tilemap.SetTile(pos, data.tile);
            }
        }
    }
}
