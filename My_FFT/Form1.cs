using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace My_FFT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double samp = 50;
            float[] test = new float[] { 0,1,2,3,4,5,6,7 };
            var fft= new FFT(test.Length);
            var fft_cpx = fft.ToComplex(test);
            fft.DoFFT(fft_cpx);
            var tb = new DataTable();
            tb.Columns.Add("Frequency", typeof(float));
            tb.Columns.Add("Amplitude", typeof(float));
            for(int i=0;i<fft_cpx.Length;i++)
            {
                var freq = samp / fft_cpx.Length*i;
                var amp = fft_cpx[i].Magnitude;
                tb.Rows.Add(freq, amp);
            }
            dataGridView1.DataSource = tb;
        }
    }
}
