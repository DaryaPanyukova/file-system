using FileSystem.Entities.Command.Connect;
using FileSystem.Entities.Command.Disconnect;
using FileSystem.Entities.Command.File;
using FileSystem.Entities.Command.Tree;
using FileSystem.Entities.FileSystemManagers;
using FileSystem.Entities.FileSystems;
using FileSystem.Models.Results;
using Xunit;
using ConsoleOutput = FileSystem.Entities.Outputs.ConsoleOutput;

namespace FileSystem.Tests;

public class ParserTests
{
    [Fact]
    public void ParseUnknownCommand()
    {
        // Arrange
        string command = "file open test.txt";
        var manager = new FileSystemManager();

        // Act
        Result result = manager.Parse(command);

        // Assert
        Assert.Equal(new Status.Fail.SyntaxError.UnknownCommand(), result.Status);
    }

    [Fact]
    public void ParseConnectSuccess()
    {
        // Arrange
        string command = "connect C:\\Drivers -m local";
        var manager = new FileSystemManager();

        // Act
        Result result = manager.Parse(command);

        // Assert
        Assert.Equal(new Status.Success(), result.Status);

        Assert.IsType<ConnectCommand>(result.Command);
        var connectCommand = (ConnectCommand)result.Command;

        Assert.Equal("C:\\Drivers", connectCommand.Address);
        Assert.IsType<LocalFileSystem>(connectCommand.FileSystem);
    }

    [Fact]
    public void ParseConnectCommandNotInitializedMissedPath()
    {
        // Arrange
        string command = "connect -m local";
        var manager = new FileSystemManager();

        // Act
        Result result = manager.Parse(command);

        // Assert
        Assert.Equal(new Status.Fail.SyntaxError.UnknownFlag(), result.Status);
    }

    [Fact]
    public void ParseConnectCommandUnknownFlagValue()
    {
        // Arrange
        string command = "connect D:\\Drivers -m yandexDisk";
        var manager = new FileSystemManager();

        // Act
        Result result = manager.Parse(command);

        // Assert
        Assert.Equal(new Status.Fail.SyntaxError.UnknownFlagValue(), result.Status);
    }

    [Fact]
    public void ParseConnectCommandTooManyArguments()
    {
        // Arrange
        string command = "connect D:\\Drivers -m local -s 4";
        var manager = new FileSystemManager();

        // Act
        Result result = manager.Parse(command);

        // Assert
        Assert.Equal(new Status.Fail.SyntaxError.TooManyArguments(), result.Status);
    }

    [Fact]
    public void ParseConnectCommandUnknownFlag()
    {
        // Arrange
        string command = "connect D:\\Drivers -s 4 -m local";
        var manager = new FileSystemManager();

        // Act
        Result result = manager.Parse(command);

        // Assert
        Assert.Equal(new Status.Fail.SyntaxError.UnknownFlag(), result.Status);
    }

    [Fact]
    public void ParseDisconnectSuccess()
    {
        // Arrange
        string command = "disconnect";
        var manager = new FileSystemManager();

        // Act
        Result result = manager.Parse(command);

        // Assert
        Assert.Equal(new Status.Success(), result.Status);

        Assert.IsType<DisconnectCommand>(result.Command);
    }

    [Fact]
    public void ParseTreeGotoSuccess()
    {
        // Arrange
        string command = "tree goto C:\\Drivers";
        var manager = new FileSystemManager();

        // Act
        Result result = manager.Parse(command);

        // Assert
        Assert.Equal(new Status.Success(), result.Status);

        Assert.IsType<TreeGotoCommand>(result.Command);
    }

    [Fact]
    public void ParseTreeGotoCommandTooManyArguments()
    {
        // Arrange
        string command = "tree goto C:\\Drivers -f file.txt";
        var manager = new FileSystemManager();

        // Act
        Result result = manager.Parse(command);

        // Assert
        Assert.Equal(new Status.Fail.SyntaxError.TooManyArguments(), result.Status);
    }

    [Fact]
    public void ParseTreeListSuccess()
    {
        // Arrange
        string command = "tree list -m console -d 7";
        var manager = new FileSystemManager();

        // Act
        Result result = manager.Parse(command);

        // Assert
        Assert.Equal(new Status.Success(), result.Status);

        Assert.IsType<TreeListCommand>(result.Command);
    }

    [Fact]
    public void ParseTreeListCommandNotInitialized()
    {
        // Arrange
        string input = "tree list -d";
        var manager = new FileSystemManager();

        // Act
        Result result = manager.Parse(input);

        // Assert
        Assert.Equal(new Status.Fail.SyntaxError.CommandNotInitialized(), result.Status);
    }

    [Fact]
    public void ParseFileShowSuccess()
    {
        // Arrange
        string input = "file show C:\\Drivers -m console";
        var manager = new FileSystemManager();

        // Act
        Result result = manager.Parse(input);

        // Assert
        Assert.Equal(new Status.Success(), result.Status);

        Assert.IsType<FileShowCommand>(result.Command);
        var command = (FileShowCommand)result.Command;

        Assert.Equal("C:\\Drivers", command.Path);
        Assert.IsType<ConsoleOutput>(command.Output);
    }

    [Fact]
    public void ParseFileShowCommandNotInitializedMissedPath()
    {
        // Arrange
        string input = "file show -m local";
        var manager = new FileSystemManager();

        // Act
        Result result = manager.Parse(input);

        // Assert
        Assert.Equal(new Status.Fail.SyntaxError.UnknownFlag(), result.Status);
    }

    [Fact]
    public void ParseFileShowCommandUnknownFlagValue()
    {
        // Arrange
        string command = "file show D:\\Drivers -m fileOutput";
        var manager = new FileSystemManager();

        // Act
        Result result = manager.Parse(command);

        // Assert
        Assert.Equal(new Status.Fail.SyntaxError.UnknownFlagValue(), result.Status);
    }

    [Fact]
    public void ParseFileShowCommandTooManyArguments()
    {
        // Arrange
        string command = "file show D:\\Drivers -m console -s 4";
        var manager = new FileSystemManager();

        // Act
        Result result = manager.Parse(command);

        // Assert
        Assert.Equal(new Status.Fail.SyntaxError.TooManyArguments(), result.Status);
    }

    [Fact]
    public void ParseFileShowCommandUnknownFlag()
    {
        // Arrange
        string command = "file show D:\\Drivers -s 4 -m local";
        var manager = new FileSystemManager();

        // Act
        Result result = manager.Parse(command);

        // Assert
        Assert.Equal(new Status.Fail.SyntaxError.UnknownFlag(), result.Status);
    }

    [Fact]
    public void ParseFileMoveSuccess()
    {
        // Arrange
        string input = "file move source.txt destination.txt";
        var manager = new FileSystemManager();

        // Act
        Result result = manager.Parse(input);

        // Assert
        Assert.Equal(new Status.Success(), result.Status);

        Assert.IsType<FileMoveCommand>(result.Command);
        var command = (FileMoveCommand)result.Command;

        Assert.Equal("source.txt", command.SourcePath);
        Assert.Equal("destination.txt", command.DestinationPath);
    }

    [Fact]
    public void ParseFileMoveNotInitialized()
    {
        // Arrange
        string input = "file move source.txt";
        var manager = new FileSystemManager();

        // Act
        Result result = manager.Parse(input);

        // Assert
        Assert.Equal(new Status.Fail.SyntaxError.CommandNotInitialized(), result.Status);
    }

    [Fact]
    public void ParseFileCopySuccess()
    {
        // Arrange
        string input = "file copy source.txt destination.txt";
        var manager = new FileSystemManager();

        // Act
        Result result = manager.Parse(input);

        // Assert
        Assert.Equal(new Status.Success(), result.Status);

        Assert.IsType<FileCopyCommand>(result.Command);
        var command = (FileCopyCommand)result.Command;

        Assert.Equal("source.txt", command.SourcePath);
        Assert.Equal("destination.txt", command.DestinationPath);
    }

    [Fact]
    public void ParseFileCopyTooManyArguments()
    {
        // Arrange
        string input = "file move source.txt destination.txt -m smth";
        var manager = new FileSystemManager();

        // Act
        Result result = manager.Parse(input);

        // Assert
        Assert.Equal(new Status.Fail.SyntaxError.TooManyArguments(), result.Status);
    }

    [Fact]
    public void ParseDeleteSuccess()
    {
        // Arrange
        string input = "file delete source.txt";
        var manager = new FileSystemManager();

        // Act
        Result result = manager.Parse(input);

        // Assert
        Assert.Equal(new Status.Success(), result.Status);

        Assert.IsType<FileDeleteCommand>(result.Command);
        var command = (FileDeleteCommand)result.Command;

        Assert.Equal("source.txt", command.Path);
    }

    [Fact]
    public void ParseFileDeleteNotInitialized()
    {
        // Arrange
        string input = "file delete";
        var manager = new FileSystemManager();

        // Act
        Result result = manager.Parse(input);

        // Assert
        Assert.Equal(new Status.Fail.SyntaxError.CommandNotInitialized(), result.Status);
    }

    [Fact]
    public void ParseFileDeleteTooManyArguments()
    {
        // Arrange
        string input = "file delete file.txt -m soft";
        var manager = new FileSystemManager();

        // Act
        Result result = manager.Parse(input);

        // Assert
        Assert.Equal(new Status.Fail.SyntaxError.TooManyArguments(), result.Status);
    }

    [Fact]
    public void ParseFileRenameSuccess()
    {
        // Arrange
        string input = "file rename C:\\Drivers\\test.txt newName";
        var manager = new FileSystemManager();

        // Act
        Result result = manager.Parse(input);

        // Assert
        Assert.Equal(new Status.Success(), result.Status);

        Assert.IsType<FileRenameCommand>(result.Command);
        var command = (FileRenameCommand)result.Command;

        Assert.Equal("C:\\Drivers\\test.txt", command.Path);
        Assert.Equal("newName", command.Name);
    }

    [Fact]
    public void ParseFileRenameNotInitialized()
    {
        // Arrange
        string input = "file rename file.txt";
        var manager = new FileSystemManager();

        // Act
        Result result = manager.Parse(input);

        // Assert
        Assert.Equal(new Status.Fail.SyntaxError.CommandNotInitialized(), result.Status);
    }
}