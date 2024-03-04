using System;
using System.Collections;
using System.Collections.Generic;
using Arficord.SavingSystem.Demo.Controllers;
using Arficord.SavingSystem.Savers;
using Arficord.SavingSystem.Serializers;
using UnityEngine;

namespace Arficord.SavingSystem.Demo.Savers
{
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
            if (!serializer.TryDeserialize(out PointsData data, serializedString))
            {
                Debug.LogError("Deserialization failed", this);
                return;
            }

            pointsController.Points = data.Points;
        }
    }
}