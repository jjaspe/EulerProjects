using System;
using System.Collections.Generic;
using System.Linq;

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
        public List<(int,int)> Corners { get; set; }
        public Piece Piece { get; set; }
    }

    public class Piece
    {
        public List<RotatedPiece> rotatedPieces { get; set; }
        public string Name { get; set; }
    }

    public class Runner
    {
        public RotatedPiece finalPiece { get; set; }
        public int top { get; set; } = 0;
        public int right { get; set; } = 0;
        List<Piece> fullList { get; set; }

        public void FindSolution(List<RotatedPiece> pieces)
        {
            var borders = Borders(finalPiece);

        }

        public List<RotatedPiece> RecursiveFunction(List<RotatedPiece> usedList, int index)
        {
            var solution = default(List<RotatedPiece>);
            if(index > fullList.Count)
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
                            usedList.Add(moved);
                            RecursiveFunction(usedList, index + 1);
                            if (solution != default(List<RotatedPiece>))
                                return solution;
                            usedList.re();
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

        public List<(int,int)> GetAllCorners(List<RotatedPiece> pieces)
        {
            List<(int, int)> corners = new List<(int, int)>();
            foreach (var piece in pieces)
            {
                corners = AddCorners(corners, piece);
            }
            return corners;
        }
        public List<(int,int)> AddCorners(List<(int,int)> currentCorners, RotatedPiece newPiece)
        {
            newPiece.Corners.ForEach(n => currentCorners.Add(n));
            return currentCorners;
        }

        public bool IsSolution(List<(int,int)> corners, RotatedPiece finalPiece)
        {
            foreach(var corner in finalPiece.Corners)
            {
                var inList = corners.Find(n => n.Item1 == corner.Item1 && n.Item2 == corner.Item2);
                if(inList == default((int,int)))
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
                    right = corner.Item1;
                }
                if (corner.Item2 > top)
                {
                    top = corner.Item2;
                }
            }

            return (top, right);
        }
    }
}
