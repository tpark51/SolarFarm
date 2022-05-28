using SolarFarm.Core.DTO;
using SolarFarm.Core.Interface;
using System;
using System.Collections.Generic;
using System.IO;

namespace SolarFarm.DAL
{
    public class PanelRepository : IPanelRepository
    {
        private List<Panel> _panels;
        public PanelRepository()
        {
            _panels = ReadFile(Directory.GetCurrentDirectory() + @"\SolarFarmPanel.csv");
        }

        public  List<Panel> ReadFile(string path)
        {
            List<Panel> panelList = new();
            if (File.Exists(path))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string currentLine = reader.ReadLine();
                    currentLine = reader.ReadLine();
                    int lineCount = 0;

                    while (currentLine != null)
                    {
                        lineCount++;
                        Panel panel = new();
                        string[] columns = currentLine.Split(",");

                        panel.Section = columns[0];
                        panel.Row = int.Parse(columns[1]);
                        panel.Column = int.Parse(columns[2]);
                        panel.YearInstalled = DateTime.Parse(columns[3]);
                        panel.Material = (Material)Enum.Parse(typeof(Material), columns[4]);
                        panel.IsTracking = bool.Parse(columns[5]);

                        panelList.Add(panel);
                        currentLine = reader.ReadLine();
                    }
                }
            }
            else
            {
                return panelList;
            }
            return panelList;
        }

        public bool AddPanel(Panel panel)
        {
            int beforeAdd = _panels.Count;
            foreach (var savedPanel in _panels)
            {
                if(savedPanel.Section == panel.Section && savedPanel.Row == panel.Row && savedPanel.Column == panel.Column)
                {
                    Console.WriteLine("Cannot add a duplicate panel.");
                    Console.ReadKey();
                    return false;
                }
            }
            _panels.Add(panel);
            int afterAdd = _panels.Count;
            WriteFile(Directory.GetCurrentDirectory() + @"\SolarFarmPanel.csv");
            return beforeAdd != afterAdd;
        }

        public Panel FindPanel(Panel panel)
        {
            foreach (var savedPanel in _panels)
            {
                if (panel.Section == savedPanel.Section && panel.Row == savedPanel.Row && panel.Column == savedPanel.Column)
                {
                    return savedPanel;
                }
            }
            return new Panel();
        }

        public List<Panel> FindPanelsBySection(string section)
        {
            List<Panel> panelList = new();
            foreach (var savedPanel in _panels)
            {
                if (section == savedPanel.Section)
                {
                    panelList.Add(savedPanel);
                }
            }
            return panelList;
        }

        public bool RemovePanel(Panel panel)
        {
            int beforeRemove = _panels.Count;

            bool isEmpty = true;
            foreach (var savedPanel in _panels)
            {
                if(savedPanel.Section == panel.Section && savedPanel.Row == panel.Row && savedPanel.Column == panel.Column)
                {
                    isEmpty = false;
                    break;
                }
            }
            if (!isEmpty)
            {
                _panels.Remove(panel);
            }
            else
            {
                Console.WriteLine("Panel doesn't exist.");
                Console.ReadKey();
                return false;
            }

            int afterRemove = _panels.Count;
            WriteFile(Directory.GetCurrentDirectory() + @"\SolarFarmPanel.csv");
            return beforeRemove != afterRemove;
        }

        public bool UpdatePanel(Panel originalPanel, Panel updatedPanel)
        {
            for (int i = 0; i < _panels.Count; i++)
            { 
                if (_panels[i].Section == updatedPanel.Section && _panels[i].Row == originalPanel.Row && _panels[i].Column == originalPanel.Column)
                {
                    _panels[i] = updatedPanel;
                    WriteFile(Directory.GetCurrentDirectory() + @"\SolarFarmPanel.csv");
                    return true;
                }
            }
            return false;
        }
        public void WriteFile(string path)
        {
            using (StreamWriter write = new StreamWriter(path, false))
            {
                write.WriteLine("Section,Row,Column,YearInstalled,Material,IsTracking");
                foreach (var panel in _panels)
                {
                    write.WriteLine($"{panel.Section},{panel.Row},{panel.Column},{panel.YearInstalled},{panel.Material},{panel.IsTracking}");
                }
            }
            return;
        }
    }
}
