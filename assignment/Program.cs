using System;
using System.IO;
using System.Collections.Generic;

public class InputData
{
    public int R { get; set; }
    public int N { get; set; }
    public int ID { get; set; }
    public int M { get; set; }
    public List<int> Events { get; set; } = new List<int>();
}

class Program
{
    public static InputData? ReadInputFile(string filePath)
    {
        try
        {
            string[] lines = File.ReadAllLines(filePath);
            
            var data = new InputData
            {
                R = int.Parse(lines[0]),
                N = int.Parse(lines[1]),
                ID = int.Parse(lines[2]),
                M = int.Parse(lines[3]),
                Events = new List<int>(Array.ConvertAll(lines[4].Split(' '), int.Parse))
            };
            
            return data;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi đọc file: {ex.Message}");
            Console.WriteLine($"Đường dẫn file: {filePath}");
            return null;
        }
    }

    public static void Display(InputData? data)
    {
        if (data == null)
        {
            Console.WriteLine("Không có dữ liệu để hiển thị");
            return;
        }
        
        Console.WriteLine("Dữ liệu đầu vào:");
        Console.WriteLine($"R: {data.R}");
        Console.WriteLine($"N: {data.N}");
        Console.WriteLine($"ID: {data.ID}");
        Console.WriteLine($"M: {data.M}");
        Console.Write("Events: ");
        foreach (var evt in data.Events)
        {
            Console.Write($"{evt} ");
        }
        Console.WriteLine();
    }

    static void Main(string[] args)
    {
        //Tránh lỗi tiếng Việt
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        // Đọc dữ liệu từ file input.txt
        var inputData = ReadInputFile("C:\\Users\\Khanh\\RiderProjects\\Assignment_2025\\assignment\\input1.txt");
        Display(inputData);
        
        if (inputData != null)
        {
            int result = FirstFight.ProcessGame(inputData);
            Console.WriteLine($"Kết quả: {result}");
        }
    }
}
