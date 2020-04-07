using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace problem
{
public partial class Form1 : Form
    {
        public int reader_counter = 0;
        public int writer_counter = 0;
        public bool writterIsWorking = false;
        public bool readerIsWorkong = false;
        public class Node
        {
            public Control current;
            public int time;
            public int state;
            public string name;
        }
        public List<Node> FIFO = new List<Node>();//Очередь для компонентов
        public Form1()
        {
            InitializeComponent();
        }
        private void butWrit_Click(object sender, EventArgs e)
        {
            if (!writterIsWorking)
            {
                Node writer = new Node();
                ask temp = new ask();
                temp.ShowDialog();
                writer.time = Convert.ToInt32(temp.Mask);//Записываем время
                Button button = new Button();
                writer.current = button;//Сохраняем указатель на кнопку
                writer.name = "w";
                if(reader_counter != 0)
                {
                    writer.state = 2;//Ожидает завершения работы читателей
                }
                else
                {
                    writer.state = 1;//Работает
                }
                button.Size = new System.Drawing.Size(87, 23);
                this.splitContainer1.Panel2.Controls.Add(button);
                button.Location = new System.Drawing.Point(12, 42);
                button.Text = "Писатель";
                button.Name = "Writter";
                FIFO.Add(writer);
                writterIsWorking = true;//Появился писатель
            }
            else
            {
                MessageBox.Show("Сейчас работает другой писатель","Уведомление");
            }
        }

        private void butRead_Click(object sender, EventArgs e)
        { 
            reader_counter++;
            Node reader = new Node();
            ask temp = new ask();
            temp.ShowDialog();
            reader.time = Convert.ToInt32(temp.Mask);//Записали время
            Button button = new Button();
            reader.current = button;
            reader.name = "r";
            if (writterIsWorking)
            {
                reader.state = 2;//Если писатель работает, то читатель ждет
            }
            else
            {
                reader.state = 1;//Если писатель не работает, то читатель работает
            }
            button.Size = new System.Drawing.Size(87, 23);
            this.splitContainer1.Panel1.Controls.Add(button);
            button.Location = new System.Drawing.Point(12, 12 + 30 * reader_counter);
            button.Text = "Читатель" + reader_counter;
            button.Name = "Reader" + reader_counter;
            FIFO.Add(reader);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for(int i = 0; i < FIFO.LongCount(); i++)
            {
                if(FIFO[i].name == "w")//Находим писателя
                {
                    if(i == 0)//Если он первый в очереди
                    {
                        FIFO[i].current.BackColor = Color.Green;
                        writterIsWorking = true;
                    }
                    else
                    {
                        FIFO[i].current.BackColor = Color.Yellow;
                        writterIsWorking = false;
                    }

                    for(int j = i + 1; j < FIFO.LongCount(); j++)
                    {
                        FIFO[j].state = 2;//Делаем всех следующих читателей ожидающими
                        FIFO[j].current.BackColor = Color.Yellow;
                    }
                    break;
                }
                else
                {
                    FIFO[i].current.BackColor = Color.Green;
                    FIFO[i].state = 1;
                    FIFO[i].time--;
                    if(FIFO[i].time == 0)
                    {
                        reader_counter--;
                        Controls.Remove(FIFO[i].current);
                        FIFO[i].current.Dispose();
                        FIFO.RemoveAt(i);
                    }                    
                }
            }

            if (writterIsWorking)
            {
                FIFO[0].time--;
                if(FIFO[0].time == 0)
                {
                    Controls.Remove(FIFO[0].current);
                    FIFO[0].current.Dispose();
                    writterIsWorking = false;
                    FIFO.RemoveAt(0);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string test = Convert.ToString(FIFO.LongCount());
            MessageBox.Show(test);
        }
    }
}
