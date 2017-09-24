using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAffordance : MonoBehaviour
{
    [SerializeField]
    private CameraRaycaster cameraRayCast;

    [SerializeField]
    private Texture2D walk;

    [SerializeField]
    private Texture2D attack;

    [SerializeField]
    private Texture2D unkown;

    [SerializeField]
    private Vector2 cursorHotSpot = new Vector2(96f,96f);

    [SerializeField]
    private const int walkableLayerNumber = 8;

    [SerializeField]
    private const int enemyLayerNumber = 9;

    private void Awake()
    {
        cameraRayCast.notifyLayerChangeObservers += OnLayerChange;
    }

    private void OnLayerChange(int newLayer)
    {
        switch (newLayer)
        {
            case walkableLayerNumber:
                Cursor.SetCursor(walk, cursorHotSpot, CursorMode.Auto);
                break;

            case enemyLayerNumber:
                Cursor.SetCursor(attack, cursorHotSpot, CursorMode.Auto);
                break;

            default:
                Cursor.SetCursor(unkown, cursorHotSpot, CursorMode.Auto);
                break;
        }
    }
}
