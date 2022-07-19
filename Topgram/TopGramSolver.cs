using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
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
    public class History
    {
        public List<List<RotatedPiece>> Steps { get; set; }
        = new List<List<RotatedPiece>>();
    }

    public class RotatedPiece
    {
        public string Name { get; set; }
        public List<Vector2> Corners { get; set; }
        public Piece Piece { get; set; }
    }

    public class Piece
    {
        public List<RotatedPiece> rotatedPieces { get; set; }
        public string Name { get; set; }
    }

    public class FinalPiece : RotatedPiece
    {
        public List<RotatedPiece> Triangles { get; set; }
         = new List<RotatedPiece>();
    }

    public class PieceBuilder
    {
        public Piece BuildIsoscelesTriangle(int side, string name)
        {
            return new Piece()
            {
                Name = name,
                rotatedPieces = new List<RotatedPiece>()
                {
                    new RotatedPiece()
                    {
                        Corners = new List<Vector2>()
                        {
                            new Vector2(0,0),
                            new Vector2(0,side),
                            new Vector2(side,0)
                        }
                    },
                    new RotatedPiece()
                    {
                        Corners = new List<Vector2>()
                        {
                            new Vector2(0,0),
                            new Vector2(side,0),
                            new Vector2(0,-side)
                        }
                    },
                    new RotatedPiece()
                    {
                        Corners = new List<Vector2>()
                        {
                            new Vector2(0,0),
                            new Vector2(0,-side),
                            new Vector2(-side,0)
                        }
                    },
                    new RotatedPiece()
                    {
                        Corners = new List<Vector2>()
                        {
                            new Vector2(0,0),
                            new Vector2(-side,0),
                            new Vector2(0,side)
                        }
                    }
                }
            };
        }

        public Piece BuildSquare(int side, string name)
        {
            return new Piece()
            {
                Name = name,
                rotatedPieces = new List<RotatedPiece>()
                {
                    new RotatedPiece()
                    {
                        Name = name,
                        Corners = new List<Vector2>()
                        {
                            new Vector2(0,0),
                            new Vector2(0,side),
                            new Vector2(side,side),
                            new Vector2(side,0)
                        }
                    }
                }
            };
        }

        public Piece BuilRombus(int side, string name)
        {
            var sideLength = (float)Math.Sqrt(3 * side / 8);
            return new Piece()
            {
                Name = name,
                rotatedPieces = new List<RotatedPiece>()
                {
                    new RotatedPiece()
                    {
                        Name = name,
                        Corners = new List<Vector2>()
                        {
                            new Vector2(0,0),
                            new Vector2(sideLength, sideLength),
                            new Vector2(sideLength + side, sideLength),
                            new Vector2(side, 0)
                        }
                    },
                    new RotatedPiece()
                    {
                        Name = name,
                        Corners = new List<Vector2>()
                        {
                            new Vector2(0, sideLength),
                            new Vector2(side, sideLength),
                            new Vector2(sideLength, 0),
                            new Vector2(side + sideLength, 0)
                        }
                    },
                    new RotatedPiece()
                    {
                        Name = name,
                        Corners = new List<Vector2>()
                        {
                            new Vector2(0,0),
                            new Vector2(0, side),
                            new Vector2(sideLength, side + sideLength),
                            new Vector2(sideLength, sideLength)
                        }
                    },
                    new RotatedPiece()
                    {
                        Name = name,
                        Corners = new List<Vector2>()
                        {
                            new Vector2(0,sideLength),
                            new Vector2(sideLength, sideLength + side),
                            new Vector2(sideLength, side),
                            new Vector2(sideLength, 0)
                        }
                    }
                }
            };
        }
    }

    public class TopgramSolver
    {
        public FinalPiece finalPiece { get; set; }
        public float top { get; set; } = 0;
        public float right { get; set; } = 0;
        public List<Piece> fullList { get; set; }
        public History history { get; set; }
        public bool recordHistory { get; set; }

        public Stack<RotatedPiece> Solve(List<Piece> pieces, FinalPiece finalPiece)
        {
            Setup(pieces, finalPiece);
            history = new History();
            return RecursiveFunction(new Stack<RotatedPiece>(), 0);
        }

        public void Setup(List<Piece> pieces, FinalPiece finalPiece)
        {
            var borders = Borders(finalPiece);
            this.top = borders.Y;
            this.right = borders.X;
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
                                UpdateHistory(history, usedList);
                                if (solution != default(Stack<RotatedPiece>))
                                {
                                    return solution;
                                }
                                usedList.Pop();
                            }    
                            else
                            {
                                UpdateHistory(history, usedList);
                            }
                        }
                    }
                }
            }
            return solution;
        }

        public bool PartIntersects(Stack<RotatedPiece> pieces, RotatedPiece newPiece)
        {
            if (newPiece.Corners.Count == 3)
            {
                foreach (var piece in pieces)
                {
                    if (DoTrianglesOverlap(piece, newPiece))
                        return true;
                }
            }
            else if (newPiece.Corners.Count == 4)
            {
                if (newPiece.Name.ToLower().Contains("square"))
                {
                    var squareTriangles = GetTrianglesFromSquare(newPiece);
                    foreach (var piece in pieces)
                    {
                        if (DoTrianglesOverlap(piece, squareTriangles[0]))
                            return true;
                        if (DoTrianglesOverlap(piece, squareTriangles[1]))
                            return true;
                    }
                }
            }
            
            return false;
        }

        public RotatedPiece MoveRotatedPiece(RotatedPiece piece, float x, float y)
        {
            RotatedPiece moved = new RotatedPiece()
            {
                Name = piece.Name,
                Piece = piece.Piece,
                Corners = piece.Corners.Select(n => new Vector2(n.X + x, n.Y + y)).ToList()
            };
            return moved;
        }

        public HashSet<Vector2> GetAllCorners(Stack<RotatedPiece> pieces)
        {
            HashSet<Vector2> corners = new HashSet<Vector2>();
            foreach (var piece in pieces)
            {
                corners = AddCorners(corners, piece);
            }
            return corners;
        }

        public HashSet<Vector2> AddCorners(HashSet<Vector2> currentCorners, RotatedPiece newPiece)
        {
            newPiece.Corners.ForEach(n => currentCorners.Add(n));
            return currentCorners;
        }

        public bool IsSolution(HashSet<Vector2> corners, FinalPiece finalPiece)
        {
            //Are all corners of final piece in built piece?
            foreach(var corner in finalPiece.Corners)
            {
                var inList = corners.Where(n => n.X == corner.X && n.Y == corner.Y)
                    .FirstOrDefault();
                if(inList == default(Vector2))
                {
                    return false;
                }
            }

            //Are all corners of built piece inside or on edges of final piece
            foreach (var corner in corners)
            {
                //First check if built corner is also final piece corner
                var inList = finalPiece.Corners
                    .Where(n => n.X == corner.X && n.Y == corner.Y)
                    .FirstOrDefault();

                //If not, check if built corner is inside final piece
                if(inList == default(Vector2))
                {
                    var inPiece = false;
                    foreach (var triangle in finalPiece.Triangles)
                    {
                        if (IsPointInTriangle(corner, triangle, true))
                        {
                            inPiece = true;
                            break;
                        }
                    }
                    if (!inPiece)
                    {
                        return false;
                    }
                }                
            }
            return true;
        }

        public bool PointInLine(Vector2 point, Vector2 linePoint1, Vector2 linePoint2)
        {
            var tolerance = 0.0001;
            var diff = SegmentLength(point, linePoint1) + SegmentLength(point, linePoint2)
                - SegmentLength(linePoint1, linePoint2);
            return diff > -tolerance && diff < tolerance;
        }

        public Vector2 Borders(RotatedPiece finalPiece)
        {
            for (int i = 0; i < finalPiece.Corners.Count; i++)
            {
                var corner = finalPiece.Corners[i];
                if(corner.X > right)
                {
                    right = corner.X;
                }
                if (corner.Y > top)
                {
                    top = corner.Y;
                }
            }

            return new Vector2(top, right);
        }        

        public void UpdateHistory(History history, Stack<RotatedPiece> newOne)
        {
            if (recordHistory)
            {
                history.Steps.Add(newOne.Select(n => new RotatedPiece()
                {
                    Corners = n.Corners,
                    Piece = n.Piece
                }).ToList());
            }            
        }

        public void Save(History history, string path)
        {
            var ser = JsonConvert.SerializeObject(history);
            File.WriteAllText(path, ser);
        }

        public List<RotatedPiece> GetTrianglesFromSquare(RotatedPiece square)
        {
            return new List<RotatedPiece>()
            {
                new RotatedPiece()
                {
                    Name = "Square Triangle 1",
                    Corners = new List<Vector2>()
                    {
                        square.Corners[0],
                        square.Corners[1],
                        square.Corners[2]
                    }
                },
                new RotatedPiece()
                {
                    Name = "Square Triangle 2",
                    Corners = new List<Vector2>()
                    {
                        square.Corners[2],
                        square.Corners[3],
                        square.Corners[0]
                    }
                }
            };
        }

        #region Helpers

        public bool DoLinesIntercept(Vector2 p1, Vector2 p2, Vector2 q1, Vector2 q2,
            bool includeEnds = false)
        {
            float x1 = p1.X,
                x2 = p2.X,
                x3 = q1.X,
                x4 = q2.X,
                y1 = p1.Y,
                y2 = p2.Y,
                y3 = q1.Y,
                y4 = q2.Y;

            var den = (y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1);
            if (den == 0)
            {
                return false;
            }
            else
            {
                var Ua = (float)((x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3)) / den;
                var Ub = (float)((x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3)) / den;
                return (Ua > 0 && Ua < 1 && Ub > 0 && Ub < 1);
                
            }
        }

        public bool DoTrianglesOverlap(RotatedPiece p1, RotatedPiece p2)
        {
            var c1 = GetTriangleIncenter(p1);
            if (IsPointInTriangle(c1, p2))
                return true;
            var c2 = GetTriangleIncenter(p2);
            if (IsPointInTriangle(c2, p1))
                return true;
            for (int i = 0; i < p1.Corners.Count; i++)
            {
                var c11 = p1.Corners[i];
                var c12 = p1.Corners[(i + 1) % p1.Corners.Count];
                if (IsPointInTriangle(c11, p2))
                    return true;
                for (int j = 0; j < p2.Corners.Count; j++)
                {
                    var d11 = p2.Corners[j];
                    var d12 = p2.Corners[(j + 1) % p2.Corners.Count];
                    if (DoLinesIntercept(c11, c12, d11, d12))
                        return true;
                    if (IsPointInTriangle(d11, p1))
                        return true;
                }
            }
            return false;
        }

        public Vector2 GetTriangleIncenter(RotatedPiece triangle)
        {
            var xa = triangle.Corners[0].X;
            var ya = triangle.Corners[0].Y;
            var xb = triangle.Corners[1].X;
            var yb = triangle.Corners[1].Y;
            var xc = triangle.Corners[2].X;
            var yc = triangle.Corners[2].Y;

            var a = SegmentLength(triangle.Corners[1], triangle.Corners[2]);
            var b = SegmentLength(triangle.Corners[0], triangle.Corners[2]);
            var c = SegmentLength(triangle.Corners[0], triangle.Corners[1]);

            var den = a + b + c;
            var x = (a * xa + b * xb + c * xc) / den;
            var y = (a * ya + b * yb + c * yc) / den;
            return new Vector2(x, y);
        }

        public float SegmentLength(Vector2 v1, Vector2 v2)
        {
            var x = (v1.X - v2.X);
            var y = (v1.Y - v2.Y);
            return (float)Math.Sqrt(x * x + y * y);
        }

        public float DotProduct(Vector2 v1, Vector2 v2)
        {
            return (v1.X * v2.X + v1.Y * v2.Y);
        }

        public bool IsPointInTriangle(Vector2 P, RotatedPiece triangle)
        {
            Vector2 c = new Vector2(P.X, P.Y);
            return IsPointInTriangle(c, triangle);
        }

        public bool IsPointInTriangle(Vector2 P, RotatedPiece triangle, bool includeEdges = false)
        {
            var A = triangle.Corners[0];
            var B = triangle.Corners[1];
            var C = triangle.Corners[2];

            var v0 = new Vector2(C.X - A.X, C.Y - A.Y);
            var v1 = new Vector2(B.X - A.X, B.Y - A.Y);
            var v2 = new Vector2(P.X - A.X, P.Y - A.Y);

            var dot00 = DotProduct(v0, v0);
            var dot01 = DotProduct(v0, v1);
            var dot02 = DotProduct(v0, v2);
            var dot11 = DotProduct(v1, v1);
            var dot12 = DotProduct(v1, v2);

            var invDenom = (1 / (float)(dot00 * dot11 - dot01 * dot01));
            float u = (dot11 * dot02 - dot01 * dot12) * invDenom;
            float v = (dot00 * dot12 - dot01 * dot02) * invDenom;

            return includeEdges ? (u >= 0 && v >= 0 && u + v <= 1) : (u > 0 && v > 0 && u + v < 1);
        }
        #endregion
    }
}
