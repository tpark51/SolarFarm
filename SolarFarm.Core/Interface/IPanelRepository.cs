using SolarFarm.Core.DTO;
using System;
using System.Collections.Generic;


namespace SolarFarm.Core.Interface
{
    public interface IPanelRepository
    {
        Panel FindPanel(Panel panel);
        bool AddPanel(Panel panel);
        bool RemovePanel(Panel panel);
        bool UpdatePanel(Panel originalPanel, Panel updatedPanel);
        List<Panel> FindPanelsBySection(string section);

    }
}
