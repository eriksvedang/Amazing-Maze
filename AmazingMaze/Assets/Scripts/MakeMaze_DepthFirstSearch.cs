using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeMaze_DepthFirstSearch : MonoBehaviour {

    public int size = 20;
    public GameObject wallPrefab;
    public float spacing = 1.1f;
    public float height = 1.0f;

    Transform[,] maze;

    Point pos;
    Stack<Point> visited = new Stack<Point>();

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
        walker.transform.position = new Vector3(pos.x * spacing, 1, pos.y * spacing) - halfBoard;
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
        pos = new Point(1, 1);
        SetMaze(pos.x, pos.y, false);
        visited.Push(pos);
        StartCoroutine(TakeRandomStep());
    }

    public enum Direction {
        East = 0,
        North = 1,
        West = 2,
        South = 3
    }

    void Step(Direction direction) {
        if(direction == Direction.East && pos.x < size - 3) {
            pos.x++;
            SetMaze(pos.x, pos.y, false);
            pos.x++;
        } else if(direction == Direction.North && pos.y < size - 3) {
            pos.y++;
            SetMaze(pos.x, pos.y, false);
            pos.y++;
        } else if(direction == Direction.West && pos.x > 1) {
            pos.x--;
            SetMaze(pos.x, pos.y, false);
            pos.x--;
        } else if(direction == Direction.South && pos.y > 1) {
            pos.y--;
            SetMaze(pos.x, pos.y, false);
            pos.y--;
        }
        SetMaze(pos.x, pos.y, false);
        visited.Push(pos);
    }

    List<Direction> GetGoodDirections() {
        var dirs = new List<Direction>();

        if(pos.x < size - 4 && !Visited(new Point(pos.x + 2, pos.y))) {
            dirs.Add(Direction.East);
        }

        if(pos.y < size - 4 && !Visited(new Point(pos.x, pos.y + 2))) {
            dirs.Add(Direction.North);
        }

        if(pos.x > 3 && !Visited(new Point(pos.x - 2, pos.y))) {
            dirs.Add(Direction.West);
        }

        if(pos.y > 3 && !Visited(new Point(pos.x, pos.y - 2))) {
            dirs.Add(Direction.South);
        }

        return dirs;
    }

    IEnumerator TakeRandomStep() {
        while(true) {

            int badDirections = 0;

            if(pos.x >= size - 4 || Visited(new Point(pos.x + 2, pos.y))) {
                badDirections++;
            }
            if(pos.x <= 3  || Visited(new Point(pos.x - 2, pos.y))) {
                badDirections++;
            }
            if(pos.y >= size - 4 || Visited(new Point(pos.x, pos.y + 2))) {
                badDirections++;
            }
            if(pos.y <= 3 || Visited(new Point(pos.x, pos.y - 2))) {
                badDirections++;
            }

            if(badDirections == 4) {
                if(visited.Count == 0) {
                    print("Done.");
                    break;
                } else {
                    pos = visited.Pop();
                }
            }
            else {
                var okDirections = GetGoodDirections();
                if(okDirections.Count > 0) {
                    Direction randomDir = okDirections[(int)(Random.value * okDirections.Count)];
                    Step(randomDir);
                }
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    bool Visited(Point p) {
        return !maze[p.x, p.y].GetComponent<Renderer>().enabled;
    }
}
