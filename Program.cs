using OOP_LAB_2;
// Driver code for class Cat
// Not the best code, but it works ¯\_(ツ)_/¯

static Cat CreateCat() {
    string? input;

    // Name
    while (true) {
        Console.Write("Cat name: ");
        input = Console.ReadLine();
        if (input != null) { break; }
    }
    string catName = Utils.Capitalize(input.Trim());

    // Age
    while (true) {
        Console.Write("Cat age (in years): ");
        input = Console.ReadLine();
        if (Double.TryParse(input, out double tmp) && tmp > 0) { break; }
    }
    double catAge = double.Parse(input);

    // Gender
    while (true) {
        Console.Write("Cat gender (M or F): ");
        input = Console.ReadLine();
        if (input != null) {
            input = input.Trim().ToLower();
            if (input == "f" || input == "m") { break; }
        }
    }
    Gender catGender = (input == "m") ? Gender.Male : Gender.Female;

    // Type
    while (true) {
        Console.Write("Cat type (British, Jellie, Persian, Ragdoll, Siamese, Tabby, Tuxedo): ");
        input = Console.ReadLine();
        if (input != null) {
            // Do not allow typing in numbers
            if (int.TryParse(input, out _)) { continue; }
            input = Utils.Capitalize(input.Trim());
            if (Enum.TryParse(typeof(CatType), input, true, out _)) { break; }
        }
    }
    CatType catType = (CatType) Enum.Parse(typeof(CatType), input);

    return new Cat(catName, catAge, catGender, catType);
}

static Cat EditCat(Cat cat) {
    string? input = "";
    Console.WriteLine($"Editing (id: {cat.Id}, name: {cat.Name}).");
    Console.WriteLine($"Press Enter to skip the field.");

    var (catName, catAge, catGender, catType) = cat;

    // Name
    // This while is kinda useless
    while (true) {
        Console.Write($"Cat name [{catName}]: ");
        input = Console.ReadLine();
        // Change name if new was given.
        if (input != "" && input != null) { catName = Utils.Capitalize(input.Trim()); break; }
        // Just exit if not
        break;
    }

    // Age
    while (true) {
        Console.Write($"Cat age (in years) [{catAge}]: ");
        input = Console.ReadLine();
        if (Double.TryParse(input, out double tmp) && tmp > 0) { catAge = double.Parse(input); break; }
        // Exit without changes
        if (input == "") { break; }
    }

    // Gender
    while (true) {
        Console.Write($"Cat gender (M or F) [{catGender}]: ");
        input = Console.ReadLine();
        // Try changing age if new was given (Will loop back if value is invalid).
        if (input != "" && input != null) {
            input = input.Trim().ToLower();
            if (input == "f" || input == "m") { catGender = (input == "m") ? Gender.Male : Gender.Female; break; }
            continue;
        }
        // Just exit if not
        break;
    }

    // Type
    while (true) {
        Console.Write($"Cat type (British, Jellie, Persian, Ragdoll, Siamese, Tabby, Tuxedo) [{catType}]: ");
        input = Console.ReadLine();
        // Try changing type if new was given (Will loop back if value is invalid).
        if (input != "" && input != null) {
            // Do not allow typing in numbers
            if (int.TryParse(input, out _)) { continue; }
            input = Utils.Capitalize(input.Trim());
            if (Enum.TryParse(typeof(CatType), input, true, out _)) { catType = (CatType) Enum.Parse(typeof(CatType), input);  break; }
            continue;
        }
        // Just exit if not
        break;
    }

    // Update properties
    cat.Name = catName;
    cat.Age = catAge;
    cat.Gender = catGender;
    cat.Type = catType;
    // and return
    return cat;
}

string? input;
while (true) {
    Console.Write("# of objects: ");
    input = Console.ReadLine();
    if (int.TryParse(input, out int tmp) && tmp > 0) { break; }
}
int N = int.Parse(input);
List<Cat> cats = new(N);

static void PrintHelp() => Console.WriteLine("Commands: add, list, find, delete, edit, meow, purr, exit");
IEnumerable<Cat> FindCats(string filter) => cats.Where(x => x.Name.ToLower() == filter || x.Id.ToString() == filter);

PrintHelp();
while (true) {
    Console.Write("> ");
    input = Console.ReadLine();
    if (input == null) { continue; }

    // Some variables
    string filter;
    Cat[] result;

    var cmdArgs = input.Trim().Split(' ', 2);
    if (input != null) {
        switch (cmdArgs[0]) {
            case "?":
            case "help":
                PrintHelp();
                break;
            case "+":
            case "add":
                cats.Add(CreateCat());
                break;
            case "ls":
            case "list":
                // List all cats
                Console.WriteLine($"Cats ({cats.Count}):");
                foreach (Cat cat in cats) {
                    Console.WriteLine($"{cat.Id} | {cat}");
                }
                break;
            case "show":
            case "find":
                // Find cat(s) by Name/Id
                if (cmdArgs.Length != 2) { Console.WriteLine($"Usage: {cmdArgs[0]} < Id | Name>"); break; }
                filter = cmdArgs[1].ToLower();
                result = FindCats(filter).ToArray();
                if (result.Length != 0) {
                    Console.WriteLine($"Cats ({cats.Count}):");
                    foreach (Cat cat in result) {
                        Console.WriteLine($"{cat.Id} | {cat}");
                    }
                }
                else {
                    Console.WriteLine("No results for given Id/Name");
                }
                break;
            case "meow":
                // Find cat(s) by Name/Id
                if (cmdArgs.Length != 2) { Console.WriteLine($"Usage: {cmdArgs[0]} < Id | Name>"); break; }
                filter = cmdArgs[1].ToLower();
                result = FindCats(filter).ToArray();
                if (result.Length != 0) {
                    foreach (Cat cat in result) {
                        cat.Meow();
                    }
                }
                else {
                    Console.WriteLine("No results for given Id/Name");
                }
                break;
            case "purr":
                // Find cat(s) by Name/Id
                if (cmdArgs.Length != 2) { Console.WriteLine($"Usage: {cmdArgs[0]} < Id | Name>"); break; }
                filter = cmdArgs[1].ToLower();
                result = FindCats(filter).ToArray();
                if (result.Length != 0) {
                    foreach (Cat cat in result) {
                        cat.Purr();
                    }
                }
                else {
                    Console.WriteLine("No results for given Id/Name");
                }
                break;
            case "-":
            case "rm":
            case "del":
            case "delete":
                // Delete cat(s) by Name/Id
                if (cmdArgs.Length != 2) { Console.WriteLine($"Usage: {cmdArgs[0]} <Id | Name>"); break; }
                filter = cmdArgs[1].ToLower();

                result = cats.Where(x => (x.Name.ToLower() == filter || x.Id.ToString() == filter)).ToArray();
                if (result.Length != 0) {
                    Console.WriteLine($"The following entries were deleted:");
                    foreach (Cat cat in result) {
                        Console.WriteLine($"{cat.Id} | {cat}");
                    }
                }
                else {
                    Console.WriteLine("No results for given Id/Name");
                }
                // Actually remove from the list
                cats.RemoveAll(x => (x.Name.ToLower() == filter || x.Id.ToString() == filter));

                break;
            case "edit":
                // Edit cat by Id
                if (cmdArgs.Length != 2) { Console.WriteLine("Usage: edit <Id>"); break; }
                filter = cmdArgs[1].ToLower();
                if (!int.TryParse(filter, out int filter_id)) {
                    Console.WriteLine("Usage: edit <Id>");
                }

                int index = -1;
                // Iterate through array
                for (int i = 0; i < cats.Count; i++) {
                    if (cats[i].Id == filter_id) {
                        cats[i] = EditCat(cats[i]);
                        index = i;
                        break;
                    }
                }

                if (index >= 0) {
                    Console.WriteLine("Edit result:");
                    Console.WriteLine($"{cats[index].Id} | {cats[index]}");
                }
                else {
                    Console.WriteLine("No results for given Id");
                }
                break;
            case "exit":
                Console.WriteLine("Bye bye.");
                Environment.Exit(0);
                // Fall through case after exit? What? VS, you good?
                break;
            default:
                Console.WriteLine("Unknown command.");
                break;
        }
    }
}