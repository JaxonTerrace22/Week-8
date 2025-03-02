using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace prove_08
{
    public static class RecursionTester
    {
        /// <summary>
        /// Entry point for the Prove 8 tests
        /// </summary>
        public static void Run()
        {
            // ---------------------------
            // Problem 1 - Recursive Squares Sum
            // ---------------------------
            Console.WriteLine("\n=========== PROBLEM 1 TESTS ===========");
            Console.WriteLine(SumSquaresRecursive(10));   // expected 385
            Console.WriteLine(SumSquaresRecursive(100));  // expected 338350

            // ---------------------------
            // Problem 2 - Permutations Choose
            // ---------------------------
            Console.WriteLine("\n=========== PROBLEM 2 TESTS ===========");
            PermutationsChoose("ABCD", 3);
            Console.WriteLine("---------");
            PermutationsChoose("ABCD", 2);
            Console.WriteLine("---------");
            PermutationsChoose("ABCD", 1);

            // ---------------------------
            // Problem 3 - Climbing Stairs (with memoization)
            // ---------------------------
            Console.WriteLine("\n=========== PROBLEM 3 TESTS ===========");
            Console.WriteLine(CountWaysToClimb(5));   // expected 13
            Console.WriteLine(CountWaysToClimb(20));  // expected 121415
            // Uncomment the test below after verifying memoization:
            // Console.WriteLine(CountWaysToClimb(100));

            // ---------------------------
            // Problem 4 - Wildcard Binary Patterns
            // ---------------------------
            Console.WriteLine("\n=========== PROBLEM 4 TESTS ===========");
            WildcardBinary("110*0*");
            // expected (order may vary):
            // 110000
            // 110001
            // 110100
            // 110101
            WildcardBinary("***");
            // expected (order may vary):
            // 000
            // 001
            // 010
            // 011
            // 100
            // 101
            // 110
            // 111

            // ---------------------------
            // Problem 5 - Maze (recursive backtracking)
            // ---------------------------
            Console.WriteLine("\n=========== PROBLEM 5 TESTS ===========");
            Maze smallMaze = new Maze(3, 3, new[] { 1, 1, 1,
                                                     1, 0, 1,
                                                     1, 1, 2 });
            SolveMaze(smallMaze);
            // expected two solutions (order may vary):
            // <List>{(0, 0), (0, 1), (0, 2), (1, 2), (2, 2)}
            // <List>{(0, 0), (1, 0), (2, 0), (2, 1), (2, 2)}
        }

        // ---------------------------
        // Problem 1 Implementation
        // ---------------------------
        public static int SumSquaresRecursive(int n)
        {
            if (n <= 0)
                return 0;
            return n * n + SumSquaresRecursive(n - 1);
        }

        // ---------------------------
        // Problem 2 Implementation
        // ---------------------------
        public static void PermutationsChoose(string letters, int size, string word = "")
        {
            if (word.Length == size)
            {
                Console.WriteLine(word);
                return;
            }
            foreach (char c in letters)
            {
                if (!word.Contains(c))
                    PermutationsChoose(letters, size, word + c);
            }
        }

        // ---------------------------
        // Problem 3 Implementation (with memoization)
        // ---------------------------
        public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
        {
            if (remember == null)
                remember = new Dictionary<int, decimal>();

            if (remember.ContainsKey(s))
                return remember[s];

            // Base cases:
            if (s == 0)
                return 0;
            if (s == 1)
                return 1;
            if (s == 2)
                return 2;
            if (s == 3)
                return 4;

            decimal ways = CountWaysToClimb(s - 1, remember) +
                           CountWaysToClimb(s - 2, remember) +
                           CountWaysToClimb(s - 3, remember);
            remember[s] = ways;
            return ways;
        }

        // ---------------------------
        // Problem 4 Implementation
        // ---------------------------
        public static void WildcardBinary(string pattern)
        {
            int index = pattern.IndexOf('*');
            if (index == -1)
            {
                Console.WriteLine(pattern);
                return;
            }
            // replacefirst occurrence of '*' with '0' and '1' and recurse
            WildcardBinary(pattern.Substring(0, index) + "0" + pattern.Substring(index + 1));
            WildcardBinary(pattern.Substring(0, index) + "1" + pattern.Substring(index + 1));
        }

        // ---------------------------
        // Problem 5 Implementation
        // ---------------------------
        public static void SolveMaze(Maze maze, int x = 0, int y = 0, List<(int, int)>? currPath = null)
        {
            // initializes current path on the first call
            if (currPath == null)
                currPath = new List<(int, int)>();

            // Checks for out-of-bounds
            if (x < 0 || x >= maze.Width || y < 0 || y >= maze.Height)
                return;

            // compute index into maze grid (assumed row-major order)
            int index = y * maze.Width + x;
            int cell = maze.Grid[index];

            // If the cell is blocked (0), return
            if (cell == 0)
                return;

            // if already visited cell, avoid cycles
            if (currPath.Contains((x, y)))
                return;

            // Add current cell to path.
            currPath.Add((x, y));

            // If this cell is the destination (2), print the path
            if (cell == 2)
            {
                Console.WriteLine("<List>{" + string.Join(", ", currPath.Select(p => $"({p.Item1}, {p.Item2})")) + "}");
                currPath.RemoveAt(currPath.Count - 1);
                return;
            }

            // Explore the adjacent (up, right, down, left).
            SolveMaze(maze, x, y - 1, currPath); // up
            SolveMaze(maze, x + 1, y, currPath); // right
            SolveMaze(maze, x, y + 1, currPath); // down
            SolveMaze(maze, x - 1, y, currPath); // left

            // backtracking
            currPath.RemoveAt(currPath.Count - 1);
        }
    }

    /// <summary>
    ///simple Maze class for testing SolveMaze.
    /// </summary>
    public class Maze
    {
        public int Width { get; }
        public int Height { get; }
        public int[] Grid { get; }

        public Maze(int width, int height, int[] grid)
        {
            if (grid.Length != width * height)
                throw new ArgumentException("the grid length doesn't match dimensions");
            Width = width;
            Height = height;
            Grid = grid;
        }
    }

    /// <summary>
    /// Program class with Main method to serve as entry point
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            RecursionTester.Run();
        }
    }
}
