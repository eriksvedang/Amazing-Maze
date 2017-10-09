int n = 25;
int[][] maze = new int[n][n];
int cellSize = 10;

void setup() {
  frameRate(1);
  size(512, 512);
  cellSize = width / n;
  noStroke();
  randomAlgorithm();
}

void randomAlgorithm() {
  for(int y = 0; y < n; y++) {
    for(int x = 0; x < n; x++) {
      maze[x][y] = (int)random(2);
    }
  }
}

void draw() {
  randomAlgorithm();
  background(200);
  for(int y = 0; y < n; y++) {
    for(int x = 0; x < n; x++) {
      if(maze[x][y] == 1) {
        fill(200);
      } else {
        fill(50);
      }
      rect(x * cellSize + 6, y * cellSize + 6, cellSize - 1, cellSize - 1);
    }
  }
}