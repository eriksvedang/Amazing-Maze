using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeMaze_RandomWalk : MonoBehaviour {

    public int size = 20;
    public GameObject wallPrefab;
    public float spacing = 1.1f;
    public float height = 1.0f;
    public float chanceOfVisible = 0.5f;

    Transform[,] maze;

    int x = 0;
    int z = 0;
    public Transform walker;

    void Start() {
        maze = new Transform[size, size];

        for (int j = 0; j < size; j++) {
            for (int i = 0; i < size; i++) {
                var position = new Vector3(i * spacing, height, j * spacing) - halfBoard;
                GameObject wall = Instantiate(wallPrefab, position, Quaternion.identity);
                maze[i, j] = wall.transform;
                wall.transform.SetParent(this.transform);
            }
        }

        GenerateNewMazeLayout();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            GenerateNewMazeLayout();
        }
        walker.transform.position = new Vector3(x * spacing, 1, z * spacing) - halfBoard;
    }

    Vector3 halfBoard {
        get {
            return new Vector3(size / 2.0f * spacing, 0, size / 2.0f * spacing);
        }
    }

    void SetMaze(int x, int z, bool on) {
        var renderer = maze[x, z].GetComponent<Renderer>();
        renderer.enabled = on;
        var collider = maze[x, z].GetComponent<Collider>();
        collider.enabled = on;
    }

    void GenerateNewMazeLayout() {
        x = 1;
        z = 1;
        SetMaze(x, z, false);
        StartCoroutine(TakeRandomStep());
    }

    public enum Direction {
        East = 0,
        North = 1,
        West = 2,
        South = 3
    }

    void Step(Direction direction) {
        if(direction == Direction.East && x < size - 3) {
            x++;
            SetMaze(x, z, false);
            x++;
        } else if(direction == Direction.North && z < size - 3) {
            z++;
            SetMaze(x, z, false);
            z++;
        } else if(direction == Direction.West && x > 1) {
            x--;
            SetMaze(x, z, false);
            x--;
        } else if(direction == Direction.South && z > 1) {
            z--;
            SetMaze(x, z, false);
            z--;
        }
        SetMaze(x, z, false);
    }

    IEnumerator TakeRandomStep() {
        while(true) {
            Direction[] directions = { Direction.East, Direction.North, Direction.West, Direction.South };
            Step(directions[(int)(Random.value * 4)]);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
