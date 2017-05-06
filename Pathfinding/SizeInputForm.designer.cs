namespace Pathfinding
{
    partial class SizeInputForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SizeInputForm));
            this.inputWidthText = new System.Windows.Forms.Label();
            this.inputHeightText = new System.Windows.Forms.Label();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.textBoxHeight = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // inputWidthText
            // 
            this.inputWidthText.AutoSize = true;
            this.inputWidthText.Location = new System.Drawing.Point(19, 20);
            this.inputWidthText.Name = "inputWidthText";
            this.inputWidthText.Size = new System.Drawing.Size(35, 13);
            this.inputWidthText.TabIndex = 0;
            this.inputWidthText.Text = "Width";
            this.inputWidthText.Visible = false;
            // 
            // inputHeightText
            // 
            this.inputHeightText.AutoSize = true;
            this.inputHeightText.Location = new System.Drawing.Point(19, 42);
            this.inputHeightText.Name = "inputHeightText";
            this.inputHeightText.Size = new System.Drawing.Size(54, 13);
            this.inputHeightText.TabIndex = 1;
            this.inputHeightText.Text = "Level size";
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Location = new System.Drawing.Point(155, 17);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.Size = new System.Drawing.Size(54, 20);
            this.textBoxWidth.TabIndex = 2;
            this.textBoxWidth.Visible = false;
            // 
            // textBoxHeight
            // 
            this.textBoxHeight.Location = new System.Drawing.Point(155, 39);
            this.textBoxHeight.Name = "textBoxHeight";
            this.textBoxHeight.Size = new System.Drawing.Size(54, 20);
            this.textBoxHeight.TabIndex = 3;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(84, 91);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(65, 32);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "Confirm";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // SizeInputForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(238, 135);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.textBoxHeight);
            this.Controls.Add(this.textBoxWidth);
            this.Controls.Add(this.inputHeightText);
            this.Controls.Add(this.inputWidthText);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SizeInputForm";
            this.Text = "Change level size";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label inputWidthText;
        private System.Windows.Forms.Label inputHeightText;
        private System.Windows.Forms.TextBox textBoxWidth;
        private System.Windows.Forms.TextBox textBoxHeight;
        private System.Windows.Forms.Button okButton;
    }
}