using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    // consts //
    public const float MAX_DISTANCE_TO_NEXT_LEVEL_POINT = 300;
    public const float FLOOR_SIZE = 50;
    public const float FLOOR_WIDTH = 30;
    public const float FLOOR_SIZE_MARGIN = 4;
    public const float FLOOR_WIDTH_MARGIN = 3;
    private Vector3 deltaCreationDot = new Vector3(50, 0, 0);
    // end - consts //

    // members //
    public GameObject player;
    public Vector3 nextCreationDot;
    public int levelIndex;
    public GameObject coin;
    public GameObject bomb;
    public GameObject clock;
    public GameObject enemy;
    public GameObject arrow;
    public GameObject floor;
    public GameObject wall;
    public GameObject wallLight;
    public GameObject bg;
    // end - members //

    // Start is called before the first frame update
    void Start()
    {
        nextCreationDot = new Vector3(1025, 0, 0);
        player = GameObject.Find("player");
        levelIndex = 1;
        coin = GameObject.Find("coin_manager");
        bomb = GameObject.Find("bomb_manager");
        clock = GameObject.Find("clock_manager");
        enemy = GameObject.Find("enemy_manager");
        arrow = GameObject.Find("arrow_manager");
        floor = GameObject.Find("floor_manager");
        wall = GameObject.Find("wall_manager");
        wallLight = GameObject.Find("wall_light_manager");
        bg = GameObject.Find("bg_manager");
    }

    // Update is called once per frame
    void Update()
    {
        // check if need a creation of next level
        if (Vector3.Distance(nextCreationDot, player.transform.position) < MAX_DISTANCE_TO_NEXT_LEVEL_POINT)
        {
            // TODO: create next level - https://docs.unity3d.com/ScriptReference/Object.Instantiate.html
            /* 
             * create next floor: center of mass: (x,y,z) -> (25 + levelIndex * 50, 0, 0)
             * create next WALL-lights: center of mass: (x,y,z) -> (50 + levelIndex * 50, 0, -14) & (50 + levelIndex * 50, 0, 14)
             * create next walls: center of mass: (x,y,z) -> (25 + levelIndex * 50, 0, -15) & (25 + levelIndex * 50, 0, 15)
             * create next enemies: between (levelIndex * 50, 1, -13) & ((levelIndex + 1) * 50, 1, 13)
             * create next coins: between (levelIndex * 50, 1, -13) & ((levelIndex + 1) * 50, 1, 13)
             * create next clocks: between (levelIndex * 50, 1, -13) & ((levelIndex + 1) * 50, 1, 13)
             * create next bombs: between (levelIndex * 50, 1, -13) & ((levelIndex + 1) * 50, 1, 13)
             */
            Vector3 levelCenterMass = new Vector3(nextCreationDot.x + FLOOR_SIZE / 2, 0, 0);
            createNextLevel(levelCenterMass);
            createRandomElements(levelCenterMass);

            // update the next location after creation of the next part of the level
            updateCreationPoint();
            levelIndex += 1;
        }
    }

    /// <summary>
    /// update the next point to make creation at
    /// </summary>
    public void updateCreationPoint()
    {
        nextCreationDot = new Vector3(nextCreationDot.x + deltaCreationDot.x,
            nextCreationDot.y + deltaCreationDot.y,
            nextCreationDot.z + deltaCreationDot.z);
    }

    /// <summary>
    /// creates the floor, walls and lights
    /// </summary>
    public void createNextLevel(Vector3 centerMassPoint)
    {
        // holder
        GameObject newObject;
        // create next floor
        newObject = Instantiate(floor, centerMassPoint, Quaternion.identity) as GameObject;
        newObject.name = "floor";
        // create next walls
        newObject = Instantiate(wall, centerMassPoint + new Vector3(0, 0, -15), Quaternion.identity) as GameObject;
        newObject.name = "wall";
        newObject = Instantiate(wall, centerMassPoint + new Vector3(0, 0, 15), Quaternion.identity) as GameObject;
        newObject.name = "wall";
        // create next back ground
        newObject = Instantiate(bg, centerMassPoint + new Vector3(0, -1, 0), Quaternion.identity) as GameObject;
        newObject.name = "gt";
        // create next walls lights
        newObject = Instantiate(wallLight, centerMassPoint + new Vector3(20, 1, 14), Quaternion.Euler(new Vector3(0, 0, 90))) as GameObject;
        newObject.name = "wall_light";
        newObject = Instantiate(wallLight, centerMassPoint + new Vector3(20, 1, -14), Quaternion.Euler(new Vector3(0, 0, 90))) as GameObject;
        newObject.name = "wall_light";
    }

    /// <summary>
    /// creates the bombs, coins, clocks and enemys on the new level
    /// </summary>
    public void createRandomElements(Vector3 centerMassPoint)
    {
        System.Random random = new System.Random();
        int changeToPutPercent = 80;
        int changeToPutCoinPercent = 40;
        int changeToPutBombPercent = 55; // 15
        int changeToPutClockPercent = 80; // 25
        int changeToPutEnemyPercent = 95; // 15
        int changeToPutArrowPercent = 100; // 5

        // make a grid of 5 on 5 given the level width and hight (x,z)
        int height = 3;
        int gridSize = 5;
        float deltaX = (FLOOR_SIZE - 2 * FLOOR_SIZE_MARGIN) / gridSize;
        float deltaZ = (FLOOR_WIDTH - 2 * FLOOR_WIDTH_MARGIN) / gridSize;
        // put randomly on the grid objects and there types
        for (int i = -1 * (int)Math.Floor((double)gridSize / 2); i <= (int)Math.Floor((double)gridSize / 2); i++)
        {
            for (int j = -1 * (int)Math.Floor((double)gridSize / 2); j < (int)Math.Floor((double)gridSize / 2); j++)
            {
                if (random.Next(101) > changeToPutPercent)
                {
                    // generate empty object
                    GameObject newObject = null;
                    // right location to create
                    Vector3 newObjectLocation = new Vector3(centerMassPoint.x + i * deltaX, centerMassPoint.y + height, centerMassPoint.z + j * deltaZ);
                    int randomNumber = random.Next(101);
                    if (randomNumber < changeToPutCoinPercent)
                    {
                        // generate coin
                        newObject = Instantiate(coin, newObjectLocation, Quaternion.Euler(new Vector3(0, random.Next(180), 0))) as GameObject;
                        newObject.name = "coin";
                    }
                    else if (randomNumber < changeToPutBombPercent)
                    {
                        // generate bomb
                        newObject = Instantiate(bomb, newObjectLocation, Quaternion.Euler(new Vector3(0, random.Next(180), 0))) as GameObject;
                        newObject.name = "bomb";
                    }
                    else if (randomNumber < changeToPutClockPercent)
                    {
                        // generate clock
                        newObject = Instantiate(clock, newObjectLocation, Quaternion.Euler(new Vector3(0, random.Next(180), 0))) as GameObject;
                        newObject.name = "clock";
                    }
                    else if (randomNumber < changeToPutEnemyPercent)
                    {
                        // generate enemy
                        newObject = Instantiate(enemy, newObjectLocation, Quaternion.Euler(new Vector3(0, 270, 0))) as GameObject;
                        newObject.name = "enemy";
                    }
                    else if (randomNumber < changeToPutArrowPercent)
                    {
                        // generate arrow

                        newObject = Instantiate(arrow, newObjectLocation, Quaternion.identity) as GameObject;
                        newObject.name = "arrow";
                    }
                }
            }
        }
    }
}
