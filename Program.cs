using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TreePlanter
{
    class Program
    {
        static void Main(string[] args)
        {

            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);

            // Prepare planting areas data
            var plantingAreasJsonFile = Path.Combine(directory.FullName, "PlantingAreasData.json");
            // var <List<PlantingArea>>
            var areas = DeserializePlantingAreas(plantingAreasJsonFile);

            // modify data for display to user if display option chosen

            // Greet the user
            Console.WriteLine("Welcome to Tree Planter!\n");
            Console.WriteLine("Find, add, and fill places to plant trees in Louisville\n");


            // Display main menu and get user choice
            Console.WriteLine(string.Concat(Enumerable.Repeat("/", 43)));
            Console.WriteLine("// Enter your choice from the menu below //");
            Console.WriteLine("// 1. See available areas to plant       //");
            Console.WriteLine("// 2. Add an area to plant               //");
            Console.WriteLine("// 3. Edit an area to plant              //");
            Console.WriteLine("// 4. Fill an area to plant              //");
            Console.WriteLine("// 5. Quit application                   //");
            Console.WriteLine(string.Concat(Enumerable.Repeat("/", 43)));

            bool runapp = true;
            int userInput = 0;
            do
            {

                try
                {

                    // ask for and get user input
                    Console.WriteLine("\nChoice: ");
                    userInput = Convert.ToInt32(Console.ReadLine());

                    // process choice
                    switch (userInput)
                    {
                        // list available areas and details to user 
                        case 1:
                            Console.WriteLine("Available Areas:\n");
                            Console.WriteLine(String.Format("{0,-10} | {1,-11} | {2,5}", "Name", "Open Spaces", "Address"));
                            foreach (var area in areas)
                            {
                                Console.WriteLine(String.Format("{0,-10} | {1,-11} | {2,5}", area.ShortName, area.OpenSpaces, area.Address));
                            }
                            break;

                        // add a new planting area
                        case 2:
                            Console.WriteLine("Add an area");
                            Console.WriteLine("___________");
                            Console.WriteLine("Name: ");
                            string name = Console.ReadLine();
                            Console.WriteLine("Address: ");
                            string address = Console.ReadLine();
                            Console.WriteLine("Spaces avaialbe: ");
                            int spaces = Convert.ToInt32(Console.ReadLine());

                            // make new obj with user input data
                            PlantingArea newArea = new PlantingArea
                            {
                                UniqueID = areas.Count + 1,
                                ShortName = name,
                                Address = address,
                                OpenSpaces = spaces,
                                DateSubmitted = DateTime.Now

                            };

                            // add new planting area to list
                            areas.Add(newArea);

                            // save/serialize list back to file
                            using (StreamWriter file = File.CreateText(plantingAreasJsonFile))
                            {
                                JsonSerializer serializer = new JsonSerializer();
                                serializer.Serialize(file, areas);
                            }

                            break;

                        case 3:
                            // edit an area
                            break;

                        case 4:
                            // delete an area
                            Console.WriteLine("Remove an area");
                            Console.WriteLine("___________");
                            Console.WriteLine("Name: ");
                            var nameToDelete = Console.ReadLine();
                            areas.RemoveAll(area => area.ShortName == nameToDelete);
                            Console.WriteLine(nameToDelete + " has been deleted.\nPress any key to continue...");
                            Console.ReadKey();
                            break;

                        case 5:
                            runapp = false;
                            // Exit script
                            Console.WriteLine("Press any key to exit...\n");
                            Console.ReadKey();
                            break;

                        default:
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(" Error: Invalid Choice");
                            Console.ResetColor();
                            System.Threading.Thread.Sleep(2000);
                            break;
                    }

                } catch(Exception e)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" Unexpected Error:");
                    Console.WriteLine(e);
                    Console.ResetColor();
                    System.Threading.Thread.Sleep(2000);
                }

            } while (runapp != false);

        }       

        // Deserialize json data

        public static List<PlantingArea> DeserializePlantingAreas(string fileName)
        {
            var plantingAreas = new List<PlantingArea>();
            var serializer = new JsonSerializer();
            using (var reader = new StreamReader(fileName))
            using (var jsonReader = new JsonTextReader(reader))
            {
                plantingAreas = serializer.Deserialize<List<PlantingArea>>(jsonReader);
            }

            return plantingAreas;
        }

    }
}
