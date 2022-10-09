# AssemblyCompilerPipeline
Very basic way to compile and execute win32 NASM files

How it works:
1. Provide path to nasm.exe
2. Provide path to gcc.exe
(get both from mingw)
3. Provide path to asm file you want to execute.

Program pipes your file into nasm, then links it with gcc, and finally executes it (optional).
