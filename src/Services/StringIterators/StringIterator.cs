namespace FileSystem.Services.StringIterators;

public class StringIterator : IStringIterator
{
    private readonly string[] _terms;
    private int _currentPosition;

    public StringIterator(string source)
    {
        Source = source;
        _terms = Source.Split(' ');
        _currentPosition = 0;
    }

    public string Source { get; set; }

    public string? GetTerm()
    {
        if (_currentPosition < _terms.Length)
        {
            return _terms[_currentPosition++];
        }
        else
        {
            return null;
        }
    }

    public void PutTerm()
    {
        _currentPosition = _currentPosition > 0 ? _currentPosition - 1 : 0;
    }

    public bool IsEnd()
    {
        return _currentPosition == _terms.Length;
    }
}