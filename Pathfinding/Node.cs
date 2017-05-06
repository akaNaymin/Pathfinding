using System;
using System.Drawing;

namespace aStar
{
    public enum NodeState { Closed, Open, Untested } // closed = does not require further check, open = added but a better route may be possible, untested... yeah

    public class Node
    {

        public Point Location { get; set; } //set by the Pathfinder class
        public Node Parent { get; set; } //the node from which this one was reached
        public int G { get; set; } //cost to reach node
        public int H { get; set; } //estimated cost to reach finish (heuristic cost)
        public int F { get; set; } //combined G + H or "path cost", the lower the better
        public NodeState State { get; set; } = NodeState.Untested;


        public Node(Point location) //initialize a new Node and sets location
        {
            Location = location;
        }

        public void SetH(Point goal) //calculates heuristic by the manhattan approach
        {
            H = (Math.Abs(goal.X - Location.X) + Math.Abs(goal.Y - Location.Y)) * 10;
        }

        public void SetF() //sets F to combined movement cost
        {
            F = G + H;
        }

        public static int CalcG(Directions dir, Node parent) //calculates movement cost to current node
        {
            if ((int)dir < 4) //horizontal directions
                return (parent.G + 10);
            else
                return (parent.G + 14); //14 = sqrt(200) rounded to save process time

        }

    }
}
