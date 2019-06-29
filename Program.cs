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
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            var fileName = Path.Combine(directory.FullName, "PlantingAreasData.json");
            string jsonData = File.ReadAllText(fileName);
            Console.WriteLine(jsonData);

            // get data from json file

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
                    Console.WriteLine("List avaialbe areas");
                    // list available areas and details to user
                    break;
                    

                case 2:
                    Console.WriteLine("Add an area");
                    // display data types to enter
                    // capture user input
                    // 
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
