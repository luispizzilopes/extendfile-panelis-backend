using FluentValidation;

namespace ExtendFile.Panelis.Application.Modules.Setting.Requests.UpsertSetting;

/// <summary>
/// Request para criação ou atualização de configurações do sistema
/// </summary>
public class UpsertSettingRequest
{
    /// <summary>
    /// Limite mínimo considerado "insuficiente" (deve ser >= 0)
    /// </summary>
    public decimal LessThanEnoughThreshold { get; set; }
    
    /// <summary>
    /// Limite mínimo considerado "suficiente" (deve ser > LessThanEnoughThreshold)
    /// </summary>
    public decimal MoreThanEnoughThreshold { get; set; }
}