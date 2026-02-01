using System.Collections.Generic;
using _Farm._02._Scripts;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundPrefab;
    public GameObject spacePrefab;
    public Transform playerTransform;
    public GameObject coinPrefab;

    private float spawnZ = 0f;
    private int numberOfTiles = 10;
    private float safeZone = 60f;
    private List<TileInfo> groundTiles = new();

    private int previousTile = 2;

    void Start()
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        if (playerTransform.position.z - safeZone > (spawnZ - GetTotalTileSize()))
        {
            SpawnTile();
            DeleteTile();
        }
    }

    float GetTotalTileSize()
    {
        float tileSize = 0;
        foreach (TileInfo tile in groundTiles)
        {
            tileSize += tile.GetTileZSize();
        }

        return tileSize;
    }

    void SpawnTile()
    {
        TileInfo tileInfo;
        int tileIndex = Random.Range(0, 3);
        if (previousTile == 2) tileIndex = 0;
        previousTile = tileIndex;
        switch (tileIndex)
        {
            case 2:
            {
                tileInfo = CreateSpaceTile();
                break;
            }
            default:
            {
                tileInfo = CreateGroundTile();
                break;
            }
        }

        tileInfo.SetPosition(spawnZ);
        spawnZ += tileInfo.GetTileZSize();
        groundTiles.Add(tileInfo);
    }

    TileInfo CreateGroundTile()
    {
        GameObject tile = Instantiate(groundPrefab, transform);
        CreateCoins(10, 1, tile.transform);
        return new TileInfo(tile, 20f);
    }

    TileInfo CreateSpaceTile()
    {
        GameObject space = Instantiate(spacePrefab, transform);
        CreateCoins(5, 3, space.transform);
        return new TileInfo(space, 10f);
    }

    void CreateCoins(int coinCount, int positionY, Transform parent)
    {
        // X position 은 -3, 0, 3
        int randomPositionX = (Random.Range(0, 3) * 3) - 3;
        for (int i = 0; i < coinCount; i++)
        {
            GameObject coin = Instantiate(coinPrefab, parent);
            coin.transform.position = new Vector3(randomPositionX, positionY, i * 2 + 1);
        }
    }

    void DeleteTile()
    {
        TileInfo tileInfo = groundTiles[0];
        tileInfo.Destroy();
        groundTiles.Remove(tileInfo);
    }
}