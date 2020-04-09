using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

    
   
    public class Level
    {
        public Level(int index,int jogadas)
        {
            _indexFase = index;
            CasasAtivas = new List<CasaAtiva>();
            jogadasMinimas = jogadas;

        }
        public List<CasaAtiva> CasasAtivas;
	    public int jogadasMinimas;
        private int _indexFase;
        public int Score
        {
            #region Get
            get
            {
                if (PlayerPrefs.HasKey("score" + _indexFase))
                {
                    return PlayerPrefs.GetInt("score" + _indexFase);
                }
                else
                {
                    PlayerPrefs.SetInt("score" + _indexFase, 0);
                    return PlayerPrefs.GetInt("score" + _indexFase);
                }
            }
            #endregion

            #region Set

            set
            {

                PlayerPrefs.SetInt("score" + _indexFase, value);
                PlayerPrefs.Save();
            }
            #endregion
        }
    }

