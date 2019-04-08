using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeMaze_TotallyRandom : MonoBehaviour {

    public int size = 20;
    public GameObject wallPrefab;
    public float spacing = 1.1f;
    public float height = 1.0f;
    public float chanceOfVisible = 0.5f;

    Transform[,] maze;

    void Start() {
        maze = new Transform[size, size];
        Vector3 halfBoard = new Vector3(size / 2.0f * spacing, 0, size / 2.0f * spacing);

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
    }

    void SetMaze(int x, int z, bool on) {
        var renderer = maze[x, z].GetComponent<Renderer>();
        renderer.enabled = on;
        var collider = maze[x, z].GetComponent<Collider>();
        collider.enabled = on;
    }

    void GenerateNewMazeLayout() {
        for (int j = 0; j < size; j++) {
            for (int i = 0; i < size; i++) {
                SetMaze(i, j, UnityEngine.Random.value < chanceOfVisible);
            }
        }
    }
}
