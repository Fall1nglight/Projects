using System.Text.Json.Serialization;

namespace TodosCli.Records;

public record Todo(
    int CreatedAt,
    string Name,
    int Importance,
    string Description,
    bool Finished,
    int Deadline,
    string Id
);
