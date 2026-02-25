using ExtendFile.Panelis.BuildingBlocks.Common.Class.Base;

namespace ExtendFile.Panelis.BuildingBlocks.Common.Class.AggregateRoot;

public abstract class AggregateRoot<TId> : Entity<TId> where TId : notnull
{
    protected AggregateRoot(TId id) : base(id) { }
    protected AggregateRoot() { }
}
