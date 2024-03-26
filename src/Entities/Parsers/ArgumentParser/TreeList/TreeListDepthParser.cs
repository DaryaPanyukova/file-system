using System.Globalization;
using FileSystem.Entities.Command;
using FileSystem.Models.Results;
using FileSystem.Services.Builders.Tree;
using FileSystem.Services.StringIterators;

namespace FileSystem.Entities.Parsers.ArgumentParser.TreeList;

public class TreeListDepthParser : IArgumentParser<TreeListCommandBuilder>
{
    public IArgumentParser<TreeListCommandBuilder>? NextArgumentParser { get; set; }

    public Result Parse(IStringIterator input, TreeListCommandBuilder builder)
    {
        string? flag = input.GetTerm();
        string? depth = input.GetTerm();
        builder.WithDepth(int.Parse(depth ?? "0", CultureInfo.InvariantCulture));
        if (flag == null || depth == null || NextArgumentParser == null)
        {
            try
            {
                ICommand command = builder.Build();
                return new Result(new Status.Success(), command);
            }
            catch (ArgumentNullException)
            {
                return new Result(new Status.Fail.SyntaxError.CommandNotInitialized(), null);
            }
        }

        return NextArgumentParser.Parse(input, builder);
    }
}