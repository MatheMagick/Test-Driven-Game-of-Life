using System;

namespace Test_Driven_Game_of_Life
{
    public class GameBoard
    {
        private const byte NeighbourCountAlone = 1;
        private const byte NeighbourCountOvercrowded = 4;
        private const byte NeighbourCountIsReproduced = 3;

        public int SizeX { get; private set; }
        public int SizeY { get; private set; }

        protected bool Equals(GameBoard other)
        {
            bool result = SizeY == other.SizeY && SizeX == other.SizeX;

            if (result)
            {
                for (int i = 0; i < _values.GetLength(0); ++i)
                {
                    for (int j = 0; j < _values.GetLength(1); ++j)
                    {
                        if (_values[i, j] != other._values[i, j])
                        {
                            result = false;
                            break;
                        }
                    }

                    if (!result)
                    {
                        break;
                    }
                }
            }

            return result;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((GameBoard) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (_values != null ? _values.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ SizeX;
                hashCode = (hashCode*397) ^ SizeY;
                return hashCode;
            }
        }

        public GameBoard(int sizeX, int sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            _values = new bool[sizeX * 2 + 1, sizeY * 2 + 1];
        }

        public bool this[int x, int y]
        {
            get { return _values[SizeX + x, SizeY + y]; }
            set { _values[SizeX + x, SizeY + y] = value; }
        }

        private readonly bool[,] _values;

        public GameBoard Evolve()
        {
            GameBoard result = new GameBoard(SizeX, SizeY);

            for (int x = 0; x < _values.GetLength(0); x++)
            {
                for (int y = 0; y < _values.GetLength(1); y++)
                {
                    byte neighbourCount = GetNeighbourCount(x, y);
                    bool isAlive = _values[x, y];
                    bool willLive = (isAlive && (neighbourCount > NeighbourCountAlone) && neighbourCount < NeighbourCountOvercrowded) || (neighbourCount == NeighbourCountIsReproduced);
                    result._values[x, y] = willLive;
                }
            }

            return result;
        }

        private byte GetNeighbourCount(int x, int y)
        {
            byte result = 0;

            for (int i = Math.Max(0, x - 1); i <= Math.Min(_values.GetLength(0) - 1, x + 1); i++)
            {
                for (int j = Math.Max(0, y - 1); j <= Math.Min(_values.GetLength(1) - 1, y + 1); j++)
                {
                    if ((i != x || j != y) && _values[i, j])
                    {
                        ++result;
                    }
                }
            }

            return result;
        }
    }
}