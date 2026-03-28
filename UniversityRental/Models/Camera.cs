namespace UniversityRental.Models;

public class Camera : Equipment
{
    public Camera(string name, string manufacturer, string sensorType, int opticalZoom) : base(name, manufacturer)
    {
        SensorType = sensorType;
        OpticalZoom = opticalZoom;
    }
    
    public string SensorType { get; }
    public int OpticalZoom { get; }

    public override string GetDetails()
    {
        return $"Matryca: {SensorType}, Zoom optyczny: {OpticalZoom}x";
    }
}