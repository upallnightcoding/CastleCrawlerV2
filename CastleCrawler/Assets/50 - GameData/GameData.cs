using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GameData", menuName = "CasteCrawler/Game Data")]
public class GameData : ScriptableObject
{
    [Header("Constants")]
    public static readonly Step NORTH_STEP = new Step(0, 1);
    public static readonly Step SOUTH_STEP = new Step(0, -1);
    public static readonly Step EAST_STEP = new Step(1, 0);
    public static readonly Step WEST_STEP = new Step(-1, 0);

    public Vector3 GetTilePos(int x, int z) => new Vector3(x, 0.0f, z);

    [Header("Tile Images")]
    public Material bombMaterial;
    public Material crownMaterial;
    public Material castelMaterial;

    [Header("Game Attributes")]
    public static int width = 10;
    public static int height = 10;
    public int level;
    public int safeGuardLimit;
    public bool debugSw;

    [Header("Moves")]
    public string[] listOfMoves;

    [Header("Materials")]
    public Material TileGreen;
    public Material StartEndTileColor;
    public Material TileGray;
    public Material BombTileColor;
    public Material TileBlack;

    public Sprite[] btnSprite;
    public Material[] tileMaterial;
}
