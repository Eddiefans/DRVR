using UnityEngine;

public class MeasureGameObjectSpeed : MonoBehaviour
{
    public float xSpeed;
    public float ySpeed;
    public Vector3 speedVector;
    public float speedMagnitude;
    private Vector3 lastPos;
    [SerializeField] private bool useFixedUpdate;
    private void Start()
    {
        lastPos = transform.position;
    }

    private void Update()
    {
        if(useFixedUpdate)
            return;
        MeasureLoop(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if(!useFixedUpdate)
            return;
        MeasureLoop(Time.fixedDeltaTime);
    }

    void MeasureLoop(float delta)
    {
        var position = transform.position;
        xSpeed = (position.x - lastPos.x)/delta;
        ySpeed = (position.y - lastPos.y)/delta;
        speedVector.x = xSpeed;
        speedVector.y = ySpeed;
        lastPos = position;
        speedMagnitude = speedVector.magnitude;
    }
}
