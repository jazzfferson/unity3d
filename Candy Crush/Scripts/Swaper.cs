using UnityEngine;
using System.Collections;

public class Swaper
{

    public Candy candyA;
    public Candy candyB;
  

   
   
    /// <summary>
    /// Sobrescreve o metodo equals para comparar se a classe
    /// Swaper tem os mesmos candyA e candyB que a classe comparada.
    /// Independentemente da ordem em que eles estão
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        Swaper other = (Swaper)obj;
        return (other.candyA == this.candyA && other.candyB == this.candyB) ||
         (other.candyB == this.candyA && other.candyA == this.candyB);
    }

    /// <summary>
    /// Sobrescreve o meto gethashcode para retornar um numero unico de indentificação
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return this.candyA.GetHashCode()^this.candyB.GetHashCode();
    }
}
