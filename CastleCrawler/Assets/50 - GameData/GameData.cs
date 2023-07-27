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

    [Header("Game Attributes")]
    public int width;
    public int hieght;

    [Header("Moves")]
    public string[] listOfMoves;
}
