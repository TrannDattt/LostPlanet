using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Tilemap map;
    [SerializeField] private List<Tile> groundTiles;
    [SerializeField] private List<Tile> borderTiles;
    [SerializeField] private Tile gateTile;
    [SerializeField] private GameObject mapDecor;
    [SerializeField] private List<MapDecor> decorObjects;
    [SerializeField] private List<Gate> gates;
    [SerializeField] private Transform startPos;

    [SerializeField] private int mapWidth;
    [SerializeField] private int mapHeight;
    [SerializeField] private int borderThickness;

    public TileData[,] TileDatas { get; private set; }

    private List<Vector2Int> borderTileCord = new(); 

    public void InitMap()
    {
        map.transform.position = GetTileCenter(new Vector2Int((int)(-mapHeight * 0.5f) + 1, (int)(-mapHeight * 0.5f) + 1));

        GenerateMap();
        GenerateDecor();
        DeactivateGates();
        SpawnPlayer();
    }

    private void GenerateMap()
    {
        TileDatas = new TileData[mapWidth, mapHeight];

        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                TileDatas[i, j] = new TileData();

                if(i == mapWidth * 0.5f && j == mapHeight - borderThickness)
                {
                    map.SetTile(new Vector3Int(i, j), gateTile);
                    TileDatas[i, j].walkable = true;
                }
                else if(i >= borderThickness && j >= borderThickness && i < mapWidth - borderThickness && j < mapHeight - borderThickness)
                {
                    map.SetTile(new Vector3Int(i, j), GetRandomTile(groundTiles));
                    TileDatas[i, j].walkable = true;
                }
                else
                {
                    map.SetTile(new Vector3Int(i, j), GetRandomTile(borderTiles));
                    TileDatas[i, j].walkable = false;

                    if(i == borderThickness - 1 || j == borderThickness - 1 || i == mapWidth - borderThickness || j == mapHeight - borderThickness)
                    { 
                        borderTileCord.Add(new Vector2Int(i, j)); 
                    }
                }
            }
        }
    }

    private void GenerateDecor()
    {
        while(decorObjects.Count > 0)
        {
            int randomIndex = Random.Range(0, borderTileCord.Count - 1);
            var randomCellCord = borderTileCord[randomIndex];
            var randomCellData = TileDatas[randomCellCord.x, randomCellCord.y];

            if(randomCellData.decor == null)
            {
                var newDecor = Instantiate(GetDecor(), GetTileCenter(randomCellCord), Quaternion.identity);
                newDecor.transform.SetParent(mapDecor.transform);
                borderTileCord.Remove(randomCellCord);
            }
        }
    }

    private void SpawnPlayer()
    {
        Player.Instance.transform.position = startPos.position;
    }

    private Vector2 GetTileCenter(Vector2Int tileCordinate)
    {
        return map.CellToWorld(new Vector3Int (tileCordinate.x, tileCordinate.y));
    }

    private Tile GetRandomTile(List<Tile> tiles)
    {
        int randomIndex = Random.Range(0, tiles.Count - 1);
        return tiles[randomIndex];
    }

    private GameObject GetDecor()
    {
        var decorObject = decorObjects[0];

        decorObject.amount--;
        if (decorObject.amount == 0)
        {
            decorObjects.RemoveAt(0);
        }

        return decorObject.decor;
    }

    private void DeactivateGates()
    {
        foreach (var gate in gates)
        {
            gate.gameObject.SetActive(false);
        }
    }

    private void ActivateGates()
    {
        foreach (var gate in gates)
        {
            gate.gameObject.SetActive(true);
        }
    }

    private void OnEnable()
    {
        if(GameManager.Instance != null)
        {
            GameManager.Instance.OnLevelStarted += InitMap;
            GameManager.Instance.OnLevelCleared += ActivateGates;
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnLevelStarted -= InitMap;
        GameManager.Instance.OnLevelCleared -= ActivateGates;
    }
}

[System.Serializable]
public class MapDecor
{
    public GameObject decor;
    public int amount;
}

public class TileData
{
    public GameObject decor;
    public bool walkable;
}
