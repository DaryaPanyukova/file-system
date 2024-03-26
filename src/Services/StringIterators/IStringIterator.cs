namespace FileSystem.Services.StringIterators;

public interface IStringIterator
{
    string Source { get; set; }
    string? GetTerm();
    void PutTerm();
    bool IsEnd();
}