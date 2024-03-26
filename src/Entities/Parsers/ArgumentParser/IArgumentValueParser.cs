using FileSystem.Models.Results;
using FileSystem.Services.StringIterators;

namespace FileSystem.Entities.Parsers.ArgumentParser;
public interface IArgumentValueParser<T>
{
    Status Parse(IStringIterator input, T builder);
}