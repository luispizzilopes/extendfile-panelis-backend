using System.Globalization;
using ExtendFile.Panelis.Application.Modules.Test.Responses.CreateTest;

namespace ExtendFile.Panelis.Application.Modules.Test.UseCases.ProcessTest;

public class ProcessTestUseCase
{
    private const string BlockSeparator = "----------------------";
    private const string CatCodePrefix = "B ";
    private const string DateTimeFormat = "dd/MM/yyyy HH:mm:ss";
    private const int FoodChannel1 = 1;
    private const int FoodChannel2 = 2;

    /// <summary>
    /// Ponto de entrada do use case.
    /// Divide o texto bruto em blocos, parseia cada um e retorna um TestResponse.
    /// </summary>
    public TestResponse Execute(string rawText)
    {
        var blocks = SplitIntoBlocks(rawText);
        var lines = blocks
            .Select(ParseBlock)
            .Where(line => line is not null)
            .ToList();

        return new TestResponse
        {
            TestDate = ExtractFirstDate(rawText),
            Lines = lines
        };
    }

    /// <summary>
    /// Divide o texto completo em blocos individuais usando o separador de bloco.
    /// </summary>
    private static List<string> SplitIntoBlocks(string rawText)
    {
        return rawText
            .Split([BlockSeparator], StringSplitOptions.RemoveEmptyEntries)
            .ToList();
    }

    /// <summary>
    /// Extrai linhas não vazias e sem espaços extras de um bloco de texto bruto.
    /// </summary>
    private static List<string> ExtractValidLines(string block)
    {
        return block
            .Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Trim())
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .ToList();
    }

    /// <summary>
    /// Encontra o código do gato nas linhas procurando pelo prefixo "B ".
    /// Retorna null se não encontrado.
    /// </summary>
    private static string ExtractCatCode(List<string> lines)
    {
        return lines
            .FirstOrDefault(line => line.StartsWith(CatCodePrefix))
            ?.Substring(CatCodePrefix.Length)
            .Trim();
    }

    /// <summary>
    /// Parseia um bloco de texto em um objeto TestLineResponse com os dados brutos do arquivo.
    /// CatName, FoodAmountStatus e campos de domínio são preenchidos posteriormente no Handler.
    /// Retorna null se o bloco não contiver código de gato válido ou linhas de medição.
    /// </summary>
    private static TestLineResponse ParseBlock(string block)
    {
        var lines = ExtractValidLines(block);
        if (lines.Count == 0) return null;

        var catCode = ExtractCatCode(lines);
        if (catCode is null) return null;

        var measurementLines = lines
            .Select(TryParseMeasurementLine)
            .Where(m => m != null)
            .ToList();

        var food1Measurements = measurementLines.Where(m => m.Channel == FoodChannel1).ToList();
        var food2Measurements = measurementLines.Where(m => m.Channel == FoodChannel2).ToList();

        var firstFood = CalculateConsumption(food1Measurements);
        var secondFood = CalculateConsumption(food2Measurements);

        return new TestLineResponse
        {
            CatHash = catCode,
            FirstFood = firstFood,
            SecondFood = secondFood,
            TotalAmountFood = firstFood + secondFood
        };
    }

    /// <summary>
    /// Calcula quanto de alimento foi consumido subtraindo o último peso do primeiro.
    /// Retorna 0 se a lista estiver vazia ou se o peso aumentou.
    /// </summary>
    private static decimal CalculateConsumption(List<MeasurementLine> measurements)
    {
        if (measurements.Count == 0) return 0;

        var initialWeight = measurements.First().Weight;
        var finalWeight = measurements.Last().Weight;

        return Math.Max(0, initialWeight - finalWeight);
    }

    /// <summary>
    /// Tenta parsear uma linha bruta em um MeasurementLine.
    /// Formato esperado: {canal} {peso} {data} {hora} {status}
    /// Retorna null se a linha não corresponder ao formato esperado.
    /// </summary>
    private static MeasurementLine TryParseMeasurementLine(string line)
    {
        var parts = line.Split([' '], StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length != 5) return null;
        if (parts[0] != "1" && parts[0] != "2") return null;

        if (!int.TryParse(parts[0], out int channel)) return null;
        if (!decimal.TryParse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal weight)) return null;
        if (!TryParseDateTime(parts[2], parts[3], out DateTime dateTime)) return null;

        return new MeasurementLine(channel, weight, dateTime);
    }

    /// <summary>
    /// Tenta converter strings de data e hora em um DateTime usando o formato esperado.
    /// </summary>
    private static bool TryParseDateTime(string date, string time, out DateTime dateTime)
    {
        return DateTime.TryParseExact(
            $"{date} {time}",
            DateTimeFormat,
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out dateTime);
    }

    /// <summary>
    /// Percorre o texto bruto completo para encontrar e retornar a primeira data válida.
    /// Usa DateTime.Today como fallback se nenhuma data for encontrada.
    /// </summary>
    private static DateTime ExtractFirstDate(string rawText)
    {
        foreach (var line in rawText.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries))
        {
            var parts = line.Trim().Split([' '], StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 5 && TryParseDateTime(parts[2], parts[3], out DateTime dateTime))
                return dateTime.Date;
        }

        return DateTime.Today;
    }

    /// <summary>
    /// Record interno que representa uma linha de medição já parseada.
    /// </summary>
    private record MeasurementLine(int Channel, decimal Weight, DateTime DateTime);
}