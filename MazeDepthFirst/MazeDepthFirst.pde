import java.util.Stack;

int n = 25;
int[][] maze = new int[n][n];
int cellSize = 10;

// Position of gnome:
Coord pos = new Coord(1, 1);

// History where the gnome has been:
Stack<Coord> visited = new Stack<Coord>();

class Coord {
  public int x;
  public int y;
  public Coord(int x, int y) {
    this.x = x;
    this.y = y;
  }
  public boolean equals(Object other) {
    Coord otherCoord = (Coord)other;
    return (this.x == otherCoord.x) && (this.y == otherCoord.y);
  }
}

void setup() {
  frameRate(60);
  size(512, 512);
  cellSize = width / n;
  noStroke();
  visited.push(new Coord(pos.x, pos.y));
}

void draw() {
  background(200);
  for(int y = 0; y < n; y++) {
    for(int x = 0; x < n; x++) {
      if(x == pos.x && y == pos.y) {
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
  depthFirstAlgorithm();
}

void depthFirstAlgorithm() {
  mark();
  
  int visitCount = 0;
  if(pos.x + 2 > n - 2 || taken(new Coord(pos.x + 2, pos.y))) {
    visitCount++;
  }
  if(pos.y - 2 < 1 || taken(new Coord(pos.x, pos.y - 2))) {
    visitCount++;
  }
  if(pos.x - 2 < 1 || taken(new Coord(pos.x - 2, pos.y))) {
    visitCount++;
  }
  if(pos.y + 2 > n - 2 || taken(new Coord(pos.x, pos.y + 2))) {
    visitCount++;
  }
  
  if(visitCount == 4) {
    // Can't go anywhere
    if(visited.size() > 0) {
      pos = visited.pop();
    }
  }
  else {
    tryMakeMove();
  }
}

boolean taken(Coord coord) {
  return maze[coord.x][coord.y] == 1;
}

void tryMakeMove() {
  int randomDirection = (int)random(4);
  if(randomDirection == 0 && pos.x < n - 2 && !taken(new Coord(pos.x + 2, pos.y))) {
    pos.x++;
    mark();
    pos.x++;
    visited.push(new Coord(pos.x, pos.y));
  }
  else if(randomDirection == 1 && pos.y > 1 && !taken(new Coord(pos.x, pos.y - 2))) {
    pos.y--;
    mark();
    pos.y--;
    visited.push(new Coord(pos.x, pos.y));
  }
  else if(randomDirection == 2 && pos.x > 1 && !taken(new Coord(pos.x - 2, pos.y))) {
    pos.x--;
    mark();
    pos.x--;
    visited.push(new Coord(pos.x, pos.y));
  }
  else if(randomDirection == 3 && pos.y < n - 2 && !taken(new Coord(pos.x, pos.y + 2))) {
    pos.y++;
    mark();
    pos.y++;
    visited.push(new Coord(pos.x, pos.y));
  }
  else {
    // ???
    tryMakeMove();
  }
}

void mark() {
  maze[pos.x][pos.y] = 1;
}