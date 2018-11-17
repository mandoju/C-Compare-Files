# Binary Files comparator and new files generator based on differences
 This program compare many different files and generate new files, putting random values in bytes that are different among the input files.
 This program was made in C# using .NET.
 The program run the following steps:
 - Read all the files and put in the memory
 - Compare all the files to know what bytes are different among them.
 - Create new files beginning with letter 'E' (e.g: 'E001.bin') with different values in the indexes of the different bytes found in the previous step.
 - Make a file to evaluate which bytes where manipulated during the run.

# Future 
 The next steps, i'll try to implement the following features:
 - Read part of the file and work with it, to not consume all the memory of the system.
 - Utilize parallel computing to do the comparisons among the files



# Comparador de arquivos binários e gerador de novos arquivos baseado nas diferenças
 Este programa  compara vários arquivos binários diferentes e gera arquivos novos, colocando valores randômicos nos bytes onde são diferentes entre os arquivos de entrada.
 Este programa foi feito em C# utilizando .NET .
 O programa executa da seguinte maneira:

 - Lê todos os arquivos e coloca na memória.
 - Faz uma comparação para descobrir quais são os bytes que são diferentes em todos os arquivos
 - Cria novos arquivos com a inicial 'E' (ex: 'E001.bin') com valores diferentes nos índices dos bytes descobertos no passo anterior.
 - Cria um arquivo para avaliação de quais bytes foram alterados no processo.

# Futuro
 Para o futuro será implementado as seguintes funcionalidades:
 - Leitura de parte dos arquivos em casos de arquivos muito grandes
 - Utilização de computação concorrente para paralelizar o processo de comparação

