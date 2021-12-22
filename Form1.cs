using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Laba2___VvSII
{
    public partial class Form1 : Form
    {
        string[] Рост = new string[] {"маленький","низкий","средний","высокий" };
        string[] Вес = new string[] { "легкий", "нормальный", "тяжелый"};

        //массив значений функций принадлежности роста при заданном росте
        double[] Рост_x = new double[4];
        Label[] labelsIf = new Label[3];
        //Массив сколько условий в каждом Правиле
        int[] nBox = new int[3];

        //Функция распределения Входногое значения роста
        double[] Около_Х = new double[4];

        double среднее_значение = 0;

        void setDefault()
        {
            //Значения по умолчанию
            //x0_маленький.Text = 0.2.ToString();
            //xd_маленький.Text = 1.0.ToString();

            //x0_низкий.Text = 1.4.ToString();
            //xd_низкий.Text = 0.4.ToString();

            //x0_средний.Text = 1.6.ToString();
            //xd_средний.Text = 0.4.ToString();

            //x0_высокий.Text = 2.0.ToString();
            //xd_высокий.Text = 0.6.ToString();

            //x0_легкий.Text = 2.0.ToString();
            //xd_легкий.Text = 50.0.ToString();

            //x0_нормальный.Text = 65.0.ToString();
            //xd_нормальный.Text = 25.0.ToString();

            //x0_тяжелый.Text = 150.0.ToString();
            //xd_тяжелый.Text = 70.0.ToString();

            //textBox1.Text = 1.3.ToString();

            x0_маленький.Text = 0.2.ToString();
            xd_маленький.Text = 0.8.ToString();

            x0_низкий.Text = 1.2.ToString();
            xd_низкий.Text = 0.4.ToString();

            x0_средний.Text = 1.6.ToString();
            xd_средний.Text = 0.4.ToString();

            x0_высокий.Text = 2.0.ToString();
            xd_высокий.Text = 0.2.ToString();

            x0_легкий.Text = 2.0.ToString();
            xd_легкий.Text = 35.0.ToString();

            x0_нормальный.Text = 60.0.ToString();
            xd_нормальный.Text = 30.0.ToString();

            x0_тяжелый.Text = 120.0.ToString();
            xd_тяжелый.Text = 40.0.ToString();

            textBox1.Text = 1.3.ToString();

            for (int i = 0; i < 3; i++)
                ((ComboBox)panel_If.Controls["cBox" + i + 0]).SelectedIndex = i;
            
        }

        void Init()
        {
            for (int i = 0; i < 3; i++)
            {
                labelsIf[i] = new Label();
                for (int j = 0; j < 3; j++)
                    //инициализация labels с текстом "или"                    
                 {
                        Label l = (Label)panel_If.Controls["labels" + i + j];
                        l.Size = new Size(25, 13);
                        l.Visible = false;
                        l.Location = new Point(120 + 85 * (j + 1) + j * 26, 15 * (i + 1) + i * 21);
                 }
            }

            labelsIf[0].Text = "Если ЛЕГКИЙ, то    ";
            labelsIf[1].Text = "Если НОРМ, то";
            labelsIf[2].Text = "Если ТЯЖЕЛЫЙ, то   ";


            for (int i = 0; i < 3; i++)
            {
                nBox[i] = 1;
                //panel_If.Controls["buttonRem" + i].Enabled = false;
                for (int j = 0; j < 4; j++)
                {
                    ComboBox c = (ComboBox)panel_If.Controls["cBox" + i + j];
                    c.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

                    c.Size = new Size(85, 21);
                    c.Location = new Point(120 + 85 * j + j * 26, 15 * (i + 1) + i * 21);
                    if (j != 0)
                        c.Visible = false;
                }

                labelsIf[i].Location = new Point(12, 15 * (i + 1) + i * 21);
                panel_If.Controls.Add(labelsIf[i]);
            }

            setDefault(); 
        }

        public Form1()
        {
            InitializeComponent();
            Init();          
        }

        private void buttonIf_Click(object sender, EventArgs e)
        {
            if ((sender as Button).Tag == "Add")
            {
                int indexButton;
                if ((sender as Button).Name == "buttonAdd1")
                    indexButton = 0;
                else if ((sender as Button).Name == "buttonAdd2")
                    indexButton = 1;
                else 
                    indexButton = 2;
                
                nBox[indexButton]++;                
                int i = nBox[indexButton] -1;
                ComboBox c = (ComboBox)panel_If.Controls["cBox" + indexButton + i]; 
               
                c.Visible = true;
                c.SelectedIndex = 0;

                

                panel_If.Controls["labels" + indexButton + (i-1)].Visible = true;
                                
                if (nBox[indexButton] > 1)
                    panel_If.Controls["buttonRem" + (indexButton + 1)].Enabled = true;
                
                if (nBox[indexButton] == 4)
                    panel_If.Controls["buttonAdd" + (indexButton + 1)].Enabled = false;
            }
            if ((sender as Button).Tag == "Remove")
            {
                int indexButton;
                if ((sender as Button).Name == "buttonRem1")
                    indexButton = 0;
                else if ((sender as Button).Name == "buttonRem2")
                    indexButton = 1;
                else
                    indexButton = 2;
                                
                int i = nBox[indexButton] - 1;
                ComboBox c = (ComboBox)panel_If.Controls["cBox" + indexButton + i];
                c.Visible = false;
                

                panel_If.Controls["labels" + indexButton + (i-1)].Visible = false;

                nBox[indexButton]--;

                if (nBox[indexButton] == 1)
                    panel_If.Controls["buttonRem" + (indexButton + 1)].Enabled = false;
                if (nBox[indexButton] < 4)
                    panel_If.Controls["buttonAdd" + (indexButton + 1)].Enabled = true;
            }
        }

        double S(double x, double a, double b)
        {
            double result;
            if (x <= a)
                return 0.0;
            else if (x <= (a + b) / 2)
                result = 2.0 * Math.Pow(((x - a) / (b - a)), 2);
            else if (x <= b)
                result = 1.0 - 2.0 * Math.Pow(((b - x) / (b - a)), 2);
            else
                return 1.0;
            return result;
        }

        double pi(double x, double a, double b, double d)
        {
            double result;
            if (x <= b)
                result = S(x, a, b);
            else
                result = 1 - S(x, b, d);
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            int N = 100;
            Chart chart;
            for (int iChart = 1; iChart < 3; iChart++)
            {
                chart = (Chart)Controls["chart" + iChart];
                chart.Series.Clear();
                double x, y, a, b, d, step;

                string[] param;
                int n;
                if (iChart == 1)
                {
                    n = 4;
                    param = Рост;
                    
                    chart.ChartAreas[0].AxisX.Interval = 0.1;
                    chart.ChartAreas[0].AxisX.Minimum = 0.2;
                }
                else
                {
                    n = 3;
                    param = Вес;
                    
                    chart.ChartAreas[0].AxisX.Interval = 10;
                    chart.ChartAreas[0].AxisX.Minimum = 2;
                }
                for (int i = 0; i < n; i++)
                {
                    chart.Series.Add(param[i]);
                    chart.Series[i].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                    chart.Series[i].BorderWidth = 2;
                    chart.ChartAreas[0].AxisX.LabelAutoFitMinFontSize = 14;
                    chart.ChartAreas[0].AxisY.LabelAutoFitMinFontSize = 14;
                    double x0 = Convert.ToDouble((panel.Controls["x0_" + param[i]]).Text);
                    double xd = Convert.ToDouble((panel.Controls["xd_" + param[i]]).Text);
                    a = x0 - xd;
                    b = x0;
                    d = x0 + xd;
                    if (i == 0)
                        x = Convert.ToDouble((panel.Controls["x0_" + param[0]]).Text);
                    else
                        x = a;

                    if (i < n-1)
                        step = (d - a) / N;
                    else
                        step = (Convert.ToDouble((panel.Controls["x0_" + param[n-1]]).Text) - a) / N;

                    for (int j = 0; j < N; j++)
                    {
                        y = pi(x, a, b, d);
                        chart.Series[i].Points.AddXY(x, y);
                        x += step;
                    }

                    if (iChart == 1)
                        Рост_x[i] = pi(Convert.ToDouble(textBox1.Text), a, b, d);
                }
            }


        }

        private double Round(double input) => Math.Round(input, 3);


        private void button3_Click(object sender, EventArgs e)
        {
            label_0.Text = "E1 = ";
            label_1.Text = "E2 = ";
            label_2.Text = "E3 = ";
            label_res_0.Text = "Л ≤ ";
            label_res_1.Text = "Н ≤ ";
            label_res_2.Text = "Т ≤ ";
            result.Text = "Диапазон соблюдения условий:\n";

            double[] max = new double[3];
            double value;
            for (int i = 0; i < 3; i++)
            {
                max[i] = 0;

                for (int j = 0; j < nBox[i]; j++)
                {
                    value = Round(Рост_x[((ComboBox)panel_If.Controls["cBox" + i + j]).SelectedIndex]);

                    if (j == 0)
                        panel_result.Controls["label_" + i].Text += "max{";
                    panel_result.Controls["label_" + i].Text += ("" + value.ToString() + ";");
                    if (j == nBox[i] - 1)
                        panel_result.Controls["label_" + i].Text += "}";


                    if (max[i] < value)
                        max[i] = value;
                }

                panel_result.Controls["label_" + i].Text += (" = " + max[i]);
            }
            //Сортировка E
            /*double k;
            for (int i = 0; i < 3; i++)
            {
                for (int j = i + 1; j < 3; j++)
                    if (max[i] < max[j])
                    {
                        k = max[i];
                        max[i] = max[j]; 
                        max[j] = k;
                    }

            }*/
            for (int i = 0; i < 3; i++)            
                panel_result.Controls["label_res_" + i].Text += max[i];
            

            double[] interval_1 = new double[2];
            double[] interval_2 = new double[4];
            double[] interval_3 = new double[2];

            double x, y, a, b, d, step;
            int N = 100;
            for (int i = 0; i < 3; i++)
            {
                double x0 = Convert.ToDouble((panel.Controls["x0_" + Вес[i]]).Text);
                double xd = Convert.ToDouble((panel.Controls["xd_" + Вес[i]]).Text);
                a = x0 - xd;
                b = x0;
                d = x0 + xd;

                if (i == 0)
                    x = Convert.ToDouble((panel.Controls["x0_" + Вес[0]]).Text);
                else
                    x = a;

                if (i < 2)
                    step = (d - a) / N;
                else
                    step = (Convert.ToDouble((panel.Controls["x0_" + Вес[2]]).Text) - a) / N;

                if (i == 0)
                {
                    y = pi(x, a, b, d);
                    while (y > max[i])
                    {
                        x += step;
                        y = pi(x, a, b, d);
                    }
                    interval_1[0] = x;
                    interval_1[1] = 150;
                    panel_result.Controls["label_res_" + i].Text += (" при [" + Round(x) + "; " + "150]");
                           
                }
                else if (i == 1)
                {
                    y = pi(x, a, b, d);
                    while (y <= max[i])
                    {
                        x += step;
                        y = pi(x, a, b, d);
                    }

                    interval_2[0] = 2;
                    interval_2[1] = x;
                    panel_result.Controls["label_res_" + i].Text += (" при [2; " + Round(x) + "]");

                    y = pi(x, a, b, d);
                    while (y > max[i])
                    {
                        x += step;
                        y = pi(x, a, b, d);
                    }

                    interval_2[2] = x;
                    interval_2[3] = 150;
                    panel_result.Controls["label_res_" + i].Text += (" V [" + Round(x) + "; " + "150]");

                }
                else 
                {
                    y = pi(x, a, b, d);
                    while (y <= max[i])
                    { 
                        x += step;
                        y = pi(x, a, b, d);
                    }                       
                        
                     interval_3[0] = 2;
                    interval_3[1] = x;
                    panel_result.Controls["label_res_" + i].Text += (" при [2; " + string.Format("{0:0.##}", x) + "]");
                }
            }
        
            double []_interval = new double[2];
            _interval[0] = interval_1[0];
            _interval[1] = interval_3[1];


            double[] res_interval;
            if (interval_2[1] == interval_2[2])
            {
                res_interval = new double[] { _interval[0], _interval[1] };
            }
            else
            {
                res_interval = new double[] { _interval[0], interval_2[1], interval_2[2], _interval[1] };
            }
            double[] result_interval;
            if (res_interval[0] <= res_interval[1])
            {
                if (res_interval[2] <= res_interval[3])
                    result_interval = new double[] { res_interval[0], res_interval[1], res_interval[2], res_interval[3] };
                else
                    result_interval = new double[] { res_interval[0], res_interval[1] };
            }
            else
            {
                if (res_interval[2] <= res_interval[3])
                    result_interval = new double[] { res_interval[2], res_interval[3] };
                else
                    result_interval = new double[] { 0,0 };
            }

                result.Text += "[" + Round(result_interval[0]) + "; " + Round(result_interval[1]) + "]";
            if (result_interval.Length >2)
                result.Text += " V [" + Round(result_interval[2]) + "; " + string.Format("{0:0.##}", Round(result_interval[3])) + "]";

        }
    }
}
