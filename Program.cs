using System;
using static System.Math;
// breanch test
enum leyersType
{
	Ileyer,
	Hleyer,
	Oleyer
}
namespace Neironi_mani
{
	


	/// //----------------------------------------------------------------------
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
			for(int i = 0; i < cikW; i++)
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
	//----------------------------------------------------------------------
	public class NeironNetwork
	{
		private int leyerI;
		private Neiron[][] leyerH;
		private Neiron[] leyerO;
		public NeironNetwork(int cikI, int[] cikH, int cikO)
			
		{
			this.leyerI = cikI;
			
			leyerH = new Neiron[cikH.Length][]; // izveido vietu atmiņa masīvu ar H slāņiem
			for (int i = 0; i < cikH.Length; i++) // Aizpilda katru H slāni ar neironiem
			{
				leyerH[i] = new Neiron[cikH[i]]; // Pārlasa katru slāni
				for (int j = 0; j < leyerH[i].Length; j++)
                {
					if (i == 0)
					{
						leyerH[i][j] = new Neiron(leyerI); // ja tas ir pirmais H slanis tad izveido neironus ar tik svariem cik ir I slānī
					}
					else if(i > 0)
                    {
						leyerH[i][j] = new Neiron(leyerH[i - 1].Length); // ja tas nau pirmais H slānis tad izveido neironu ar svariem tik cik bij iepriekšējā H slānī
					}
                }
			}
			leyerO = new Neiron[cikO]; // atbrīvo atmiņu O slanim
			int value = leyerH.Length - 1;
			for(int i = 0; i < cikO; i++)
            {
				leyerO[i] = new Neiron(leyerH[value].Length);// aispilda O slāni ar neironiem
            }
		}
		//________________________________
		public int GetAnswer(ref double[] question, ref double[] answer)
        {
			if (question.Length != leyerI || answer.Length != leyerO.Length)
			{
				return 0;
			}
            //else { return 1; }

			for(int i = 0; i < leyerH.Length; i++) // cikls pārlasa Hiden visus slāņus
            {
				if(i == 0) // Ja tas ir Hiden 1 slānis tad nostrādā šis for cikls
                {
					
					for (int j = 0; j < leyerH[i].Length; j++)
                    {
						double value = 0.0;
						for (int k = 0; k < question.Length; k++)
						{
							value += question[k] * leyerH[i][j].GetW(k);
						}
						leyerH[i][j].setInput(value);
						leyerH[i][j].activation();

					}

                }
                else
                {
					for (int j = 0; j < leyerH[i].Length; j++)
					{
						double value = 0.0;
						for (int k = 0; k <leyerH[i - 1].Length; k++)
						{
							value += leyerH[i- 1][j].GetOutput() * leyerH[i][j].GetW(k);
						}
						leyerH[i][j].setInput(value);
						leyerH[i][j].activation();
					}
				}
				//else { return 1; }
			}
			for(int i = 0; i < leyerO.Length; i++) // Pārlasa izejas slāņa neironus
            {
				double value = 0.0;
				for (int j  = 0; j < leyerH[leyerH.Length - 1].Length; j++) // cikls pārlasa noteikta neirona svarus
                {
					value += leyerH[leyerH.Length - 1][j].GetOutput() * leyerO[i].GetW(j);
					//Console.WriteLine((leyerH.Length - 1));
                }
				leyerO[i].setInput(value); // pietrūka šī rinda
				leyerO[i].activation();
            }
			return 1;

		}
	}
	//----------------------------------------------------------------------------
	class Program
    {
        static void Main(string[] args)
        {
			int eror;
			//int[] leyers = new int[] { 4, 2, 3, 5, 9 };
			//PrintArray(new int[] { 1, 3, 5, 7, 9 });
			double[] jautajums = new double[] { 1.0, 1.0 };
			double[] atbilde = new double[1];
			NeironNetwork NC = new NeironNetwork(2, new int[] {2, 2}, 1);
			eror = NC.GetAnswer(ref jautajums, ref atbilde);
			Console.WriteLine(eror);
			
			
		}
    }
}
