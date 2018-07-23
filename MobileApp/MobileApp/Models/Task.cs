using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Models
{
    public class Task   
    {
        public bool Compl { get; set; } = false;
        public bool Started { get; set; } = false;
        public string Id { get; set; }
        public string Description { get; set; }
        public SimpleRef MaintenanceType { get; set; }
        public DateTime PlainDate { get; set; }
        public DateTime FactDate { get; set; }
        public Location GPS { get; set; }
        public SimpleRef ComplexObject { get; set; }
        public List<ResourceItem> Resources { get; set; }
        public List<JobItem> Jobs { get; set; }
        public List<Defect> Defects { get; set; }
        public List<SimpleRef> Units { get; set; }
        public string GPSProperty
        {
            get
            {
                return string.Format("{0} : {1}", GPS.X, GPS.Y);
            }
        }
        public List<string> Photos { get; set; } = new List<string>();
    }

    public class JobItem
    {
        public SimpleRef Job { get; set; }
        public string JobCode { get; set; }
        public float QuantityPlain { get; set; }
        public float QuantityFact { get; set; }
        public string Measure { get; set; }
    }

    public class Defect
    {
        public bool Completed { get; set; }
        public SimpleRef Job { get; set; }
        public SimpleRef Unit { get; set; }
        public SimpleRef MeasurementGroup { get; set; }
        public SimpleRef DefectType { get; set; }
        public float QuantityPlan { get; set; }
        public float QuantityFact { get; set; }

        public bool IsNew { get; set; } = false;
    }

    public enum ResourceType
    {
        Materials = 0,
        Workforce,
        Device
    }
    public class SimpleRef
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
    public class Location
    {
        public string X { get; set; }
        public string Y { get; set; }
    }
    public class ResourceItem
    {
        public SimpleRef Job { get; set; }
        public SimpleRef Resource { get; set; }
        public ResourceType ResourceType { get; set; }
        public float QuantityPlain { get; set; }
        public float QuantityFact { get; set; }
        public string Measure { get; set; }
    }

    public interface IMessage
    {
        void LongAlert(string message);
        void ShortAlert(string message);
    }
}
