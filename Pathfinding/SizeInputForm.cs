using System;
using System.Windows.Forms;

namespace Pathfinding
{

    public partial class SizeInputForm : Form
    {

        private Main mainform; //the form to which SizeInputForm works on

        private int gridWidth;
        private int gridHeight;
        private int input;

        public SizeInputForm(Main mainform) //initialize form
        {
            InitializeComponent();
            this.mainform = mainform;
            gridWidth = mainform.Map.GetLength(0); //sets default size to current map dimensions
            gridHeight = mainform.Map.GetLength(1);
            textBoxWidth.Text = gridWidth.ToString(); //sets textbox text to defaults
            textBoxHeight.Text = gridHeight.ToString();
        }

        private bool TestValue(string _in) //tests the input is valid
        {

            int number;
            bool result = Int32.TryParse(_in, out number);

            if (result)
            {
                this.input = number;
                return true;
            }
            else
                return false;
        }

        private void OKButton_Click(object sender, EventArgs e) //on user confirmation checks the values are valid then updates them in Main
        {

            if (TestValue(this.textBoxWidth.Text) && input <= 300 && input >= 10) //tests the input isn't too big
            {
                //gridWidth = input;  //for simplicity's sake only square grids are allowed.

                if (TestValue(this.textBoxHeight.Text) && input <= 300 && input >= 10)
                {
                    gridHeight = input;
                    gridWidth = input;
                }
                else
                {
                    MessageBox.Show("Error: invalid input. Enter a number between 10 and 300.", "Error", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Error: invalid input. Enter a number between 10 and 300.", "Error", MessageBoxButtons.OK);
            }

            mainform.SetCellsCount(gridWidth, gridHeight); //sets the grid size of the main form to the user's input
        }

    }
}
