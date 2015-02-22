using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test_Driven_Game_of_Life;

namespace UnitTests
{
    [TestClass]
    public sealed class EvolutionTests
    {
        [TestMethod]
        public void TestEvolutionSize()
        {
            var board = new GameBoard(100, 100);
            var nextGen = board.Evolve();
            Assert.AreEqual(board.SizeX, nextGen.SizeX);
            Assert.AreEqual(board.SizeY, nextGen.SizeY);
        }

        [TestMethod]
        public void TestAloneCellsDie()
        {
            var board = new GameBoard(100, 100);
            board[0, 0] = true;
            board[0, 4] = true;
            var nextGen = board.Evolve();
            Assert.IsFalse(nextGen[0, 0]);
            Assert.IsFalse(nextGen[0, 4]);
        }

        [TestMethod]
        public void TestCellsWithOneNeighbourDie()
        {
            var board = new GameBoard(100, 100);
            board[0, 0] = true;
            board[0, 1] = true;
            var nextGen = board.Evolve();
            Assert.IsFalse(nextGen[0, 0]);
            Assert.IsFalse(nextGen[0, 1]);
        }

        [TestMethod]
        public void TestLiveCellsWithTwoNeighboursLive()
        {
            var board = new GameBoard(100, 100);
            board[0, 0] = true;
            board[0, 1] = true;
            board[0, 2] = true;
            var nextGen = board.Evolve();
            Assert.IsTrue(nextGen[0, 1]);
        }

        [TestMethod]
        public void TestLiveCellsWithThreeNeighboursLive()
        {
            var board = new GameBoard(100, 100);
            board[0, 0] = true;
            board[0, 1] = true;
            board[1, 1] = true;
            board[0, 2] = true;
            var nextGen = board.Evolve();
            Assert.IsTrue(nextGen[0, 1]);
        }

        [TestMethod]
        public void TestLiveCellsWithFourNeighboursDie()
        {
            var board = new GameBoard(100, 100);
            board[-1, 0] = true;
            board[0, 0] = true;
            board[0, 1] = true;
            board[1, 1] = true;
            board[0, 2] = true;
            var nextGen = board.Evolve();
            Assert.IsFalse(nextGen[0, 1]);
        }

        [TestMethod]
        public void TestLiveCellsWithMaxNeighboursDie()
        {
            var board = new GameBoard(100, 100);
            board[-1, 0] = true;
            board[-1, 1] = true;
            board[-1, 2] = true;
            board[0, 0] = true;
            board[0, 1] = true;
            board[0, 2] = true;
            board[1, 0] = true;
            board[1, 1] = true;
            board[1, 2] = true;
            var nextGen = board.Evolve();
            Assert.IsFalse(nextGen[0, 1]);
        }

        [TestMethod]
        public void TestDeadCellsStayDead()
        {
            var board = new GameBoard(100, 100);
            board[-1, 0] = true;
            board[-10, 0] = true;
            var nextGen = board.Evolve();

            bool aliveCells = false;

            for (int i = -100; i <= 100; i++)
            {
                for (int j = -100; j <= 100; j++)
                {
                    if (nextGen[i, j])
                    {
                        aliveCells = true;
                        break;
                    }
                }

                if (aliveCells)
                {
                    break;
                }
            }

            Assert.IsFalse(aliveCells);
        }

        [TestMethod]
        public void TestDeadCellsWithThreeNeighboursAreResurrected()
        {
            var board = new GameBoard(100, 100);
            board[0, 0] = true;
            board[0, 1] = true;
            board[0, 2] = true;
            var nextGen = board.Evolve();
            Assert.IsTrue(nextGen[-1, 1]);
            Assert.IsTrue(nextGen[1, 1]);
        }

        [TestMethod]
        public void TestSecondGenerationIsTheSame()
        {
            var board = new GameBoard(100, 100);
            board[-1, 0] = true;
            board[-1, 1] = true;
            board[-1, 2] = true;

            var nextBoard = board.Evolve();
            nextBoard = nextBoard.Evolve();

            Assert.IsTrue(nextBoard.Equals(board));
        }
    }
}
