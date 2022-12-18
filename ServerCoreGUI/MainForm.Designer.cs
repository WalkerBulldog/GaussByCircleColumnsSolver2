namespace ServerGUI
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialogB = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.Alabel = new System.Windows.Forms.Label();
            this.Blabel = new System.Windows.Forms.Label();
            this.Slabel = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.CClabel = new System.Windows.Forms.Label();
            this.ExecButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.resLabel = new System.Windows.Forms.Label();
            this.loadingLab = new System.Windows.Forms.Label();
            this.seidelBut = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button1.Location = new System.Drawing.Point(36, 39);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 55);
            this.button1.TabIndex = 0;
            this.button1.Text = "Open A";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button2.Location = new System.Drawing.Point(36, 118);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(106, 55);
            this.button2.TabIndex = 1;
            this.button2.Text = "Open B";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button3.Location = new System.Drawing.Point(36, 196);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(106, 55);
            this.button3.TabIndex = 2;
            this.button3.Text = "Save to";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialogA";
            // 
            // openFileDialogB
            // 
            this.openFileDialogB.FileName = "openFileDialog2";
            // 
            // Alabel
            // 
            this.Alabel.AutoSize = true;
            this.Alabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Alabel.Location = new System.Drawing.Point(172, 69);
            this.Alabel.Name = "Alabel";
            this.Alabel.Size = new System.Drawing.Size(144, 25);
            this.Alabel.TabIndex = 3;
            this.Alabel.Text = "No file choosen";
            // 
            // Blabel
            // 
            this.Blabel.AutoSize = true;
            this.Blabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Blabel.Location = new System.Drawing.Point(172, 148);
            this.Blabel.Name = "Blabel";
            this.Blabel.Size = new System.Drawing.Size(144, 25);
            this.Blabel.TabIndex = 4;
            this.Blabel.Text = "No file choosen";
            // 
            // Slabel
            // 
            this.Slabel.AutoSize = true;
            this.Slabel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Slabel.Location = new System.Drawing.Point(172, 226);
            this.Slabel.Name = "Slabel";
            this.Slabel.Size = new System.Drawing.Size(144, 25);
            this.Slabel.TabIndex = 5;
            this.Slabel.Text = "No file choosen";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(36, 316);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(236, 56);
            this.trackBar1.TabIndex = 6;
            this.trackBar1.Value = 1;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(87, 288);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Clasters count: ";
            // 
            // CClabel
            // 
            this.CClabel.AutoSize = true;
            this.CClabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CClabel.Location = new System.Drawing.Point(142, 366);
            this.CClabel.Name = "CClabel";
            this.CClabel.Size = new System.Drawing.Size(26, 29);
            this.CClabel.TabIndex = 8;
            this.CClabel.Text = "1";
            // 
            // ExecButton
            // 
            this.ExecButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ExecButton.Location = new System.Drawing.Point(70, 399);
            this.ExecButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ExecButton.Name = "ExecButton";
            this.ExecButton.Size = new System.Drawing.Size(170, 43);
            this.ExecButton.TabIndex = 9;
            this.ExecButton.Text = "Execute";
            this.ExecButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ExecButton.UseVisualStyleBackColor = true;
            this.ExecButton.Click += new System.EventHandler(this.ExecButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(36, 534);
            this.progressBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(513, 29);
            this.progressBar.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(312, 288);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 29);
            this.label2.TabIndex = 11;
            this.label2.Text = "Results:";
            // 
            // resLabel
            // 
            this.resLabel.AutoSize = true;
            this.resLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.resLabel.Location = new System.Drawing.Point(312, 324);
            this.resLabel.Name = "resLabel";
            this.resLabel.Size = new System.Drawing.Size(67, 29);
            this.resLabel.TabIndex = 12;
            this.resLabel.Text = "none";
            // 
            // loadingLab
            // 
            this.loadingLab.AutoSize = true;
            this.loadingLab.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.loadingLab.Location = new System.Drawing.Point(493, 307);
            this.loadingLab.Name = "loadingLab";
            this.loadingLab.Size = new System.Drawing.Size(34, 46);
            this.loadingLab.TabIndex = 13;
            this.loadingLab.Text = "-";
            // 
            // seidelBut
            // 
            this.seidelBut.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.seidelBut.Location = new System.Drawing.Point(70, 456);
            this.seidelBut.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.seidelBut.Name = "seidelBut";
            this.seidelBut.Size = new System.Drawing.Size(170, 70);
            this.seidelBut.TabIndex = 14;
            this.seidelBut.Text = "Execute Seidel";
            this.seidelBut.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.seidelBut.UseVisualStyleBackColor = true;
            this.seidelBut.Click += new System.EventHandler(this.seidelBut_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 576);
            this.Controls.Add(this.seidelBut);
            this.Controls.Add(this.loadingLab);
            this.Controls.Add(this.resLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.ExecButton);
            this.Controls.Add(this.CClabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.Slabel);
            this.Controls.Add(this.Blabel);
            this.Controls.Add(this.Alabel);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "Snezhko\'s solver";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialogB;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Label Alabel;
        private System.Windows.Forms.Label Blabel;
        private System.Windows.Forms.Label Slabel;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label CClabel;
        private System.Windows.Forms.Button ExecButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label resLabel;
        private Label loadingLab;
        private Button seidelBut;
    }
}

