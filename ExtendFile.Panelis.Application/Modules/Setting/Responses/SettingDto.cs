namespace ExtendFile.Panelis.Application.Modules.Setting.Responses;

/// <summary>
/// DTO de resposta para configurações do sistema
/// </summary>
public class SettingDto
{
    /// <summary>
    /// Identificador único da configuração
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Limite mínimo considerado "insuficiente"
    /// </summary>
    public decimal LessThanEnoughThreshold { get; set; }
    
    /// <summary>
    /// Limite mínimo considerado "suficiente"
    /// </summary>
    public decimal MoreThanEnoughThreshold { get; set; }
    
    /// <summary>
    /// Quantidade de dias sem comer para alerta
    /// </summary>
    public int DaysWithoutEatingForAlert { get; set; }
    
    /// <summary>
    /// Quantidade de dias sem comer para aviso
    /// </summary>
    public int DaysWithoutEatingForWarning { get; set; }
    
    /// <summary>
    /// Data de criação da configuração
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Data da última atualização da configuração
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}
