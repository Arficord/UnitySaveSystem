using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsSaver : Saver
{
    [Serializable]
    private class PointsData
    {
        public float Points;
    }

    [SerializeField] PointsController pointsController;
        
    public override string RecordData(SaveSerializer serializer)
    {
        var data = new PointsData()
        {
            Points = pointsController.Points,
        };
        return serializer.Serialize(data);
    }

    public override void ApplyData(SaveSerializer serializer, string serializedString)
    {
        var data = serializer.Deserialize<PointsData>(serializedString);
        pointsController.Points = data.Points;
    }
}
