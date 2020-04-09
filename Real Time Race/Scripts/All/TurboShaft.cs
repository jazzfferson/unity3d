
using System;
[Serializable]
public class TurboShaft
{
    /// <summary>
    /// coeficiente de fricção
    /// </summary>
    public float c = 0.01f;
    /// <summary>
    /// Aerea de friccao
    /// </summary>
    public float areaFriccao = 1;
    /// <summary>
    /// Massa do objeto em kilogramas
    /// </summary>
    public float mass = 1;

    public float Velocity
    {
        get { return angularVelocity; }
    }
    public float Rotation
    {
        get { return shaftRotation;}
    }
    public float Rpm
    {
        get
        {
            return 120 * 3.14159265359f * angularVelocity;
        }
    }

    private float angularVelocity=0;
    private float angularAcceleration = 0;
    private float shaftRotation = 0;

    /// <summary>
    /// Adiciona um torque de rotação em newtons. já incluso a fricção
    /// </summary>
    /// <param name="force"></param>  
    public void AddForce(float forceKg,float deltaTime)
    {
        float force = forceKg * 9.8066500286f;
        force += Friction();
        angularAcceleration += (force / (mass * 9.8066500286f)) * deltaTime;
        angularVelocity += angularAcceleration;
        shaftRotation += angularVelocity;
        angularAcceleration = 0; 
    }
    float Friction()
    {
        float frictionMag = c * areaFriccao;
        float friction = angularVelocity * -1;
        friction = friction * frictionMag;
        return friction * 9.8066500286f;
    }
}