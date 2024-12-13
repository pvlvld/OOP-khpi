using System.Text.Json;
using System.Globalization;
namespace lab2
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

    public class Address(string city, int zip, string street, int house, int appartment)
    {
        public string City { get; set; } = city;
        public int ZipCode { get; set; } = zip;
        public string Street { get; set; } = street;
        public int House { get; set; } = house;
        public int Appartment { get; set; } = appartment;
    }

    public class Student(string firstName, string lastName, Address address)
    {
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
        public Address Address
        {
            get; set;
        } = address;
    }

    public class Group
    {
        private readonly List<Student> students = new List<Student>();
        public void AddStudent(Student student)
        {
            students.Add(student);
        }
        public void RemoveStudent(Student student)
        {
            students.Remove(student);
        }
        public
     IReadOnlyList<Student> Students
        {
            get { return students; }
        }
        public int Size
        {
            get { return students.Count; }
        }
    }

    public class StudentSearch
    {
        public static IEnumerable<Student> SearchByLastName(IEnumerable<Student> students, string firstLetters)
        {
            return students
                .Where(s => s.LastName.StartsWith(firstLetters, StringComparison.InvariantCultureIgnoreCase))
                .OrderByDescending(s => s.LastName.Length);
        }
    }


    public class Program
    {
        public static void Main()
        {
            string[] rawArgs = Environment.GetCommandLineArgs();
            UserInputArguments args = ParseArgs(rawArgs);
            LastNames lastNames = ParseLastNamesJson(args.InputFile);
            Group group = new();
            for (int i = 0; i < lastNames.Data.Length; i++)
            {
                Student student = new("Тарас", lastNames.Data[i], new Address("Kharkiv", 61000, "Tarasa Shevchenka", 1, 1));
                group.AddStudent(student);
            }
            var searchResults = StudentSearch.SearchByLastName(group.Students, args.SearchLetters);
            PrintStudents(searchResults);
        }

        private static UserInputArguments ParseArgs(string[] args)
        {
            const string helpMessage = "Usage example: -i=lastnames.json -s=P\ni - input file\ns - search letters";

            if (args.Length != 3)
            {
                throw new Exception($"Wrong amount of arguments provided.\n{helpMessage}");
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
        private static string ToTitleCase(string text)
        {
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(textInfo.ToLower(text));

        }

        private static void PrintStudents(IEnumerable<Student> students)
        {
            foreach (Student s in students)
            {
                Console.WriteLine(s.LastName);
            }
        }
    }
}