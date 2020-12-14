using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

/// <summary>
/* 1. Create piece
1.List of corners
2. Create final piece
3. Create list of pieces
4. Push-pop recursion
    1. Before start,
        1.Create running piece, using corners.
        2.Get 4 side limits.
    2. Push piece into running piece
        1. Put piece in one rotation in one corner
        2. Check running piece agains final piece by comparing corners.
        3. If not solution, pop rotation, go to next
</summary>
*/

namespace Topgram
{
    public class RotatedPiece
    {
        public List<(int?, int?)> Corners { get; set; }
        public Piece Piece { get; set; }
    }

    public class Piece
    {
        public List<RotatedPiece> rotatedPieces { get; set; }
        public string Name { get; set; }
    }

    public class TopgramSolver
    {
        public RotatedPiece finalPiece { get; set; }
        public int top { get; set; } = 0;
        public int right { get; set; } = 0;
        public List<Piece> fullList { get; set; }

        public Stack<RotatedPiece> Solve(List<Piece> pieces, RotatedPiece finalPiece)
        {
            Setup(pieces, finalPiece);
            return RecursiveFunction(new Stack<RotatedPiece>(), 0);
        }

        public void Setup(List<Piece> pieces, RotatedPiece finalPiece)
        {
            var borders = Borders(finalPiece);
            this.top = borders.top;
            this.right = borders.right;
            this.finalPiece = finalPiece;
            this.fullList = pieces;
        }

        public Stack<RotatedPiece> RecursiveFunction(Stack<RotatedPiece> usedList, int index)
        {
            var solution = default(Stack<RotatedPiece>);
            if(index == fullList.Count)
            {
                if(IsSolution(GetAllCorners(usedList), finalPiece))
                    return usedList;
            }
            else
            {
                for (int i = 0; i <= right; i++)
                {
                    for (int j = 0; j <= top; j++)
                    {
                        foreach (var rotatedPiece in fullList[index].rotatedPieces)
                        {
                            var moved = MoveRotatedPiece(rotatedPiece, i, j);
                            if (!PartIntersects(usedList, moved))
                            {
                                usedList.Push(moved);
                                solution = RecursiveFunction(usedList, index + 1);
                                if (solution != default(Stack<RotatedPiece>))
                                    return solution;
                                usedList.Pop();
                            }                                
                        }
                    }
                }
            }
            return solution;
        }

        public bool InBounds(RotatedPiece piece)
        {
            foreach(var corner in piece.Corners)
            {
                if (corner.Item1 > right || corner.Item2 > top)
                    return false;
            }
            return true;
        }

        public RotatedPiece MoveRotatedPiece(RotatedPiece piece, int x, int y)
        {
            RotatedPiece moved = new RotatedPiece()
            {
                Piece = piece.Piece,
                Corners = piece.Corners.Select(n => (n.Item1 + x, n.Item2 + y)).ToList()
            };
            return moved;
        }

        public List<(int?, int?)> GetAllCorners(Stack<RotatedPiece> pieces)
        {
            List<(int?, int?)> corners = new List<(int?, int?)>();
            foreach (var piece in pieces)
            {
                corners = AddCorners(corners, piece);
            }
            return corners;
        }
        public List<(int?, int?)> AddCorners(List<(int?, int?)> currentCorners, RotatedPiece newPiece)
        {
            newPiece.Corners.ForEach(n => currentCorners.Add(n));
            return currentCorners;
        }

        public bool IsSolution(List<(int?, int?)> corners, RotatedPiece finalPiece)
        {
            foreach(var corner in finalPiece.Corners)
            {
                var inList = corners.Find(n => n.Item1 == corner.Item1 && n.Item2 == corner.Item2);
                if(inList == default((int?,int?)))
                {
                    return false;
                }
            }
            foreach (var corner in corners)
            {
                var inList = finalPiece.Corners.Find(n => n.Item1 == corner.Item1 && n.Item2 == corner.Item2);
                if (inList == default((int?, int?)))
                {
                    return false;
                }
            }
            return true;
        }

        public (int top, int right) Borders(RotatedPiece finalPiece)
        {
            for (int i = 0; i < finalPiece.Corners.Count; i++)
            {
                var corner = finalPiece.Corners[i];
                if(corner.Item1 > right)
                {
                    right = corner.Item1.Value;
                }
                if (corner.Item2 > top)
                {
                    top = corner.Item2.Value;
                }
            }

            return (top, right);
        }

        public bool PartIntersects(Stack<RotatedPiece> pieces, RotatedPiece newPiece)
        {
            foreach(var piece in pieces)
            {
                if (DoPartsIntersect(piece, newPiece))
                    return true;
            }
            return false;
        }

        public bool DoPartsIntersect(RotatedPiece p1, RotatedPiece p2)
        {
            for (int i = 0; i < p1.Corners.Count; i++)
            {
                for (int j = 0; j < p2.Corners.Count; j++)
                {
                    var c11 = p1.Corners[i];
                    var c12 = p1.Corners[(i + 1)%p1.Corners.Count];
                    var d11 = p2.Corners[j];
                    var d12 = p2.Corners[(j + 1)%p2.Corners.Count];
                    if (DoLinesIntercept(c11, c12, d11, d12))
                        return true;
                }
            }
            return false;
        }

        public bool DoLinesIntercept( (int?, int?) p1, (int?, int?) p2, (int?, int?) q1, (int?, int?) q2)
        {
            int left = Math.Min(p1.Item1.Value, Math.Min(p2.Item1.Value, Math.Min(q1.Item1.Value, q2.Item1.Value)));
            int right = Math.Max(p1.Item1.Value, Math.Max(p2.Item1.Value, Math.Max(q1.Item1.Value, q2.Item1.Value)));
            int bottom = Math.Min(p1.Item2.Value, Math.Min(p2.Item2.Value, Math.Min(q1.Item2.Value, q2.Item2.Value)));
            int top = Math.Max(p1.Item2.Value, Math.Max(p2.Item2.Value, Math.Max(q1.Item2.Value, q2.Item2.Value)));

            var verCheck1 = CheckVerticalLineAgainstAnother(p1, p2, q1, q2);
            if (verCheck1.Item1)
                return verCheck1.Item2;
            var verCheck2 = CheckVerticalLineAgainstAnother(q1, q2, p1, p2);
            if (verCheck2.Item1)
                return verCheck2.Item2;

            var slopeP = (double)(p2.Item2.Value - p1.Item2.Value) / (p2.Item1.Value - p1.Item1.Value);
            var slopeQ = (double)(q2.Item2.Value - q1.Item2.Value) / (q2.Item1.Value - q1.Item1.Value);
            if (slopeP == slopeQ)
                return false;
            double x = (q1.Item2.Value - p1.Item2.Value - slopeQ*q1.Item1.Value + slopeP*p1.Item1.Value) / (slopeP - slopeQ);
            double y = slopeP*(x - p1.Item1.Value) + p1.Item2.Value;
            return (x > left && x < right && y > bottom && y < top);
        }

        public (bool,bool) CheckVerticalLineAgainstAnother((int?, int?) p1, (int?, int?) p2, (int?, int?) q1, (int?, int?) q2)
        {
            int left = Math.Min(p1.Item1.Value, Math.Min(p2.Item1.Value, Math.Min(q1.Item1.Value, q2.Item1.Value)));
            int right = Math.Max(p1.Item1.Value, Math.Max(p2.Item1.Value, Math.Max(q1.Item1.Value, q2.Item1.Value)));
            int bottom = Math.Min(p1.Item2.Value, Math.Min(p2.Item2.Value, Math.Min(q1.Item2.Value, q2.Item2.Value)));
            int top = Math.Max(p1.Item2.Value, Math.Max(p2.Item2.Value, Math.Max(q1.Item2.Value, q2.Item2.Value)));

            if (p2.Item1.Value == p1.Item1.Value)
            {
                if (q2.Item1.Value == q1.Item1.Value)
                {
                    return (true,false);
                }
                else
                {
                    var slopeQ1 = (q2.Item2.Value - q1.Item2.Value) / (q2.Item1.Value - q1.Item1.Value);
                    var y1 = slopeQ1 * (p2.Item1.Value - q1.Item1.Value) + q1.Item2.Value;
                    return (true, y1 < bottom || y1 > top);
                }
            }
            return (false,false);
        }

        public string Visualize(Stack<RotatedPiece> rotatedPieces)
        {
            var layers = rotatedPieces.Select(n => Visualize(n)).ToList();
            char[][] final = OverlayLayers(layers);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = top - 1; i >= 0; i--)
            {
                stringBuilder.AppendLine(new string(final[i]));
            }
            File.WriteAllText("test", stringBuilder.ToString());
            return stringBuilder.ToString();
        }

        public char[][] Visualize(RotatedPiece piece)
        {
            var layers = new List<char[][]>();
            for(int i = 0; i < piece.Corners.Count; i++)
            {
                var c1 = piece.Corners[i];
                var c2 = piece.Corners[(i + 1) % piece.Corners.Count];
                var layer = Visualize(c1, c2);
                layers.Add(layer);
            }
            return OverlayLayers(layers);
        }

        public char[][] Visualize((int?,int?) c1, (int?,int?) c2)
        {
            var grid = new char[(int)right + 1][];
            for (int i = 0; i < right + 1; i++)
            {
                grid[i] = new char[top + 1];
                for (int j = 0; j < top + 1; j++)
                {
                    grid[i][j] = ' ';
                }
            }
            var charToUse = ' ';
            if (c1.Item1.Value == c2.Item1.Value)
            {
                charToUse = '|';
                var low = Math.Min(c1.Item2.Value, c2.Item2.Value);
                var high = Math.Max(c1.Item2.Value, c2.Item2.Value);
                for (var i = low; i <= high; i++)
                {
                    grid[c1.Item1.Value][i] = charToUse;
                }
            }
            else if (c1.Item2.Value == c2.Item2.Value)
            {
                charToUse = '-';
                var low = Math.Min(c1.Item1.Value, c2.Item1.Value);
                var high = Math.Max(c1.Item1.Value, c2.Item1.Value);
                for (var i = low; i <= high; i++)
                {
                    grid[i][(int)c1.Item1.Value] = charToUse;
                }
            }
            else
            {
                int slope = (c2.Item2.Value - c1.Item2.Value) / (c2.Item1.Value - c1.Item1.Value);
                if (slope > 0)
                {
                    charToUse = '7';
                    var lowY = Math.Min(c1.Item2.Value, c2.Item2.Value);
                    var highY = Math.Max(c1.Item2.Value, c2.Item2.Value);
                    var lowX = Math.Min(c1.Item1.Value, c2.Item1.Value);
                    var highX = Math.Max(c1.Item1.Value, c2.Item1.Value);
                    for (var i = lowY; i <= highY; i++)
                    {
                        grid[(int)lowX+(i-lowY)][i] = charToUse;
                    }
                }
                else
                {
                    charToUse = 'Y'; 
                    var lowY = Math.Min(c1.Item2.Value, c2.Item2.Value);
                    var highY = Math.Max(c1.Item2.Value, c2.Item2.Value);
                    var lowX = Math.Min(c1.Item1.Value, c2.Item1.Value);
                    var highX = Math.Max(c1.Item1.Value, c2.Item1.Value);
                    for (var i = lowY; i <= highY; i++)
                    {
                        grid[(int)highX - (i - lowY)][i] = charToUse;
                    }
                }
            }

            return grid;
        }

        public char[][] OverlayLayers(List<char[][]> layers)
        {
            char[][] final = new char[(int)right + 1][];
            for (int i = 0; i < right + 1; i++)
            {
                final[i] = new char[top + 1];
            }
            foreach (var layer in layers)
            {
                for (int i = 0; i < (int)right + 1; i++)
                {
                    for (int j = 0; j < (int)top + 1; j++)
                    {
                        if(final[i][j] == ' ' || final[i][j] == default(char))
                        {
                            final[i][j] = layer[i][j];
                        }
                    }
                }
            }
            return final;            
        }
    }
}
