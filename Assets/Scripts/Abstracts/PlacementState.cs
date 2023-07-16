using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementState : IBuildingState
{
    private int selectedObjectIndex = -1;
    int ID;
    Grid grid;
    PreviewSystem previewSystem;
    UnitPrefabDatabase unitPrefabDatabase;
    GridData floorData;
    GridData unitData;
    ObjectPlacer objectPlacer;

    public PlacementState(int iD,
                          Grid grid,
                          PreviewSystem previewSystem,
                          UnitPrefabDatabase unitPrefabDatabase,
                          GridData floorData,
                          GridData unitData,
                          ObjectPlacer objectPlacer)
    {
        ID = iD;
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.unitPrefabDatabase = unitPrefabDatabase;
        this.floorData = floorData;
        this.unitData = unitData;
        this.objectPlacer = objectPlacer;

        selectedObjectIndex = unitPrefabDatabase.objectsData.FindIndex(data => data.ID == ID);

        if (selectedObjectIndex > -1)
        {

            previewSystem.StartShowingPlacementPReview(unitPrefabDatabase.objectsData[selectedObjectIndex].Prefab,
                unitPrefabDatabase.objectsData[selectedObjectIndex].Size);

        }
        else
            throw new System.Exception($"No object with ID {iD}");

    }
    public void EndState()
    { previewSystem.StopShowingPreview(); }


    public void OnAction(Vector3Int gridPosition)
    {

        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
        if (placementValidity == false)
            return;

        int index = objectPlacer.PlaceObject(unitPrefabDatabase.objectsData[selectedObjectIndex].Prefab, grid.CellToWorld(gridPosition));


        GridData selectedData = unitPrefabDatabase.objectsData[selectedObjectIndex].ID == 0 ? floorData : unitData;

        selectedData.AddObjectAt(gridPosition, unitPrefabDatabase.objectsData[selectedObjectIndex].Size, unitPrefabDatabase.objectsData[selectedObjectIndex].ID,
            index);

        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), false);
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        GridData selectedData = unitPrefabDatabase.objectsData[selectedObjectIndex].ID == 0 ? floorData : unitData;

        return selectedData.CanPlaceObjectAt(gridPosition, unitPrefabDatabase.objectsData[selectedObjectIndex].Size);
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);

        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);
    }
}
