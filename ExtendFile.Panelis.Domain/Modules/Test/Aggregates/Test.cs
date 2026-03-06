using ExtendFile.Panelis.BuildingBlocks.Common.Class.AggregateRoot;
using ExtendFile.Panelis.Domain.Modules.House.ValueObject;
using ExtendFile.Panelis.Domain.Modules.Test.Entities;
using ExtendFile.Panelis.Domain.Modules.Test.ValueObject;

namespace ExtendFile.Panelis.Domain.Modules.Test.Aggregates;

public class Test : AggregateRoot<TestId>
{
    public string Name { get; private set; }
    public string FileName { get; private set; }
    public BoxId BoxId { get; private set; }
    public DateTime TestDate { get; private set; }
    public DateTime CreatedAt { get; private set; }
    private readonly List<TestLine> _lines = [];
    public IReadOnlyList<TestLine> Lines => _lines.AsReadOnly();

    private Test(
        TestId id,
        string name,
        string fileName,
        BoxId boxId,
        DateTime testDate) : base(id)
    {
        Name = name;
        FileName = fileName;
        BoxId = boxId;
        TestDate = testDate;
        CreatedAt = DateTime.UtcNow;
    }

    public static Test Create(
        string name,
        string fileName,
        BoxId boxId,
        DateTime testDate)
    {
        var id = TestId.CreateIdentifier();

        return new Test(
            id,
            name,
            fileName,
            boxId,
            testDate);
    }

    public void AddLineRange(IEnumerable<TestLine> lines)
    {
        _lines.AddRange(lines);
    }

    public void AddLine(TestLine line)
    {
        var alreadyExists = _lines.Any(l => l.Id == line.Id);
        if (alreadyExists)
            throw new InvalidOperationException($"Line {line.Id} already exists in this Test.");

        _lines.Add(line);
    }

    public void RemoveLine(TestLineId lineId)
    {
        var line = _lines.FirstOrDefault(l => l.Id == lineId)
                   ?? throw new InvalidOperationException($"Line {lineId} not found in this Test.");

        _lines.Remove(line);
    }
}