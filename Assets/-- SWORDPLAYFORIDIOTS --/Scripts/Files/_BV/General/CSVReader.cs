using UnityEngine;
using System.Linq;
using System.IO;

public static class CSVReader
{
    /// <summary>
    /// Gets the path of where the application is installed
    /// </summary>
    /// <returns>The path of the games instal location</returns>
    static string GetPath(string _fileName)
    {
        return Application.dataPath.Substring(0,
            Application.dataPath.LastIndexOf('/')) + _fileName;
    }

    static public string[,] GetCSVGrid(string _fileName)
    {
        string path = GetPath(_fileName);
        StreamReader reader = new StreamReader(path);
        string textData = reader.ReadToEnd();
        //Debug.Log(reader.ReadToEnd());
        reader.Close();

        return SplitCsvGrid(textData);
    }

    static public string[,] GetCSVGrid(TextAsset _file)
    {
        Debug.Log("file name: " + _file);
        //string path = GetPath(_fileName);
        StreamReader reader = new StreamReader(_file.text);
        string textData = reader.ReadToEnd();
        Debug.Log(reader.ReadToEnd());
        reader.Close();

        return SplitCsvGrid(textData);
    }


    // outputs the content of a 2D array, useful for checking the importer
    static public void DebugOutputGrid(string[,] grid)
    {
        string textOutput = "";
        for (int y = 0; y < grid.GetUpperBound(1); y++)
        {
            textOutput += grid[0, y];
            for (int x = 0; x < grid.GetUpperBound(0); x++)
            {
                textOutput += grid[x, y];
                textOutput += "|";
            }
            textOutput += "\n";
        }
        Debug.Log(textOutput);
    }

    // splits a CSV file into a 2D string array
    static public string[,] SplitCsvGrid(string csvText)
    {
        string[] lines = csvText.Split("\n"[0]);

        // finds the max width of row
        int width = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            string[] row = SplitCsvLine(lines[i]);
            width = Mathf.Max(width, row.Length);
        }

        // creates new 2D string grid to output to
        string[,] outputGrid = new string[width, lines.Length];
        for (int y = 0; y < lines.Length; y++)
        {
            string[] row = SplitCsvLine(lines[y]);
            for (int x = 0; x < row.Length; x++)
            {
                outputGrid[x, y] = row[x];

                // This line was to replace "" with " in my output. 
                // Include or edit it as you wish.
                outputGrid[x, y] = outputGrid[x, y].Replace("\"\"", "\"");
            }
        }

        return outputGrid;
    }

    // splits a CSV row 
    static public string[] SplitCsvLine(string line)
    {
        return (from System.Text.RegularExpressions.Match m in System.Text.RegularExpressions.Regex.Matches(line,
        @"(((?<x>(?=[,\r\n]+))|""(?<x>([^""]|"""")+)""|(?<x>[^,\r\n]+)),?)",
        System.Text.RegularExpressions.RegexOptions.ExplicitCapture)
                select m.Groups[1].Value).ToArray();
    }
}