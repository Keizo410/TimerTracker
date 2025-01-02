using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleAppCheck
{
    internal class GetUserInput
    {
        CodingController codingController = new();
        internal void MainMenu(){
            bool closeApp = false;

            while(closeApp==false){
                Console.WriteLine("\n\nMAIN MENU");
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("\nType 0 to Close Application.");
                Console.WriteLine("Type 1 to View record");
                Console.WriteLine("Type 2 to Add record");
                Console.WriteLine("Type 3 to Delete record");
                Console.WriteLine("Type 4 to Update record");

                string commandInput = Console.ReadLine();

                while (string.IsNullOrEmpty(commandInput)){
                    Console.WriteLine("\nInvalid Command. Please type a number form 0 to 4.\n");
                    commandInput = Console.ReadLine();
                }

                switch(commandInput){
                    case "0":
                        closeApp=true;
                        Environment.Exit(0);
                        break;
                    case "1":
                        codingController.Get();
                        break;
                    case "2":
                        ProcessAdd();
                        break;
                    case "3":
                        ProcessDelete();
                        break;
                    case "4":
                        ProcessUpdate();
                        break;
                    default:
                        Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.\n");
                        break;
                }
            }
        }

        private void ProcessUpdate(){
            codingController.Get();

            Console.WriteLine("Please add id of the category you want to update (or 0 to return to Main Menu). \n\n");
            string commandInput = Console.ReadLine();

            while (!Int32.TryParse(commandInput, out _) || string.IsNullOrEmpty(commandInput) || Int32.Parse(commandInput) < 0)
            {
                Console.WriteLine("\nYou have to type an Id (or 0 to return to Main Menu).\n");
                commandInput = Console.ReadLine();
            } 

            var id = Int32.Parse(commandInput);

            if(id == 0) MainMenu();

            var coding = codingController.GetById(id);

            while (coding.Id == 0){
                Console.WriteLine($"\nRecord with {id} doesn't exist\n");
                ProcessUpdate();
            }

            bool updating = true;
            var updateInput = "";

            while(updating == true)
            {
                Console.WriteLine($"\nType 'd' for Date\n");
                Console.WriteLine($"\nType 't' for Duration\n");
                Console.WriteLine($"\nType 's' to save update\n");
                Console.WriteLine($"\nType '0' to Go Back to Main Menu\n");

                updateInput = Console.ReadLine();

                switch(updateInput){
                    case "d":
                    coding.Date = GetDateInput();
                    break;
                    case "t":
                    coding.Duration = GetDurationInput();
                    break;
                    case "0":
                    MainMenu();
                    break;
                    case "s":
                    updating = false;
                    break;
                    default:
                    Console.WriteLine($"\nType '0' to Go Back to Main Menu\n");
                    break;
                }
            }
            codingController.Update(coding);

            MainMenu();
        }

        private void ProcessDelete(){


            codingController.Get();
            Console.WriteLine("Please add id of the category you want to delte (or 0 to return to the Main Menu).");
            string commandInput = Console.ReadLine();
            while(!Int32.TryParse(commandInput, out _) || string.IsNullOrEmpty(commandInput) || Int32.Parse(commandInput) < 0)
            {
                Console.WriteLine("\nYou have to type a valid Id (or 0 to return to Main Menu).\n");
                commandInput = Console.ReadLine();
            }

            var id = Int32.Parse(commandInput);

            if(id ==0) MainMenu();

            var coding = codingController.GetById(id);

            while (coding.Id == 0)
            {
                Console.WriteLine($"\nRecord with id {id} doesn't exist\n");
                ProcessDelete();
            }

            codingController.Delete(id);

        }

        private void ProcessAdd(){
            var date = GetDateInput();
            var duration = GetDurationInput();

            Coding coding = new();

            coding.Date = date;
            coding.Duration = duration;
            
            codingController.Post(coding);
        }

        internal string GetDateInput(){
            Console.WriteLine("\n\nPlease insert the date: (Format: dd-mm-yy). Type 0 to return to main menu.\n\n");
            string dateInput = Console.ReadLine();
            if(dateInput == "0") MainMenu();
            while (!DateTime.TryParseExact(dateInput, "dd-MM-yy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
            {
                Console.WriteLine("\n\nNot a valid date. Please insert the date with the format: dd-mm-yy.\n\n");
                dateInput = Console.ReadLine();
            }

            return dateInput;
        }

        internal string GetDurationInput()
        {
            Console.WriteLine("\n\nPlease insert the duration: (Format: hh:mm). Type 0 to return to main menu.\n\n");
            string durationInput = Console.ReadLine();
            if(durationInput == "0") MainMenu();
            while (!TimeSpan.TryParseExact(durationInput, "hh\\:mm", CultureInfo.InvariantCulture, out _))
            {
                Console.WriteLine("\n\nNot a valid duration. Please insert the duration with the format: hh:mm.\n\n");
                durationInput = Console.ReadLine();
            }

            return durationInput;
        }
    }
}