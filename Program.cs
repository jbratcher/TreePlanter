﻿using Newtonsoft.Json;
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

            // Create tree planting areas list from json file and PlantingArea class

            string currentDirectory = Directory.GetCurrentDirectory();
            Console.WriteLine(currentDirectory);
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);

            // Prepare planting areas data
            var plantingAreasJsonFile = Path.Combine(directory.FullName, "PlantingAreasData.json");
            Console.WriteLine(plantingAreasJsonFile);
            // var <List<PlantingArea>>
            var areas = DeserializePlantingAreas(plantingAreasJsonFile);
            foreach (var area in areas)
            {
                Console.WriteLine(area.ShortName);
            }

            // modify data for display to user if display option chosen

            // Greet the user
            Console.WriteLine("Welcome to Tree Planter!\n");
            Console.WriteLine("Find or add places to plant trees in Louisville\n");

            // Display main menu and get user choice
            Console.WriteLine(string.Concat(Enumerable.Repeat("/", 43)));
            Console.WriteLine("// Enter your choice from the menu below //");
            Console.WriteLine("// 1. See available areas to plant       //");
            Console.WriteLine("// 2. Add and area to plant              //");
            Console.WriteLine(string.Concat(Enumerable.Repeat("/", 43)));

            // ask for and get user input
            Console.WriteLine("Choice: ");
            var input = Console.ReadLine();

            // process choice
            switch (Convert.ToInt32(input))
            {
                case 1:
                    // list available areas and details to user
                    Console.WriteLine("Available Areas:\n");
                    Console.WriteLine(String.Format("{0,-10} | {1,-11} | {2,5}", "Name", "Open Spaces", "Address"));
                    foreach (var area in areas)
                    {
                        Console.WriteLine(String.Format("{0,-10} | {1,-11} | {2,5}", area.ShortName, area.OpenSpaces, area.Address));
                    }
                    break;
                    

                case 2:
                    Console.WriteLine("Add an area");
                    // capture user input
                    Console.WriteLine("Enter the area name: "); 
                    string name = Console.ReadLine();
                    Console.WriteLine("Enter the area address: ");
                    string address = Console.ReadLine();
                    Console.WriteLine("Enter the number of spaces avaialbe: ");
                    int spaces = Convert.ToInt32(Console.ReadLine());
                    // make new obj with input data
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
                    Console.WriteLine(areas.Count);
                    // save list back to file
                    using (StreamWriter file = File.CreateText(plantingAreasJsonFile))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(file, areas);
                    }

                    break;
                    
            }

            // Exit script
            Console.WriteLine("Press any key to exit...\n");
            Console.ReadKey();


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
