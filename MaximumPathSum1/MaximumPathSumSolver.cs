using EulerMisc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximumPathSum1
{
    public class Spot
    {
        public int i, j;
        public Spot(int I, int J)
        {
            i = I;
            j = J;
        }

        public bool isEqual(Spot spot)
        {
            return i == spot.i && j == spot.j;
        }
    }

    public class Path
    {
        //Left if true, right is false
        public LinkedList<bool> paths = new LinkedList<bool>();
        public int sum = 0;
        public Spot first, last;

        public Path(int startI, int startJ)
        {
            first = new Spot(startI, startJ);
            last = first;
        }

        public Path(Spot spot)
        {
            first = spot;
            last = spot;
        }

        /// <summary>
        /// Moves path to left, new last is spot
        /// </summary>
        /// <param name="spot"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Path addLeft(Spot spot, int value)
        {
            paths.AddLast(true);
            sum += value;
            last = spot;
            return this;
        }
        /// <summary>
        /// Moves path to right, new last is spot
        /// </summary>
        /// <param name="spot"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Path addRight(Spot spot, int value)
        {
            paths.AddLast(false);
            sum += value;
            last = spot;
            return this;
        }
        
    }


    public class MaximumPathSumSolver
    {
        
        #region STRING
        public string[] rows ={
                        "75",
                        "95 64",
                        "17 47 82",
                        "18 35 87 10",
                        "20 04 82 47 65",
                        "19 01 23 75 03 34",
                        "88 02 77 73 07 63 67",
                        "99 65 04 28 06 16 70 92",
                        "41 41 26 56 83 40 80 70 33",
                        "41 48 72 33 47 32 37 16 94 29",
                        "53 71 44 65 25 43 91 52 97 51 14",
                        "70 11 33 28 77 73 17 78 39 68 17 57",
                        "91 71 52 38 17 14 91 43 58 50 27 29 48",
                        "63 66 04 68 89 53 67 30 73 16 69 87 40 31",
                        "04 62 98 27 23 09 70 98 73 93 38 53 60 04 23"
                        };
        #endregion

        int[][] grid;

        /// <summary>
        /// Translates strings to int grid. Parses the strings first
        /// </summary>
        /// <param name="rows"></param>
        /// <returns></returns>
        public int[][] toIntGrid(string[] rows)
        {
            string[][] parsedStrings = Util.parseStrings(rows,' ');
            int[][] ints = new int[parsedStrings.Length][];
            for (int i = 0; i < parsedStrings.Length; i++)
            {
                ints[i] = new int[parsedStrings[i].Length];
                for (int j = 0; j < parsedStrings[i].Length; j++)
                {
                    ints[i][j] = Int32.Parse(parsedStrings[i][j]);
                }
            }
            return ints;
        }

        public void init(string[] Rows=null)
        {
            if (Rows == null)
                Rows = rows;
            grid = toIntGrid(Rows);
        }

        /// <summary>
        /// Get spot to the left. Does not check if it's the last row
        /// </summary>
        /// <param name="spot"></param>
        /// <returns></returns>
        public Spot getLeft(Spot spot)
        {
            //Spot to the left is at the same index on the next row
            return new Spot(spot.i + 1,spot.j);
        }

        public Spot getRight(Spot spot)
        {
            //Spot to the right is at next index on next row
            return new Spot(spot.i+1,spot.j+1);
        }

        public Path newPath(Spot spot)
        {
            Path path = new Path(spot);
            path.sum += grid[spot.i][spot.j];
            return path;
        }

        public Path addLeft(Path path)
        {
            Spot left=getLeft(path.last);
            path.addLeft(left, grid[left.i][left.j]);
            return path;
        }
        public Path addRight(Path path)
        {                     
            Spot right = getRight(path.last);
            path.addRight(right, grid[right.i][right.j]);
            return path;
        }
        public Path copy(Path path)
        {
            Path nPath = newPath(path.first);

            LinkedList<bool>.Enumerator en = path.paths.GetEnumerator();
            
            while (en.MoveNext())
            {
                nPath = en.Current ? addLeft(nPath) : addRight(nPath);
            } 
            return nPath;
        }

        /// <summary>
        /// Creates new path appending next to first. Doesn't change first or next
        /// </summary>
        /// <param name="first"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public Path extendPath(Path first, Path next)
        {
            LinkedList<bool>.Enumerator en = first.paths.GetEnumerator();
            Path path=newPath(first.first);

            en.MoveNext();
            do
            {
                path = en.Current ? addLeft(path) : addRight(path);
            } while (en.MoveNext());

            en=next.paths.GetEnumerator();

            if (path.last.isEqual(next.first))
            {
                en.MoveNext();
                do
                {
                    path = en.Current ? addLeft(path) : addRight(path);
                } while (en.MoveNext());
            }

            return path;
        }

        /// <summary>
        /// Get best path array for 3 rows below i,j,
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public Path[] nextThree(int i,int j)
        {
            Spot start = new Spot(i, j);
            Path current = newPath(start);
            Path[] paths = new Path[3];

            //Straight left
            paths[0] = addLeft(addLeft(newPath(start)));
            //Straight righ
            paths[2] = addRight(addRight(newPath(start)));

            //Check next row, get highest, use that to make middle path
            if (grid[i + 1][j] > grid[i + 1][j + 1])//left highest, so go left then right
                paths[1] = addRight(addLeft(newPath(start)));
            else
                paths[1] = addLeft(addRight(newPath(start)));

            return paths;
        }

        public Path[] nextThree(Spot spot)
        {
            return nextThree(spot.i, spot.j);
        }

        public Path[] nextTwo(Spot spot)
        {
            int i = spot.i, j = spot.j;
            Path[] paths = new Path[2];

            paths[0] = addLeft(newPath(spot));
            paths[1] = addRight(newPath(spot));

            return paths;
        }

        public Path[] solve(Spot spot)
        {
            Path[] paths = new Path[grid.Length - spot.i];
            paths[0] = newPath(spot);
            for (int i = spot.i; i < grid.Length-1; i++)
                paths = getNextRow(paths);
            return paths;            
        }

        public Path[] getNextRow(Path[] previousRow)
        {
            int i = 0;

            Path current = copy(previousRow[0]),temp;
            //Do leftmost
            addLeft(previousRow[0]);
            
            //Do middle ones
            while (previousRow[i+1] != null)
            {
                //check for right one
                if (current.sum > previousRow[i + 1].sum)
                {
                    temp = copy(previousRow[i+1]);
                    previousRow[i + 1] = addRight(current);
                    current = temp;
                }
                else
                {
                    current = copy(previousRow[i + 1]);
                    previousRow[i + 1] = addLeft(previousRow[i + 1]);
                }
                    
                i++;
            }

            //Do rightmost
            previousRow[++i] = addRight(current);

            return previousRow;
        }

        //Do top five
        public Path[] topFive(int i,int j)
        {
            Path[] paths = new Path[5];
            Path[] topThree=nextThree(i,j);

            //Get three from each middle leaf
            Path[] left = nextThree(topThree[0].last),
                middle=nextThree(topThree[1].last),
                right=nextThree(topThree[2].last);

            //i=0
            paths[0] = extendPath(topThree[0], left[0]);

            //i==1 Check second from left with first from middle
            if (topThree[0].sum + left[1].sum > topThree[1].sum + middle[0].sum)
                paths[1] = extendPath(topThree[0], left[1]);
            else
                paths[1] = extendPath(topThree[1], middle[0]);


            return paths;

        }

        public Path maxPath(Path[] paths)
        {
            Path max = paths[0];
            foreach (Path p in paths)
                max = max.sum > p.sum ? max : p;
            return max;
        }
        
    }
}
