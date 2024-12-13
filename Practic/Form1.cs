using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Practic
{
    public partial class Form1 : Form
    {
        Graphics g;
        Graphs myTr;

        public Form1()
        {
            InitializeComponent();
        }
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
        private void button1_Click(object sender, EventArgs e)
        {
            string str = textBox1.Text;
            if (str == "" || !(int.TryParse(str, out int number)))
            {
                MessageBox.Show("Введіть ціле значення", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int count = int.Parse(textBox1.Text);
                if (count < 1 || count > 10)
                {
                    MessageBox.Show("Значення за межами допустимого проміжку", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    myTr.gr = new Graph();
                    myTr.ownGraph(count);
                    textBox1.Enabled = false;
                    button1.Enabled = false;
                    textBox2.Enabled = true;
                    button2.Enabled = true;
                    button5.Enabled = true;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            myTr = new Graphs();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string str = textBox2.Text;
            if (str == "" || !(int.TryParse(str, out int number)))
            {
                MessageBox.Show("Введіть ціле значення", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }
            else
            {
                int count = int.Parse(textBox2.Text);
                if (count < 1 || count > 10)
                {
                    MessageBox.Show("Значення за межами допустимого проміжку", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int a = myTr.addLevel(count);
                    if (a == myTr.gr.levels)
                    {
                        myTr.addKnots();
                        showGraph(myTr.gr);
                        textBox2.Enabled = false;
                        button2.Enabled = false;
                        textBox3.Enabled = true;
                        textBox4.Enabled = true;
                        button3.Enabled = true;
                        textBox5.Enabled = true;
                        button6.Enabled = true;
                    }
                    else
                    {
                        label2.Text = "Введіть кількість вершин на рівні " + (a + 1).ToString() + " (від 1 до 10):";
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string str1 = textBox3.Text;
            string str2 = textBox4.Text;
            if (str1 == "" || str2 == "" || !(int.TryParse(str1, out int number1)) || !(int.TryParse(str2, out int number2)))
            {
                MessageBox.Show("Введіть ціле значення номерів вершин", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int outKN = int.Parse(textBox3.Text);
                int inKN = int.Parse(textBox4.Text);
                outKN--;
                inKN--;

                if (outKN < 0 || outKN >= myTr.gr.knots.Count || inKN < 0 || inKN >= myTr.gr.knots.Count)
                {
                    MessageBox.Show("Такої вершини немає", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (myTr.gr.knots[outKN].level >= myTr.gr.knots[inKN].level)
                    {
                        MessageBox.Show("Зверніть увагу на попередження! Дуга повинна йти з верхнього рівня на нижній!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (myTr.gr.ifArcExist(outKN, inKN))
                        {
                            MessageBox.Show("Така дуга вже існує!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            myTr.addArc(outKN, inKN);
                            showGraph(myTr.gr);

                        }
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            listBox1.Items.Clear();
            textBox1.Enabled = true;
            button1.Enabled = true;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            textBox8.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button19.Enabled = false;
            label2.Text = "Введіть кількість вершин на рівні 1 (від 1 до 10):";
            label12.Text = "Введіть вагу вершини 1 (від 1 до 100):";
            labelL.Text = "_";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string str = textBox5.Text;
            if (str == "" || !(int.TryParse(str, out int number)))                        
            {
                MessageBox.Show("Введіть ціле значення", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int count = int.Parse(textBox5.Text);
                if (count < 1 || count > 10)
                {
                    MessageBox.Show("Значення за межами допустимого проміжку", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    myTr.gr.h = int.Parse(str);
                    myTr.fillPriorityList_L();
                    for (int i = 0; i < myTr.gr.L_priorityList.Count; i++)
                    {
                        listBox1.Items.Add(myTr.gr.L_priorityList[i] + 1);
                    }
                    textBox3.Enabled = false;
                    textBox4.Enabled = false;
                    textBox5.Enabled = false;
                    button3.Enabled = false;
                    button6.Enabled = false;
                    textBox8.Enabled = true;
                    button8.Enabled = true;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string str1 = textBox6.Text;
            string str2 = textBox7.Text;
            if (str1 == "" || str2 == "" || !(int.TryParse(str1, out int number1)) || !(int.TryParse(str2, out int number2)))
            {
                MessageBox.Show("Введіть ціле значення", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int knot_num = int.Parse(textBox6.Text);
                int position = int.Parse(textBox7.Text);
                knot_num--;
                position--;

                if (knot_num < 0 || knot_num >= myTr.gr.knots.Count || position < 0 || position >= myTr.gr.knots.Count)
                {
                    MessageBox.Show("Неправильно введено значення", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    myTr.changePositionInListL(knot_num, position);
                    listBox1.Items.Clear();

                    for (int i = 0; i < myTr.gr.L_priorityList.Count; i++)
                    {
                        listBox1.Items.Add(myTr.gr.L_priorityList[i] + 1);
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            button7.Enabled = false;
            button4.Enabled = false;
            button19.Enabled = true;

            myTr.gr.fillMatrixOfAdjacency();
            myTr.gr.findOrder();

            labelL.Text = myTr.gr.l.ToString();
            dataGridView1.RowCount = myTr.gr.h;
            dataGridView1.ColumnCount = myTr.gr.l;
            for (int i = 0; i < myTr.gr.h; i++)
            {
                for (int j = 0; j < myTr.gr.l; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = myTr.gr.S[i][j] + 1;
                }
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string str = textBox8.Text;
            if (str == "" || !(int.TryParse(str, out int number)))
            {
                MessageBox.Show("Введіть ціле значення", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int weight = int.Parse(textBox8.Text);
                if (weight < 1 || weight > 100)
                {
                    MessageBox.Show("Значення за межами допустимого проміжку", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int a = myTr.addWeight(weight);
                    if (a == myTr.gr.knots.Count)
                    {
                        textBox8.Enabled = false;
                        button8.Enabled = false;
                        textBox6.Enabled = true;
                        textBox7.Enabled = true;
                        button7.Enabled = true;
                        button4.Enabled = true;
                    }
                    else
                    {
                        label12.Text = "Введіть вагу вершини " + (a + 1).ToString() + " (від 1 до 100):";
                    }
                    showGraph(myTr.gr);
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            groupBox1.Visible = false;
            groupBox2.Visible = true;
            labelPr.Text = "";
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            listBox2.Items.Clear();
            labelCountProc.Text = "";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            groupBox2.Visible = false;
            dataGridView1.Rows.Clear();

            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            listBox1.Items.Clear();
            textBox1.Enabled = true;
            button1.Enabled = true;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button19.Enabled = false;
            label2.Text = "Введіть кількість вершин на рівні 1 (від 1 до 10):";
            label12.Text = "Введіть вагу вершини 1 (від 1 до 100):";
            labelL.Text = "_";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            listBox2.Items.Clear();

            Graphs gr = new Graphs();
            Graph tr = new Graph();
            gr.craeteGraph6(tr);

            tr.fillMatrixOfAdjacency();
            tr.findOrder();
            labelPr.Text = tr.l.ToString();
            dataGridView1.RowCount = tr.h;
            dataGridView1.ColumnCount = tr.l;
            for (int i = 0; i < tr.h; i++)
            {
                for (int j = 0; j < tr.l; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = tr.S[i][j] + 1;
                }
            }
            showGraph(tr);
            labelCountProc.Text = tr.h.ToString();
            for (int i = 0; i < tr.L_priorityList.Count; i++)
            {
                listBox2.Items.Add(tr.L_priorityList[i] + 1);
            }
        }
        private void button12_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            Graphs gr = new Graphs();
            Graph tr = new Graph();
            gr.craeteGraph6(tr);

            gr.findAnomalies(tr);
            frm2.myGraphs = gr;
            frm2.Show();

            /*
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            listBox2.Items.Clear();

            Graphs gr = new Graphs();
            Graph tr = new Graph();
            gr.craeteGraph1_2(tr);

            tr.fillMatrixOfAdjacency();
            tr.findOrder();
            labelPr.Text = tr.l.ToString();
            dataGridView1.RowCount = tr.h;
            dataGridView1.ColumnCount = tr.l;
            for (int i = 0; i < tr.h; i++)
            {
                for (int j = 0; j < tr.l; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = tr.S[i][j] + 1;
                }
            }
            showGraph(tr);
            labelCountProc.Text = tr.h.ToString();
            for (int i = 0; i < tr.L_priorityList.Count; i++)
            {
                listBox2.Items.Add(tr.L_priorityList[i] + 1);
            }
            */
        }
        private void button13_Click(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            listBox2.Items.Clear();


            Graphs gr = new Graphs();
            Graph tr = new Graph();
            gr.craeteGraph2_1(tr);

            tr.fillMatrixOfAdjacency();
            tr.findOrder();
            labelPr.Text = tr.l.ToString();
            dataGridView1.RowCount = tr.h;
            dataGridView1.ColumnCount = tr.l;
            for (int i = 0; i < tr.h; i++)
            {
                for (int j = 0; j < tr.l; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = tr.S[i][j] + 1;
                }
            }
            showGraph(tr);
            labelCountProc.Text = tr.h.ToString();
            for (int i = 0; i < tr.L_priorityList.Count; i++)
            {
                listBox2.Items.Add(tr.L_priorityList[i] + 1);
            }
        }
        private void button14_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            Graphs gr = new Graphs();
            Graph tr = new Graph();
            gr.craeteGraph2_1(tr);

            gr.findAnomalies(tr);
            frm2.myGraphs = gr;
            frm2.Show();

            /*
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            listBox2.Items.Clear();

            Graphs gr = new Graphs();
            Graph tr = new Graph();
            gr.craeteGraph2_2(tr);

            tr.fillMatrixOfAdjacency();
            tr.findOrder();
            labelPr.Text = tr.l.ToString();
            dataGridView1.RowCount = tr.h;
            dataGridView1.ColumnCount = tr.l;
            for (int i = 0; i < tr.h; i++)
            {
                for (int j = 0; j < tr.l; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = tr.S[i][j] + 1;
                }
            }
            showGraph(tr);
            labelCountProc.Text = tr.h.ToString();
            for (int i = 0; i < tr.L_priorityList.Count; i++)
            {
                listBox2.Items.Add(tr.L_priorityList[i] + 1);
            }
            */
        }
        private void button15_Click(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            listBox2.Items.Clear();

            Graphs gr = new Graphs();
            Graph tr = new Graph();
            gr.craeteGraph3_1(tr);

            tr.fillMatrixOfAdjacency();
            tr.findOrder();
            labelPr.Text = tr.l.ToString();
            dataGridView1.RowCount = tr.h;
            dataGridView1.ColumnCount = tr.l;
            for (int i = 0; i < tr.h; i++)
            {
                for (int j = 0; j < tr.l; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = tr.S[i][j] + 1;
                }
            }
            showGraph(tr);
            labelCountProc.Text = tr.h.ToString();
            for (int i = 0; i < tr.L_priorityList.Count; i++)
            {
                listBox2.Items.Add(tr.L_priorityList[i] + 1);
            }
        }
        private void button16_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            Graphs gr = new Graphs();
            Graph tr = new Graph();
            gr.craeteGraph3_1(tr);

            gr.findAnomalies(tr);
            frm2.myGraphs = gr;
            frm2.Show();

            /*Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            listBox2.Items.Clear();

            Graphs gr = new Graphs();
            Graph tr = new Graph();
            gr.craeteGraph3_2(tr);

            tr.fillMatrixOfAdjacency();
            tr.findOrder();
            labelPr.Text = tr.l.ToString();
            dataGridView1.RowCount = tr.h;
            dataGridView1.ColumnCount = tr.l;
            for (int i = 0; i < tr.h; i++)
            {
                for (int j = 0; j < tr.l; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = tr.S[i][j] + 1;
                }
            }
            showGraph(tr);
            labelCountProc.Text = tr.h.ToString();
            for (int i = 0; i < tr.L_priorityList.Count; i++)
            {
                listBox2.Items.Add(tr.L_priorityList[i] + 1);
            }*/
        }
        private void button17_Click(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            listBox2.Items.Clear();

            Graphs gr = new Graphs();
            Graph tr = new Graph();
            gr.craeteGraph4_1(tr);

            tr.fillMatrixOfAdjacency();
            tr.findOrder();
            labelPr.Text = tr.l.ToString();
            dataGridView1.RowCount = tr.h;
            dataGridView1.ColumnCount = tr.l;
            for (int i = 0; i < tr.h; i++)
            {
                for (int j = 0; j < tr.l; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = tr.S[i][j] + 1;
                }
            }
            showGraph(tr);
            labelCountProc.Text = tr.h.ToString();
            for (int i = 0; i < tr.L_priorityList.Count; i++)
            {
                listBox2.Items.Add(tr.L_priorityList[i] + 1);
            }
        }
        private void button18_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            Graphs gr = new Graphs();
            Graph tr = new Graph();
            gr.craeteGraph4_1(tr);

            gr.findAnomalies(tr);
            frm2.myGraphs = gr;
            frm2.Show();

            /*
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            listBox2.Items.Clear();

            Graphs gr = new Graphs();
            Graph tr = new Graph();
            gr.craeteGraph4_2(tr);

            tr.fillMatrixOfAdjacency();
            tr.findOrder();
            labelPr.Text = tr.l.ToString();
            dataGridView1.RowCount = tr.h;
            dataGridView1.ColumnCount = tr.l;
            for (int i = 0; i < tr.h; i++)
            {
                for (int j = 0; j < tr.l; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = tr.S[i][j] + 1;
                }
            }
            showGraph(tr);
            labelCountProc.Text = tr.h.ToString();
            for (int i = 0; i < tr.L_priorityList.Count; i++)
            {
                listBox2.Items.Add(tr.L_priorityList[i] + 1);
            }
            */
        }
        private void button19_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            myTr.gr.knotsAreNotProceed();
            myTr.findAnomalies(myTr.gr);
            if (myTr.anomalyList.Count != 0)
            {
                frm2.myGraphs = myTr;
                frm2.Show();
            }
            else
            {
                MessageBox.Show("Аномалій не знайдено", "Повідомлення", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}