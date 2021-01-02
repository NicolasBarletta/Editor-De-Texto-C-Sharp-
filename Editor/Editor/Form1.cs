
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
//using Microsoft.Office.Interop;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public string FileFilter = "Rich Textm Format|*.rtf*|Texto sem Formatação|*.txt*|All Files|*.*";
        public Form1()
        {
            InitializeComponent();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = FileFilter;
            if (ofd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                if (Path.GetExtension(ofd.FileName) == ".rtf")
                {
                    richTextBox1.LoadFile(ofd.FileName);
                }
                else if (Path.GetExtension(ofd.FileName) == ".txt")
                {
                    richTextBox1.Text = "";
                    StreamReader sr = new StreamReader(ofd.FileName);
                    string InputLine = "";
                    while ((InputLine = sr.ReadLine()) != null)
                    {
                        richTextBox1.Text += InputLine + "\n";
                    }
                }
                else
                {
                    richTextBox1.Text = "";
                    StreamReader sr = new StreamReader(ofd.FileName);
                    string InputLine = "";
                    while ((InputLine = sr.ReadLine()) != null)
                    {
                        richTextBox1.Text += InputLine + "\n";
                    }
                    sr.Close();
                }
            }


            /*openFileDialog1.ShowDialog();
            StreamReader leitor = new StreamReader(openFileDialog1.FileName);
            while (!leitor.EndOfStream)
            {
                textBox1.Text += leitor.ReadLine();
            }
            leitor.Close();
            leitor.Dispose();
            */

        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Salvar();


            /*saveFileDialog1.ShowDialog();
            StreamWriter escrita = new StreamWriter(saveFileDialog1.FileName);
            escrita.Write(richTextBox1.Text);
            escrita.Flush();
            escrita.Close(); */

        }
        public void Salvar()
        {
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = FileFilter;
                if (sfd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {

                    if (sfd.FilterIndex == 1)
                    {
                        //RTF
                        if (File.Exists(sfd.FileName + ".rtf"))
                        {
                            File.Delete(sfd.FileName + ".rtf");
                        }
                        richTextBox1.SaveFile(sfd.FileName + ".rtf");
                    }

                    else if (sfd.FilterIndex == 2)
                    {
                        //TXT
                        if (File.Exists(sfd.FileName + ".txt"))
                        {
                            File.Delete(sfd.FileName + ".txt");
                        }
                        richTextBox1.SaveFile(sfd.FileName);
                        StreamWriter sw = new StreamWriter(sfd.FileName + ".txt");
                        for (int linha = 0; linha < richTextBox1.Lines.Length; linha++)
                        {
                            sw.WriteLine(richTextBox1.Lines[linha]);
                        }
                        sw.Close();
                    }

                    else if (sfd.FilterIndex == 3)
                    {
                        //ALL
                        if (File.Exists(sfd.FileName))
                        {
                            File.Delete(sfd.FileName);
                        }
                        richTextBox1.SaveFile(sfd.FileName);
                        StreamWriter sw = new StreamWriter(sfd.FileName);
                        for (int linha = 0; linha < richTextBox1.Lines.Length; linha++)
                        {
                            sw.WriteLine(richTextBox1.Lines[linha]);
                        }
                        sw.Close();
                    }
                }
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Programa feito por Nicolas Barletta");
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            { 
                if (richTextBox1.Text.Length > 0)
                {
                    DialogResult dr = MessageBox.Show("Deseja Salvar seu Arquivo?", "Quetão", MessageBoxButtons.YesNo);
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        Salvar();
                    }
                richTextBox1.Text = "";
            }
                else
                {

                    richTextBox1.Text = "";
                }
            }
        }

        private void modificarFonteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.Font = richTextBox1.SelectionFont;
            if (fd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                richTextBox1.SelectionFont = fd.Font;
            }
        }

        private void modificarCorDaFonteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = richTextBox1.SelectionColor;
            if (cd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                richTextBox1.SelectionColor = cd.Color;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (richTextBox1.Text.Length > 0)
            {
                DialogResult dr = MessageBox.Show("Deseja Salvar seu Arquivo?", "Quetão", MessageBoxButtons.YesNoCancel);
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    Salvar();
                }
                if (dr == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }


        
    }
}
