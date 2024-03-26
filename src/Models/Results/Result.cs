using FileSystem.Entities.Command;

namespace FileSystem.Models.Results;

public record Result(Status Status, ICommand? Command);