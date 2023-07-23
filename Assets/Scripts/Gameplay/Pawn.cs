using Assets.Scripts.Exceptions;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public PawnSpawn SpawnPoint;
    public Animator OnSelectedAnimation;

    public PlayerCreed Creed { get; private set; }
    public bool IsAlive { get; set; } = true;
    public IField CurrentField { get; set; } = null;

    private Vector3 _startingPosition;

    public bool IsOnTheBoard { get; private set; } = false;

    public bool Highlight
    {
        set => GetComponent<Light>().enabled = value;
    }

    public delegate void PawnStateChange(Pawn pawn);
    /// <summary>
    /// Player is just messing around and pressed once on this pawn to see what happens.
    /// </summary>
    public event PawnStateChange OnSelectedStateChanged;
    /// <summary>
    /// Player deems this pawn worthy of the next move.
    /// </summary>
    public event PawnStateChange OnActivated;

    private bool _selected = false;
    /// <summary>
    /// This is true when the player selects this pawn by clicking it once.
    /// </summary>
    public bool Selected
    {
        get => _selected;
        set
        {
            if (Selectable)
            {
                _selected = value;
                OnSelectedAnimation.enabled = value;
                OnSelectedStateChanged?.Invoke(this);
            }
        }
    }

    private bool _activated = false;
    /// <summary>
    /// This is true when the player chooses this pawn by double-clicking it.
    /// It's a final selection meant to activate the pawn for whatever action player has in mind.
    /// When this property changes, it automatically resets the Selected property to false as well.
    /// </summary>
    public bool Activated
    {
        get => _activated;
        set
        {
            if (Selectable)
            {
                _activated = value;

                _selected = false;
                OnSelectedAnimation.enabled = false;

                if (_activated)
                {
                    OnActivated?.Invoke(this);
                }
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
                Activated = true;
                _doubleClickStart = -1;
            }
            else
            {
                Selected = true;
                _doubleClickStart = Time.time;
            }
        }
    }

    public void Die()
    {
        transform.position = _startingPosition;
        IsAlive = false;
        IsOnTheBoard = false;
    }

    internal void MoveOnField(GameObject fieldObject)
    {
        IField newField = fieldObject.GetComponent<IField>() ?? throw new InvalidFieldException();
        Bounds pawnBounds = GetComponent<Renderer>().bounds;

        float pawnCenterPointHeight = (pawnBounds.max.y + pawnBounds.min.y) / 2;

        transform.position = new Vector3(
            fieldObject.transform.position.x,
            fieldObject.transform.position.y + pawnCenterPointHeight,
            fieldObject.transform.position.z
        );

        CurrentField = newField;
    }

    internal void SpawnOnSpawnpoint()
    {
        IsOnTheBoard = true;
        MoveOnField(SpawnPoint.SpawnpointField.gameObject);
    }
}
