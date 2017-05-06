using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Media;
using aStar;


namespace Pathfinding
{

    public enum Sounds { Collision, Move, GameOver, Win } //sound effects list


    public partial class Main : Form
    {

        private int gamestatus = 0; //is the splash screen open
        private Random rng = new Random();

        private int xcells = 20; //initial board dimensions
        private int ycells = 20;
        public bool[,] Map { get; set; }
        private Point start; //player starting position
        private Point finish; //player goal

        private bool reqRedraw = true; //toggles drawing the board
        private Grid myGrid; //board grid

        private Pathfinder2 pathfinder;
        private List<Point> path = new List<Point>();

        public Player MyPlayer { get; set; }
        private List<Enemy> enemies = new List<Enemy>();

        private static List<SoundPlayer> loadedSounds = new List<SoundPlayer>();
        private static System.Windows.Media.MediaPlayer musicPlayer = new System.Windows.Media.MediaPlayer();


        public Main()
        {
            InitializeComponent();
            ResizeRedraw = true;
            DoubleBuffered = true;
        }

        //static calculation functions

        public static int DisPwr(Point p1, Point p2) //distance power
        {
            return ((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }

        public static bool[,] GenerateRandomMap(int width, int height) //generates random "map" in the specified dimensions
        {
            bool[,] grid = new bool[width, height];

            Random rng = new Random();

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    grid[i, j] = (rng.Next(4) % 4 != 0); //4 = the portion of the map blocked, calculated 1/density(4)
                }
            }

            grid[0, 0] = true; //start &finish are always unblocked
            grid[grid.GetLength(0) - 1, grid.GetLength(1) - 1] = true;

            return grid;
        }


        //save and load functions

        private void LoadFile() //loads a text file details a game board
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension
            dlg.ShowDialog();
            string file = dlg.FileName;

            if (file != "")
            {
                try
                {
                    List<Setting> settings = Setting.ReadSettings(file);
                    Point _player = new Point(0, 0);
                    List<Point> _enemies = new List<Point>();

                    foreach (Setting s in settings)
                    {
                        switch (s.Name)
                        {
                            case "player":
                                _player.X = Convert.ToInt32(s.Value.Split(',')[0]);
                                _player.Y = Convert.ToInt32(s.Value.Split(',')[1]);
                                break;
                            case "enemy":
                                Point _enemy = new Point(0, 0);
                                _enemy.X = Convert.ToInt32(s.Value.Split(',')[0]);
                                _enemy.Y = Convert.ToInt32(s.Value.Split(',')[1]);
                                _enemies.Add(_enemy);
                                break;
                            case "xcells":
                                int _xcells = Convert.ToInt32(s.Value);
                                if (_xcells <= 300 && _xcells >= 6)
                                    this.xcells = _xcells;
                                break;
                            case "ycells":
                                int _ycells = Convert.ToInt32(s.Value);
                                if (_ycells <= 300 && _ycells >= 6)
                                    this.ycells = _ycells;
                                break;
                            case "finish":
                                this.finish.X = Convert.ToInt32(s.Value.Split(',')[0]);
                                this.finish.Y = Convert.ToInt32(s.Value.Split(',')[1]);
                                break;
                            default:
                                break;
                        }
                    }

                    bool[,] _map = ReadMap(file, xcells, ycells);
                    ResetBoard(_map, _player, _enemies);

                }
                catch
                {
                    MessageBox.Show("Invalid map file.", "Error", MessageBoxButtons.OK);
                }
            }
        }

        private void SaveFile() //saves current board to file
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "myLevel";
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension
            dlg.ShowDialog();
            string file = dlg.FileName;

            if (file != "")
            {
                List<Setting> _s = new List<Setting>();
                _s.Add(new Setting("xcells", this.xcells.ToString()));
                _s.Add(new Setting("ycells", this.ycells.ToString()));
                _s.Add(new Setting("player", MyPlayer.Location.X + "," + MyPlayer.Location.Y));
                _s.Add(new Setting("finish", finish.X + "," + finish.Y));
                foreach (Enemy e in enemies)
                {
                    _s.Add(new Setting("enemy", e.Location.X + "," + e.Location.Y));
                }
                Setting.WriteSettings(_s, file, "//This is a level file generated by Pathfinding.exe.\r\n");
                WriteMap(file, this.Map);
            }
        }

        public static bool[,] ReadMap(String filename, int width, int height) //reads a character map from file
        {
            bool[,] myMap = new bool[width, height];
            using (StreamReader reader = new StreamReader(filename))
            {
                string line = "";

                while (!reader.EndOfStream)
                {
                    if (line != "" && line[0] == '*') //skips input until it reaches the map "drawing" character
                        break;
                    line = reader.ReadLine();
                }

                for (int y = 0; y < height; y++)
                {
                    line = reader.ReadLine();
                    for (int x = 0; x < width; x++)
                    {
                        if (x < line.Length && line[x].Equals('O'))
                            myMap[x, y] = true;
                        else
                            myMap[x, y] = false;
                    }
                }
            }
            return myMap;
        }

        public static void WriteMap(string filename, bool[,] myMap) //appends the boolean map to the end of the save file
        {
            List<string> map = new List<string>();
            for (int x = 0; x < myMap.GetLength(0); x++)
            {
                string column = "";
                for (int y = 0; y < myMap.GetLength(1); y++)
                {
                    if (myMap[y, x])
                        column += 'O';
                    else
                        column += 'M';
                }
                map.Add(column);
            }

            using (StreamWriter writer = new StreamWriter(filename, true))
            {
                writer.WriteLine("*");
                foreach (string s in map)
                {
                    writer.WriteLine(s);
                }
            }
        }


        //sound related

        public void SoundPlayer(Sounds s) //plays sounds
        {
            if (enableSoundEffectsToolStripMenuItem.Checked)
            {
                switch (s)
                {
                    case Sounds.Collision:
                        loadedSounds[0].Play();
                        break;
                    case Sounds.Move:
                        loadedSounds[1].Play();
                        break;
                    case Sounds.GameOver:
                        loadedSounds[2].Play();
                        break;
                    case Sounds.Win:
                        loadedSounds[3].Play();
                        break;
                    default:
                        break;
                }
            }
        }

        private List<SoundPlayer> LoadSounds() //loads all sound files
        {
            musicPlayer.Open(new Uri(Application.StartupPath + "/Ttrs_-_BgMusic.wav"));
            musicPlayer.MediaEnded += MediaEndedEvent;

            List<SoundPlayer> loaded = new List<SoundPlayer>();
            loaded.Add(new SoundPlayer(Properties.Resources.Ttrs___Land));
            loaded.Add(new SoundPlayer(Properties.Resources.Ttrs___Move));
            loaded.Add(new SoundPlayer(Properties.Resources.Ttrs___GameOver));
            loaded.Add(new SoundPlayer(Properties.Resources.Ttrs___Win));
            return loaded;
        }

        private void MediaEndedEvent(object sender, EventArgs e) //replays the background music
        {
            musicPlayer.Position = TimeSpan.Zero;
            musicPlayer.Play();
        }


        //graphics

        private Rectangle GetDrawRectangle() //the area in which to draw
        {
            Rectangle rect = new Rectangle();
            rect.Size = new Size(this.ClientSize.Height, this.ClientSize.Height);
            rect.Location = new Point((this.ClientSize.Width - this.ClientSize.Height) / 2, 0);
            return rect;
        }

        private void DrawBoard(object sender, PaintEventArgs e) //paint event
        {
            Graphics g = e.Graphics;

            if (gamestatus == 0)
                g.DrawImage(Properties.Resources.pathfindingexe, GetDrawRectangle());
            else
            {
                Pen myPen = new Pen(Color.Black, 1);
                SolidBrush blackBrush = new SolidBrush(Color.Black);
                SolidBrush whiteBrush = new SolidBrush(Color.White);
                SolidBrush blueBrush = new SolidBrush(Color.Blue);
                SolidBrush forestGreenBrush = new SolidBrush(Color.ForestGreen);
                SolidBrush redBrush = new SolidBrush(Color.Red);
                SolidBrush darkRedBrush = new SolidBrush(Color.DarkRed);
                SolidBrush yellowBrush = new SolidBrush(Color.LightYellow);
                SolidBrush lightBlueBrush = new SolidBrush(Color.LightBlue);

                g.Clear(Color.Black);

                if (reqRedraw)
                {
                    for (int x = 0; x < Map.GetLength(0); x++)
                    {
                        for (int y = 0; y < Map.GetLength(1); y++)
                        {
                            if (Map[x, y])
                            {
                                g.FillRectangle(whiteBrush, myGrid.GetRectangle(x, y));
                                g.DrawRectangle(myPen, myGrid.GetRectangle(x, y));
                            }
                            else
                            {
                                g.FillRectangle(blackBrush, myGrid.GetRectangle(x, y));
                            }
                        }
                    }
                }

                foreach (Point p in path)
                {
                    g.FillRectangle(yellowBrush, myGrid.GetRectangle(p.X, p.Y));
                    g.DrawRectangle(myPen, myGrid.GetRectangle(p.X, p.Y));
                }

                if (reqRedraw)
                {
                    List<Point> points = MyPlayer.DisplayRange();
                    foreach (Point _p in points)
                    {
                        if (Map[_p.X, _p.Y])
                        {
                            g.FillRectangle(lightBlueBrush, myGrid.GetRectangle(_p.X, _p.Y));
                            g.DrawRectangle(myPen, myGrid.GetRectangle(_p.X, _p.Y));
                        }
                    }

                    g.FillRectangle(blueBrush, myGrid.GetRectangle(MyPlayer.Location.X, MyPlayer.Location.Y));
                    g.DrawRectangle(myPen, myGrid.GetRectangle(MyPlayer.Location.X, MyPlayer.Location.Y));
                }

                g.FillRectangle(forestGreenBrush, myGrid.GetRectangle(finish.X, finish.Y));
                g.DrawRectangle(myPen, myGrid.GetRectangle(finish.X, finish.Y));

                foreach (Enemy enemy in enemies)
                {
                    if (enemy.Active)
                        g.FillRectangle(redBrush, myGrid.GetRectangle(enemy.Location.X, enemy.Location.Y));
                    else
                        g.FillRectangle(darkRedBrush, myGrid.GetRectangle(enemy.Location.X, enemy.Location.Y));
                    g.DrawRectangle(myPen, myGrid.GetRectangle(enemy.Location.X, enemy.Location.Y));
                }
            }

        }

        private void OnGameOver() //message displayed on game over
        {
            DialogResult result = MessageBox.Show("Restart?", "Game over", MessageBoxButtons.YesNo);
            switch (result)
            {
                case DialogResult.Yes:
                    ResetBoard();
                    break;
                case DialogResult.No:
                    this.Close();
                    break;
                default:
                    break;
            }
        }


        //configuration

        public void SetCellsCount(int x, int y) //the grid resize function, normally triggered by filling the pop up form
        {
            xcells = x;
            ycells = y;
            ResetBoard();
        }

        private Point CreateRandomEnemy()
        {
            Point _location = new Point(0, 0);
            while ((DisPwr(_location, MyPlayer.Location) <= (MyPlayer.Radius * MyPlayer.Radius)) || //makes sure enemies dont spawn too close to the player
                (DisPwr(_location, new Point(xcells - 1, ycells - 1))) <= (MyPlayer.Radius * MyPlayer.Radius)) //or the finish point
            {
                _location = new Point(rng.Next(xcells), rng.Next(ycells));
            }
            enemies.Add(new Enemy(_location, this));
            return (_location);
        }

        private Point CreateEnemy(Point _location)
        {
            enemies.Add(new Enemy(_location, this));
            return (_location);
        }


        //game update

        public void UpdateEntities() //moves all enemies each time the player moves
        {

            if (MyPlayer.Location.Equals(finish)) //winning message
            {
                this.Invalidate();
                SoundPlayer(Sounds.Win);
                MessageBox.Show("You win!", "Game over", MessageBoxButtons.OK);
                OnGameOver();
            }

            foreach (Enemy e in enemies)
            {

                if (e.Location.Equals(MyPlayer.Location)) //checks to see if the player moved on an enemy
                {
                    reqRedraw = false;
                    this.Invalidate();
                    SoundPlayer(Sounds.GameOver);
                    OnGameOver();
                    break;
                }

                if (e.IsInRange())
                    e.Detected(MyPlayer.Location); //if enemy is in the player's range, return enemy location

                e.Move(); //moves

                if (e.Location.Equals(MyPlayer.Location)) //checks to see if the enemy moved on the player
                {
                    reqRedraw = false;
                    this.Invalidate();
                    SoundPlayer(Sounds.GameOver);
                    OnGameOver();
                    break;
                }

                this.Invalidate(); //forces redraw

            }
        }

        private void ResetBoard() //generates a new map, pathfinder and grid
        {

            start = new Point(0, 0);
            finish = new Point(xcells - 1, ycells - 1);

            path.Clear();
            while (path.Count == 0) //to prevent a blocked grid from generating
            {

                Map = GenerateRandomMap(xcells, ycells);

                pathfinder = new Pathfinder2(Map, start, finish, false);
                path = pathfinder.FindPath();

            }

            myGrid = new Grid(GetDrawRectangle(), Map.GetLength(0), Map.GetLength(1));
            reqRedraw = true;

            MyPlayer = new Player(start, this);
            enemies.Clear();
            for (int i = 0; i <= (xcells / 2 - 3); i++) //creates new enemies
            {
                Point ePoint = CreateRandomEnemy();
                Map[ePoint.X, ePoint.Y] = true;
            }

            this.Invalidate(); //to trigger redrawing of the form

        }

        private void ResetBoard(bool[,] _map, Point _player, List<Point> _enemies) //resets board to loaded file
        {
            path.Clear();
            Map = _map;

            pathfinder = new Pathfinder2(Map, _player, finish, false);
            path = pathfinder.FindPath();

            myGrid = new Grid(GetDrawRectangle(), Map.GetLength(0), Map.GetLength(1));
            reqRedraw = true;

            MyPlayer = new Player(_player, this);

            enemies.Clear();
            foreach (Point p in _enemies) //creates new enemies
            {
                CreateEnemy(p);
                Map[p.X, p.Y] = true;
            }

            this.Invalidate(); //to trigger redrawing of the form

        }


        #region Event handlers

        private void Main_Load(object sender, EventArgs e)
        {
            ResetBoard();
            loadedSounds = LoadSounds();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            myGrid.SetSize(GetDrawRectangle());
        }


        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (gamestatus == 0)
            {
                gamestatus = 1;
                //musicPlayer.Play();
                this.Invalidate();
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        MyPlayer.Move(Directions.Up);
                        break;
                    case Keys.Right:
                        MyPlayer.Move(Directions.Right);
                        break;
                    case Keys.Down:
                        MyPlayer.Move(Directions.Down);
                        break;
                    case Keys.Left:
                        MyPlayer.Move(Directions.Left);
                        break;
                    case Keys.Escape:
                        this.Close();
                        break;
                    case Keys.Tab:
                        gamestatus = 0;
                        this.Invalidate();
                        break;
                    case Keys.R:
                        ResetBoard();
                        break;
                    default:
                        break;
                }
            }
        }

        private void Main_Click(object sender, EventArgs e)
        {
            if (gamestatus == 0)
            {
                gamestatus = 1;
                this.Invalidate();
            }
        }


        private void ResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetBoard();
        }

        private void ChangeDimensionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SizeInputForm inputForm = new SizeInputForm(this);
            inputForm.Show();
        }

        private void LoadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadFile();
        }

        private void SaveBoardToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void MusicOnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (musicOnToolStripMenuItem.Checked)
                musicPlayer.Play();
            else
                musicPlayer.Stop();
        }


        #endregion

    }
}
