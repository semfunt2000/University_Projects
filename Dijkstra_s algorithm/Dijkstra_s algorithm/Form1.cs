using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace Dijkstra_s_algorithm
{
    public partial class Form1 : Form
    {
        int versh, s, j = 0;
        List<string> smej = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (j < 1)
                {
                    versh = int.Parse(textBox1.Text);
                    j++;
                    label1.Text = "Введите номер вершины";
                    textBox1.Text = "";
                }
                else
                {
                    s = int.Parse(textBox1.Text);
                    button2.Visible = true;
                    textBox2.Visible = true;
                    label2.Visible = true;
                    button1.Enabled = false;
                }
            }
            catch
            {
                MessageBox.Show("Проверьте правильность ввода!");
                Application.Restart();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (j != versh)
                {
                    label2.Text = "Введите построчно матрицу смежности. Сейчас нужно ввести " + ++j + " строку.";
                    smej.Add(textBox2.Text);
                    textBox2.Text = "";
                }
                else
                {
                    smej.Add(textBox2.Text);
                    textBox2.Text = "";
                    input(info(smej));
                    richTextBox1.Visible = true;
                    richTextBox2.Visible = true;
                    richTextBox2.Text = Dijkstra(info(smej));
                }
            }
            catch
            {
                MessageBox.Show("Проверьте правильность ввода!");
                Application.Restart();
            }
        }
        private int[,] info(List<string> smej)
        {
            int[,] num = new int[smej.Count, smej.Count];
            for (int i = 0; i < smej.Count; i++)
            {
                string[] line = smej[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < smej.Count; j++)
                {
                    num[i, j] = int.Parse(line[j]);
                }
            }
            return num;
        }
        private void input(int[,] num)
        {
            button2.Enabled = false;
            string smejn = "Матрица смежности:\n" + "        ";
            for (int i = 1; i <= versh; i++)
            {
                smejn += i.ToString() + ' ';
            }
            smejn += "\n\n";
            for (int i = 1; i <= versh; i++)
            {
                smejn += i.ToString() + "      ";
                for (int j = 1; j <= versh; j++)
                {
                    smejn += num[i - 1, j - 1].ToString() + " ";
                }
                smejn += "\n";
            }
            richTextBox1.Text = smejn;
        }
        private string Dijkstra(int[,] num)
        {
            int[] d = new int[versh];
            bool[] used = new bool[versh];
            for (int v = 0; v < versh; v++)
            {
                d[v] = int.MaxValue;
                used[v] = true;
            }
            d[s - 1] = 0;
            for (int i = 0; i < versh; i++)
            {
                int v = int.MinValue;
                for (int j = 0; j < versh; j++)
                {
                    if (used[j] && (v == int.MinValue || d[j] < d[v]))
                    {
                        v = j;
                    }
                }
                if (d[v] == int.MaxValue)
                    break;               
                used[v] = false;
                for (int e = 0; e < versh; e++)
                {
                    if (num[v, e] != 0)
                    {
                        if (d[v] + num[v, e] < d[e])
                            d[e] = d[v] + num[v, e];
                    }
                }
            }
            label3.Visible = true;
            label3.Text = "Кратчайшие пути из вершины " + s + ":";
            string dstr = null;
            for (int i = 1; i <= versh; i++)
            {
                dstr += i.ToString() + ' ';
            }
            dstr += "\n";
            for (int i = 0; i < versh; i++)
            {
                if(d[i] < int.MaxValue)
                    dstr += d[i].ToString() + ' ';
                else
                    dstr += "нет ";
            }        
                return dstr;
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}