namespace MiAppPronostico.Models;

public class ForecastResult
{
    public string MethodName { get; set; } = string.Empty;
    public List<DataPoint> HistoricalData { get; set; } = new();
    public List<DataPoint> ForecastedData { get; set; } = new();

    // Métricas de validación (Backtesting)
    public double? MAE { get; set; }
    public double? RMSE { get; set; }
    public double? MAPE { get; set; }
    
}