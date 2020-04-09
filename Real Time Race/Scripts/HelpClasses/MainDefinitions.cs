using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    public enum Unit { Kilometer, Mile };
    public enum ECameraMode { Static, Dinamic };
    public enum ECameraPosition : int { Free = 0, Roof = 1, Back = 2 };

	public class MainDefinitions
    {
        
        
        #region CameraGame
        public static int[] CameraFovs = new int[] {55,60,50 };
       // public static int[] CameraFovs = new int[] { 35, 40, 45 };
        public static float CameraHeightDamping = 4f,
                           CameraRotationDamping = 5f;
        
        #endregion

        #region Units

        //Variáveis
        public const float Mile = 2.237f,
                           Meter = 3.6f;
        public const string MilePerHour = "MPH",
                            KilometerPerHour = "KPH";


        private static float currentUnit = Meter;
        private static string currentSpeedUnit = KilometerPerHour;

        

        //Propriedades
        public static float CurrentUnit
        {
            get { return currentUnit; }
        }
        public static string CurrentSpeedUnit
        {
            get { return currentSpeedUnit; }
        }

        //Métodos
        public static void SetUnit(Unit unit)
        {
            switch (unit)
            {
                case Unit.Kilometer:
                    currentUnit = Meter;
                    currentSpeedUnit = KilometerPerHour;
                    break;
                case Unit.Mile:
                    currentUnit = Mile;
                    currentSpeedUnit = MilePerHour;
                    break;
            }
        }

        #endregion

      

        
    }

