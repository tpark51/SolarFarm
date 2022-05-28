using SolarFarm.BLL;
using SolarFarm.Core.Interface;
using SolarFarm.DAL;
using System;

namespace SolarFarm.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleIO ui = new ConsoleIO();
            MenuController controller = new MenuController(ui);
            IPanelRepository repo = new PanelRepository(); //is this proper?
            IPanelService service = new PanelService(repo);
            controller._service = service;
            controller.Run();
        }
    }
}
