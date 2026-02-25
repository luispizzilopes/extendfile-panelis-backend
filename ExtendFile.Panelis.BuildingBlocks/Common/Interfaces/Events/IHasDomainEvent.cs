namespace ExtendFile.Panelis.BuildingBlocks.Common.Interfaces.Events;

public interface IHasDomainEvent
{
    public IReadOnlyList<IDomainEvent> DomainEvents { get; }
    public void ClearDomainEvents();
}