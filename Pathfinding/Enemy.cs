using System.Collections.Generic;
using System.Drawing;
using aStar;

namespace Pathfinding
{

    public class Enemy
    {

        public Point Location { get; set; }
        private Point goal;
        private Main form;

        private Pathfinder2 pfinder;
        public Queue<Point> Path { get; set; } = new Queue<Point>();

        public bool Active { get; set; } = false;

        public Enemy(Point position, Main form) //initalizer
        {
            this.Location = position;
            this.form = form;
            pfinder = new Pathfinder2(form.Map, Location, Location, false);
        }

        public void Move() //moves one step
        {
            if (Path.Count > 0)
                Location = Path.Dequeue();
        }

        public void Detected(Point at) //runs when detects the player in Main
        {
            if (!at.Equals(goal)) //when the goal is changed
            {
                Active = true;
                goal = at; //sets goal to new point
                pfinder = new Pathfinder2(form.Map, Location, at, false); //finds new path
                Path = new Queue<Point>(pfinder.FindPath());
                if (Path.Count > 0)
                    Path.Dequeue(); //remove the top of the list since the first node is the enemy's location
            }
        }

        public bool IsInRange() //checks if this enemy is in the player's detection range
        {
            int radius = form.MyPlayer.Radius;
            if (Main.DisPwr(this.Location, form.MyPlayer.Location) <= (radius * radius))
                return true;
            else
                return false;
        }
    }

}
