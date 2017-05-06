using System.Drawing;

namespace Pathfinding
{

    class Grid
    {

        public int Xcells { get; set; }
        public int Ycells { get; set; }
        public Rectangle Board { get; set; } //grid location and size

        public Point[,] BoardCoord { get; set; } //stores individual cells location
        public Size TileSize { get; set; }


        public Grid(Rectangle board, int xcells, int ycells) //creats a resizeable grid then maps cells to given coordinates;
        {
            this.Xcells = xcells;
            this.Ycells = ycells;
            this.Board = board;

            BoardCoord = new Point[xcells, ycells];
            SetSize(board);
        }

        public Rectangle GetRectangle(int x, int y)  //gets the specific tile's coord + size as Rectangle
        {
            return new Rectangle(BoardCoord[x, y], TileSize);
        }

        public void SetSize(Rectangle rect) // Resize function;
        {
            this.Board = rect;
            TileSize = new Size(Board.Height / Xcells, Board.Height / this.Ycells);
            CalcBoard();
        }

        private void CalcBoard()
        {
            for (int i = 0; i < Ycells; i++)
            {
                for (int j = 0; j < Xcells; j++)
                    BoardCoord[i, j] = CalcTile(new Point(i, j));
            }
        }

        private Point CalcTile(Point tileCoord)
        {
            Point location = new Point();
            location.X = (TileSize.Width * tileCoord.X) + (Board.Left);
            location.Y = TileSize.Height * tileCoord.Y;
            return location;
        }


    }
}
