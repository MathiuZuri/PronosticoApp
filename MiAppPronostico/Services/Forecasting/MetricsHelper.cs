namespace MiAppPronostico.Services.Forecasting;

public static class MetricsHelper
{
    // MAE: Error Medio Absoluto (Promedio de las diferencias en valor absoluto)
    public static double CalculateMAE(List<double> actual, List<double> forecast)
    {
        if (actual.Count != forecast.Count || actual.Count == 0) return 0;
        return actual.Zip(forecast, (a, f) => Math.Abs(a - f)).Average();
    }

    // RMSE: Raíz del Error Cuadrático Medio (Penaliza más los errores grandes)
    public static double CalculateRMSE(List<double> actual, List<double> forecast)
    {
        if (actual.Count != forecast.Count || actual.Count == 0) return 0;
        double mse = actual.Zip(forecast, (a, f) => Math.Pow(a - f, 2)).Average();
        return Math.Sqrt(mse);
    }

    // MAPE: Error Porcentual Absoluto Medio (Qué porcentaje nos equivocamos en promedio)
    public static double CalculateMAPE(List<double> actual, List<double> forecast)
    {
        if (actual.Count != forecast.Count || actual.Count == 0) return 0;
        
        // Evitamos dividir por cero si algún valor real es 0
        var validPairs = actual.Zip(forecast, (a, f) => new { Actual = a, Forecast = f })
            .Where(x => x.Actual != 0).ToList();

        if (!validPairs.Any()) return 0;

        return validPairs.Average(x => Math.Abs((x.Actual - x.Forecast) / x.Actual)) * 100;
    }
}