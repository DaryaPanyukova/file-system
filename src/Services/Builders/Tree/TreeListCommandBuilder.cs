using FileSystem.Entities.Command;
using FileSystem.Entities.Command.Tree;
using FileSystem.Entities.Outputs;

namespace FileSystem.Services.Builders.Tree;

 public class TreeListCommandBuilder : ICommandBuilder
    {
        private int? _depth;
        private IOutput? _output;

        public TreeListCommandBuilder WithDepth(int depth)
        {
            _depth = depth;
            return this;
        }

        public TreeListCommandBuilder WithOutput(IOutput output)
        {
            _output = output;
            return this;
        }

        public ICommand Build()
        {
            return new TreeListCommand(_depth, _output);
        }
    }