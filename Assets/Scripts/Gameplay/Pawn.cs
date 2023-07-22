using Assets.Scripts.Exceptions;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public PawnSpawn SpawnPoint;
    public Animator OnActiveAnimation;

    public PlayerCreed Creed { get; private set; }
    public bool IsAlive { get; set; } = true;

    private Vector3 _startingPosition;

    public int MovesMade { get; private set; } = 0;

    public bool Highlight
    {
        set => GetComponent<Light>().enabled = value;
    }

    public delegate void SelectionStateChange(Pawn pawn);
    public event SelectionStateChange OnSelectionStateChanged;

    private bool selected = false;
    public bool Selected
    {
        get => selected;
        set
        {
            if (Selectable)
            {
                selected = value;
                OnActiveAnimation.enabled = value;
                OnSelectionStateChanged?.Invoke(this);
            }
        }
    }

    public Color Color
    {
        set
        {
            GetComponent<Renderer>().material.color = value;
            GetComponent<Light>().color = value;
        }
    }

    public bool Selectable { get; set; } = false;

    private void Start()
    {
        _startingPosition = transform.position;
        Creed = SpawnPoint.Creed;
    }

    private void Update()
    {

    }

    private float _doubleClickStart = 0;
    private void OnMouseUp()
    {
        if (Selectable)
        {
            if ((Time.time - _doubleClickStart) < 0.3f)
            {
                Selected = true;
                _doubleClickStart = -1;
            }
            else
            {
                _doubleClickStart = Time.time;
            }
        }
    }

    public void Die()
    {
        transform.position = _startingPosition;
        IsAlive = false;
        MovesMade = 0;
    }

    internal void MoveToField(GameObject fieldObject)
    {
        _ = fieldObject.GetComponent<IField>() ?? throw new InvalidFieldException();
        Bounds pawnBounds = GetComponent<Renderer>().bounds;

        float pawnCenterPointHeight = (pawnBounds.max.y + pawnBounds.min.y) / 2;

        transform.position = new Vector3(
            fieldObject.transform.position.x,
            fieldObject.transform.position.y + pawnCenterPointHeight,
            fieldObject.transform.position.z
        );
    }
}
