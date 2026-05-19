namespace MiAppPronostico.Models;

public class MultivariateDataRow
{
    public int IndiceFila { get; set; }
    
    // Aquí guardamos columnas como "Usuarios", "CPU %", "RAM"
    public Dictionary<string, double> ValoresNumericos { get; set; } = new();
    
    // Aquí guardamos columnas de texto como "Estado del Servidor", "Categoría", o fechas sin procesar
    public Dictionary<string, string> ValoresCategoricos { get; set; } = new();
}

// Clases auxiliares para devolver los resultados matemáticos
public class RegressionResult
{
    public double Intercepto { get; set; }
    public Dictionary<string, double> Coeficientes { get; set; } = new();
    public double RSquared { get; set; }
    public List<double> Predicciones { get; set; } = new();
}

public class ConfusionMatrixResult
{
    public string ClasePositiva { get; set; } = string.Empty;
    public int VerdaderosPositivos { get; set; }
    public int VerdaderosNegativos { get; set; }
    public int FalsosPositivos { get; set; }
    public int FalsosNegativos { get; set; }
    
    public int Total => VerdaderosPositivos + VerdaderosNegativos + FalsosPositivos + FalsosNegativos;
    public double Exactitud => Total > 0 ? (VerdaderosPositivos + VerdaderosNegativos) / (double)Total : 0;
}