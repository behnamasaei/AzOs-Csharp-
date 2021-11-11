// See https://aka.ms/new-console-template for more information
// Program useage c# v10

using Assignment_6;


Main();

static void Main()
{
    Console.Clear();
    Console.WriteLine("Loading...");

    string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "./words_bank.txt");
    string fileWords = File.ReadAllText(pathFile);

    var words = fileWords
                    .Split(new[] { '\r', '\n' })
                    .ToList<string>()
                    .FindAll(EmptyString);

    List<LanModel> Dict = new List<LanModel>();

    for (int i = 0, j = 1; j < words.Count; i += 2, j += 2)
    {
        Dict.Add(new LanModel
        {
            EnWord = words[i].ToString(),
            FaWord = words[j].ToString()
        });
    }

    Console.WriteLine("Hello, select from menu...");
    Console.WriteLine($"1. add new word\n" +
        $"2. translation english2persian\n" +
        $"3. translation persian2english\n" +
        $"4. Exit");

    int select = Convert.ToInt32(Console.ReadLine());
    while (true)
    {
        switch (select)
        {
            case 1:
                AddNewWord(pathFile);
                break;

            case 2:
                english2persian(Dict);
                break;

            case 3:
                persian2english(Dict);
                break;

            case 4:
                return;

            default:
                break;
        }
    }
}



static async void AddNewWord(string pathFile)
{
    Console.WriteLine("Enter English word:");
    string en = Console.ReadLine();
    Console.WriteLine("Enter Persian word:");
    string fa = Console.ReadLine();

    string? [] lines = {en, fa };
    await File.AppendAllTextAsync(pathFile,"\n"+ en+"\n"+fa);
    Console.WriteLine("Word added.");

    Console.ReadKey();
    Main();
}

static bool EmptyString(string s) => s.Length != 0;

static void english2persian(List<LanModel> dict)
{
    Console.Clear();
    Console.WriteLine("Enter your text:");
    var text = Console.ReadLine();

    var words = text
                .Split(new[] { '\r', '\n', ' ', '.', '?' })
                .ToList<string>();

    string? translate = null;
    foreach (string word in words)
    {

        if (dict.Any(d => d.EnWord.Contains(word.ToLower()))
            && !string.IsNullOrWhiteSpace(word)
            && !string.IsNullOrEmpty(word))
            translate = dict.SingleOrDefault(x => x.EnWord == word.ToLower()).FaWord;
        else
            translate = word;


        Console.Write(translate + ' ');
    }
    Console.ReadKey();
    Main();
}

static void persian2english(List<LanModel> dict)
{

    Console.Clear();
    Console.WriteLine("Enter your text:");
    var text = Console.ReadLine();

    var words = text
                .Split(new[] { '\r', '\n', ' ', '.', '?' })
                .ToList<string>();

    string? translate = null;
    foreach (string word in words)
    {

        if (dict.Any(d => d.FaWord.Contains(word.ToLower()))
            && !string.IsNullOrWhiteSpace(word)
            && !string.IsNullOrEmpty(word))
            translate = dict.FirstOrDefault(x => x.FaWord == word.ToLower()).EnWord;
        else
            translate = word;


        Console.Write(translate + ' ');
    }
    Console.ReadKey();
    Main();
}