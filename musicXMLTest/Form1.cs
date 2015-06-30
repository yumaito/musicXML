using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using musicXml;
using musicXml.NoteElements;
using musicXml.AttributeElements;

namespace musicXMLTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        InputMusicXml currentFile;

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                currentFile = new InputMusicXml(openFileDialog1.FileName);

                //label1.Text = "";
                textBox1.Text = "";
                //for(int i = 0;i<currentFile.Track[0].Notes.Count;i++)
                //{
                //    textBox1.Text += "(" + currentFile.Track[0].Notes[i].Notation.Tech.StringNum.ToString()
                //        + "," + currentFile.Track[0].Notes[i].Notation.Tech.FretNum.ToString() + ")";
                //    //textBox1.Text = Environment.NewLine;

                //}
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = saveFileDialog1.FileName;
                string title = "test music";
                string gerenerator = "musicXML library";
                PartClass part = new PartClass("P1");
                //音符リスト
                //List<Note> notes = new List<Note>();
                //notes.Add(new Note())
                //
                //Track track = new Track()
            }
        }
    }
}
