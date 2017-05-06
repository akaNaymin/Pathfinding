using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Pathfinding
{

    public enum Directions { Up, Right, Down, Left }

    public class Player
    {
        public Point Location { get; set; }
        public int Radius { get; set; } = 3; //detection radius
        private Main form;

        public Player(Point location, Main form) //initialize Player
        {
            Location = location;
            this.form = form;
        }

        public void Move(Directions dir) //checks if the cell is traversable, if so moves in said direction
        {
            switch (dir)
            {
                case Directions.Up:
                    if ((Location.Y > 0) && form.Map[Location.X, Location.Y - 1])
                    {
                        Location = new Point(Location.X, Location.Y - 1);
                        form.SoundPlayer(Sounds.Move);
                    }
                    else
                        form.SoundPlayer(Sounds.Collision);
                    break;
                case Directions.Right:
                    if ((Location.X < form.Map.GetLength(0) - 1) && form.Map[Location.X + 1, Location.Y])
                    {
                        Location = new Point(Location.X + 1, Location.Y);
                        form.SoundPlayer(Sounds.Move);
                    }
                    else
                        form.SoundPlayer(Sounds.Collision);
                    break;
                case Directions.Down:
                    if ((Location.Y < form.Map.GetLength(1) - 1) && form.Map[Location.X, Location.Y + 1])
                    {
                        Location = new Point(Location.X, Location.Y + 1);
                        form.SoundPlayer(Sounds.Move);
                    }
                    else
                        form.SoundPlayer(Sounds.Collision);
                    break;
                case Directions.Left:
                    if ((Location.X > 0) && form.Map[Location.X - 1, Location.Y])
                    {
                        Location = new Point(Location.X - 1, Location.Y);
                        form.SoundPlayer(Sounds.Move);
                    }
                    else
                        form.SoundPlayer(Sounds.Collision);
                    break;
                default:
                    break;
            }
            form.UpdateEntities();
        }

        public List<Point> DisplayRange() //gets the area that is the player's detection radius
        {
            List<Point> points = new List<Point>(); //points to color
            for (int x = (Location.X - this.Radius); x <= (Location.X + Radius); x++) //2d loop that checks all points in the square that encloses the circle which its center is the player and radius is detection radius
            {
                for (int y = (Location.Y - Radius); y <= (Location.Y + Radius); y++)
                {
                    if (Main.DisPwr(new Point(x, y), this.Location) <= (Radius * Radius)) //if said point is in said circle
                    {
                        if ((x >= 0 && x < form.Map.GetLength(0) && y >= 0 && y < form.Map.GetLength(1))) //if the point is inside the map
                        {
                            points.Add(new Point(x, y)); //adds to list
                        }
                    }
                }
            }
            return points;
        }

    }
}
