using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.SpatialMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomAppState : Singleton<CustomAppState>, IInputClickHandler
{
    public TextMesh DebugDisplay;
    public string PrimaryText { get; private set; }
    const int QueryResultMaxCount = 10;

    //public ContentObjectManager COMObject;

    private bool ObjectSummoned = false;

    // Enums
    public enum QueryStates
    {
        None,
        Processing,
        Finished
    }

    // Structs
    private struct QueryStatus
    {
        public void Reset()
        {
            State = QueryStates.None;
            Name = "";
            CountFail = 0;
            CountSuccess = 0;
            QueryResult = new List<SpatialUnderstandingDllObjectPlacement.ObjectPlacementResult>();
        }

        public QueryStates State;
        public string Name;
        public int CountFail;
        public int CountSuccess;
        public List<SpatialUnderstandingDllObjectPlacement.ObjectPlacementResult> QueryResult;
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        PrimaryText = "Scanning in finishing, wait for finalization";

        if ((SpatialUnderstanding.Instance.ScanState == SpatialUnderstanding.ScanStates.Scanning) &&
            !SpatialUnderstanding.Instance.ScanStatsReportStillWorking)
        {
            SpatialUnderstanding.Instance.RequestFinishScan();
            PrimaryText = "Scanning Is Finished";

            gameObject.SetActive(false);

            SpatialUnderstanding.Instance.UnderstandingCustomMesh.DrawProcessedMesh = false;

            //if (!ObjectSummoned)
            //{
            //    COMObject.OnScanFinished();
            //    ObjectSummoned = true;
            //    gameObject.SetActive(false);
            //}

        }
    }

    private void Update_DebugDisplay()
    {
        // Basic checks
        if (DebugDisplay == null)
        {
            return;
        }

        // Update display text
        DebugDisplay.text = PrimaryText;
    }

    // Use this for initialization
    void Start()
    {
        // Not Implemented Yet
        PrimaryText = "Start Scanning";
    }

    // Update is called once per frame
    void Update()
    {
        // Not Implemented Yet
        if (SpatialUnderstanding.Instance.ScanState == SpatialUnderstanding.ScanStates.Scanning)
        {
            PrimaryText = "Scanning in progress, Tap this message to finish scanning";
        }

        Update_DebugDisplay();
    }
}