using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test_Driven_Game_of_Life;

namespace UnitTests
{
    [TestClass]
    public class GameBoardTets
    {
        private const int SizeX = 100;
        private const int SizeY = 100;
        private readonly GameBoard _board = new GameBoard(SizeX, SizeY);

        [TestMethod]
        public void CheckDefaultState()
        {
            Assert.IsFalse(_board[0, 0]);
            Assert.IsFalse(_board[0, 1]);
            Assert.IsFalse(_board[0, 2]);
        }

        [TestMethod]
        public void CheckNegativeIndexes()
        {
            Assert.IsFalse(_board[-1, 0]);
            Assert.IsFalse(_board[0, 11]);
            Assert.IsFalse(_board[0, -2]);
        }

        [TestMethod]
        public void CheckMaxValidIndexes()
        {
            Assert.IsFalse(_board[SizeX, SizeY]);
            Assert.IsFalse(_board[-SizeX, -SizeY]);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void CheckExtremeIndexes()
        {
            Assert.IsFalse(_board[int.MinValue, int.MaxValue]);
        }

        [TestMethod]
        public void SetCell()
        {
            _board[0, 0] = true;
            Assert.IsTrue(_board[0, 0]);
        }

        [TestMethod]
        public void CheckSizeProperties()
        {
            Assert.AreEqual(_board.SizeX, 100);
            Assert.AreEqual(_board.SizeY, 100);
        }

        [TestMethod]
        public void CheckEqualitySameEmptyBoards()
        {
            GameBoard board1 = new GameBoard(100, 150);
            GameBoard board2 = new GameBoard(100, 150);
            Assert.IsTrue(board1.Equals(board2));
        }
    }
}