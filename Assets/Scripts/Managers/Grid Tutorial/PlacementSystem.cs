using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{

    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private Grid grid;

    [SerializeField]
    private UnitPrefabDatabase unitPrefabDatabase;


    [SerializeField]
    private GameObject gridVisualization;

    private GridData floorData, unitData;

 

    [SerializeField]
    private PreviewSystem preview;

    private Vector3Int lastDetectedPosition = Vector3Int.zero;

    [SerializeField]
    private ObjectPlacer objectPlacer;

    IBuildingState buildingState;

    

    private void Start()
    {
        StopPlacement();
        floorData = new ();
        unitData = new();
       
    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        gridVisualization.SetActive(true);
        buildingState = new PlacementState(ID,
                                           grid,
                                           preview,
                                           unitPrefabDatabase,
                                           floorData,
                                           unitData,
                                           objectPlacer);
        inputManager.OnClicked += PlaceUnit;
        inputManager.OnExit += StopPlacement;

    }

    public void StartRemoving()
    {
        StopPlacement();
        gridVisualization.SetActive(true );
        buildingState = new RemovingState(grid, preview, floorData, unitData, objectPlacer);
        inputManager.OnClicked += PlaceUnit;
        inputManager.OnExit += StopPlacement;
    }

    private void PlaceUnit()
    {
       if(inputManager.IsPointerOverUI())
        {
            return;
        }
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        buildingState.OnAction(gridPosition);
    }

    //private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    //{
    //    GridData selectedData = unitPrefabDatabase.objectsData[selectedObjectIndex].ID == 0 ? floorData : unitData;

    //    return selectedData.CanPlaceObjectAt(gridPosition, unitPrefabDatabase.objectsData[selectedObjectIndex].Size);
    //}

    private void StopPlacement()
    {
        if (buildingState == null)
            return;
        gridVisualization.SetActive(false);
        buildingState.EndState();
        inputManager.OnClicked -= PlaceUnit;
        inputManager.OnExit -= StopPlacement;
        lastDetectedPosition = Vector3Int.zero;
        buildingState = null;
    }


    private void Update()
    {
        if (buildingState == null)
            return;
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        if(lastDetectedPosition != gridPosition)
        {
           buildingState.UpdateState(gridPosition);
            lastDetectedPosition = gridPosition;
        }
       
    }
}
