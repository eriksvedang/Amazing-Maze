int n = 25;
int[][] maze = new int[n][n];
int cellSize = 10;

void setup() {
  frameRate(15);
  size(512, 512);
  cellSize = width / n;
  noStroke();
}

void draw() {
  background(200);
  for(int y = 0; y < n; y++) {
    for(int x = 0; x < n; x++) {
      if(x == posX && y == posY) {
        fill(255, 0, 0);
      }
      else if(maze[x][y] == 1) {
        fill(200);
      } else {
        fill(50);
      }
      rect(x * cellSize + 6, y * cellSize + 6, cellSize - 1, cellSize - 1);
    }
  }
  randomWalkAlgorithm();
}

// Position of gnome:
int posX = 1;
int posY = 1;

void randomWalkAlgorithm() {
  mark();
  int randomDirection = (int)random(4);
  if(randomDirection == 0 && posX < n - 2) {
    posX++;
    mark();
    posX++;
  }
  else if(randomDirection == 1 && posY > 1) {
    posY--;
    mark();
    posY--;
  }
  else if(randomDirection == 2 && posX > 1) {
    posX--;
    mark();
    posX--;
  }
  else if(randomDirection == 3 && posY < n - 2) {
    posY++;
    mark();
    posY++;
  }
  else {
    // ???
  }
}

void mark() {
  maze[posX][posY] = 1;
}