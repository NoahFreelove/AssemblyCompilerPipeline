namespace AssemblyCompilerPipeline;

public static class Program
{
    public static string Run(string[] args)
    {
        switch (args.Length)
        {
            case < 3:
                return "invalid arguments: expected at least 3";
            case >= 4:
                args[3] = "execute=false";
                break;
        }

        Main(args);
        
        return ExecuteWithoutOutput(args[2]);
    }

    private static void Main(string[] args)
    {
        if(args.Length < 3)
            return;
        
        var nasmPath = args[0];
        var gccPath = args[1];
        var asmFile = args[2];
        var execute = true;

        if (args.Length >= 4)
        {
            execute = args[3] switch
            {
                "execute=false" => false,
                "execute=true" => true,
                _ => execute
            };
        }

        CompileToObject(nasmPath, asmFile);
        Link(gccPath, asmFile);
        
        if(execute)
            Execute(asmFile);
        
    }

    private static void CompileToObject(string nasm, string file)
    {
        Console.WriteLine("Compiling to Object file...");

        var text = nasm + " -fwin32 " + file;
        Console.WriteLine("CompileResult: " + ExecuteCommands.ExecuteCommand(text));
    }
    
    private static void Link(string gcc, string file)
    {
        Console.WriteLine("Linking to Executable file...");
        var rawFilePath = file.Replace(".asm", "");
        var text = gcc  + " " + rawFilePath + ".obj" + " -o " + rawFilePath + ".exe"; 
        
        Console.WriteLine("LinkResult: " + ExecuteCommands.ExecuteCommand(text));

    }
    
    private static void Execute(string file)
    {
        Console.WriteLine("Executing...");
        var text = file.Replace(".asm", ".exe");
        Console.WriteLine("Output: " + ExecuteCommands.ExecuteCommand(text));
    }

    private static string ExecuteWithoutOutput(string file)
    {
        var text = file.Replace(".asm", ".exe");
        return ExecuteCommands.ExecuteCommand(text);
    }
}