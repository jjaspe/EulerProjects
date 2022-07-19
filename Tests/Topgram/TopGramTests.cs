using Problems.Problem_69;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Topgram;
using Xunit;

namespace Tests.TopgramTests
{
    public class TopgramTests
    {
        TopgramSolver solver;
        PieceBuilder builder;

        public TopgramTests()
        {
            solver = new TopgramSolver();
            solver.recordHistory = true;
            builder = new PieceBuilder();
        }

        void Save()
        {
            solver.Save(solver.history, "C:\\Users\\Juan Jaspe\\Documents\\Projects\\EulerProjects\\2DSandbox\\bin\\x86\\Debug\\AppX\\Topgram_Pieces.txt");
        }

        void SaveSolution(Stack<RotatedPiece> solution)
        {
            var History = new History()
            {
                Steps = new List<List<RotatedPiece>>()
                {
                    solution.ToList()
                }
            };
            solver.Save(History, "C:\\Users\\Juan Jaspe\\Documents\\Projects\\EulerProjects\\2DSandbox\\bin\\x86\\Debug\\AppX\\Topgram_Solution.txt");
        }

        [Fact]
        public void SolvesSquareWithSquare()
        {
            var finalPiece = new FinalPiece()
            {
                Corners = new List<Vector2>()
                {
                    new Vector2(0,0),
                    new Vector2(0,2),
                    new Vector2(2,2),
                    new Vector2(2,0)
                },
                Triangles = new List<RotatedPiece>()
                {
                    new RotatedPiece()
                    {
                        Corners = new List<Vector2>()
                        {
                            new Vector2(0,0),
                            new Vector2(0,2),
                            new Vector2(2,2),
                            new Vector2(2,0)
                        }
                    },
                    new RotatedPiece()
                    {
                        Corners = new List<Vector2>()
                        {
                            new Vector2(0,0),
                            new Vector2(2,2),
                            new Vector2(2,0)
                        }
                    }
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
                            Name = "Square",
                            Corners = new List<Vector2>()
                            {
                                new Vector2(0,0),
                                new Vector2(0,2),
                                new Vector2(2,2),
                                new Vector2(2,0)
                            }
                        }
                    }
                }
            };

            var solution = solver.Solve(fullList, finalPiece);

            Assert.NotNull(solution);
        }

        [Fact]
        public void SolvesSquareWithTrianglesOneRotation()
        {
            var finalPiece = new FinalPiece()
            {
                Corners = new List<Vector2>()
                {
                    new Vector2(0,0),
                    new Vector2(0,1),
                    new Vector2(1,1),
                    new Vector2(1,0)
                },
                Triangles = new List<RotatedPiece>()
                {
                    new RotatedPiece()
                    {
                        Corners = new List<Vector2>()
                        {
                            new Vector2(0,0),
                            new Vector2(0,1),
                            new Vector2(1,1)
                        }
                    },
                    new RotatedPiece()
                    {
                        Corners = new List<Vector2>()
                        {
                            new Vector2(0,0),
                            new Vector2(1,1),
                            new Vector2(1,0)
                        }
                    }
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
                            Name = "Triangle 1",
                            Corners = new List<Vector2>()
                            {
                                new Vector2(0,0),
                                new Vector2(0,1),
                                new Vector2(1,0)
                            }
                        }
                    }
                },
                new Piece()
                {
                    rotatedPieces = new List<RotatedPiece>()
                    {
                        new RotatedPiece()
                        {
                            Name = "Triangle 2",
                            Corners = new List<Vector2>()
                            {
                                new Vector2(0,1),
                                new Vector2(1,1),
                                new Vector2(1,0)
                            }
                        }
                    }
                }
            };

            solver.recordHistory = true;
            var solution = solver.Solve(fullList, finalPiece);

            Save();

            Assert.NotNull(solution);
        }

        [Fact]
        public void SolvesSquareWithTriangles()
        {
            var finalPiece = new FinalPiece()
            {
                Corners = new List<Vector2>()
                {
                    new Vector2(0,0),
                    new Vector2(0,1),
                    new Vector2(1,1),
                    new Vector2(1,0)
                },
                Triangles = new List<RotatedPiece>()
                {
                    new RotatedPiece()
                    {
                        Corners = new List<Vector2>()
                        {
                            new Vector2(0,0),
                            new Vector2(0,1),
                            new Vector2(1,1)
                        }
                    },
                    new RotatedPiece()
                    {
                        Corners = new List<Vector2>()
                        {
                            new Vector2(0,0),
                            new Vector2(1,1),
                            new Vector2(1,0)
                        }
                    }
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
                            Corners = new List<Vector2>()
                            {
                                new Vector2(0,0),
                                new Vector2(0,1),
                                new Vector2(1,0)
                            }
                        },
                        new RotatedPiece()
                        {
                            Corners = new List<Vector2>()
                            {

                                new Vector2(0,0),
                                new Vector2(1,0),
                                new Vector2(0,-1)
                            }
                        },
                        new RotatedPiece()
                        {
                            Corners = new List<Vector2>()
                            {

                                new Vector2(0,0),
                                new Vector2(0,-1),
                                new Vector2(-1,0)
                            }
                        },
                        new RotatedPiece()
                        {
                            Corners = new List<Vector2>()
                            {

                                new Vector2(0,0),
                                new Vector2(-1,0),
                                new Vector2(0,1)
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
                            Corners = new List<Vector2>()
                            {

                                new Vector2(0,0),
                                new Vector2(0,1),
                                new Vector2(1,0)
                            }
                        },
                        new RotatedPiece()
                        {
                            Corners = new List<Vector2>()
                            {

                                new Vector2(0,0),
                                new Vector2(1,0),
                                new Vector2(0,-1)
                            }
                        },
                        new RotatedPiece()
                        {
                            Corners = new List<Vector2>()
                            {

                                new Vector2(0,0),
                                new Vector2(0,-1),
                                new Vector2(-1,0)
                            }
                        },
                        new RotatedPiece()
                        {
                            Corners = new List<Vector2>()
                            {

                                new Vector2(0,0),
                                new Vector2(-1,0),
                                new Vector2(0,1)
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
            var finalPiece = new FinalPiece()
            {
                Corners = new List<Vector2>()
                {
                    new Vector2(0,0), new Vector2(0,2), new Vector2(1,2), new Vector2(1,3), new Vector2(2,3), new Vector2(2,0)
                },
                Triangles = new List<RotatedPiece>()
                {
                    new RotatedPiece()
                    {
                        Corners = new List<Vector2>()
                        {
                            new Vector2(0,0), new Vector2(0,2), new Vector2(2,2)
                        }
                    },
                    new RotatedPiece()
                    {
                        Corners = new List<Vector2>()
                        {
                            new Vector2(0,0), new Vector2(2,2), new Vector2(2,0)
                        }
                    },
                    new RotatedPiece()
                    {
                        Corners = new List<Vector2>()
                        {
                            new Vector2(1,2), new Vector2(1,3), new Vector2(2,3)
                        }
                    },
                    new RotatedPiece()
                    {
                        Corners = new List<Vector2>()
                        {
                            new Vector2(1,2), new Vector2(2,3), new Vector2(2,2)
                        }
                    }
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
                            Name = "Big Triangle 1",
                            Corners = new List<Vector2>()
                            {
                                new Vector2(0,0), new Vector2(0,2), new Vector2(2,0)
                            }
                        }
                    }
                },
                new Piece()
                {
                    rotatedPieces = new List<RotatedPiece>()
                    {
                        new RotatedPiece()
                        {
                            Name = "Big Triangle 2",
                            Corners = new List<Vector2>()
                            {
                                new Vector2(0,0), new Vector2(0,-2), new Vector2(-2,0)
                            }
                        }
                    }
                },
                new Piece()
                {
                    rotatedPieces = new List<RotatedPiece>()
                    {
                        new RotatedPiece()
                        {
                            Name = "Small Triangle 1",
                            Corners = new List<Vector2>()
                            {
                                new Vector2(0,0), new Vector2(1,0), new Vector2(0,1)
                            }
                        }
                    }
                },
                new Piece()
                {
                    rotatedPieces = new List<RotatedPiece>()
                    {
                        new RotatedPiece()
                        {
                            Name = "Small Triangle 2",
                            Corners = new List<Vector2>()
                            {
                                new Vector2(0,0), new Vector2(0,-1), new Vector2(-1,0)
                            }
                        }
                    }
                }
            };

            var solution = solver.Solve(fullList, finalPiece);

            Save();

            Assert.NotNull(solution);

            SaveSolution(solution);
        }

        [Fact]
        public void SolvesTwoSquareWithTrianglesOneRotationCorrectSpots()
        {
            var finalPiece = new FinalPiece()
            {
                Corners = new List<Vector2>()
                {
                    new Vector2(0,0), new Vector2(0,2), new Vector2(1,2), new Vector2(1,3), new Vector2(2,3), new Vector2(2,0)
                },
                Triangles = new List<RotatedPiece>()
                {
                    new RotatedPiece()
                    {
                        Corners = new List<Vector2>()
                        {
                            new Vector2(0,0), new Vector2(0,2), new Vector2(2,2)
                        }
                    },
                    new RotatedPiece()
                    {
                        Corners = new List<Vector2>()
                        {
                            new Vector2(0,0), new Vector2(2,2), new Vector2(2,0)
                        }
                    },
                    new RotatedPiece()
                    {
                        Corners = new List<Vector2>()
                        {
                            new Vector2(1,2), new Vector2(1,3), new Vector2(2,3)
                        }
                    },
                    new RotatedPiece()
                    {
                        Corners = new List<Vector2>()
                        {
                            new Vector2(1,2), new Vector2(2,3), new Vector2(2,2)
                        }
                    }
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
                            Name = "Big Triangle 1",
                            Corners = new List<Vector2>()
                            {
                                new Vector2(0,0), new Vector2(0,2), new Vector2(2,0)
                            }
                        }
                    }
                },
                new Piece()
                {
                    rotatedPieces = new List<RotatedPiece>()
                    {
                        new RotatedPiece()
                        {
                            Name = "Big Triangle 2",
                            Corners = new List<Vector2>()
                            {
                                new Vector2(0,2), new Vector2(2,2), new Vector2(2,0)
                            }
                        }
                    }
                },
                new Piece()
                {
                    rotatedPieces = new List<RotatedPiece>()
                    {
                        new RotatedPiece()
                        {
                            Name = "Small Triangle 1",
                            Corners = new List<Vector2>()
                            {
                                new Vector2(1,2), new Vector2(1,3), new Vector2(2,3)
                            }
                        }
                    }
                },
                new Piece()
                {
                    rotatedPieces = new List<RotatedPiece>()
                    {
                        new RotatedPiece()
                        {
                            Name = "Small Triangle 2",
                            Corners = new List<Vector2>()
                            {
                                new Vector2(1,2), new Vector2(2,3), new Vector2(2,2)
                            }
                        }
                    }
                }
            };

            var solution = solver.Solve(fullList, finalPiece);

            Save();

            Assert.NotNull(solution);

            SaveSolution(solution);
        }

        [Fact]
        public void SolvesTwoSquareWithTriangles()
        {
            var finalPiece = new FinalPiece()
            {
                Corners = new List<Vector2>()
                {
                    new Vector2(0,0), new Vector2(0,2), new Vector2(1,2), new Vector2(1,3), new Vector2(2,3), new Vector2(2,0)
                },
                Triangles = new List<RotatedPiece>()
                {
                    new RotatedPiece()
                    {
                        Corners = new List<Vector2>()
                        {
                            new Vector2(0,0), new Vector2(0,2), new Vector2(2,2)
                        }
                    },
                    new RotatedPiece()
                    {
                        Corners = new List<Vector2>()
                        {
                            new Vector2(0,0), new Vector2(2,2), new Vector2(2,0)
                        }
                    },
                    new RotatedPiece()
                    {
                        Corners = new List<Vector2>()
                        {
                            new Vector2(1,2), new Vector2(1,3), new Vector2(2,3)
                        }
                    },
                    new RotatedPiece()
                    {
                        Corners = new List<Vector2>()
                        {
                            new Vector2(1,2), new Vector2(2,3), new Vector2(2,2)
                        }
                    }
                }
            };
            var fullList = new List<Piece>()
            {
                builder.BuildIsoscelesTriangle(1,"Small Triangle 1"),
                builder.BuildIsoscelesTriangle(1,"Small Triangle 2"),
                builder.BuildIsoscelesTriangle(2,"Big Triangle 2"),
                builder.BuildIsoscelesTriangle(2,"Big Triangle 1")
            };

            solver.recordHistory = false;

            var solution = solver.Solve(fullList, finalPiece);

            Save();

            Assert.NotNull(solution);
            SaveSolution(solution);
        }

        [Fact]
        public void SolvesBoxWithTrianglesAndSquareOneRotationCorrectSpots()
        {
            var finalPiece = new FinalPiece()
            {
                Corners = new List<Vector2>()
                {
                    new Vector2(0,0), new Vector2(0,3), new Vector2(2,3), new Vector2(2,0)
                },
                Triangles = new List<RotatedPiece>()
                {
                    new RotatedPiece()
                    {
                        Corners = new List<Vector2>()
                        {
                            new Vector2(0,0), new Vector2(0,3), new Vector2(2,3)
                        }
                    },
                    new RotatedPiece()
                    {
                        Corners = new List<Vector2>()
                        {
                            new Vector2(0,0), new Vector2(2,3), new Vector2(2,0)
                        }
                    }
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
                            Name = "Big Triangle 1",
                            Corners = new List<Vector2>()
                            {
                                new Vector2(0,0), new Vector2(0,2), new Vector2(2,0)
                            }
                        }
                    }
                },
                new Piece()
                {
                    rotatedPieces = new List<RotatedPiece>()
                    {
                        new RotatedPiece()
                        {
                            Name = "Big Triangle 2",
                            Corners = new List<Vector2>()
                            {
                                new Vector2(0,2), new Vector2(2,2), new Vector2(2,0)
                            }
                        }
                    }
                },
                new Piece()
                {
                    rotatedPieces = new List<RotatedPiece>()
                    {
                        new RotatedPiece()
                        {
                            Name = "Small Triangle 1",
                            Corners = new List<Vector2>()
                            {
                                new Vector2(1,2), new Vector2(1,3), new Vector2(2,3)
                            }
                        }
                    }
                },
                new Piece()
                {
                    rotatedPieces = new List<RotatedPiece>()
                    {
                        new RotatedPiece()
                        {
                            Name = "Small Triangle 2",
                            Corners = new List<Vector2>()
                            {
                                new Vector2(1,2), new Vector2(2,3), new Vector2(2,2)
                            }
                        }
                    }
                },
                new Piece()
                {
                    Name = "Square",
                    rotatedPieces = new List<RotatedPiece>()
                    {
                        new RotatedPiece()
                        {
                            Name = "Square",
                            Corners = new List<Vector2>()
                            {
                                new Vector2(0,2),new Vector2(0,3),new Vector2(1,3),new Vector2(1,2)
                            }
                        }
                    }
                }
            };

            solver.recordHistory = false;

            var solution = solver.Solve(fullList, finalPiece);

            Save();

            Assert.NotNull(solution);
            SaveSolution(solution);
        }

        [Fact]
        public void SolvesBoxWithTrianglesAndSquare()
        {
            var finalPiece = new FinalPiece()
            {
                Corners = new List<Vector2>()
                {
                    new Vector2(0,0), new Vector2(0,3), new Vector2(2,3), new Vector2(2,0)
                },
                Triangles = new List<RotatedPiece>()
                {
                    new RotatedPiece()
                    {
                        Corners = new List<Vector2>()
                        {
                            new Vector2(0,0), new Vector2(0,3), new Vector2(2,3)
                        }
                    },
                    new RotatedPiece()
                    {
                        Corners = new List<Vector2>()
                        {
                            new Vector2(0,0), new Vector2(2,3), new Vector2(2,0)
                        }
                    }
                }
            };
            var fullList = new List<Piece>()
            {
                builder.BuildIsoscelesTriangle(1,"Small Triangle 1"),
                builder.BuildIsoscelesTriangle(1,"Small Triangle 2"),
                builder.BuildIsoscelesTriangle(2,"Big Triangle 2"),
                builder.BuildIsoscelesTriangle(2,"Big Triangle 1"),
                builder.BuildSquare(1, "Square")
            };

            solver.recordHistory = true;

            var solution = solver.Solve(fullList, finalPiece);

            Save();

            Assert.NotNull(solution);
            SaveSolution(solution);
        }

        [Fact]
        public void TestIntersect()
        {
            var p1 = new RotatedPiece()
            {
                Corners = new List<Vector2>()
                {
                    new Vector2(0,1),new Vector2(1,1),new Vector2(0,0)
                }
            };
            var p2 = new RotatedPiece()
            {
                Corners = new List<Vector2>()
                {
                    new Vector2(0,0),new Vector2(0,1),new Vector2(1,0)
                }
            };

            var result = solver.DoTrianglesOverlap(p1, p2);

            Assert.True(result);
        }

        [Fact]
        public void DoTrianglesOverlap_ReturnsTrue_WhenTrianglesOverlapCompletely()
        {
            var triangle1 = new RotatedPiece()
            {
                Name = "Big Triangle 1",
                Corners = new List<Vector2>()
                        {
                            new Vector2(0,0), new Vector2(0,2), new Vector2(2,0)
                        }
            };
            var triangle2 = new RotatedPiece()
            {
                Name = "Big Triangle 2",
                Corners = new List<Vector2>()
                    {
                        new Vector2(0,0), new Vector2(0,2), new Vector2(2,0)
                    }
            };

            var result = solver.DoTrianglesOverlap(triangle1, triangle2);

            Assert.True(result);
        }
    }
}
