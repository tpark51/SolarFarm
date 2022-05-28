using SolarFarm.Core.DTO;
using SolarFarm.Core.Interface;
using System;
using System.Collections.Generic;


namespace SolarFarm.BLL
{
    public class PanelService : IPanelService
    {
        private IPanelRepository _repo;

        public PanelService(IPanelRepository repo)
        {
            _repo = repo;
        }
        public bool AddPanel(Panel panel)
        {
            return _repo.AddPanel(panel);
        }

        public Panel FindPanel(Panel panel)
        {
            return _repo.FindPanel(panel);
        }

        public List<Panel> FinePanelsBySection(string section)
        {
            return _repo.FindPanelsBySection(section);
        }

        public bool RemovePanel(Panel panel)
        {
            return _repo.RemovePanel(panel);
        }

        public bool UpdatePanel(Panel originalPanel, Panel updatedPanel)
        {
            return _repo.UpdatePanel(originalPanel, updatedPanel);
        }
    }
}
