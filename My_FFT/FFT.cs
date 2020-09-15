using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace My_FFT
{
    class FFT
    {
        private Complex[] W;
        public FFT(int N)
        {
            InitW(N);
        }
        /// <summary>
        /// 初始化旋转因子
        /// </summary>
        /// <param name="N"></param>
        /// <returns></returns>
        private void  InitW(int N)
        {
            W = new Complex[N];
            for(int i=0;i<W.Length;i++)
            {
                double real = Math.Cos(2 * Math.PI * i / N);
                double imag = -1 * Math.Sin(2 * Math.PI * i / N);
                W[i] = new Complex(real, imag);
            }
        }
        public void DoFFT(Complex[] x)
        {
            int i, j, k, l = 0;
            int N = x.Length;
            Complex up, down, product;
            ReverseOrder(x);
            for (i = 0; i < Math.Log(N, 2);i++)//确定变换的级数
            {
                l = 1 << i;
                for (j = 0; j < N; j += 2 *l)
                {
                    for(k=0;k<l;k++)
                    {
                        product = x[j + k + l] * W[N * k / 2 / l];
                        up = x[j + k]+product;//上半区
                        down = x[j + k] - product;//下半区
                        x[j + k] = up;
                        x[j + k + l] = down;
                    }
                }
            }
        }
        /// <summary>
        /// 倒位序反转
        /// </summary>
        /// <param name="x"></param>
        private void ReverseOrder(Complex[] x)
        {
            Complex tmp;
            int i = 0;
            int j = 0;
            int k = 0;
            double t;
            int N = x.Length;
            for( i=0;i<N;i++)
            {
                k = i;
                j = 0;
                t = Math.Log(N, 2);
                while((t--)>0)
                {
                    j = j << 1;
                    j |= (k & 1);
                    k = k >> 1;
                }
                if(j>i)
                {
                    tmp = x[i];
                    x[i] = x[j];
                    x[j] = tmp;
                }
            }
        }
        public Complex[] ToComplex(float[] re)
        {
            var complx = new Complex[re.Length];
            for(int i=0;i<re.Length;i++)
            {
                var comp = new Complex(re[i], 0);
                complx[i] = comp;
            }
            return complx;
        }
       

    }
}
