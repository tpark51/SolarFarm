using SolarFarm.Core.DTO;
using SolarFarm.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarFarm.UI
{
    public class MenuController
    {
        private readonly ConsoleIO _ui;
        public IPanelService _service;

        public MenuController(ConsoleIO ui)
        {
            _ui = ui;
        }

        public void Run()
        {
            ConsoleIO.Display("Welcome to Solar Farm\n===============================");
            
            bool isRunning = true;

            while (isRunning)
            {
                switch (GetMenuChoice())
                {
                    case 0:
                        isRunning = false;
                        break;
                    case 1:
                        FindPanels();
                        break;
                    case 2:
                        AddPanel();
                        break;
                    case 3:
                        UpdatePanel();
                        break;
                    case 4:
                        RemovePanel();
                        break;
                    default:
                        ConsoleIO.Display("Invalid. Please select 0-4\nPress any key to continue. ");
                        Console.ReadKey();
                        break;
                }
            }

        }
        public static int GetMenuChoice()
        {
            return ConsoleIO.DisplayMenu();

        }
        private void AddPanel()
        {
            ConsoleIO.Display("Add a Panel\n==================\n");
            var newPanel = GetNewPanelInfo();
            var result = _service.AddPanel(newPanel);
            Console.WriteLine();
            ConsoleIO.Display(result ? "Panel added" : "Panel not added");
        }
        private void FindPanels()
        {
            string section = ConsoleIO.DisplayFindMessage();
            ConsoleIO.Display($"\nPanels in {section}");
            ConsoleIO.Display("Row  Col Year  Material  Tracking");
            var panelList = _service.FinePanelsBySection(section);
            if (panelList.Count > 0)
            {
                foreach(var panel in panelList)
                {
                    ConsoleIO.Display(panel.ToString());
                }
            }
            ConsoleIO.Display("Press any key to continue");
            Console.ReadKey(); 
        }
        private void RemovePanel()
        {
            Console.WriteLine("Remove A Panel\n======================");
            Panel panel2Remove = GetUpdatePanelInfo();
            var originalPanel = _service.FindPanel(panel2Remove);

            if (string.IsNullOrEmpty(originalPanel.ToString()))
            {
                ConsoleIO.Display("Can't find the requested panel.\n\nPress any key to continue.");
                Console.ReadKey();
                return;
            }
            ConsoleIO.Display(_service.RemovePanel(originalPanel) ? $"Panel {panel2Remove.Section}-{panel2Remove.Row}-{panel2Remove.Column} removed" : "Panel not removed");
            ConsoleIO.Display("Press any key to contine.");
            Console.ReadKey();
            return;


        }
        private void UpdatePanel()
        {
            Panel panel2Update = GetUpdatePanelInfo();
            var originalPanel = _service.FindPanel(panel2Update);


            if (originalPanel.Row ==0)
            {
                ConsoleIO.Error("Request panel doesn't exitst.\nPlease any key to continue.");
                Console.ReadKey();
                return;
            }

            ConsoleIO.Display(@$"
Editing {originalPanel.Section}-{originalPanel.Row}-{originalPanel.Column}.
Press [Enter] to keep original vaule.

");
            Console.Write($"Section: [{originalPanel.Section}]: ");
            var updatedSection = Console.ReadLine();
            panel2Update.Section = String.IsNullOrEmpty(updatedSection) ? originalPanel.Section : updatedSection;

            Console.Write($"Row: [{originalPanel.Row}]: ");
            var updatedRow = Console.ReadLine();
            panel2Update.Row = String.IsNullOrEmpty(updatedRow) ? originalPanel.Row : int.Parse(updatedRow);

            Console.Write($"Column: [{originalPanel.Column}]: ");
            var updatedCol = Console.ReadLine();
            panel2Update.Column = String.IsNullOrEmpty(updatedCol) ? originalPanel.Column : int.Parse(updatedCol);

            Console.Write(@$"Material List
============
1. MultiCrySi - MultiCrystalline Silicon 
2. MonoCryS i- MonoCrystalline Silicon 
3. AmorSi - Amorphous Silicon 
4. CadTel - Cadmium Telluride 
5. CopGalSel - Copper Indium Gallium Selenide
 
Material[1-5] [{originalPanel.Material}]: ");
            var updatedMat = Console.ReadLine();
            var matInt = string.IsNullOrEmpty(updatedMat) ? (int)originalPanel.Material : int.Parse(updatedMat);
            while (matInt < 1 || matInt > 5)
            {
                matInt = Validation.PromptUser4EnumInt("Invalid input. ");
            }
            panel2Update.Material = (Material)matInt;

            Console.Write($"Installion Year [{originalPanel.YearInstalled:yyyy}]: ");
            var updatedYear = Console.ReadLine();
            //DateTime updatedYearDate = DateTime.Parse("1/1/" + updatedYear);
            panel2Update.YearInstalled = string.IsNullOrEmpty(updatedYear) ? originalPanel.YearInstalled : DateTime.Parse("1/1/" + updatedYear);


            Console.Write($"Tracked [{originalPanel.IsTracking}] [y/n]: ");
            var updatedTrack = Console.ReadLine();
            panel2Update.IsTracking = string.IsNullOrEmpty(updatedTrack) ? originalPanel.IsTracking : updatedTrack.ToLower() == "y" ? true : false;

            Console.WriteLine();
            ConsoleIO.Display(_service.UpdatePanel(originalPanel, panel2Update) ? "Panel updated" : "Panel not updated");

        }
        private static Panel GetNewPanelInfo()
        {
            var section = Validation.PromptRequired("Section: ");
            var row = Validation.PromptUser4Int("Row: ", 1, 250);
            var col = Validation.PromptUser4Int("Column: ", 1, 250);
            var mat = Validation.PromptUser4EnumInt(@"Material List
============
1. MultiCrySi - MultiCrystalline Silicon 
2. MonoCryS i- MonoCrystalline Silicon 
3. AmorSi - Amorphous Silicon 
4. CadTel - Cadmium Telluride 
5. CopGalSel - Copper Indium Gallium Selenide

Select [1-5]: ");
            Console.Write($"Installion Year: ");
            var yearInput = Console.ReadLine();
            DateTime installYear = DateTime.Parse("1/1/" + yearInput);
            var isTracked = Validation.PromptRequired("Tracked [y/n]: ");

            return new Panel
            {
                Section = section,
                Row = row,
                Column = col,
                Material = (Material)mat,
                YearInstalled = installYear,
                IsTracking = isTracked.ToLower() == "y" ? true : false
            };
        }
        private static Panel GetUpdatePanelInfo()
        {
            var section = Validation.PromptRequired("Section: ");
            var row = Validation.PromptUser4Int("Row: ", 1, 250);
            var col = Validation.PromptUser4Int("Column: ", 1, 250);


            return new Panel
            {
                Section = section,
                Row = row,
                Column = col,
                Material = (Material)1,
                YearInstalled = DateTime.Parse("1/1/2020"),
                IsTracking = true
            };
        }

    }
}

