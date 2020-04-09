using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum EAxis {X,Y,Z,ALL};
public class GroupTween {
   
    public Vector3 Position{get;set;}
    private List<TransformHandler> transformList;
    public void AddTransform(Transform transform)
    {
        if (transformList == null)
            transformList = new List<TransformHandler>();

        transformList.Add(new TransformHandler(transform));


    }
    /// <summary>
    /// Posicao , rotacao e escala tem que ser: VECTOR3
    /// </summary>
    /// <param name="objArray"></param>
    /// <param name="tween"></param>

    public void Initialize(GoTween tween)
    {

        tween.setOnUpdateHandler(update =>
        {

            for (int i = 0; i < transformList.Count; i++)
            {
                transformList[i].Position(Position);
            }
        }); 
    }

    public class TransformHandler
    {
        public Transform _transform;
        public Vector3 posicaoOriginal;
        public Quaternion rotacao;
        public Vector3 escala;

        public TransformHandler(Transform transform)
        {
            _transform = transform;
            posicaoOriginal = _transform.position;
        }
        public void Position(Vector3 position)
        {
            _transform.position = posicaoOriginal + position;
        }
    }

}
