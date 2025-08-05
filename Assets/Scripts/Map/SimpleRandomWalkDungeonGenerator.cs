using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkDungeonGenerator : AbtractDungeonGenerator
{

    [SerializeField] protected SimpleRandomWalkSO randomWalkParameters;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRadomWalk(randomWalkParameters, startPosition);
        tilemapVisualizer.Clear();
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWall(floorPositions, tilemapVisualizer);
    }

    protected HashSet<Vector2Int> RunRadomWalk(SimpleRandomWalkSO parameters, Vector2Int position)
    {
        var currentPosition = position;
        HashSet<Vector2Int> floortPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < parameters.iterations; i++)
        {
            var path = ProceduralGenerationAlgorilthms.SimpleRandomWalk(currentPosition, parameters.walkLength);
            floortPositions.UnionWith(path);
            if (parameters.startRandomlyEachIteration) currentPosition = floortPositions.ElementAt(Random.Range(0, floortPositions.Count));
        }
        return floortPositions;
    }
}
