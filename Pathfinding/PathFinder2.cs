using System.Collections.Generic;
using System.Drawing;
using Priority_Queue; //NuGet by BlueRaja which implements priority queue in c# https://github.com/BlueRaja/High-Speed-Priority-Queue-for-C-Sharp

namespace aStar
{

    public enum Directions { Up, Right, Down, Left, UpRight, DownRight, DownLeft, UpLeft }

    public class Pathfinder2
    {

        private bool[,] boolMap; //input map
        private Point start;
        private Point finish;
        private Node[,] nodes; //2D array of nodes corresponding the input map
        private SimplePriorityQueue<Node> frontier = new SimplePriorityQueue<Node>(); //the expanding frontier
        private bool vertical; //enables vertical movement

        public Pathfinder2(bool[,] boolMap, Point start, Point finish, bool vertical) //initialize a grid(graph) of nodes and resets them
        {
            this.boolMap = boolMap;
            this.start = start;
            this.finish = finish;
            this.vertical = vertical;
            nodes = new Node[boolMap.GetLength(0), boolMap.GetLength(1)];
            for (int x = 0; x < boolMap.GetLength(0); x++)
            {
                for (int y = 0; y < boolMap.GetLength(1); y++)
                {
                    nodes[x, y] = new Node(new Point(x, y));
                }
            }
            nodes[start.X, start.Y].SetH(finish);
            nodes[start.X, start.Y].G = 0;
            nodes[start.X, start.Y].SetF();
            nodes[start.X, start.Y].State = NodeState.Open;
            frontier.Enqueue(nodes[start.X, start.Y], nodes[start.X, start.Y].F);
        }

        public List<Point> FindPath() //initializes the search, if a path was found returns it
        {

            List<Point> path = new List<Point>();
            if (Search()) //if a path was found, follow it from finish node backwards
            {
                Node curNode = this.nodes[finish.X, finish.Y];
                while (curNode != null)
                {
                    path.Add(curNode.Location);
                    curNode = curNode.Parent;
                }
                path.Reverse(); //since the returned path is flipped
            }

            return path;
        }

        private bool Search() //main function
        {
            while (frontier.Count != 0) //while there are nodes left to check
            {
                Node current = frontier.Dequeue(); //fetches the node with the best (lowest) movement cost which wasnt checked
                current.State = NodeState.Closed; //marks it as checked ("closed")

                if (current.Location.Equals(this.finish)) //if end was reached, break
                    return true;

                foreach (Node n in GetNearbyNodes(current)) //fetches adjecent nodes to the one currently tested
                {
                    frontier.Enqueue(n, n.F); //adds each to the frontier
                }
            }
            return false;
        }

        private List<Node> GetNearbyNodes(Node myNode) //returns nodes adjacent to argument
        {
            List<Node> nodes = new List<Node>();

            int dirs = 8;
            if (!vertical) //if vertical movement is disabled check only four directions
                dirs = 4;

            for (int i = 0; i < dirs; i++) //reads for each direction
            {
                Node _node = NodeInDir(myNode, (Directions)i); //fetches node in specified direction

                if (_node.State != NodeState.Closed && boolMap[_node.Location.X, _node.Location.Y]) //skips nodes that are closed or untraversable
                {
                    if (_node.State == NodeState.Open && _node.G > Node.CalcG((Directions)i, myNode)) //if in open list, checks if the current path is better
                    {
                        _node.Parent = myNode;
                        _node.G = Node.CalcG((Directions)i, myNode);
                        nodes.Add(_node);
                    }

                    if (_node.State == NodeState.Untested) //if untested, the node is initialized and added to the open list
                    {
                        _node.Parent = myNode;
                        _node.State = NodeState.Open;
                        _node.G = Node.CalcG((Directions)i, myNode);
                        _node.SetH(finish);
                        _node.SetF();
                        nodes.Add(_node);
                    }
                }
            }
            return nodes;
        }

        private Node NodeInDir(Node myNode, Directions dir) //fetches node in specified direction
        {
            switch (dir)
            {
                case Directions.Up:
                    if (myNode.Location.Y > 0)
                        return (nodes[myNode.Location.X, myNode.Location.Y - 1]);
                    break;
                case Directions.Right:
                    if (myNode.Location.X < boolMap.GetLength(0) - 1)
                        return (nodes[myNode.Location.X + 1, myNode.Location.Y]);
                    break;
                case Directions.Down:
                    if (myNode.Location.Y < boolMap.GetLength(1) - 1)
                        return (nodes[myNode.Location.X, myNode.Location.Y + 1]);
                    break;
                case Directions.Left:
                    if (myNode.Location.X > 0)
                        return (nodes[myNode.Location.X - 1, myNode.Location.Y]);
                    break;
                case Directions.UpRight:
                    if (myNode.Location.Y > 0 && myNode.Location.X < boolMap.GetLength(0) - 1)
                        return (nodes[myNode.Location.X, myNode.Location.Y - 1]);
                    break;
                case Directions.DownRight:
                    if (myNode.Location.Y < boolMap.GetLength(1) - 1 && myNode.Location.X < boolMap.GetLength(0) - 1)
                        return (nodes[myNode.Location.X + 1, myNode.Location.Y]);
                    break;
                case Directions.DownLeft:
                    if (myNode.Location.Y < boolMap.GetLength(1) - 1 && myNode.Location.X > 0)
                        return (nodes[myNode.Location.X, myNode.Location.Y + 1]);
                    break;
                case Directions.UpLeft:
                    if (myNode.Location.Y > 0 && myNode.Location.X > 0)
                        return (nodes[myNode.Location.X - 1, myNode.Location.Y]);
                    break;
                default:
                    return myNode;
            }
            return myNode;
        }
    }
}
