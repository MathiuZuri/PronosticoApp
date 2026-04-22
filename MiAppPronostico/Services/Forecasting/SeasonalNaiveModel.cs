using MiAppPronostico.Models;

namespace MiAppPronostico.Services.Forecasting;

public class SeasonalNaiveModel : IForecastModel
{
    public string Name => "Método Ingenuo Estacional";

    public ForecastResult Calculate(List<DataPoint> data, int horizon, int? seasonality = null)
    {
        var result = new ForecastResult { MethodName = Name, HistoricalData = data };
        
        // Si no hay estacionalidad definida, por defecto usamos 7
        int m = seasonality ?? 7;

        // Verificamos que tengamos al menos una temporada completa de historia
        if (data == null || data.Count < m) return result;

        DateTime lastDate = data.Last().Fecha;

        for (int i = 1; i <= horizon; i++)
        {
            // La magia del módulo (%): Nos permite ciclar iterativamente sobre la temporada anterior
            // Si m=7, para i=1 mira 7 días atrás; para i=8 vuelve a mirar el mismo día, etc.
            int indexToCopy = data.Count - m + ((i - 1) % m);
            double valueToCopy = data[indexToCopy].Valor;

            result.ForecastedData.Add(new DataPoint
            {
                Fecha = lastDate.AddDays(i),
                Valor = valueToCopy
            });
        }

        return result;
    }
}