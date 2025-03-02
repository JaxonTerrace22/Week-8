using System;
using System.Collections.Generic;
using System.Linq;

public class Maze {
    public int Width { get; }
    public int Height { get; }
    public readonly int[] Data;

    public Maze(int width, int height, int[] data) {
        if (data.Length != width * height)
            throw new ArgumentException("Grid length does not match dimensions");
        Width = width;
        Height = height;
        Data = data;
    }

    /// <summary>
    /// Returns true if the (x,y) cell is the end (cell value equals 2)
    /// </summary>
    public bool IsEnd(int x, int y) {
        return Data[y * Height + x] == 2;
    }

    /// <summary>
    /// Returns true if moving to (x,y) is allowed given maze boundaries,
    /// the cell content (0 = wall), and the current path (no repeats)
    /// </summary>
    public bool IsValidMove(List<(int, int)> currPath, int x, int y) {
        if (x < 0 || x >= Width)
            return false;
        if (y < 0 || y >= Height)
            return false;
        if (Data[y * Height + x] == 0)
            return false;
        if (currPath.Contains((x, y)))
            return false;
        return true;
    }

    /// <summary>
    /// Recursively explores the maze to display all paths from (0,0) to the end cell
    /// When a path is found, it is printed as a list of (x,y) coordinates
    /// </summary>
    public static void SolveMaze(Maze maze, int x = 0, int y = 0, List<(int, int)>? currPath = null) {
        // initialize current path if first call.
        if (currPath == null)
            currPath = new List<(int, int)>();

        // if move isn't valid, return
        if (!maze.IsValidMove(currPath, x, y))
            return;

        // add current cell to path
        currPath.Add((x, y));

        // if ending reached, print path
        if (maze.IsEnd(x, y)) {
            Console.WriteLine("<List>{" + string.Join(", ", currPath.Select(p => $"({p.Item1}, {p.Item2})")) + "}");
            currPath.RemoveAt(currPath.Count - 1); // backtrack before returning
            return;
        }

        // explore neighbors: up, right, down, or left
        SolveMaze(maze, x, y - 1, currPath); // up
        SolveMaze(maze, x + 1, y, currPath); // right
        SolveMaze(maze, x, y + 1, currPath); // down
        SolveMaze(maze, x - 1, y, currPath); // left

        // backtrack: remove cell
        currPath.RemoveAt(currPath.Count - 1);
    }
}
