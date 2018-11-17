using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace C_Compare_Files
{
    class Program
    {
        static void Main(string[] args)
        {

            if(args.Length < 1)
            {
                print_welcome();
                return;
            }

            if(!int.TryParse(args[0], out int num_new_files))
            {
                print_no_num_files_error();
                return;
            }

            int num_files = args.Length - 1;
            int num_arguments_used = 1;
            Byte[][] files = new Byte[num_files][];
            List<int> different_indexes = new List<int>();

            Console.WriteLine("Estou lendo " + num_files + " arquivos");

            for(int i = 0;i < num_files;i++)
            {
                files[i] = read_files(args[i + num_arguments_used]);
            }

            different_indexes =  collect_true_indexes(difference_of_files(files));

            create_new_files(num_new_files,files[0],different_indexes);
            
            create_difference_file(files[0],different_indexes);
            
            
        }


        static void print_welcome(){

                Console.WriteLine("Bem-vindo ao programa para gerar novos programas binários");
                Console.WriteLine(String.Empty);
                Console.WriteLine("O programa deve ser executado da seguinte Maneira:");
                Console.WriteLine("programa.exe <num_files> <file_1> <file_2> ....");
                Console.WriteLine(String.Empty);
                Console.WriteLine("<num_files>: Número de arquivos que irá ser gerados ao executar o programa.");
                Console.WriteLine("<file_1>,<file_2>,...: Arquivos que servirão de entrada para o programa, pode colocar quantos arquivos quiser desde que sejam divididos por espaço");
                Console.WriteLine(String.Empty);
                Console.WriteLine("Os arquivos gerados pelo programa Estarão na pasta output");
                Console.WriteLine("Pressione qualquer tecla para sair");
                Console.ReadKey();
        }

        static void print_no_num_files_error(){

                Console.WriteLine("ERROR: Primeiro Argumento não é um número.");
                Console.WriteLine("Por favor, coloque um número para definir o número de arquivos que irá ser gerado");
                Console.WriteLine(String.Empty);
                Console.WriteLine("Pressione qualquer tecla para sair");
                Console.ReadKey();
        }
        static Byte[] read_files(string path)
        {
            return File.ReadAllBytes(path);
        }

        static Boolean[] difference_of_files(Byte[][] files)
        {
            int file_size = files[0].Length;
            Console.WriteLine("O tamanho do aquivo é " + file_size);
            int num_files = files.Length;
            Boolean[] res = new Boolean[file_size];

            for(int byte_index=0; byte_index < file_size; byte_index++)
            {
                for(int arq1 = 0; arq1 < num_files - 1; arq1++)
                {
                    for(int arq2 = arq1 + 1 ; arq2 < num_files; arq2++)
                    {
                      res[byte_index] = byte_difference(
                        files[arq1][byte_index],
                        files[arq2][byte_index],
                        res[byte_index]);   
                    }
                }
            }

            return res;
        }

        static Boolean byte_difference(Byte b1, Byte b2, Boolean response)
        {
            return !b1.Equals(b2) || response;
        }

        static List<int> collect_true_indexes(Boolean[] input){
            
            List<int> indices = new List<int>();

            for (int i=0;i < input.Length; ++i)
            {
                if (input[i])
                {
                    indices.Add(i);
                }
            }

            return indices;
        }

        static void create_new_files(int num_new_files,Byte[] file_input, List<int> indexes)
        {
            Byte[] temp_byte = new Byte[1];
            Random rnd = new Random();
            DirectoryInfo di = Directory.CreateDirectory("output");

            for(int i = 1; i <= num_new_files; i++)
            {
                foreach(int index in indexes)
                {
                    rnd.NextBytes(temp_byte);
                    file_input[index] = temp_byte[0];
                }

                File.WriteAllBytes(@"output\\E" + i.ToString().PadLeft(3, '0') + ".bin", file_input);
            }

        }

        static void create_difference_file(Byte[] file_input, List<int> indexes){
            
            int file_size = file_input.Length;
            int line_size = 16;
            int num_lines = (int) Math.Ceiling( (double) file_size / line_size ) ;

            string[] lines = new String[num_lines + 1];
            lines[0] = "       0  1  2  3  4  5  6  7  8- 9  A  B  C  D  E  F";
            
            StringBuilder hex;
            int memory_position;
            string memory_position_string;
            byte[] line_bytes;
            int index_byte_line;
            int index_string;

            for(int i=1;i<=num_lines;i++)
            {
                memory_position = ((i-1) * line_size);
                memory_position_string =  memory_position.ToString("X").PadLeft(4,'0') + "  ";

                line_bytes = new byte[line_size];
                Array.Copy(file_input, memory_position, line_bytes, 0, line_size);

                hex = new StringBuilder(BitConverter.ToString(line_bytes).Replace("-", " "));
                
                foreach(int index in indexes)
                {
                    if(index > memory_position && index < memory_position + line_size)
                    {
                        index_byte_line = index % line_size;
                        index_string =  index_byte_line * 4;
                        hex[index_string] = '_';
                        hex[index_string + 1] = '_';
                    }
                }
                
                lines[i] = memory_position_string + hex.ToString();
            }
            
            DirectoryInfo di = Directory.CreateDirectory("output");
            File.WriteAllLines(@"output\\E.txt", lines);

        }
    }
}
