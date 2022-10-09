
namespace AssemblyCompilerPipeline;

internal static class Program
{
    private static void Main(string[] args)
    {
        if(args.Length < 3)
            return;
        
        var nasmPath = args[0];
        var gccPath = args[1];
        var asmFile = args[2];

        Console.WriteLine("Compiling to Object file...");
        CompileToObject(nasmPath, asmFile, out var compileResult);
        Console.WriteLine("CompileResult: " + compileResult);
        Console.WriteLine("Linking to Executable file...");
        Link(gccPath, asmFile, out var linkResult);
        Console.WriteLine("LinkResult: " + linkResult);
        Console.WriteLine("Executing...");
        Execute(asmFile, out var output);
        Console.WriteLine("Output: " + output);
    }

    private static void CompileToObject(string nasm, string file, out string result)
    {
        string text = nasm + " -fwin32 " + file;
        result = ExecuteCommands.ExecuteCommand(text);
    }
    
    private static void Link(string gcc, string file, out string result)
    {
        string rawFilePath = file.Replace(".asm", "");
        string text = gcc  + " " + rawFilePath + ".obj" + " -o " + rawFilePath + ".exe"; 
        result = ExecuteCommands.ExecuteCommand(text);
    }
    
    private static void Execute(string file, out string result)
    {
        string text = file.Replace(".asm", ".exe");
        result = ExecuteCommands.ExecuteCommand(text);
    }
}