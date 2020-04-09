using System;
using UnityEngine;

    [Serializable]
	public abstract class BaseReplayData
	{
        public BodyFrame[] body;
        public InputFrame[] input;
        
        public BodyFrame GetBodyFrame(int frame)
        {           
            return body[frame];
        }
        public InputFrame GetInputFrame(int frame)
        {
            return input[frame];
        }
        public int Length()
        {
            return body.Length;
        }
       
	}

    [Serializable]
    public class BodyFrame
    {
        public BodyFrame()
        {
        }
        public Vector3 GetPosition()
        {
            return new Vector3(_posicaoX, _posicaoY, _posicaoZ);
        }
        public Quaternion GetRotation()
        {
            return new Quaternion(_rotacaoX, _rotacaoY, _rotacaoZ, _rotacaoW);
        }

        public Vector3 GetVelocity()
        {
            return new Vector3(_velocityX, _velocityY, _velocityZ);
        }
        public Vector3 GetAngularVelocity()
        {
            return new Vector3(_angularvelocityX, _angularvelocityY, _angularvelocityZ);
        }

        public BodyFrame(Rigidbody rigibody)
        {
           
            _posicaoX = rigibody.position.x;
            _posicaoY = rigibody.position.y;
            _posicaoZ = rigibody.position.z;

            _rotacaoX = rigibody.rotation.x;
            _rotacaoY = rigibody.rotation.y;
            _rotacaoZ = rigibody.rotation.z;
            _rotacaoW = rigibody.rotation.w;

            _velocityX = rigibody.velocity.x;
            _velocityY = rigibody.velocity.y;
            _velocityZ = rigibody.velocity.z;

            _angularvelocityX = rigibody.angularVelocity.x;
            _angularvelocityY = rigibody.angularVelocity.y;
            _angularvelocityZ = rigibody.angularVelocity.z;
        }
        public BodyFrame(Transform transform)
        {

            _posicaoX = transform.position.x;
            _posicaoY = transform.position.y;
            _posicaoZ = transform.position.z;

            _rotacaoX = transform.rotation.x;
            _rotacaoY = transform.rotation.y;
            _rotacaoZ = transform.rotation.z;
            _rotacaoW = transform.rotation.w;

            /*_velocityX = rigibody.velocity.x;
            _velocityY = rigibody.velocity.y;
            _velocityZ = rigibody.velocity.z;

            _angularvelocityX = rigibody.angularVelocity.x;
            _angularvelocityY = rigibody.angularVelocity.y;
            _angularvelocityZ = rigibody.angularVelocity.z;*/
        }

        public float _posicaoX;

        public float _posicaoY;

        public float _posicaoZ;

        public float _rotacaoX;

        public float _rotacaoY;

        public float _rotacaoZ;

        public float _rotacaoW;

        public float _velocityX;
        public float _velocityY;
        public float _velocityZ;

        public float _angularvelocityX;
        public float _angularvelocityY;
        public float _angularvelocityZ;
    }
    [Serializable]
    public class InputFrame
    {
        public InputFrame()
        {

        }
        public InputFrame(float direcao, float acelerador, float freio, int transmissao)
        {
            _direcao = direcao;
            _acelerador = acelerador;
            _freio = freio;
            _transmissao = transmissao;
        }
        public float _direcao;
        public float _acelerador;
        public float _freio;
        public int _transmissao;
    }
    [Serializable]
    public class LapInfo
    {
        public LapInfo(int firstFrame, int lastFrame, float time)
        {
            _time = time;
            _firstFrame = firstFrame;
            _lastFrame = lastFrame;
        }
        public float _time;
        public int _firstFrame;
        public int _lastFrame;
    }

