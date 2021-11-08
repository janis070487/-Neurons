using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace Neironi_mani
{
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
					else if (i > 0)
					{
						leyerH[i][j] = new Neiron(leyerH[i - 1].Length); // ja tas nau pirmais H slānis tad izveido neironu ar svariem tik cik bij iepriekšējā H slānī
					}
				}
			}
			leyerO = new Neiron[cikO]; // atbrīvo atmiņu O slanim
			int value = leyerH.Length - 1;
			for (int i = 0; i < cikO; i++)
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

			for (int i = 0; i < leyerH.Length; i++) // cikls pārlasa Hiden visus slāņus
			{
				if (i == 0) // Ja tas ir Hiden 1 slānis tad nostrādā šis for cikls
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
						for (int k = 0; k < leyerH[i - 1].Length; k++)
						{
							value += leyerH[i - 1][j].GetOutput() * leyerH[i][j].GetW(k);
						}
						leyerH[i][j].setInput(value);
						leyerH[i][j].activation();
					}
				}
				//else { return 1; }
			}
			for (int i = 0; i < leyerO.Length; i++) // Pārlasa izejas slāņa neironus
			{
				double value = 0.0;
				for (int j = 0; j < leyerH[leyerH.Length - 1].Length; j++) // cikls pārlasa noteikta neirona svarus
				{
					value += leyerH[leyerH.Length - 1][j].GetOutput() * leyerO[i].GetW(j);
					//Console.WriteLine((leyerH.Length - 1));
				}
				leyerO[i].setInput(value); // pietrūka šī rinda
				leyerO[i].activation();
			}
			return 1;

		}
		//----------------------------------------------------------------------------------------
		private void roO()
        {

        }
		public void NcTrain(ref double[] jautajumi, ref double[] atbildes, int paaudzes)
		{
			Random rnd = new Random();
			int grupas = jautajumi.Length / leyerI;
			
			for(int i = 0; i < paaudzes; i++) // cikls paaudzes
            {
				int value = rnd.Next(0, (grupas - 1));
				
				int ar_kuru = value * leyerI;
				int ar_kuruO = value * leyerO.Length;
				for (int j = 0; j < grupas; j++) // Iziet grupas
                {
					double[] ieejasDati = new double[leyerI];    //Parvietot metodes sakuma
					double[] izejasdati = new double[leyerO.Length];
					
					for(int k = 0; k < leyerI; k++)
                    {
						ieejasDati[k] = jautajumi[ar_kuru + k];
                    }
					for(int a = 0; a < leyerO.Length; a++)
                    {
						izejasdati[a] = atbildes[ar_kuruO + a];
                    }
					ar_kuru += leyerI;
					ar_kuruO += leyerO.Length;
					if(ar_kuru >= (jautajumi.Length - 1))
                    {
						ar_kuru = 0;
                    }
					if(ar_kuruO >= (atbildes.Length - 1))
                    {
						ar_kuruO = 0;
                    }
					//__________________________________________________________________
					//Seit jābūt apmācībai kas laiž signālu uz prieksu un tad atpakal un tad nosledzas i ēra
					GetAnswer(ref ieejasDati, ref izejasdati);
					for(int z = 0; z < leyerO.Length; z++) // Apreiķina ro izejas slānim
                    {
						leyerO[z].setRo(izejasdati[z]) ; // ro apreiķināšana konkrētam slānim
                    }

					//__________________________________________________________________

				}

			}
        }
	}
}
