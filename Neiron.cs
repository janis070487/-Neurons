using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;
namespace Neironi_mani
{
    public class Neiron
    {
		private double ro;
		// katram neironam savs ro
		private double[] w;
		// svari katrai ienākošai sinapsei
		//private double[] w_ie;
		private double[] grad;
		//grad gradients katram svaram savs
		private double input;
		//kopèjais inputs apreiķinas no katra svara
		// un atiecīgās ieejas
		private double output;
		// outputs apreiķinas input * activator
		public Neiron(int cikW)
		{
			Random rnd = new Random();
			w = new double[cikW];
			for (int i = 0; i < cikW; i++)
			{
				int value = rnd.Next(2, 80);
				w[i] = (double)value / 100;
			}
		}
		public double GetW(int kursW)
		{
			return w[kursW];
		}
		public void activation()
		{
			this.output = Pow(1 + Exp(-this.input), -1);
			//this.output = 1 / (1 + Exp(-this.input));
		}
		public void setInput(double value)
		{
			this.input = value;
		}
		public double GetOutput()
		{
			return this.output;

		}


		}
}
