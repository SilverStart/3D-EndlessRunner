using UnityEngine;

public class Mover : MonoBehaviour
{
    public bool isRun, isFly, isSwim;

    private IMove move;
    private MoveRun run;
    private MoveFly fly;
    private MoveSwim swim;

    void Start()
    {
        run = new MoveRun();
        fly = new MoveFly();
        swim = new MoveSwim();

        move = run;
    }
    
    void Update()
    {
        if (Input.anyKey)
            move.Move();
        
        // if (물에 들어가면)
        // {
        //     move = swim;
        // }

        if (Input.GetKey(KeyCode.Space))
        {
            move = fly;
        }
    }
}