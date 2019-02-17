namespace MotionTracker
{
    partial class Form1
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
            this.captureButton = new System.Windows.Forms.Button();
            this.Camera_Selection = new System.Windows.Forms.ComboBox();
            this.captureBox = new System.Windows.Forms.PictureBox();
            this.diffBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.captureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.diffBox)).BeginInit();
            this.SuspendLayout();
            // 
            // captureButton
            // 
            this.captureButton.Location = new System.Drawing.Point(701, 436);
            this.captureButton.Name = "captureButton";
            this.captureButton.Size = new System.Drawing.Size(75, 23);
            this.captureButton.TabIndex = 0;
            this.captureButton.Text = "Start";
            this.captureButton.UseVisualStyleBackColor = true;
            this.captureButton.Click += new System.EventHandler(this.captureButton_Click);
            // 
            // Camera_Selection
            // 
            this.Camera_Selection.FormattingEnabled = true;
            this.Camera_Selection.Location = new System.Drawing.Point(497, 435);
            this.Camera_Selection.Name = "Camera_Selection";
            this.Camera_Selection.Size = new System.Drawing.Size(181, 24);
            this.Camera_Selection.TabIndex = 1;
            // 
            // captureBox
            // 
            this.captureBox.Location = new System.Drawing.Point(12, 12);
            this.captureBox.Name = "captureBox";
            this.captureBox.Size = new System.Drawing.Size(664, 403);
            this.captureBox.TabIndex = 3;
            this.captureBox.TabStop = false;
            // 
            // diffBox
            // 
            this.diffBox.Location = new System.Drawing.Point(691, 12);
            this.diffBox.Name = "diffBox";
            this.diffBox.Size = new System.Drawing.Size(664, 403);
            this.diffBox.TabIndex = 4;
            this.diffBox.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1367, 473);
            this.Controls.Add(this.diffBox);
            this.Controls.Add(this.captureBox);
            this.Controls.Add(this.Camera_Selection);
            this.Controls.Add(this.captureButton);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.captureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.diffBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button captureButton;
        private System.Windows.Forms.ComboBox Camera_Selection;
        private System.Windows.Forms.PictureBox captureBox;
        private System.Windows.Forms.PictureBox diffBox;
    }
}

