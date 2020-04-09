using UnityEngine;
using System.Collections;

public static class Maths2D {

	public static void LookAt (Transform alvo,Vector3 mira) {

        //Checa a direcao entre os dois vetores
        Vector3 dir = alvo.position - mira;
        //Transforma a direcao em rotação (rad) e depois converte para angulo.
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //Transforma angulo em quaternion e aplica no transform alvo
        alvo.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

    public static void LookAtSmooth(Transform alvo, Vector3 mira, float suavidade)
    {
        //Checa a direcao entre os dois vetores
        Vector3 dir = alvo.position - mira;
        //Transforma a direcao em rotação (rad) e depois converte para angulo.
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //Transforma angulo em quaternion e aplica no transform alvo e faz a suavização de quaternions.
        alvo.rotation = Quaternion.Lerp(alvo.rotation, 
        Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * suavidade);
    }

}
