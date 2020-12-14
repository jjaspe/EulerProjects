using Problems.Problem_69;
using System;
using System.Collections.Generic;
using System.Text;
using Topgram;
using Xunit;

namespace Tests.TopgramTests
{
    public class TopgramTests
    {
        TopgramSolver solver;

        public TopgramTests()
        {
            solver = new TopgramSolver();
        }

        [Fact]
        public void SolvesSquareWithSquare()
        {
            var finalPiece = new RotatedPiece()
            {
                Corners = new List<(int?, int?)>()
                {
                    (0,0), (0,2), (2,0), (2,2)
                }
            };
            var fullList = new List<Piece>()
            {
                new Piece()
                {
                    rotatedPieces = new List<RotatedPiece>()
                    {
                        new RotatedPiece()
                        {
                            Corners = new List<(int?, int?)>()
                            {
                                (0,0), (0,2), (2,0), (2,2)
                            }
                        }
                    }
                }
            };

            var solution = solver.Solve(fullList, finalPiece);

            var visual = solver.Visualize(solution);

            Assert.NotNull(solution);
        }

        Piece BuildIsoscelesTriangle(int side, string name)
        {
            return new Piece()
            {
                Name = name,
                rotatedPieces = new List<RotatedPiece>()
                {
                    new RotatedPiece()
                    {
                        Corners = new List<(int?, int?)>()
                        {
                            (0,0), (0,side), (side,0)
                        }
                    },
                    new RotatedPiece()
                    {
                        Corners = new List<(int?, int?)>()
                        {
                            (0,0), (side,0), (0,-side)
                        }
                    },
                    new RotatedPiece()
                    {
                        Corners = new List<(int?, int?)>()
                        {
                            (0,0), (0,-side), (-side,0)
                        }
                    },
                    new RotatedPiece()
                    {
                        Corners = new List<(int?, int?)>()
                        {
                            (0,0), (-side,0), (0,side)
                        }
                    }
                }
            };
        }

        [Fact]
        public void SolvesSquareWithTrianglesOneRotation()
        {
            var finalPiece = new RotatedPiece()
            {
                Corners = new List<(int?, int?)>()
                {
                    (0,0), (0,1), (1,0), (1,1)
                }
            };
            var fullList = new List<Piece>()
            {
                new Piece()
                {
                    Name = "Triangle 1",
                    rotatedPieces = new List<RotatedPiece>()
                    {
                        new RotatedPiece()
                        {
                            Corners = new List<(int?, int?)>()
                            {
                                (0,0), (0,1), (1,0)
                            }
                        }
                    }
                },
                new Piece()
                {
                    Name = "Triangle 2",
                    rotatedPieces = new List<RotatedPiece>()
                    {
                        new RotatedPiece()
                        {
                            Corners = new List<(int?, int?)>()
                            {
                                (0,0), (0,-1), (-1,0)
                            }
                        }
                    }
                }
            };

            var solution = solver.Solve(fullList, finalPiece);

            Assert.NotNull(solution);
        }

        [Fact]
        public void SolvesSquareWithTriangles()
        {
            var finalPiece = new RotatedPiece()
            {
                Corners = new List<(int?, int?)>()
                {
                    (0,0), (0,1), (1,0), (1,1)
                }
            };
            var fullList = new List<Piece>()
            {
                new Piece()
                {
                    Name = "Triangle 1",
                    rotatedPieces = new List<RotatedPiece>()
                    {
                        new RotatedPiece()
                        {
                            Corners = new List<(int?, int?)>()
                            {
                                (0,0), (0,1), (1,0)
                            }
                        },
                        new RotatedPiece()
                        {
                            Corners = new List<(int?, int?)>()
                            {
                                (0,0), (1,0), (0,-1)
                            }
                        },
                        new RotatedPiece()
                        {
                            Corners = new List<(int?, int?)>()
                            {
                                (0,0), (0,-1), (-1,0)
                            }
                        },
                        new RotatedPiece()
                        {
                            Corners = new List<(int?, int?)>()
                            {
                                (0,0), (-1,0), (0,1)
                            }
                        }
                    }
                },
                new Piece()
                {
                    Name = "Triangle 2",
                    rotatedPieces = new List<RotatedPiece>()
                    {
                        new RotatedPiece()
                        {
                            Corners = new List<(int?, int?)>()
                            {
                                (0,0), (0,1), (1,0)
                            }
                        },
                        new RotatedPiece()
                        {
                            Corners = new List<(int?, int?)>()
                            {
                                (0,0), (1,0), (0,-1)
                            }
                        },
                        new RotatedPiece()
                        {
                            Corners = new List<(int?, int?)>()
                            {
                                (0,0), (0,-1), (-1,0)
                            }
                        },
                        new RotatedPiece()
                        {
                            Corners = new List<(int?, int?)>()
                            {
                                (0,0), (-1,0), (0,1)
                            }
                        }
                    }
                }
            };

            var solution = solver.Solve(fullList, finalPiece);

            Assert.NotNull(solution);
        }

        [Fact]
        public void SolvesTwoSquareWithTrianglesOneRotation()
        {
            var finalPiece = new RotatedPiece()
            {
                Corners = new List<(int?, int?)>()
                {
                    (0,0), (0,2), (1,2), (1,3), (2,3),(2,0)
                }
            };
            var fullList = new List<Piece>()
            {
                new Piece()
                {
                    Name = "Triangle 1",
                    rotatedPieces = new List<RotatedPiece>()
                    {
                        new RotatedPiece()
                        {
                            Corners = new List<(int?, int?)>()
                            {
                                (0,0), (0,1), (1,0)
                            }
                        }
                    }
                },
                new Piece()
                {
                    Name = "Triangle 2",
                    rotatedPieces = new List<RotatedPiece>()
                    {
                        new RotatedPiece()
                        {
                            Corners = new List<(int?, int?)>()
                            {
                                (0,0), (0,-1), (-1,0)
                            }
                        }
                    }
                },
                new Piece()
                {
                    Name = "Triangle 1",
                    rotatedPieces = new List<RotatedPiece>()
                    {
                        new RotatedPiece()
                        {
                            Corners = new List<(int?, int?)>()
                            {
                                (0,0), (0,2), (2,0)
                            }
                        }
                    }
                },
                new Piece()
                {
                    Name = "Triangle 2",
                    rotatedPieces = new List<RotatedPiece>()
                    {
                        new RotatedPiece()
                        {
                            Corners = new List<(int?, int?)>()
                            {
                                (0,0), (0,-2), (-2,0)
                            }
                        }
                    }
                }
            };

            var solution = solver.Solve(fullList, finalPiece);

            Assert.NotNull(solution);
        }

        [Fact]
        public void SolvesTwoSquareWithTriangles()
        {
            var finalPiece = new RotatedPiece()
            {
                Corners = new List<(int?, int?)>()
                {
                    (0,0), (0,2), (1,2), (1,3), (2,3),(2,0)
                }
            };
            var fullList = new List<Piece>()
            {
                BuildIsoscelesTriangle(1,"Small Triangle 1"),
                BuildIsoscelesTriangle(1,"Small Triangle 2"),
                BuildIsoscelesTriangle(2,"Big Triangle 2"),
                BuildIsoscelesTriangle(2,"Big Triangle 1")
            };

            var solution = solver.Solve(fullList, finalPiece);

            Assert.NotNull(solution);
        }

        [Fact]
        public void TestIntersect()
        {
            var p1 = new RotatedPiece()
            {
                Corners = new List<(int?, int?)>()
                {
                    (0,1),(1,1),(0,0)
                }
            };
            var p2 = new RotatedPiece()
            {
                Corners = new List<(int?, int?)>()
                {
                    (0,0),(0,1),(1,0)
                }
            };

            var result = solver.DoPartsIntersect(p1, p2);

            Assert.True(result);
        }
    }
}
