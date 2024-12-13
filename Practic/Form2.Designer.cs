namespace Practic
{
    partial class Form2
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
            pictureBox1 = new PictureBox();
            dataGridView1 = new DataGridView();
            labelCountProc = new Label();
            listBox1 = new ListBox();
            label19 = new Label();
            label18 = new Label();
            labelPr = new Label();
            label17 = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            button4 = new Button();
            label8 = new Label();
            labelAnomalyCount = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.ControlLightLight;
            pictureBox1.Location = new Point(27, 26);
            pictureBox1.Margin = new Padding(4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(875, 875);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(934, 480);
            dataGridView1.Margin = new Padding(4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(966, 383);
            dataGridView1.TabIndex = 3;
            // 
            // labelCountProc
            // 
            labelCountProc.AutoSize = true;
            labelCountProc.Location = new Point(1177, 144);
            labelCountProc.Margin = new Padding(4, 0, 4, 0);
            labelCountProc.Name = "labelCountProc";
            labelCountProc.Size = new Size(19, 25);
            labelCountProc.TabIndex = 51;
            labelCountProc.Text = "_";
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 25;
            listBox1.Location = new Point(1644, 73);
            listBox1.Margin = new Padding(4);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(186, 279);
            listBox1.TabIndex = 50;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(1645, 25);
            label19.Margin = new Padding(4, 0, 4, 0);
            label19.Name = "label19";
            label19.Size = new Size(187, 25);
            label19.TabIndex = 49;
            label19.Text = "Список пріоритетів L:";
            // 
            // label18
            // 
            label18.Location = new Point(957, 144);
            label18.Margin = new Padding(4, 0, 4, 0);
            label18.Name = "label18";
            label18.Size = new Size(261, 55);
            label18.TabIndex = 48;
            label18.Text = "Кількість процесорів:\r\n";
            // 
            // labelPr
            // 
            labelPr.AutoSize = true;
            labelPr.Location = new Point(1246, 212);
            labelPr.Margin = new Padding(4, 0, 4, 0);
            labelPr.Name = "labelPr";
            labelPr.Size = new Size(19, 25);
            labelPr.TabIndex = 53;
            labelPr.Text = "_";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(957, 212);
            label17.Margin = new Padding(4, 0, 4, 0);
            label17.Name = "label17";
            label17.Size = new Size(256, 25);
            label17.TabIndex = 52;
            label17.Text = "Довжина упорядкування l(S): \r\n";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(957, 25);
            label1.Name = "label1";
            label1.Size = new Size(120, 25);
            label1.TabIndex = 54;
            label1.Text = "Вид аномалії:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(957, 79);
            label2.Name = "label2";
            label2.Size = new Size(109, 25);
            label2.TabIndex = 55;
            label2.Text = "Комментар:";
            // 
            // label3
            // 
            label3.Location = new Point(957, 256);
            label3.Name = "label3";
            label3.Size = new Size(308, 58);
            label3.TabIndex = 56;
            label3.Text = "Величина, на яку збільшилась довжина упорядкування:\r\n";
            // 
            // button1
            // 
            button1.Location = new Point(955, 371);
            button1.Name = "button1";
            button1.Size = new Size(122, 35);
            button1.TabIndex = 57;
            button1.Text = "<-\r\n\r\n";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Enabled = false;
            button2.Location = new Point(1083, 371);
            button2.Name = "button2";
            button2.Size = new Size(122, 35);
            button2.TabIndex = 58;
            button2.Text = "->\r\n";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(1626, 371);
            button3.Name = "button3";
            button3.Size = new Size(225, 75);
            button3.TabIndex = 59;
            button3.Text = "Повернутися до початкового прикладу";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1116, 25);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(19, 25);
            label4.TabIndex = 60;
            label4.Text = "_";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(1116, 79);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(19, 25);
            label5.TabIndex = 61;
            label5.Text = "_";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(1246, 275);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(19, 25);
            label6.TabIndex = 62;
            label6.Text = "_";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(934, 437);
            label7.Name = "label7";
            label7.Size = new Size(143, 25);
            label7.TabIndex = 63;
            label7.Text = "Упорядкування:";
            // 
            // button4
            // 
            button4.Location = new Point(1380, 372);
            button4.Name = "button4";
            button4.Size = new Size(225, 74);
            button4.TabIndex = 64;
            button4.Text = "Відобразити аномалії";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // label8
            // 
            label8.Location = new Point(1380, 301);
            label8.Name = "label8";
            label8.Size = new Size(182, 51);
            label8.TabIndex = 65;
            label8.Text = "Кількість знайдених аномалій:\r\n";
            // 
            // labelAnomalyCount
            // 
            labelAnomalyCount.AutoSize = true;
            labelAnomalyCount.Location = new Point(1569, 327);
            labelAnomalyCount.Margin = new Padding(4, 0, 4, 0);
            labelAnomalyCount.Name = "labelAnomalyCount";
            labelAnomalyCount.Size = new Size(19, 25);
            labelAnomalyCount.TabIndex = 66;
            labelAnomalyCount.Text = "_";
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1924, 1029);
            Controls.Add(labelAnomalyCount);
            Controls.Add(label8);
            Controls.Add(button4);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(labelPr);
            Controls.Add(label17);
            Controls.Add(labelCountProc);
            Controls.Add(listBox1);
            Controls.Add(label19);
            Controls.Add(label18);
            Controls.Add(dataGridView1);
            Controls.Add(pictureBox1);
            Name = "Form2";
            Text = "Аномалії";
            Load += Form2_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private DataGridView dataGridView1;
        private Label labelCountProc;
        private ListBox listBox1;
        private Label label19;
        private Label label18;
        private Label labelPr;
        private Label label17;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button button1;
        private Button button2;
        private Button button3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Button button4;
        private Label label8;
        private Label labelAnomalyCount;
    }
}