using FileSystem.Entities.FileSystems;
using FileSystem.Entities.Parsers;
using FileSystem.Entities.Parsers.ArgumentParser.Connect;
using FileSystem.Entities.Parsers.ArgumentParser.FileCopy;
using FileSystem.Entities.Parsers.ArgumentParser.FileDelete;
using FileSystem.Entities.Parsers.ArgumentParser.FileMove;
using FileSystem.Entities.Parsers.ArgumentParser.FileRename;
using FileSystem.Entities.Parsers.ArgumentParser.FileShow;
using FileSystem.Entities.Parsers.ArgumentParser.TreeGoto;
using FileSystem.Entities.Parsers.ArgumentParser.TreeList;
using FileSystem.Entities.Parsers.CommandParsers.Connect;
using FileSystem.Entities.Parsers.CommandParsers.Disconnect;
using FileSystem.Entities.Parsers.CommandParsers.File;
using FileSystem.Entities.Parsers.CommandParsers.Tree;
using FileSystem.Models.Results;
using FileSystem.Services.StringIterators;

namespace FileSystem.Entities.FileSystemManagers;

public class FileSystemManager
{
    private Parser _parser = new Parser();

    public FileSystemManager()
    {
        var connect = new ConnectParser();
        var disconnect = new DisconnectParser();
        var tree = new TreeParser();
        var treeGoto = new TreeGotoParser();
        var treeList = new TreeListParser();
        var file = new FileParser();
        var fileShow = new FileShowParser();
        var fileMove = new FileMoveParser();
        var fileCopy = new FileCopyParser();
        var fileDelete = new FileDeleteParser();
        var fileRename = new FileRenameParser();

        connect.OtherCommandParser = disconnect;
        disconnect.OtherCommandParser = tree;
        tree.OtherCommandParser = file;
        tree.ConcreteTreeCommandParser = treeGoto;
        treeGoto.OtherCommandParser = treeList;
        file.ConcreteFileCommandParser = fileShow;
        fileShow.OtherCommandParser = fileMove;
        fileMove.OtherCommandParser = fileCopy;
        fileCopy.OtherCommandParser = fileDelete;
        fileDelete.OtherCommandParser = fileRename;

        var addressConnect = new ConnectAddressParser();
        var modeConnect = new ConnectModeParser();
        var localModeConnect = new ConnectLocalModeParser();
        var pathGoto = new TreeGotoPathParser();
        var modeList = new TreeListModeParser();
        var consoleModeList = new TreeListConsoleModeParser();
        var depthList = new TreeListDepthParser();
        var pathShow = new FileShowPathParser();
        var modeShow = new FileShowModeParser();
        var consoleModeShow = new FileShowConsoleModeParser();
        var sourceMove = new FileMoveSourcePathParser();
        var destinationMove = new FileMoveDestinationPathParser();
        var sourceCopy = new FileCopySourcePathParser();
        var destinationCopy = new FileCopyDestinationPathParser();
        var pathDelete = new FileDeletePathParser();
        var pathRename = new FileRenamePathParser();
        var nameRename = new FileRenameNameParser();

        connect.ArgumentParser = addressConnect;
        addressConnect.ArgumentParser = modeConnect;
        modeConnect.ModeValueParser = localModeConnect;
        treeGoto.ArgumentParser = pathGoto;
        treeList.ArgumentParser = modeList;
        modeList.ModeValueParser = consoleModeList;
        modeList.NextArgumentParser = depthList;
        fileShow.ArgumentParser = pathShow;
        pathShow.NextArgumentParser = modeShow;
        modeShow.ModeValueParser = consoleModeShow;
        fileMove.ArgumentParser = sourceMove;
        sourceMove.NextArgumentParser = destinationMove;
        fileCopy.ArgumentParser = sourceCopy;
        sourceCopy.NextArgumentParser = destinationCopy;
        fileDelete.ArgumentParser = pathDelete;
        fileRename.ArgumentParser = pathRename;
        pathRename.NextArgumentParser = nameRename;

        _parser.FirstParser = connect;
    }

    public Context Context { get; private set; } = new Context();

    public Result Parse(string command)
    {
        var stringIterator = new StringIterator(command);
        Result result = _parser.Parse(stringIterator);
        return result;
    }

    public Status Execute(Result parsingResult)
    {
        if (parsingResult.Status is Status.Success)
        {
            parsingResult.Command?.Execute(Context);
        }

        return parsingResult.Status;
    }

    public Status Execute(string command)
    {
        Result result = Parse(command);
        return Execute(result);
    }
}