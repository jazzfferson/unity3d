
public interface IState
{
    string Name { get; }
    void Enter();
    void Update();
    void LateUpdate();
    void FixedUpdate();
    void Exit();
}
