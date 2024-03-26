namespace FileSystem.Models.Results;

public abstract record Status()
{
    public record Success : Status
    {
        public record EmptyCommand : Success;
    }

    public record Fail : Status
    {
        public record SyntaxError : Fail
        {
            public record UnknownCommand : SyntaxError;

            public record UnknownFlag : SyntaxError;

            public record UnknownFlagValue : SyntaxError;

            public record TooManyArguments : SyntaxError;

            public record CommandNotInitialized : SyntaxError;
        }

        public record ExecutionError : Fail
        {
            public record NoConnectionToFileSystem : ExecutionError;

            public record NotSupportedFileFormat : ExecutionError;

            public record DeletingFileCurrentlyInUse : ExecutionError;

            public record FilenameDuplication : ExecutionError;

            public record UnableToConnectFileSystem : ExecutionError;

            public record NonExistentFile : ExecutionError;
        }
    }
}