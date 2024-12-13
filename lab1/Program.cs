using System.Text.Json;
using System.Globalization;
namespace lab1

{
  public class LastNames(string[] data)
  {
    public string[] Data { get; set; } = data;
  }

  public class UserInputArguments(string inputFile, string searchLetters)
  {
    public string InputFile { get; } = inputFile;
    public string SearchLetters { get; } = searchLetters;
  }
  public class Program
  {
    public static void Main()
    {
      string[] rawArgs = Environment.GetCommandLineArgs();
      UserInputArguments args = ParseArgs(rawArgs);
      LastNames lastNames = ParseLastNamesJson(args.InputFile);
      SortByLength(lastNames.Data);

      for (int i = 0; i < lastNames.Data.Length; i++)
      {
        if (lastNames.Data[i].StartsWith(args.SearchLetters))
        {
          System.Console.WriteLine(lastNames.Data[i]);
        }
      }
    }

    private static UserInputArguments ParseArgs(string[] args)
    {
      const string helpMessage = "Usage example: -i=lastnames.json -s=P\ni - input file\ns - search letters";

      if (args.Length != 3)
      {
        throw new Exception($"Wrong amount of arguments privided.\n{helpMessage}");
      }

      int inputFileArg = Array.FindIndex(args, e => e.StartsWith("-i="));
      int searchLettersArg = Array.FindIndex(args, e => e.StartsWith("-s="));
      if (inputFileArg == -1 || searchLettersArg == -1)
      {
        throw new Exception($"Invalid arguments.\n{helpMessage}");
      }

      string inputFile = args[inputFileArg].Split("=")[1];
      string searchLetters = ToTitleCase(args[searchLettersArg].Split("=")[1]);

      return new UserInputArguments(inputFile, searchLetters);
    }

    private static LastNames ParseLastNamesJson(string file)
    {
      const string jsonDeserializationErrorMessage = "Error parsing JSON. Required sructure: {Data: string[]}";
      LastNames? lastNames;

      if (!File.Exists(file))
      {
        throw new Exception($"File {file} not found.");
      }

      try
      {
        string jsonString = File.ReadAllText(file);
        lastNames = JsonSerializer.Deserialize<LastNames>(jsonString);
      }
      catch (System.Exception e)
      {
        if (e is UnauthorizedAccessException)
        {
          throw new Exception("Have access to read the provided file.");
        }
        throw new Exception(jsonDeserializationErrorMessage);
      }

      if (lastNames?.Data == null || lastNames.Data.Length == 0)
      {
        throw new Exception(jsonDeserializationErrorMessage);
      }

      return lastNames;
    }

    private static void SortByLength(string[] data)
    {
      Array.Sort(data, (x, y) => { return y.Length - x.Length; });
    }
    private static string ToTitleCase(string text)
    {
      TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
      return textInfo.ToTitleCase(textInfo.ToLower(text));

    }
  }
}