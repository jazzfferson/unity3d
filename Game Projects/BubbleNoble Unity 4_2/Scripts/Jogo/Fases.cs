using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fases : MonoBehaviour {

	public static List<Level> leveis;
	
	public static int faseAtual;
	public static int NumeroDeFases = 51;
	public static int FasesDestravadas
	{
		#region Get
		get
		{
	          if(PlayerPrefs.HasKey("fasesDestravadas"))
			{
				return PlayerPrefs.GetInt("fasesDestravadas");
			}
		   else
		    {
				PlayerPrefs.SetInt("fasesDestravadas",1);
				return PlayerPrefs.GetInt("fasesDestravadas");
		    }
		}
		#endregion
		
		#region Set
		
		set
		{
			
			PlayerPrefs.SetInt("fasesDestravadas",value);
			PlayerPrefs.Save();
		}
		#endregion
	}
	
	
	void Awake()
	{
		
		leveis = new List<Level>(NumeroDeFases);
		
		
		#region Level 1
		
		leveis.Add(new Level(0,1));
		
		AddCasaInfase(0,2,3);
		AddCasaInfase(0,1,2);
		AddCasaInfase(0,2,2);
		AddCasaInfase(0,3,2);
		AddCasaInfase(0,2,1);
		

	
		#endregion
		
		#region Level 2

        leveis.Add(new Level(1, 1));

		AddCasaInfase(1,3,4);
		AddCasaInfase(1,4,4);
		AddCasaInfase(1,4,3);
	
		#endregion
		
		#region Level 3


        leveis.Add(new Level(2, 3));

		AddCasaInfase(2,0,4);
		AddCasaInfase(2,1,4);
		AddCasaInfase(2,0,3);
		AddCasaInfase(2,2,3);
		AddCasaInfase(2,2,2);
		AddCasaInfase(2,1,2);
		AddCasaInfase(2,3,2);
		AddCasaInfase(2,2,1);
		AddCasaInfase(2,4,1);
		AddCasaInfase(2,3,0);
		AddCasaInfase(2,4,0);
		
		#endregion
		
		#region Level 4
        leveis.Add(new Level(3, 4));
		
		AddCasaInfase(3,0,4);
		AddCasaInfase(3,1,4);
		AddCasaInfase(3,0,3);
		AddCasaInfase(3,0,1);
		AddCasaInfase(3,0,0);
		AddCasaInfase(3,1,0);
		AddCasaInfase(3,3,4);
		AddCasaInfase(3,4,4);
		AddCasaInfase(3,4,3);
		AddCasaInfase(3,4,1);
		AddCasaInfase(3,3,0);
		AddCasaInfase(3,4,0);
		#endregion
	
		#region Level 5

        leveis.Add(new Level(4, 5));

		AddCasaInfase(4,0,4);
		AddCasaInfase(4,1,4);
		AddCasaInfase(4,3,4);
		AddCasaInfase(4,4,4);
		AddCasaInfase(4,0,3);
		AddCasaInfase(4,2,3);
		AddCasaInfase(4,4,3);
		AddCasaInfase(4,1,2);
		AddCasaInfase(4,2,2);
		AddCasaInfase(4,3,2);
		AddCasaInfase(4,0,1);
		AddCasaInfase(4,2,1);
		AddCasaInfase(4,4,1);
		AddCasaInfase(4,0,0);
		AddCasaInfase(4,1,0);
		AddCasaInfase(4,3,0);
		AddCasaInfase(4,4,0);

		
		#endregion
		
		#region Level 6

        leveis.Add(new Level(5, 1));

		AddCasaInfase(5,0,3);
		AddCasaInfase(5,0,2);
		AddCasaInfase(5,1,2);
		AddCasaInfase(5,0,1);

		
		
		#endregion
		
		#region Level 7

        leveis.Add(new Level(6, 4));

		AddCasaInfase(6,1,4);
		AddCasaInfase(6,2,4);
		AddCasaInfase(6,3,4);
		AddCasaInfase(6,0,3);
		AddCasaInfase(6,2,3);
		AddCasaInfase(6,4,3);
		AddCasaInfase(6,0,2);
		AddCasaInfase(6,1,2);
		AddCasaInfase(6,3,2);
		AddCasaInfase(6,4,2);
		AddCasaInfase(6,0,1);
		AddCasaInfase(6,2,1);
		AddCasaInfase(6,4,1);
		AddCasaInfase(6,1,0);
		AddCasaInfase(6,2,0);
		AddCasaInfase(6,3,0);
		
		#endregion
		
		#region Level 8

        leveis.Add(new Level(7, 5));
		
		AddCasaInfase(7,0,4);
		AddCasaInfase(7,2,4);
		AddCasaInfase(7,4,4);
		AddCasaInfase(7,0,3);
		AddCasaInfase(7,2,3);
		AddCasaInfase(7,4,3);
		AddCasaInfase(7,1,2);
		AddCasaInfase(7,3,2);
		AddCasaInfase(7,0,1);
		AddCasaInfase(7,1,1);
		AddCasaInfase(7,3,1);
		AddCasaInfase(7,4,1);
		AddCasaInfase(7,1,0);
		AddCasaInfase(7,3,0);

		#endregion
		
		#region Level 9

        leveis.Add(new Level(8, 6));
		
 		AddCasaInfase(8,0,4);
		AddCasaInfase(8,4,4);
		AddCasaInfase(8,0,3);
		AddCasaInfase(8,1,3);
		AddCasaInfase(8,3,3);
		AddCasaInfase(8,4,3);
		AddCasaInfase(8,2,2);
		AddCasaInfase(8,0,1);
		AddCasaInfase(8,2,1);
		AddCasaInfase(8,0,0);
		AddCasaInfase(8,2,0);
		AddCasaInfase(8,3,0);

		
		#endregion
		
		#region Level 10

        leveis.Add(new Level(9, 5));
		
		AddCasaInfase(9,0,4);
		AddCasaInfase(9,1,4);
		AddCasaInfase(9,4,3);
		AddCasaInfase(9,0,2);
		AddCasaInfase(9,1,2);
		AddCasaInfase(9,3,2);
		AddCasaInfase(9,4,2);
		AddCasaInfase(9,0,0);
		AddCasaInfase(9,1,0);
		AddCasaInfase(9,3,0);
		AddCasaInfase(9,4,0);
		#endregion
		
		#region Level 11

        leveis.Add(new Level(10, 5));
		
		AddCasaInfase(10,4,4);
		AddCasaInfase(10,3,3);
		AddCasaInfase(10,2,2);
		AddCasaInfase(10,1,1);
		AddCasaInfase(10,0,0);
		
		#endregion
		
		#region Level 12

        leveis.Add(new Level(11, 5));
		
		AddCasaInfase(11,0,4);
		AddCasaInfase(11,1,3);
		AddCasaInfase(11,2,2);
		AddCasaInfase(11,3,1);
		AddCasaInfase(11,4,0);

		
		#endregion
		
		#region Level 13

        leveis.Add(new Level(12, 3));
		
		AddCasaInfase(12,3,3);
		AddCasaInfase(12,2,2);
		AddCasaInfase(12,4,2);
		AddCasaInfase(12,0,1);
		AddCasaInfase(12,2,1);
		AddCasaInfase(12,4,1);
		AddCasaInfase(12,0,0);
		AddCasaInfase(12,1,0);
		AddCasaInfase(12,3,0);
		
		#endregion
		
		#region Level 14

        leveis.Add(new Level(13, 3));
		
		AddCasaInfase(13,1,4);
		AddCasaInfase(13,3,4);
		AddCasaInfase(13,1,3);
		AddCasaInfase(13,3,3);
		AddCasaInfase(13,1,1);
		AddCasaInfase(13,2,1);
		AddCasaInfase(13,3,1);
		AddCasaInfase(13,2,0);
		
		#endregion
		
		#region Level 15

        leveis.Add(new Level(14, 4));
		
		AddCasaInfase(14,0,4);
		AddCasaInfase(14,1,4);
		AddCasaInfase(14,3,4);
		AddCasaInfase(14,4,4);
		AddCasaInfase(14,0,3);
		AddCasaInfase(14,2,3);
		AddCasaInfase(14,4,3);
		AddCasaInfase(14,1,2);
		AddCasaInfase(14,2,2);
		AddCasaInfase(14,3,2);
		AddCasaInfase(14,1,1);
		AddCasaInfase(14,2,1);
		AddCasaInfase(14,0,0);
		AddCasaInfase(14,1,0);
		AddCasaInfase(14,2,0);

		
		#endregion
		
		#region Level 16

        leveis.Add(new Level(15, 9));

		AddCasaInfase(15,0,4);
		AddCasaInfase(15,4,4);
		AddCasaInfase(15,1,3);
		AddCasaInfase(15,2,3);
		AddCasaInfase(15,3,3);
		AddCasaInfase(15,1,2);
		AddCasaInfase(15,2,2);
		AddCasaInfase(15,3,2);
		AddCasaInfase(15,1,1);
		AddCasaInfase(15,2,1);
		AddCasaInfase(15,3,1);
		AddCasaInfase(15,0,0);
		AddCasaInfase(15,4,0);

		#endregion
		
		#region Level 17

        leveis.Add(new Level(16, 6));
		
		AddCasaInfase(16,1,4);
		AddCasaInfase(16,2,4);
		AddCasaInfase(16,3,4);
		AddCasaInfase(16,2,3);
		AddCasaInfase(16,0,2);
		AddCasaInfase(16,2,2);
		AddCasaInfase(16,4,2);
		AddCasaInfase(16,0,1);
		AddCasaInfase(16,2,1);
		AddCasaInfase(16,4,1);
		AddCasaInfase(16,1,0);
		AddCasaInfase(16,2,0);
		AddCasaInfase(16,3,0);
		
		#endregion
		
		#region Level 18

        leveis.Add(new Level(17, 8));
		
		AddCasaInfase(17,1,4);
		AddCasaInfase(17,4,4);
		AddCasaInfase(17,0,3);
		AddCasaInfase(17,1,3);
		AddCasaInfase(17,2,3);
		AddCasaInfase(17,3,3);
		AddCasaInfase(17,1,2);
		AddCasaInfase(17,2,2);
		AddCasaInfase(17,3,2);
		AddCasaInfase(17,1,1);
		AddCasaInfase(17,2,1);
		AddCasaInfase(17,3,1);
		AddCasaInfase(17,0,0);
		AddCasaInfase(17,4,0);

		
		#endregion
		
		#region Level 19

        leveis.Add(new Level(18, 7));
		
	  	AddCasaInfase(18,0,4);
		AddCasaInfase(18,2,4);
		AddCasaInfase(18,0,3);
		AddCasaInfase(18,2,3);
		AddCasaInfase(18,3,3);
		AddCasaInfase(18,4,3);
		AddCasaInfase(18,2,2);
		AddCasaInfase(18,0,1);
		AddCasaInfase(18,1,1);
		AddCasaInfase(18,2,1);
		AddCasaInfase(18,4,1);
		AddCasaInfase(18,2,0);
		AddCasaInfase(18,4,0);

		
		#endregion
		
		#region Level 20

        leveis.Add(new Level(19, 4));
		
		AddCasaInfase(19,1,4);
		AddCasaInfase(19,0,3);
		AddCasaInfase(19,2,3);
		AddCasaInfase(19,0,2);
		AddCasaInfase(19,1,2);
		AddCasaInfase(19,2,2);
		AddCasaInfase(19,3,2);
		AddCasaInfase(19,0,1);
		AddCasaInfase(19,3,1);
		AddCasaInfase(19,4,1);
		AddCasaInfase(19,1,0);
		AddCasaInfase(19,3,0);

		
		#endregion
		
		#region Level 21

        leveis.Add(new Level(20, 15));
	
		AddCasaInfase(20,0,4);
		AddCasaInfase(20,1,4);
		AddCasaInfase(20,2,4);
		AddCasaInfase(20,3,4);
		AddCasaInfase(20,4,4);
		AddCasaInfase(20,0,3);
		AddCasaInfase(20,1,3);
		AddCasaInfase(20,2,3);
		AddCasaInfase(20,3,3);
		AddCasaInfase(20,4,3);
		AddCasaInfase(20,0,2);
		AddCasaInfase(20,1,2);
		AddCasaInfase(20,2,2);
		AddCasaInfase(20,3,2);
		AddCasaInfase(20,4,2);
		AddCasaInfase(20,0,1);
		AddCasaInfase(20,1,1);
		AddCasaInfase(20,2,1);
		AddCasaInfase(20,3,1);
		AddCasaInfase(20,4,1);
		AddCasaInfase(20,0,0);
		AddCasaInfase(20,1,0);
		AddCasaInfase(20,2,0);
		AddCasaInfase(20,3,0);
		AddCasaInfase(20,4,0);
		#endregion
		
		#region Level 22

        leveis.Add(new Level(21, 15));

		AddCasaInfase(21,0,4);
		AddCasaInfase(21,4,4);
		AddCasaInfase(21,0,3);
		AddCasaInfase(21,1,3);
		AddCasaInfase(21,3,3);
		AddCasaInfase(21,4,3);
		AddCasaInfase(21,0,2);
		AddCasaInfase(21,1,2);
		AddCasaInfase(21,2,2);
		AddCasaInfase(21,3,2);
		AddCasaInfase(21,4,2);
		AddCasaInfase(21,0,1);
		AddCasaInfase(21,1,1);
		AddCasaInfase(21,3,1);
		AddCasaInfase(21,4,1);
		AddCasaInfase(21,0,0);
		AddCasaInfase(21,4,0);
		
		#endregion
		
		#region Level 23

        leveis.Add(new Level(22, 10));
		
		AddCasaInfase(22,0,4);
		AddCasaInfase(22,1,4);
		AddCasaInfase(22,2,4);
		AddCasaInfase(22,3,4);
		AddCasaInfase(22,4,4);
		AddCasaInfase(22,0,3);
		AddCasaInfase(22,4,3);
		AddCasaInfase(22,0,2);
		AddCasaInfase(22,4,2);
		AddCasaInfase(22,0,1);
		AddCasaInfase(22,4,1);
		AddCasaInfase(22,0,0);
		AddCasaInfase(22,1,0);
		AddCasaInfase(22,2,0);
		AddCasaInfase(22,3,0);
		AddCasaInfase(22,4,0);

		
		#endregion
		
		#region Level 24

        leveis.Add(new Level(23, 10));

		AddCasaInfase(23,0,4);
		AddCasaInfase(23,1,4);
		AddCasaInfase(23,2,4);
		AddCasaInfase(23,0,3);
		AddCasaInfase(23,1,3);
		AddCasaInfase(23,0,2);
		AddCasaInfase(23,4,2);
		AddCasaInfase(23,3,1);
		AddCasaInfase(23,4,1);
		AddCasaInfase(23,2,0);
		AddCasaInfase(23,3,0);
		AddCasaInfase(23,4,0);
		
		#endregion
		
		#region Level 25

        leveis.Add(new Level(24, 11));
		
		AddCasaInfase(24,0,4);
		AddCasaInfase(24,1,4);
		AddCasaInfase(24,2,4);
		AddCasaInfase(24,3,4);
		AddCasaInfase(24,4,4);
		AddCasaInfase(24,0,3);
		AddCasaInfase(24,2,3);
		AddCasaInfase(24,4,3);
		AddCasaInfase(24,0,2);
		AddCasaInfase(24,1,2);
		AddCasaInfase(24,2,2);
		AddCasaInfase(24,3,2);
		AddCasaInfase(24,4,2);
		AddCasaInfase(24,0,1);
		AddCasaInfase(24,2,1);
		AddCasaInfase(24,4,1);
		AddCasaInfase(24,0,0);
		AddCasaInfase(24,1,0);
		AddCasaInfase(24,2,0);
		AddCasaInfase(24,3,0);
		AddCasaInfase(24,4,0);
		#endregion
		
		#region Level 26

        leveis.Add(new Level(25, 15));
		
		AddCasaInfase(25,0,4);
		AddCasaInfase(25,4,4);
		AddCasaInfase(25,1,3);
		AddCasaInfase(25,3,3);
		AddCasaInfase(25,2,2);
		AddCasaInfase(25,1,1);
		AddCasaInfase(25,3,1);
		AddCasaInfase(25,0,0);
		AddCasaInfase(25,4,0);
		
		#endregion
		
		#region Level 27

        leveis.Add(new Level(26, 10));
		
		AddCasaInfase(26,0,4);
		AddCasaInfase(26,1,4);
		AddCasaInfase(26,2,4);
		AddCasaInfase(26,3,4);
		AddCasaInfase(26,4,4);
		AddCasaInfase(26,1,3);
		AddCasaInfase(26,2,3);
		AddCasaInfase(26,3,3);
		AddCasaInfase(26,4,3);
		AddCasaInfase(26,1,2);
		AddCasaInfase(26,2,2);
		AddCasaInfase(26,3,2);
		AddCasaInfase(26,4,2);
		AddCasaInfase(26,1,1);
		AddCasaInfase(26,2,1);
		AddCasaInfase(26,3,1);
		AddCasaInfase(26,4,1);
		AddCasaInfase(26,4,0);
		AddCasaInfase(26,0,4);
		AddCasaInfase(26,1,4);
		AddCasaInfase(26,2,4);
		AddCasaInfase(26,3,4);
		AddCasaInfase(26,4,4);
		AddCasaInfase(26,1,3);
		AddCasaInfase(26,2,3);
		AddCasaInfase(26,3,3);
		AddCasaInfase(26,4,3);
		AddCasaInfase(26,1,2);
		AddCasaInfase(26,2,2);
		AddCasaInfase(26,3,2);
		AddCasaInfase(26,4,2);
		AddCasaInfase(26,1,1);
		AddCasaInfase(26,2,1);
		AddCasaInfase(26,3,1);
		AddCasaInfase(26,4,1);
		AddCasaInfase(26,4,0);
				
		#endregion
		
		#region Level 28

        leveis.Add(new Level(27, 12));
		
		AddCasaInfase(27,3,4);
		AddCasaInfase(27,4,4);
		AddCasaInfase(27,3,3); 
		AddCasaInfase(27,4,3);
		AddCasaInfase(27,3,2);
		AddCasaInfase(27,4,2);
		AddCasaInfase(27,0,1);
		AddCasaInfase(27,1,1);
		AddCasaInfase(27,2,1);
		AddCasaInfase(27,3,1);
		AddCasaInfase(27,4,1);
		AddCasaInfase(27,0,0);
		AddCasaInfase(27,1,0);
		AddCasaInfase(27,2,0);
		AddCasaInfase(27,3,0);
		AddCasaInfase(27,4,0);
		#endregion
		
		#region Level 29

        leveis.Add(new Level(28, 11));
		
		AddCasaInfase(28,0,4);
		AddCasaInfase(28,1,4);
		AddCasaInfase(28,2,4);
		AddCasaInfase(28,3,4);
		AddCasaInfase(28,4,4);
		AddCasaInfase(28,0,3);
		AddCasaInfase(28,1,3);
		AddCasaInfase(28,3,3);
		AddCasaInfase(28,4,3);
		AddCasaInfase(28,0,2);
		AddCasaInfase(28,1,2);
		AddCasaInfase(28,3,2);
		AddCasaInfase(28,4,2);
		AddCasaInfase(28,0,1);
		AddCasaInfase(28,4,1);
		AddCasaInfase(28,0,0);
		AddCasaInfase(28,1,0);
		AddCasaInfase(28,2,0);
		AddCasaInfase(28,3,0);
		AddCasaInfase(28,4,0);
		#endregion
		
		#region Level 30

        leveis.Add(new Level(29, 14));
		
		AddCasaInfase(29,0,4);
		AddCasaInfase(29,1,4);
		AddCasaInfase(29,2,4);
		AddCasaInfase(29,3,4);
		AddCasaInfase(29,4,4);
		AddCasaInfase(29,0,3);
		AddCasaInfase(29,1,3);
		AddCasaInfase(29,2,3);
		AddCasaInfase(29,3,3);
		AddCasaInfase(29,4,3);
		AddCasaInfase(29,0,2);
		AddCasaInfase(29,1,2);
		AddCasaInfase(29,2,2);
		AddCasaInfase(29,3,2);
		AddCasaInfase(29,4,2);
		AddCasaInfase(29,0,0);
		AddCasaInfase(29,4,0);

		#endregion
		
		#region Level 31

        leveis.Add(new Level(30, 11));
		
		AddCasaInfase(30,0,4);
		AddCasaInfase(30,4,4);
		AddCasaInfase(30,0,3);
		AddCasaInfase(30,4,3);
		AddCasaInfase(30,0,2);
		AddCasaInfase(30,1,2);
		AddCasaInfase(30,2,2);
		AddCasaInfase(30,3,2);
		AddCasaInfase(30,4,2);
		AddCasaInfase(30,0,1);
		AddCasaInfase(30,4,1);
		AddCasaInfase(30,0,0);
		AddCasaInfase(30,4,0);


		#endregion
		
		#region Level 32

        leveis.Add(new Level(31, 11));
		
		AddCasaInfase(31,2,3);
		AddCasaInfase(31,3,3);
		AddCasaInfase(31,1,2);
		AddCasaInfase(31,2,2);
		AddCasaInfase(31,3,2);
		AddCasaInfase(31,1,1);
		AddCasaInfase(31,2,1);



		#endregion
		
		#region Level 33

        leveis.Add(new Level(32, 10));
		
		AddCasaInfase(32,0,4);
		AddCasaInfase(32,1,4);
		AddCasaInfase(32,2,4);
		AddCasaInfase(32,3,4);
		AddCasaInfase(32,4,4);
		AddCasaInfase(32,2,3);
		AddCasaInfase(32,2,2);
		AddCasaInfase(32,0,1);
		AddCasaInfase(32,2,1);
		AddCasaInfase(32,0,0);
		AddCasaInfase(32,1,0);
		AddCasaInfase(32,2,0);



		#endregion
		
		#region Level 34

        leveis.Add(new Level(33, 10));
		
		AddCasaInfase(33,1,4);
		AddCasaInfase(33,0,4);
		AddCasaInfase(33,0,3);
		AddCasaInfase(33,1,3);
		AddCasaInfase(33,2,3);
		AddCasaInfase(33,2,2);
		AddCasaInfase(33,2,1);
		AddCasaInfase(33,3,1);
		AddCasaInfase(33,4,1);
		AddCasaInfase(33,3,0);
		AddCasaInfase(33,4,0);




		#endregion
		
		#region Level 35

        leveis.Add(new Level(34, 10));
		
		AddCasaInfase(34,0,4);
		AddCasaInfase(34,0,3);
		AddCasaInfase(34,1,3);
		AddCasaInfase(34,0,2);
		AddCasaInfase(34,2,2);
		AddCasaInfase(34,0,1);
		AddCasaInfase(34,3,1);
		AddCasaInfase(34,0,0);
		AddCasaInfase(34,1,0);
		AddCasaInfase(34,2,0);
		AddCasaInfase(34,3,0);
		AddCasaInfase(34,4,0);

		#endregion
		
		#region Level 36

        leveis.Add(new Level(35, 10));
		
		AddCasaInfase(35,1,4);
		AddCasaInfase(35,3,4);
		AddCasaInfase(35,0,3);
		AddCasaInfase(35,1,3);
		AddCasaInfase(35,3,3);
		AddCasaInfase(35,4,3);
		AddCasaInfase(35,0,2);
		AddCasaInfase(35,1,2);
		AddCasaInfase(35,2,2);
		AddCasaInfase(35,3,2);
		AddCasaInfase(35,4,2);
		AddCasaInfase(35,1,1);
		AddCasaInfase(35,3,1);
		AddCasaInfase(35,0,0);
		AddCasaInfase(35,1,0);
		AddCasaInfase(35,3,0);
		AddCasaInfase(35,4,0);


		#endregion
		
		#region Level 37

        leveis.Add(new Level(36, 11));
		
		AddCasaInfase(36,0,4);
		AddCasaInfase(36,1,4);
		AddCasaInfase(36,2,4);
		AddCasaInfase(36,3,4);
		AddCasaInfase(36,4,4);
		AddCasaInfase(36,0,3);
		AddCasaInfase(36,0,2);
		AddCasaInfase(36,1,2);
		AddCasaInfase(36,2,2);
		AddCasaInfase(36,3,2);
		AddCasaInfase(36,4,2);
		AddCasaInfase(36,4,1);
		AddCasaInfase(36,0,0);
		AddCasaInfase(36,1,0);
		AddCasaInfase(36,2,0);
		AddCasaInfase(36,3,0);
		AddCasaInfase(36,4,0);


		#endregion
		
		#region Level 38

        leveis.Add(new Level(37, 13));
		
		AddCasaInfase(37,0,4);
		AddCasaInfase(37,1,4);
		AddCasaInfase(37,0,3);
		AddCasaInfase(37,1,3);
		AddCasaInfase(37,2,3);
		AddCasaInfase(37,3,3);
		AddCasaInfase(37,4,3);
		AddCasaInfase(37,0,2);
		AddCasaInfase(37,3,2);
		AddCasaInfase(37,4,2);
		AddCasaInfase(37,0,1);
		AddCasaInfase(37,2,0);
		AddCasaInfase(37,3,0);


		#endregion
		
		#region Level 39

        leveis.Add(new Level(38, 12));
		
		AddCasaInfase(38,0,4);
		AddCasaInfase(38,2,4);
		AddCasaInfase(38,2,3);
		AddCasaInfase(38,3,3);
		AddCasaInfase(38,4,3);
		AddCasaInfase(38,1,2);
		AddCasaInfase(38,2,2);
		AddCasaInfase(38,3,2);
		AddCasaInfase(38,1,1);
		AddCasaInfase(38,3,1);
		AddCasaInfase(38,1,0);
		AddCasaInfase(38,2,0);
		AddCasaInfase(38,3,0);
		AddCasaInfase(38,4,0);


		#endregion
		
		#region Level 40

        leveis.Add(new Level(39, 12));
		
		AddCasaInfase(39,0,4);
		AddCasaInfase(39,4,4);
		AddCasaInfase(39,0,3);
		AddCasaInfase(39,1,3);
		AddCasaInfase(39,4,3);
		AddCasaInfase(39,0,2);
		AddCasaInfase(39,2,2);
		AddCasaInfase(39,4,2);
		AddCasaInfase(39,0,1);
		AddCasaInfase(39,3,1);
		AddCasaInfase(39,4,1);
		AddCasaInfase(39,0,0);
		AddCasaInfase(39,4,0);


		#endregion
		
		#region Level 41

        leveis.Add(new Level(40, 11));
		
		AddCasaInfase(40,2,2);



		#endregion
		
		#region Level 42

        leveis.Add(new Level(41, 7));
		
		AddCasaInfase(41,0,4);
		AddCasaInfase(41,1,3);
		AddCasaInfase(41,3,3);
		AddCasaInfase(41,1,2);
		AddCasaInfase(41,2,2);
		AddCasaInfase(41,3,2);
		AddCasaInfase(41,0,1); 
		AddCasaInfase(41,1,1); 
		AddCasaInfase(41,3,1);
		AddCasaInfase(41,0,0);
		AddCasaInfase(41,1,0);
		AddCasaInfase(41,4,0);



		#endregion
		
		#region Level 43

        leveis.Add(new Level(43, 10));
		
		AddCasaInfase(42,0,4);
		AddCasaInfase(42,1,4);
		AddCasaInfase(42,4,4);
		AddCasaInfase(42,0,3);
		AddCasaInfase(42,1,3);
		AddCasaInfase(42,1,2);
		AddCasaInfase(42,3,2);
		AddCasaInfase(42,4,2);
		AddCasaInfase(42,0,1);
		AddCasaInfase(42,1,1);
		AddCasaInfase(42,4,1);
		AddCasaInfase(42,3,0);
		AddCasaInfase(42,4,0);



		#endregion
		
		#region Level 44

        leveis.Add(new Level(43, 11));
		
		AddCasaInfase(43,1,4);
		AddCasaInfase(43,2,4);
		AddCasaInfase(43,3,4);
		AddCasaInfase(43,4,4);
		AddCasaInfase(43,3,3);
		AddCasaInfase(43,4,3);
		AddCasaInfase(43,1,2);
		AddCasaInfase(43,0,1);
		AddCasaInfase(43,1,1);
		AddCasaInfase(43,3,1);
		AddCasaInfase(43,0,0);
		AddCasaInfase(43,1,0);
		AddCasaInfase(43,3,0);
		AddCasaInfase(43,4,0);




		#endregion
		
		#region Level 45

        leveis.Add(new Level(44, 9));
		
		AddCasaInfase(44,1,4);
		AddCasaInfase(44,1,3);
		AddCasaInfase(44,4,3);
		AddCasaInfase(44,1,2);
		AddCasaInfase(44,2,2);
		AddCasaInfase(44,3,2);
		AddCasaInfase(44,4,2);
		AddCasaInfase(44,0,1);
		AddCasaInfase(44,1,1);
		AddCasaInfase(44,2,1);
		AddCasaInfase(44,3,1);
		AddCasaInfase(44,4,1);
		AddCasaInfase(44,2,0);
		AddCasaInfase(44,3,0);
		AddCasaInfase(44,4,0);

		#endregion
			
		#region Level 46

        leveis.Add(new Level(45, 8));
		
		AddCasaInfase(45,0,4);
		AddCasaInfase(45,1,4);
		AddCasaInfase(45,2,4);
		AddCasaInfase(45,3,4);
		AddCasaInfase(45,4,4);
		AddCasaInfase(45,0,3);
		AddCasaInfase(45,1,3);
		AddCasaInfase(45,0,2);
		AddCasaInfase(45,3,2);
		AddCasaInfase(45,4,1);
		AddCasaInfase(45,0,0);
		AddCasaInfase(45,1,0);
		AddCasaInfase(45,2,0);
		AddCasaInfase(45,3,0);
		AddCasaInfase(45,4,0);


		#endregion
		
		#region Level 47

        leveis.Add(new Level(47, 11));
		
		AddCasaInfase(46,3,4);
		AddCasaInfase(46,4,4);
		AddCasaInfase(46,0,3);
		AddCasaInfase(46,1,3);
		AddCasaInfase(46,4,3);
		AddCasaInfase(46,1,2);
		AddCasaInfase(46,2,2);
		AddCasaInfase(46,4,2);
		AddCasaInfase(46,1,1);
		AddCasaInfase(46,4,1);
		AddCasaInfase(46,0,0);
		AddCasaInfase(46,4,0);


		#endregion
		
		#region Level 48

        leveis.Add(new Level(47, 10));
		
		AddCasaInfase(47,0,4);
		AddCasaInfase(47,2,4);
		AddCasaInfase(47,0,3);
		AddCasaInfase(47,1,3);
		AddCasaInfase(47,2,3);
		AddCasaInfase(47,0,2);
		AddCasaInfase(47,1,2);
		AddCasaInfase(47,0,1);
		AddCasaInfase(47,2,1);
		AddCasaInfase(47,3,1);
		AddCasaInfase(47,0,0);
		AddCasaInfase(47,2,0);


		#endregion
		
		#region Level 49

        leveis.Add(new Level(48, 6));
		
		AddCasaInfase(48,1,4);
		AddCasaInfase(48,2,4);
		AddCasaInfase(48,3,4);
		AddCasaInfase(48,4,4);
		AddCasaInfase(48,1,3);
		AddCasaInfase(48,2,3);
		AddCasaInfase(48,3,3);
		AddCasaInfase(48,4,3);
		AddCasaInfase(48,3,2);
		AddCasaInfase(48,2,1);
		AddCasaInfase(48,3,1);
		AddCasaInfase(48,0,0);
		AddCasaInfase(48,1,0);


		#endregion
		
		#region Level 50

        leveis.Add(new Level(49, 9));
		
		AddCasaInfase(49,1,4);
		AddCasaInfase(49,0,3);
		AddCasaInfase(49,1,3);
		AddCasaInfase(49,4,3);
		AddCasaInfase(49,1,2);
		AddCasaInfase(49,3,2);
		AddCasaInfase(49,4,2);
		AddCasaInfase(49,1,1);
		AddCasaInfase(49,4,1);
		AddCasaInfase(49,4,0);
		AddCasaInfase(49,1,0);


		#endregion
		
		#region Level 51

        leveis.Add(new Level(50, 8));
		
		AddCasaInfase(50,1,4);
		AddCasaInfase(50,4,4);
		AddCasaInfase(50,1,3);
		AddCasaInfase(50,3,3);
		AddCasaInfase(50,0,2);
		AddCasaInfase(50,1,2);
		AddCasaInfase(50,2,1);
		AddCasaInfase(50,4,1);
		AddCasaInfase(50,0,0);
		AddCasaInfase(50,2,0);
		AddCasaInfase(50,4,0);



		#endregion
		
	}	
	public void AddCasaInfase(int Level, int x,int y)
	{
		leveis[Level].CasasAtivas.Add(new CasaAtiva(x,y));
	}
	
}


	
