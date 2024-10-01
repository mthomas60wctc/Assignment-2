using NLog;
string path = Directory.GetCurrentDirectory() + "//nlog.config";
// create instance of Logger
var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();

logger.Info("Program started");

string file = "mario.csv";
// make sure movie file exists
if (!File.Exists(file))
{
    logger.Error("File does not exist: {File}", file);
}
else
{
    // create parallel lists of character details
    // lists are used since we do not know number of lines of data
    List<Character> CharacterList = [];
    // to populate the lists with data, read from the data file
    try
    {
        StreamReader sr = new(file);
        // first line contains column headers
        sr.ReadLine();
        while (!sr.EndOfStream)
        {
            string? line = sr.ReadLine();
            if (line is not null)
            {
                // character details are separated with comma(,)
                string[] characterDetails = line.Split(',');
                // 1st array element contains id
                Character tempChar = new Character(
                    UInt64.Parse(characterDetails[0]),
                    // 2nd array element contains character name
                    characterDetails[1],
                    // 3rd array element contains character description
                    characterDetails[2],
                    // 3rd array element contains character description
                    characterDetails[3],
                    // 3rd array element contains character description
                    characterDetails[4],
                    // 3rd array element contains character description
                    UInt64.Parse(characterDetails[5]));
                CharacterList.Add(tempChar);

            }
        }
        sr.Close();
    }
    catch (Exception ex)
    {
        logger.Error(ex.Message);
    }
    string? choice;
    do
    {
        // display choices to user
        Console.WriteLine("1) Add Character");
        Console.WriteLine("2) Display All Characters");
        Console.WriteLine("Enter to quit");

        // input selection
        choice = Console.ReadLine();
        logger.Info("User choice: {Choice}", choice);

        if (choice == "1")
        {
            // Add Character
            Console.WriteLine("Enter new character name: ");
            string? Name = Console.ReadLine();
            if (!string.IsNullOrEmpty(Name))
            {
                // check for duplicate name
                List<string> LowerCaseNames = CharacterList.ConvertAll(n => n.Name.ToLower());
                if (LowerCaseNames.Contains(Name.ToLower()))
                {
                    logger.Info($"Duplicate name {Name}");
                }
                else
                {
                    // generate id - use max value in Ids + 1
                    UInt64 Id = CharacterList.Max(c => c.Id) + 1;
                    // input character description
                    Console.WriteLine("Enter description:");
                    string Description = Console.ReadLine() ?? "";
                    // input character description
                    Console.WriteLine("Species:");
                    string Specie = Console.ReadLine() ?? "";
                    // input character description
                    Console.WriteLine("First game appearance:");
                    string FirstAppearance = Console.ReadLine() ?? "";
                    // input character description
                    Console.WriteLine("Year of first appearance:");
                    UInt64 YearCreated = UInt64.Parse(Console.ReadLine() ?? "0");
                    Character tempChar = new Character(Id, Name, Description, Specie, FirstAppearance, YearCreated);
                    // create file from data
                    StreamWriter sw = new(file, true);
                    sw.WriteLine(tempChar.GenerateCSV());
                    sw.Close();
                    // add new character details to List
                    CharacterList.Add(tempChar);
                    // log transaction
                    logger.Info($"Character id {Id} added");
                }
            }
            else
            {
                logger.Error("You must enter a name");
            }
        }
        else if (choice == "2")
        {
            // Display All Characters   
            // loop thru Lists
            foreach (Character c in CharacterList)
            {
                // display character details
                Console.WriteLine(c.DisplayCharacter());
            }
        }
    } while (choice == "1" || choice == "2");
}

logger.Info("Program ended");