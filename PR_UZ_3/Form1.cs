using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PR_UZ_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (primeNUMBERBox1.Text == String.Empty &&
                primeNUMBERBox2.Text == String.Empty &&
                textBoxPrivate.Text == String.Empty &&
                textBoxN.Text != String.Empty &&
                textBoxPublic.Text != String.Empty)
            {
                List<int> codedLimited = MainLogic.Encoder_Limited(textBox2.Text, Convert.ToInt32(textBoxN.Text),
                    Convert.ToInt32(textBoxPublic.Text));
                
                string encryptedTextLimited = null;
                foreach (int p in codedLimited)
                {
                    encryptedTextLimited += p + "/";

                }

                encryptedTextLimited = encryptedTextLimited.Remove(encryptedTextLimited.Length - 1);
                textBox3.Text = encryptedTextLimited;
            }
            else
            {
                try
                {
                    if (primeNUMBERBox1.Text != String.Empty)
                    {
                        if (primeNUMBERBox2.Text != String.Empty)
                        {
                            if (IsPrime(Convert.ToInt32(primeNUMBERBox1.Text)))
                            {
                                if (IsPrime(Convert.ToInt32(primeNUMBERBox2.Text)))
                                {
                                    Update();
                                    List<int> coded = MainLogic.Encoder(textBox2.Text);

                                    string encryptedText = null;
                                    foreach (int p in coded)
                                    {
                                        encryptedText += p + "/";

                                    }

                                    encryptedText = encryptedText.Remove(encryptedText.Length - 1);
                                    textBox3.Text = encryptedText;

                                }
                                else MessageBox.Show("All numbers need to be prime numbers.");
                            }
                            else MessageBox.Show("All numbers need to be prime numbers.");
                        }
                        else MessageBox.Show("Please enter the prime numbers.");
                    }
                    else MessageBox.Show("Please enter the prime numbers.");

                }
                catch (FormatException)
                {
                    MessageBox.Show("Please use numbers not letters.");
                }
            }





            /*
            if (primeNUMBERBox1.Text != String.Empty && primeNUMBERBox1.TextLength >= 8)
            {
                string key = primeNUMBERBox1.Text;
                string text = textBox2.Text;

                //textBox3.Text = MainLogic.Encrypt(text, key);
            }
            else if (primeNUMBERBox1.Text == String.Empty && primeNUMBERBox1.TextLength == 0)
            {
                MessageBox.Show("Please enter the key.");
            }
            else if (primeNUMBERBox1.Text != String.Empty && primeNUMBERBox1.TextLength < 8)
            {
                MessageBox.Show("The key needs to be the length of 8 characters.");
            }
            */
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (primeNUMBERBox1.Text != String.Empty)
                {
                    if (primeNUMBERBox2.Text != String.Empty)
                    {
                        if (IsPrime(Convert.ToInt32(primeNUMBERBox1.Text)))
                        {
                            if (IsPrime(Convert.ToInt32(primeNUMBERBox2.Text)))
                            {
                                Update();
                                string[] listOfStrings = textBox2.Text.Split('/');
                                
                                List<int> nonCoded = listOfStrings.Select(Int32.Parse).ToList();;
                                

                                textBox3.Text = MainLogic.Decoder(nonCoded);

                            }
                            else MessageBox.Show("All numbers need to be prime numbers.");
                        }
                        else MessageBox.Show("All numbers need to be prime numbers.");
                    }
                    else MessageBox.Show("Please enter the prime numbers.");
                }
                else MessageBox.Show("Please enter the prime numbers.");

            }
            catch (FormatException)
            {
                MessageBox.Show("Please use numbers not letters.");
                throw;
            }
            /*
            if (primeNUMBERBox1.Text != String.Empty && primeNUMBERBox1.TextLength >= 8)
            {
                string key = primeNUMBERBox1.Text;
                string text = textBox2.Text;
                
                //textBox3.Text = MainLogic.Decrypt(text, key);
            }
            else if (primeNUMBERBox1.Text == String.Empty && primeNUMBERBox1.TextLength == 0)
            {
                MessageBox.Show("Please enter the key.");
            }
            else if (primeNUMBERBox1.Text != String.Empty && primeNUMBERBox1.TextLength < 8)
            {
                MessageBox.Show("The key needs to be the length of 8 characters.");
            }
            */
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select File";
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Filter = "Text File (*.txt)|*.txt";
            openFileDialog.FilterIndex = 1;
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName != "")
            {
                string file_content = System.IO.File.ReadAllText(openFileDialog.FileName,Encoding.ASCII);
                string[] listOfStrings = file_content.Split(' ');
                textBoxN.Text = listOfStrings[1];
                textBoxPublic.Text = listOfStrings[0];
                textBox2.Text = listOfStrings[2];
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save File";
            saveFileDialog.InitialDirectory = @"C:\";
            saveFileDialog.Filter = "Text File (*.txt)|*.txt";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.ShowDialog();
            
            if (saveFileDialog.FileName != "")
            {
                string contents = textBoxPublic.Text + " " + textBoxN.Text + " " + textBox3.Text;
                System.IO.File.WriteAllText(saveFileDialog.FileName, contents);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox3.Clear();
            textBoxN.Clear();
            textBoxPrivate.Clear();
            textBoxPublic.Clear();
            primeNUMBERBox1.Clear();
            primeNUMBERBox2.Clear();
        }
        
        private static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));
          
            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;
    
            return true;        
        }

        private new void Update()
        {
            MainLogic.SetKeys(Convert.ToInt32(primeNUMBERBox1.Text),Convert.ToInt32(primeNUMBERBox2.Text));
            textBoxN.Text = Convert.ToString(MainLogic.N);
            textBoxPublic.Text = Convert.ToString(MainLogic.PublicKey);
            textBoxPrivate.Text = Convert.ToString(MainLogic.PrivateKey);
        }
        
    }
}