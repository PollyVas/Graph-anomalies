using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practic
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public Graphs myGraphs;
        Graphics g;
        int counter;
        void showKnot(Graphics g, Graph tr, int number, int x, int y)
        {
            g.DrawEllipse(Pens.Red, x - 20, y - 20, 40, 40);
            if (number < 9)
            {
                g.DrawString((number + 1).ToString(), this.Font, Brushes.Black, x - 6, y - 8);
            }
            else
            {
                g.DrawString((number + 1).ToString(), this.Font, Brushes.Black, x - 10, y - 8);
            }

            if (tr.knots[number].weight != -1)
            {
                g.DrawString((tr.knots[number].weight).ToString(), this.Font, Brushes.DarkGreen, x - 30, y - 30);
            }
            //g.DrawString((tr.knots[number].markDown).ToString(), this.Font, Brushes.Black, x + 20, y - 20);
            //g.DrawString((tr.knots[number].markUp).ToString(), this.Font, Brushes.Black, x - 25, y - 25);
        }
        void showArrow(Graphics g, Graph tr, int number)
        {
            Pen pen = new Pen(Color.Red, 3);
            pen.CustomEndCap = new AdjustableArrowCap(3, 3);
            g.DrawLine(pen, tr.arcs[number].knotOut.x, tr.arcs[number].knotOut.y + 20, tr.arcs[number].knotIn.x, tr.arcs[number].knotIn.y - 20);
            //g.DrawString(Convert.ToString(number).ToString(), this.Font, Brushes.Blue, (tr.arcs[number].knotOut.x + tr.arcs[number].knotIn.x) / 2, (tr.arcs[number].knotOut.y + tr.arcs[number].knotIn.y) / 2);
        }
        void showGraph(Graph tr)
        {
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            int height = pictureBox1.Height / (tr.levels + 1);
            int width;
            int count = 0;
            for (int i = 0; i < tr.levels; i++)
            {
                width = pictureBox1.Width / (tr.countLevels[i] + 1);
                for (int j = 0; j < tr.countLevels[i]; j++)
                {
                    tr.knots[count].x = width * (j + 1);
                    tr.knots[count].y = height * (i + 1);
                    showKnot(g, tr, count, tr.knots[count].x, tr.knots[count].y);
                    count++;
                }
            }
            for (int i = 0; i < tr.arcs.Count; i++)
            {
                showArrow(g, tr, i);
            }
        }
        void showAnomaly()
        {
            //Graphics g = pictureBox1.CreateGraphics();
            //g.Clear(Color.White);
            showGraph(myGraphs.anomalyList[counter].grWithChanges);
            listBox1.Items.Clear();

            for (int i = 0; i < myGraphs.anomalyList[counter].grWithChanges.L_priorityList.Count; i++)
            {
                listBox1.Items.Add(myGraphs.anomalyList[counter].grWithChanges.L_priorityList[i] + 1);
            }
            labelCountProc.Text = myGraphs.anomalyList[counter].grWithChanges.h.ToString();            
            if (myGraphs.anomalyList[counter].type == 2)
            {
                label4.Text = "Аномалія 2. Послаблення часткового порядку";
            }
            if (myGraphs.anomalyList[counter].type == 3)
            {
                label4.Text = "Аномалія 3. Зменшення часу виконання завдань";
            }
            if (myGraphs.anomalyList[counter].type == 4)
            {
                label4.Text = "Аномалія 4. Збільшення кількості процесорів";
            }
            label5.Text = myGraphs.anomalyList[counter].comment.ToString();
            labelPr.Text = myGraphs.anomalyList[counter].grWithChanges.l.ToString();
            label6.Text = myGraphs.anomalyList[counter].residual.ToString();

            dataGridView1.RowCount = myGraphs.anomalyList[counter].grWithChanges.h;
            dataGridView1.ColumnCount = myGraphs.anomalyList[counter].grWithChanges.l;
            for (int i = 0; i < myGraphs.anomalyList[counter].grWithChanges.h; i++)
            {
                for (int j = 0; j < myGraphs.anomalyList[counter].grWithChanges.l; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = myGraphs.anomalyList[counter].grWithChanges.S[i][j] + 1;
                }
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            counter--;
            button2.Enabled = true;
            showAnomaly();
            if (counter == 0)
            {
                button1.Enabled = false;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            counter = 0;
            button1.Enabled = false;
            labelAnomalyCount.Text = myGraphs.anomalyList.Count.ToString();           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            counter++;
            button1.Enabled = true;
            showAnomaly();

            if (counter == myGraphs.anomalyList.Count - 1)
            {
                button2.Enabled = false;
            }          
        }

        private void button4_Click(object sender, EventArgs e)
        {
            showAnomaly();
            if (myGraphs.anomalyList.Count > 1)
            {
                button2.Enabled = true;
            }
            if (counter == myGraphs.anomalyList.Count - 1)
            {
                button2.Enabled = false;
            }
        }      
    }
}
