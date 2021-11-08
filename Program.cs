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
	/// //----------------------------------------------------------------------//----------------------------------------------------------------//----------------------------------------------------------------------------
	class Program
    {
        static void Main(string[] args)
        {
			int eror;
			//int[] leyers = new int[] { 4, 2, 3, 5, 9 };
			//PrintArray(new int[] { 1, 3, 5, 7, 9 });
			double[] varianti_jautajumi = new double[] {
			0.0, 0.0,
			0.0, 1.0,
			1.0, 0.0,
			1.0, 1.0
			};
			double[] varianti_atbildes = new double[] {
			0.0,
			1.1,
			1.1,
			1.1
			};
			double[] jautajums = new double[] { 1.0, 1.0 };
			double[] atbilde = new double[1];
			NeironNetwork NC = new NeironNetwork(2, new int[] {2, 2}, 1);
			eror = NC.GetAnswer(ref jautajums, ref atbilde);
			Console.WriteLine(eror);
			NC.NcTrain(ref varianti_jautajumi, ref varianti_atbildes, 10);
			
			
		}
    }
}
